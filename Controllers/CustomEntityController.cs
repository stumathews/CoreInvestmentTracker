using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models;
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
    public class CustomEntityController : EntityManagedController<CustomEntity>
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
                var found = EntityRepository.Db.CustomEntityTypes.Where(x => x.Id == entity.CustomEntityType.Id || x.Name.Equals(entity.CustomEntityType.Name));
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

            CustomEntity_Investment link = null; 
            if (entity.OwningCustomEntity?.Id == 0 || string.IsNullOrEmpty(entity.OwningCustomEntity.Name))
            {
                entity.OwningCustomEntity = null;
                if (entity.OwningEntityType == EntityType.None) return base.Create(entity);
                switch (entity.OwningEntityType)
                {
                    case EntityType.Investment:
                        var investment = EntityRepository.Db.Investments.Single(x => x.Id == entity.OwningEntityId);
                        link = new CustomEntity_Investment
                        {
                            InvestmentID = entity.OwningEntityId,
                            Investment = investment,
                            CustomEntity = entity,
                            CustomEntityId = entity.Id
                        };
                        entity.Investments = new List<CustomEntity_Investment> {link};
                        entity.Associations = new List<CustomEntity>();

                        break;
                    case EntityType.None:
                        break;
                    case EntityType.InvestmentGroup:
                        break;
                    case EntityType.InvestmentRisk:
                        break;
                    case EntityType.InvestmentInfluenceFactor:
                        break;
                    case EntityType.Region:
                        break;
                    case EntityType.User:
                        break;
                    case EntityType.Note:
                        break;
                    case EntityType.Activity:
                        break;
                    case EntityType.Custom:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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
            var ret =  EntityRepository.Db.CustomEntities.Include(x => x.CustomEntityType)
                .Where(x => x.CustomEntityType.Name.Equals(type) && 
                            ((x.OwningCustomEntity != null && x.OwningCustomEntity.Id == id) || 
                             (x.OwningEntityType == EntityType.Investment && x.OwningEntityId == id)) ).ToList();
            return ret;
        }

        /// <summary>
        /// Access to te underlying store of entities for this T type of managed entity controller. This is resolved by depedency injection.
        /// </summary>
        /// <summary>
        /// Constructor for dependency injection support
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="db"></param>
        public CustomEntityController(IEntityApplicationDbContext<CustomEntity> db, IMyLogger logger) : base(db, logger)
        {
        }
    }
}
