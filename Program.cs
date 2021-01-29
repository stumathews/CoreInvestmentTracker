using System;
using CoreInvestmentTracker.Models.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace CoreInvestmentTracker
{
    /// <summary>
    /// Program.cs file
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();            

            var scopedFactory = host.Services.GetService<IServiceScopeFactory>(); // create scope for lifetime of the request
            using (var scope = scopedFactory.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;   // get from within the context of the scope            
                Support.DatabaseSeeding.Setup(services);
            }

            host.Run();
        }

        /// <summary>
        /// BuildWebHost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
