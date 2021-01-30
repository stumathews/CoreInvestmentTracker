using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CoreInvestmentTracker.Common.ActionFilters;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// WebApi Risk controller
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class RiskController : EntityManagedController<InvestmentRisk>
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public RiskController(IEntityApplicationDbContext<InvestmentRisk> db, IMyLogger logger) : base(db, logger) { }
    }
}