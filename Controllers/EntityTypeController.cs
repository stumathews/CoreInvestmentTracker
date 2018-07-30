using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Controllers
{
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class CustomEntityTypeController : BaseEntityControllerFunctionality<CustomEntityType>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityRepository"></param>
        public CustomEntityTypeController(IEntityApplicationDbContext<CustomEntityType> entityRepository) : base(entityRepository)
        {
        }
    }
}
