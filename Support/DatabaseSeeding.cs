using CoreInvestmentTracker;
using CoreInvestmentTracker.Models.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Support
{
    /// <summary>
    /// Responsible for seeding the database
    /// </summary>
    public class DatabaseSeeding
    {    
        internal static void Setup(IServiceProvider services)
        {            
            try
            {   
                DatabaseTestDataInitializer.Initialize(services.GetService<InvestmentDbContext>()); // Custom initializer
            }
            catch (Exception ex)
            {
                services.GetRequiredService<ILogger<Program>>().LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}