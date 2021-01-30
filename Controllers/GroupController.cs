using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// WebAPI Controller for investment groups.
    /// Investments can be grouped into the same containers to add a relationship between them. Grouped investments share a commonality.
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class GroupController : EntityManagedController<InvestmentGroup>
    {
        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public GroupController(IEntityApplicationDbContext<InvestmentGroup> db, IMyLogger logger) :
            base(db, logger)
        {
        }
    }
}