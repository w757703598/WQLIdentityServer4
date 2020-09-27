using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Linq;
using WQLIdentityServerAPI.SeedData;

namespace WQLIdentityServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            Console.Title = "IdentityServer";

            try
            {
                var seed = args.Contains("seed");
                if (seed)
                {
                    args = args.Except(new[] { "seed" }).ToArray();
                }

                var host = CreateWebHostBuilder(args).Build();


                EnsureSeedData seedData = new EnsureSeedData();
                //seedData.EnsureSeedDataAsync(host.Services).Wait();
                if (seed)
                {

                    seedData.EnsureSeedDataAsync(host.Services).Wait();
                }
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;

            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("host.json", optional: true).Build();//发布地址配置文件
            return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .ConfigureLogging(opt => opt.AddDebug())
                    .ConfigureServices(service => service.AddAutofac())
                    .UseConfiguration(config)
                    .UseNLog()
            ;
        }
    }
}
