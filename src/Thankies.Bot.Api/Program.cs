using System;
using System.IO;
using System.Reflection;
using Ele.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Thankies.Bot.Api
{
    public class Program
    {
        private static IConfiguration Configuration { get; } = ConfigurationExtension.LoadConfiguration(Directory.GetCurrentDirectory());
        
        public static void Main(string[] args)
        {
            var assembly = Assembly.GetCallingAssembly().GetName().Name;
            
            Log.Logger = ConfigurationExtension.LoadLogger(Configuration);
            try
            {
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting Thankies Bot");

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Thankies Bot terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}