using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CoreInvestmentTracker.Common
{
    /// <inheritdoc />
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
        private readonly IMyLogger Logger;

        /// <summary>
        /// Access to te underlying store of entities for this T type of managed entity controller. This is resolved by depedency injection.
        /// </summary>
        protected readonly IEntityApplicationDbContext<T> EntityRepository;

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
        [HttpGet()]
        public IEnumerable<T> GetAll()
        {
            return EntityRepository.GetAllEntities(withChildren: true).ToList();
        }

        /// <summary>
        /// Gets all entities but not their children
        /// </summary>
        /// <returns></returns>
        [HttpGet("WithoutChildren")]
        public IEnumerable<T> GetAllWithoutChildren()
        {
            return EntityRepository.GetAllEntities(withChildren:false).ToList();
        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>item</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = EntityRepository.GetAllEntities().FirstOrDefault(p => p.Id == id);
            return item == null ? (IActionResult) NotFound() : new ObjectResult(item);
        }

        /// <summary>
        /// Create a entity
        /// </summary>
        /// <param name="entity">the entity to create</param>
        /// <returns>view details of the entity</returns>
        /// <response code="201">Returns the newly-created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost()]
        public IActionResult Create([FromBody]T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }            
            EntityRepository.Db.Add(entity);
            EntityRepository.SaveChanges();
            return CreatedAtAction("Create", new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Import a list of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost("import")]
        public IActionResult Import([FromBody] object[] entities)
        {
            if (entities.Length == 0)
            {
                return BadRequest();
            }
            EntityRepository.Db.AddRange(entities);
            EntityRepository.SaveChanges();
            return CreatedAtAction("Import", entities);
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

            var old = EntityRepository.Db.Find<T>(id);
            JsonPatchExtensions.ApplyTo(patchDocument, old, ModelState);

            EntityRepository.Db.Update(old);
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
            var entity = EntityRepository.Db.Find<T>(id);
            if (entity == null)
            {
                return NotFound();
            }
            EntityRepository.Db.Remove(entity);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }


        /// <summary>
        /// Replaces and existing Entity.
        /// Note this is not for partial updates, for that use PATCH. This is used for replacing the entire entity.
        /// At the moment it is not possible to replace everything on the existing generic entity with that on the new one and
        /// sometimes we dont want to: we dont want to replace the ID property for example, but we might want to replace
        /// collections in the orignal item with the new collections in the new item, but this is currently not possible in the
        /// implemantation. It just replaces simple members.
        /// ** So if you want to do it propery, override this method in the controller for the type you want to implementa replacement
        /// routine for.
        /// </summary>
        /// <param name="id">Id of the entity to update</param>
        /// <param name="newItem">the contents of the entity to change</param>
        /// <returns>NoContentResult</returns>
        [HttpPut("{id}")]
        public IActionResult Replace(int id, [FromBody] T newItem)
        {
            if (newItem == null || newItem.Id != id)
            {
                return BadRequest();
            }

            var old = EntityRepository.Db.Find<T>(id);
            if (old == null)
            {
                return NotFound();
            }

            /* Note we need to come up with a way to fetch the child entities */
            try
            {
                ShallowCopy.Merge(old, newItem, new string[] { "ID" });
            }
            catch
            {
                old = ShallowCopy.MergeObjects(old, newItem);
            }

            EntityRepository.Db.Update(old);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }
    }
}