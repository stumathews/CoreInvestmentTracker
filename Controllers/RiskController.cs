
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;

namespace CoreInvestmentTracker.Controllers
{
    [GlobalLogging]
    public class RiskController : EntityManagedController<Models.InvestmentRisk>
    {
        public RiskController(IEntityApplicationDbContext<InvestmentRisk> db, IMyLogger logger) 
            : base(db, logger)
        {
        }
    }
}