using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
//using Microsoft.Practices.Unity;
//using Unity;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CoreInvestmentTracker.Common
{
    /// <summary> 
    /// A controller that has access the the strongly typed entity type specified through the EntityRepository memeber.
    /// Also contains the common controller Actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GlobalControllerLogging]    
    public class EntityManagedController<T> : Controller where T : class, IDbInvestmentEntity
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

        [HttpGet]
        /// <summary>
        /// Get all Entities
        /// </summary>
        /// <returns>Gets all the entities</returns>
        public IEnumerable<T> GetAll()
        {
            return EntityRepository.Entities.ToList();
        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
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
        /// <param name="entity"></param>
        /// <returns>view details of the entity</returns>
        [HttpPost]
        public virtual IActionResult Create([FromBody]T entity)
        {
            EntityRepository.Entities.Add(entity);
            EntityRepository.SaveChanges();            
            return CreatedAtRoute("GetInvestment", new { id = entity.ID }, entity);
            
        }

        /// <summary>
        /// Updates and existing Entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] T item)
        {
            if (item == null || item.ID != id)
            {
                return BadRequest();
            }

            var entity = EntityRepository.Entities.FirstOrDefault(t => t.ID == id);
            if (entity == null)
            {
                return NotFound();
            }
            
            EntityRepository.Entities.Update(item);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }
        
        /// <summary>
        /// Deletes and Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
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

        
        ///// <summary>
        ///// Primarily used to update entities using x-editable post backs
        ///// </summary>
        ///// <param name="propertyName">name of changed entity property</param>
        ///// <param name="propertyValue">value of the property</param>
        ///// <param name="pk">the primary key of the entity</param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult UpdatePropertyOnly(string propertyName, string propertyValue, int pk)
        //{
        //    var candidate = EntityRepository.Entities.Find(pk);
        //    ReflectionUtilities.SetPropertyValue(candidate, propertyName, propertyValue);
        //    EntityRepository.SaveChanges();

        //    return StatusCode(StatusCodes.Status200OK);
        //}
       
    }
}