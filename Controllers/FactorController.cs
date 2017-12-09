using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class FactorController : EntityManagedController<InvestmentInfluenceFactor>
    {
        public FactorController(IEntityApplicationDbContext<InvestmentInfluenceFactor> db, IMyLogger logger) 
            : base(db, logger)
        {

        }
    }
}