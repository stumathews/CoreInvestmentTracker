using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL;

namespace CoreInvestmentTracker.Common
{
    /// <summary> 
    /// A controller that has access the the strongly typed entity type specified through the EntityRepository memeber.
    /// Also contains the common controller Actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GlobalControllerLogging]    
    public class EntityManagedController<T> : Controller where T : class, IDbInvestmentEntity, new()
    {
        /// <summary>
        /// Logging facility. This is resolved by dependency injection
        /// </summary>
        public readonly IMyLogger Logger;
        
        /// <summary>
        /// Access to te underlying store of entities. This is resolved by depedency injection.
        /// </summary>
        public readonly IEntityApplicationDbContext<T> EntityRepository;

        /// <summary>
        /// Constructor for dependency injection support
        /// </summary>
        /// <param name="entityApplicationDbContext"></param>
        /// <param name="logger"></param>
        public EntityManagedController(IEntityApplicationDbContext<T> entityApplicationDbContext, IMyLogger logger)
        {
            EntityRepository = entityApplicationDbContext;
            Logger = logger;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Array of entities</returns>
        [HttpGet]        
        public virtual IEnumerable<T> GetAll()
        {
            return EntityRepository.Entities.ToList();
        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>item</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = EntityRepository.Entities.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /// <summary>
        /// Create a entity
        /// </summary>
        /// <param name="entity">the entity to create</param>
        /// <returns>view details of the entity</returns>
        /// <response code="201">Returns the newly-created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost()]
        public virtual IActionResult Create([FromBody]T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            /* Save Entity Workaround:  This allows us to at least create new entitys from the incomming API requests.
             * At the moment, I cant figure out why when using a Generic entity type, EF
             does not respect the [DatabaseGenerated(DatabaseGeneratedOption.Identity)] on the underlying type. 
             I can't use the FluentAPI on a generic type either - it was CLR types or something like that...

             So for now, we're going to have to interpret the generic type to its underlying type, so that EF recognises the 
             that the ID columns of each type has [DatabaseGenerated(DatabaseGeneratedOption.Identity)] and will thus automatically generate a new value for it
             and not literally take the value weve got for ID in the entity type and try insert that... directly into the column and we dont want that 
             and the database complains rightly so saying that we dont support IDENTITY_INSERTs which is what it tries to do instead of generate a new ID...

            If anything good about this, is that we dont have to write each entry below for each controller(which would work but is just added more code). Here its in one place.
            
             */

            if (typeof(T) == typeof(InvestmentInfluenceFactor))
            {
                EntityRepository.db.Factors.Add(EntityApplicationDbContext<T>.ChangeType<InvestmentInfluenceFactor>(entity));
                EntityRepository.db.SaveChanges();
            }
            else if (typeof(T) == typeof(InvestmentRisk))
            {
                EntityRepository.db.Risks.Add(EntityApplicationDbContext<T>.ChangeType<InvestmentRisk>(entity));
                EntityRepository.db.SaveChanges();
            }
            else if (typeof(T) == typeof(InvestmentGroup))
            {
                EntityRepository.db.Groups.Add(EntityApplicationDbContext<T>.ChangeType<InvestmentGroup>(entity));
                EntityRepository.db.SaveChanges();
            }
            else if (typeof(T) == typeof(Region))
            {
                EntityRepository.db.Regions.Add(EntityApplicationDbContext<T>.ChangeType<Region>(entity));
                EntityRepository.db.SaveChanges();
            }
            else if (typeof(T) == typeof(Investment))
            {
                EntityRepository.db.Investments.Add(EntityApplicationDbContext<T>.ChangeType<Investment>(entity));
                EntityRepository.db.SaveChanges();
            } else
            {
                EntityRepository.Entities.Add(entity);
                EntityRepository.SaveChanges();
            }
               
            return CreatedAtAction("Create",new { id = entity.ID }, entity);
            
        }

        /// <summary>
        /// Updates and existing Entity
        /// </summary>
        /// <param name="id">Id of the entity to update</param>
        /// <param name="item">the contents of the entity to change</param>
        /// <returns>NoContentResult</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] T item)
        {
            if (item == null || item.ID != id)
            {
                return BadRequest();
            }

            var old = EntityRepository.Entities.FirstOrDefault(t => t.ID == id);
            if (old == null)
            {
                return NotFound();
            }
            
            /*Note we need to come up with a way to fetch the child entities*/
            try
            {
                ShallowCopy.Merge(old, item, new string[] { "ID" });
            }
            catch {
                old = ShallowCopy.MergeObjects(old, item);
            }
            
            EntityRepository.Entities.Update(old);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Updates an entity partially
        /// </summary>
        /// <param name="id">Id of entity to patch</param>
        /// <param name="patchDocument">the patched object</param>
        /// <returns>the new object updated</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<T> patchDocument)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var old = EntityRepository.Entities.FirstOrDefault(t => t.ID == id);
            
            patchDocument.ApplyTo(old, ModelState);

            EntityRepository.Entities.Update(old);
            EntityRepository.SaveChanges();

            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return Ok(old);
        }

        /// <summary>
        /// Deletes and Entity
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        /// <returns>NoContentResult</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = EntityRepository.Entities.FirstOrDefault(t => t.ID == id);
            if (todo == null)
            {
                return NotFound();
            }

            EntityRepository.Entities.Remove(todo);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }


        /// <summary>
        /// Update entities.
        /// </summary>
        /// <param name="propertyName">name of changed entity property</param>
        /// <param name="propertyValue">value of the property</param>
        /// <param name="pk">the primary key of the entity</param>
        /// <remarks>Consider using Patch method</remarks>
        /// <returns></returns>
        [HttpPost("UpdatePropertyOnly/{propertyName}/{propertyValue}/{id}")]
        public IActionResult UpdatePropertyOnly(string propertyName, string propertyValue, int pk)
        {
            var candidate = EntityRepository.Entities.Find(pk);
            ReflectionUtilities.SetPropertyValue(candidate, propertyName, propertyValue);
            EntityRepository.SaveChanges();

            return new NoContentResult();
        }

    }
}