using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;

namespace CoreInvestmentTracker.Controllers
{
    [GlobalLogging]
    public class FactorController : EntityManagedController<InvestmentInfluenceFactor>
    {
        public FactorController(IEntityApplicationDbContext<InvestmentInfluenceFactor> db, IMyLogger logger) 
            : base(db, logger)
        {
        }
    }
}