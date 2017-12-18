using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreInvestmentTracker.Controllers
{
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class RiskController : EntityManagedController<InvestmentRisk>
    {
        public RiskController(IEntityApplicationDbContext<InvestmentRisk> db, IMyLogger logger) 
            : base(db, logger)
        {
        }
    }
}