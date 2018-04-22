using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for InvestmentInfluenceFactors
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class FactorController : EntityManagedController<InvestmentInfluenceFactor>
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public FactorController(IEntityApplicationDbContext<InvestmentInfluenceFactor> db, IMyLogger logger) 
            : base(db, logger)
        {

        }
    }
}