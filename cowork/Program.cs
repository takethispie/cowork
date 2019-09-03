using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace cowork {

    public class Program {

        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) => {
                    config.AddJsonFile("appsettings.json", false);
                })
                .UseStartup<Startup>();
        }

    }

}