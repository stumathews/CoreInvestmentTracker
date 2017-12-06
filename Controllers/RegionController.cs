using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;

namespace CoreInvestmentTracker.Controllers
{
    [GlobalLogging]
    /// <summary>
    /// This automaticall maps /controller/method such as /region/xvz
    /// </summary>
    public class RegionController : EntityManagedController<Region>
    {
        public RegionController(IEntityApplicationDbContext<Region> db, IMyLogger logger) : base(db, logger)
        {
        }
    }
}