using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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
        public IEnumerable<T> GetAll()
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
        [HttpPost]
        public IActionResult Create([FromBody]T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }
            EntityRepository.Entities.Add(entity);
            EntityRepository.SaveChanges();     
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