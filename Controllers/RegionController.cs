using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// This automaticall maps /controller/method such as /region/xvz
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]    
    public class RegionController : EntityManagedController<Region>
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public RegionController(IEntityApplicationDbContext<Region> db, IMyLogger logger) : base(db, logger) { }
    }
}