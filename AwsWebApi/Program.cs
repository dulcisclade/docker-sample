using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AwsWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        configurationBuilder.AddJsonFile(
                            $"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true,
                            reloadOnChange: true);
                    }

                    configurationBuilder.AddEnvironmentVariables();
                });
    }
}