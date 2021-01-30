using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Factors are anything what influences the companies balance sheet(assets and liabilities) or income(profit/loss) statement.
    /// These factors usually affect the underlying value of the investment either positively and negatively
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