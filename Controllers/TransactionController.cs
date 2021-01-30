using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    /// <summary>
    /// Transactions can apply to an investment
    /// </summary>
    [GlobalControllerLogging]
    [Route("api/[controller]")]
    public class TransactionController : EntityManagedController<InvestmentTransaction>
    {
        /// <summary>
        /// Access to te underlying store of entities for this T type of managed entity controller. This is resolved by depedency injection.
        /// </summary>
        /// <summary>
        /// Constructor for dependency injection support
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="db"></param>
        public TransactionController(IEntityApplicationDbContext<InvestmentTransaction> db, IMyLogger logger) : base(db, logger)
        {
        }

        
    }
}