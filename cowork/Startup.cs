using System;
using System.Linq;
using System.Text;
using coworkdomain;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence;
using coworkpersistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace cowork {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var conn = Configuration["Database:ConnectionString"];
            if (Configuration["Environement"] == "Prod")
                conn = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            var DoFakeDataGeneration = Configuration["Options:FakeDataGeneration"];
            var secretKey = Configuration["Secret"];
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "http://localhost:5001",
                        ValidAudience = "http://localhost:5001",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            var userRepo = new UserRepository(conn);
            var loginRepo = new LoginRepository(conn);
            //instancing all repositories 
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
            services.AddSingleton<IWareBookingRepository>(ctx => new WareBookingRepository(conn));
            services.AddSingleton(new AuthTokenHandler() {Secret = secretKey});

            var adminEmail = Configuration["AdminAccount:Email"];
            var adminPassword = Configuration["AdminAccount:Password"];
            var hasAdmin = userRepo.GetAll().Any(user => user.FirstName == "admin" && user.LastName == "admin");
            if (!hasAdmin) {
                var result = CreateAdmin(loginRepo, userRepo,
                    new User(-1, "admin", "admin", adminEmail, false, UserType.Admin), adminEmail, adminPassword);
                if (result == -1) throw new Exception("Impossible de creer le compte administrateur par défaut");
            }

            if (DoFakeDataGeneration == "True") {
                //TODO add bogus and generate fake data
            }
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment()) spa.UseAngularCliServer("start");
            });
        }


        public int CreateAdmin(ILoginRepository loginRepository, IUserRepository userRepository, User user,
                               string email, string password) {
            var result = userRepository.Create(user);
            if (result == -1) return -1;
            user.Id = result;
            PasswordHashing.CreatePasswordHash(password, out var hash, out var salt);
            result = loginRepository.Create(new Login(-1, hash, salt, email, result));
            if (result > -1) return 0;
            userRepository.DeleteById(user.Id);
            return -1;
        }

    }

}