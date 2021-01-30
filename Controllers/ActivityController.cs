using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    /// <summary>
    /// Keep track of activities that occur to investments
    /// </summary>
    [Produces("application/json")]
    [Route("api/Activity")]
    public class ActivityController : RefersToAnEntityControllerFunctionality<RecordedActivity>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityRepository"></param>
        public ActivityController(IEntityApplicationDbContext<RecordedActivity> entityRepository, IMyLogger logger) : base(entityRepository, logger)
        {
        }
    }
}