using coworkdomain;
using coworkdomain.Cowork.Interfaces;
using coworkdomain.InventoryManagement.Interfaces;
using coworkpersistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var DoFakeDataGeneration = Configuration["Options:FakeDataGeneration"];
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            
            //instancing all repositories 
            services.AddSingleton<IMealRepository>(ctx => new MealRepository(conn));
            services.AddSingleton<IMealBookingRepository>(ctx => new MealBookingRepository(conn));
            services.AddSingleton<IPlaceRepository>(ctx => new PlaceRepository(conn));
            services.AddSingleton<IRoomBookingRepository>(ctx => new RoomBookingRepository(conn));
            services.AddSingleton<IRoomRepository>(ctx => new RoomRepository(conn));
            services.AddSingleton<ISubscriptionRepository>(ctx => new SubscriptionRepository(conn));
            services.AddSingleton<ISubscriptionTypeRepository>(ctx => new SubscriptionTypeRepository(conn));
            services.AddSingleton<ITimeSlotRepository>(ctx => new TimeSlotRepository(conn));
            services.AddSingleton<IUserRepository>(ctx => new UserRepository(conn));
            services.AddSingleton<ITicketRepository>(ctx => new TicketRepository(conn));
            services.AddSingleton<IWareRepository>(ctx => new WareRepository(conn));
            services.AddSingleton<ITicketAttributionRepository>(ctx => new TicketAttributionRepository(conn));
            services.AddSingleton<ILoginRepository>(ctx => new LoginRepository(conn));
            services.AddSingleton<ITicketCommentRepository>(ctx => new TicketCommentRepository(conn));
            services.AddSingleton<IWareBookingRepository>(ctx => new WareBookingRepository(conn));
            if (DoFakeDataGeneration == "True") {
                //TODO add bogus and generate fake data
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseMvc(routes => {
                routes.MapRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) spa.UseAngularCliServer("start");
            });
        }

    }

}