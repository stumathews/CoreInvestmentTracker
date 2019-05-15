using System;
using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// Deals with all entity persisting and commonalities between the entities at a controller level.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEntityControllerFunctionality<T> : Controller where T : class, IDbEntity, new()
    {
        /// <summary>
        /// Access to the database for this entity
        /// </summary>
        public IEntityApplicationDbContext<T> EntityRepository { get; }


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityRepository"></param>
        public BaseEntityControllerFunctionality(IEntityApplicationDbContext<T> entityRepository)
        {
            EntityRepository = entityRepository;
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Array of entities</returns>
        [HttpGet,Authorize]
        public IEnumerable<T> GetAll()
        {
            return EntityRepository.GetOneOrAllEntities(withChildren: true).ToList();
        }

        /// <summary>
        /// Gets all entities but not their children
        /// </summary>
        /// <returns></returns>
        [HttpGet("WithoutChildren"), Authorize]
        public IEnumerable<T> GetAllWithoutChildren()
        {
            return EntityRepository.GetOneOrAllEntities(withChildren:false).ToList();
        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>item</returns>
        [HttpGet("{id}"), Authorize]
        public IActionResult GetById(int id)
        {
            var item = EntityRepository.GetOneOrAllEntities().FirstOrDefault(p => p.Id == id);
            return item == null ? (IActionResult) NotFound() : new ObjectResult(item);
        }

        /// <summary>
        /// Create a entity
        /// </summary>
        /// <param name="entity">the entity to create</param>
        /// <returns>view details of the entity</returns>
        /// <response code="201">Returns the newly-created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost, Authorize]
        public virtual IActionResult Create([FromBody]T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            if (entity.CreatedTime == default)
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;
            }

            if (entity.LastModifiedTime == default)
            {
                entity.LastModifiedTime = entity.CreatedTime;
            }

            EntityRepository.Db.Add(entity);
            EntityRepository.SaveChanges();
            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(
                Name: ActivityOperation.Create.ToString(),
                description: "Created a new entity", 
                user:  GetUser(),
                tag: entity.ToString(),
                details: "Created new entity",
                atTime: DateTimeOffset.UtcNow,
                owningEntityId: entity.Id,
                owningEntityType: GetUnderlyingEntityType<T>()));
            EntityRepository.SaveChanges();
            return CreatedAtAction("Create", new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Import a list of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost("import"), Authorize]
        public IActionResult Import([FromBody] T[] entities)
        {
            if (entities.Length == 0)
            {
                return BadRequest();
            }

            foreach (var e in entities)
            {
                e.Id = 0;
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
        [HttpPatch("{id}"), Authorize]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<T> patchDocument)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            
            var old = EntityRepository.Db.Find<T>(id);
            patchDocument.ApplyTo(old, ModelState);

            old.LastModifiedTime = DateTimeOffset.UtcNow;

            EntityRepository.Db.Update(old);
            EntityRepository.SaveChanges();

            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            patchDocument.Operations.ForEach(o =>
            {
                var entry = $"Modified value '{o.path}' to {o.value}";
                EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(), "Updates an existing entity", GetUser(), entry.ToString(), entry, DateTimeOffset.UtcNow, id, GetUnderlyingEntityType<T>()));
            });

            EntityRepository.Db.SaveChanges();
            
            return Ok(old);
        }

        /// <summary>
        /// Deletes and Entity
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        /// <returns>NoContentResult</returns>
        [HttpDelete("{id}"), Authorize]
        public virtual IActionResult Delete(int id)
        { 
            var entity = EntityRepository.Db.Find<T>(id);
            if (entity == null)
            {
                return NotFound();
            }

            EntityRepository.Db.Remove(entity);
            EntityRepository.SaveChanges();

            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Delete.ToString(), "Deletes an entity", GetUser(), "", $"Deleted entity with id of {id}", DateTimeOffset.UtcNow, entity.Id, GetUnderlyingEntityType<T>()));
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
        [HttpPut("{id}"), Authorize]
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
            
            old.LastModifiedTime = DateTimeOffset.UtcNow;

            EntityRepository.Db.Update(old);
            EntityRepository.SaveChanges();
            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(), "Updates an existing entity", GetUser(), old.ToString(), $"Replaced entity with id of {id} with entity of {newItem}", DateTimeOffset.UtcNow, id, GetUnderlyingEntityType<T>()));
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        protected EntityType GetUnderlyingEntityType<T>()
        {
            if (typeof(T) == typeof(Investment))
            {
                return EntityType.Investment;
            }
            if (typeof(T) == typeof(InvestmentInfluenceFactor))
            {
                return EntityType.InvestmentInfluenceFactor;
            }
            if (typeof(T) == typeof(InvestmentRisk))
            {
                return EntityType.InvestmentRisk;
            }
            if (typeof(T) == typeof(Region))
            {
                return EntityType.Region;
            }
            if (typeof(T) == typeof(InvestmentNote))
            {
                return EntityType.Note;
            }
            if (typeof(T) == typeof(RecordedActivity))
            {
                return EntityType.Activity;
            }
            if (typeof(T) == typeof(CustomEntity))
            {
                return EntityType.Custom;
            }

            if (typeof(T) == typeof(InvestmentTransaction))
            {
                return EntityType.InvestmentTransaction;
            }

            return EntityType.None;
        }

        protected User GetUser()
        {
            return EntityRepository.Db.Users.First(u => u.UserName.Equals(u.UserName));
        }
    }
}