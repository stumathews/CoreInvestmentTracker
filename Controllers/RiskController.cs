using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreInvestmentTracker.Controllers
{
    /// <summary>
    /// WebApi Risk controller
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class RiskController : EntityManagedController<InvestmentRisk>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public RiskController(IEntityApplicationDbContext<InvestmentRisk> db, IMyLogger logger) 
            : base(db, logger)
        {
        }
    }
}