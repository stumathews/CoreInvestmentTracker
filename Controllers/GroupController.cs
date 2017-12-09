using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class GroupController : EntityManagedController<InvestmentGroup>
    {
        public GroupController(IEntityApplicationDbContext<InvestmentGroup> db, IMyLogger logger) :
            base(db, logger)
        {
        }
    }
}