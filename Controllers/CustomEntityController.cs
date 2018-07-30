using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class CustomEntityController : BaseEntityControllerFunctionality<CustomEntity>
    {

        /// <summary>
        /// Create a custom entity and if there is an associated custom entity type, looks it up and asosciates it with it,
        /// otherwise the type is set to null
        /// </summary>
        /// <param name="entity">the entity to create</param>
        /// <returns>view details of the entity</returns>
        /// <response code="201">Returns the newly-created item</response>
        /// <response code="400">If the item is null</response>
        public override IActionResult Create([FromBody]CustomEntity entity)
        {
            if (entity.CustomEntityType != null)
            {
                var found = EntityRepository.Db.CustomEntityTypes.Where(x => x.Id == entity.CustomEntityType.Id);
                if (found.Any())
                {
                    entity.CustomEntityType = found.First();
                }
                else
                {
                    // That custom entity type was not found
                    return NotFound();
                }
            }

            if (entity.OwningCustomEntity?.Id == 0 || string.IsNullOrEmpty(entity.OwningCustomEntity.Name))
            {
                entity.OwningCustomEntity = null;
            }

            if (entity.Associations != null)
            {
                if (entity.Associations.Any(x => x.Id == 0 || string.IsNullOrEmpty(x.Name)))
                {
                    entity.Associations = null;
                }
            }

            // Do normal saving which includes logging
            return base.Create(entity);
        }

        /// <summary>
        /// Gets all the custom entities for a particular type
        /// </summary>
        /// <param name="type">the type of entities to fetch</param>
        /// <param name="id">owning entity type</param>
        /// <returns></returns>
        [HttpGet("ByType/{type}/{id}"), Authorize]
        public IEnumerable<CustomEntity> ByType(string type, int id)
        {
            return EntityRepository.Db.CustomEntities.Include(x => x.CustomEntityType)
                .Where(x => x.CustomEntityType.Name.Equals(type) && 
                            (x.OwningCustomEntity != null && x.OwningCustomEntity.Id == id)).ToList();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityRepository"></param>
        public CustomEntityController(IEntityApplicationDbContext<CustomEntity> entityRepository) : base(entityRepository)
        {
        }
    }
}
