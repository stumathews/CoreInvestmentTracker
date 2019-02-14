using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Gets the custom entity types that have entities of the type for this owning entity
        /// </summary>
        /// <param name="owningId">owning entity</param>
        /// <returns>Custom Types associated with this entity (ie that have custom entities associated with this entity)</returns>
        [HttpGet("ByOwningId/{owningId}"), Authorize]
        public IEnumerable<CustomEntityType> ByOwningId( int owningId)
        {
            var ids = EntityRepository.Db.CustomEntities.Where(x => x.OwningEntityId == owningId).Select(y=>y.CustomEntityType.Id).Distinct();
            var types = EntityRepository.Db.CustomEntityTypes.Where(x => ids.Contains(x.Id));
            return types;
        }
    }
}
