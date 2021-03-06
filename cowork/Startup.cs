using System;
using System.Text;
using cowork.Service;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System.Linq;
using cowork.domain;
using cowork.domain.Interfaces;
using cowork.persistence.Repositories;
using cowork.services.Login;
using cowork.usecases.Auth;

namespace cowork {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var conn = Configuration["Database:ConnectionString"];
            services.AddHangfire(config => config.UsePostgreSqlStorage(conn));
            var secretKey = Configuration["Secret"];
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuers = new []{"http://localhost:5001", "http://localhost:8100","http://localhost"},
                        ValidAudiences = new []{"http://localhost:5001", "http://localhost:8100", "https://localhost"},
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services
                .AddCors(options =>
                {
                    options.AddPolicy("allowMobileOrigin",
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:8100", "http://localhost");
                            builder.AllowAnyHeader();
                            builder.AllowAnyMethod();
                        });
                });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            // REPOSITORIES
            var userRepo = new UserRepository(conn);
            var loginRepo = new LoginRepository(conn);
            services.AddSingleton<IMealRepository>(ctx => new MealRepository(conn));
            services.AddSingleton<IMealBookingRepository>(ctx => new MealBookingRepository(conn));
            services.AddSingleton<IPlaceRepository>(ctx => new PlaceRepository(conn));
            services.AddSingleton<IRoomBookingRepository>(ctx => new RoomBookingRepository(conn));
            services.AddSingleton<IRoomRepository>(ctx => new RoomRepository(conn));
            services.AddSingleton<ISubscriptionRepository>(ctx => new SubscriptionRepository(conn));
            services.AddSingleton<ISubscriptionTypeRepository>(ctx => new SubscriptionTypeRepository(conn));
            services.AddSingleton<ITimeSlotRepository>(ctx => new TimeSlotRepository(conn));
            services.AddSingleton<IUserRepository>(ctx => userRepo);
            services.AddSingleton<ITicketRepository>(ctx => new TicketRepository(conn));
            services.AddSingleton<IWareRepository>(ctx => new WareRepository(conn));
            services.AddSingleton<ITicketAttributionRepository>(ctx => new TicketAttributionRepository(conn));
            services.AddSingleton<ILoginRepository>(ctx => loginRepo);
            services.AddSingleton<ITicketCommentRepository>(ctx => new TicketCommentRepository(conn));
            services.AddSingleton<ITicketWareRepository>(ctx => new TicketWareRepository(conn));
            services.AddSingleton<IWareBookingRepository>(ctx => new WareBookingRepository(conn));
            services.AddSingleton<IStaffLocationRepository>(ctx => new StaffLocationRepository(conn));
            services.AddSingleton(new AuthTokenHandler() {Secret = secretKey});
            services.AddSingleton<IScheduledService, ExpiredSubscriptionDeletionService>();
            
            //SERVICES
            services.AddSingleton<ITokenHandler, AuthTokenHandler>();

            var adminEmail = Configuration["AdminAccount:Email"];
            var adminPassword = Configuration["AdminAccount:Password"];
            var hasAdmin = userRepo.GetAll().Any(user => user.FirstName == "admin" && user.LastName == "admin");
            if (hasAdmin) return;
            var adminUser = new User(-1, "admin", "admin", false, UserType.Admin);
            var result = new CreateAdminAccount(loginRepo, userRepo, adminUser, adminEmail, adminPassword);
            if (result.Execute() == -1) throw new Exception("Impossible de creer le compte administrateur par défaut");
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors("allowMobileOrigin");
            app.UseMvc(routes => {
                routes.MapRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment()) spa.UseAngularCliServer("start");
            });
            app.UseHangfireServer();
        }
    }

}