using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DAL.Interfaces;

namespace CoreInvestmentTracker.Controllers
{
    [GlobalLogging]
    public class RiskController : EntityManagedController<Models.InvestmentRisk>
    {
        public RiskController(IEntityApplicationDbContext<InvestmentRisk> entityApplicationDbContext) : base(entityApplicationDbContext)
        {
        }
    }
}