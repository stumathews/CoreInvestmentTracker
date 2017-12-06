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
    public class GroupController : EntityManagedController<InvestmentGroup>
    {
        public GroupController(IEntityApplicationDbContext<InvestmentGroup> entityApplicationDbContext) : base(entityApplicationDbContext)
        {
        }
    }
}