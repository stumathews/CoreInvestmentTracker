using System;
using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet,Authorize]
        public IEnumerable<T> GetAll()
        {
            return EntityRepository.GetAllEntities(withChildren: true).ToList();
        }

        /// <summary>
        /// Gets all entities but not their children
        /// </summary>
        /// <returns></returns>
        [HttpGet("WithoutChildren"), Authorize]
        public IEnumerable<T> GetAllWithoutChildren()
        {
            return EntityRepository.GetAllEntities(withChildren:false).ToList();
        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>item</returns>
        [HttpGet("{id}"), Authorize]
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
        [HttpPost, Authorize]
        public IActionResult Create([FromBody]T entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            User systemUser = EntityRepository.Db.Users.First(u => u.Id == 1);
            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(systemUser, "tag","Created entity", DateTimeOffset.UtcNow, entity.Id, EntityType.Investment));
            EntityRepository.Db.Add(entity);
            EntityRepository.SaveChanges();
            return CreatedAtAction("Create", new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Import a list of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost("import"), Authorize]
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
        [HttpPatch("{id}"), Authorize]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<T> patchDocument)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var old = EntityRepository.Db.Find<T>(id);
            patchDocument.ApplyTo(old, ModelState);

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
        [HttpDelete("{id}"), Authorize]
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

            EntityRepository.Db.Update(old);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        [HttpGet("GenerateSharedInvestmentsGraphDataFor"), Authorize]
        public IActionResult GenerateSharedGraphDataFor()
        {
            /*
               var data = {
                "nodes":[
                    { "name": "index", "value": 5},
                    { "name": "about", "value": 5},
                    { "name": "contact", "value": 5},
                    { "name": "store", "value": 8},
                    { "name": "cheese", "value": 8},
                    { "name": "yoghurt", "value": 10},
                    { "name": "milk", "value": 2}
                    ],
                    "links":[
                    {"source":0,"target":1,"value":25},
                    {"source":0,"target":2,"value":10},
                    {"source":0,"target":3,"value":40},
                    {"source":1,"target":2,"value":10},
                    {"source":3,"target":4,"value":25},
                    {"source":3,"target":5,"value":10},
                    {"source":3,"target":6,"value":5},
                    {"source":4,"target":6,"value":5},
                    {"source":4,"target":5,"value":15}
                ]
            }
             */

            var entities = EntityRepository.GetAllEntities(withChildren: true).ToArray();
            
            // inventmentId,List<EntityId>
            var mappings = new Dictionary<int,List<int>>();
            foreach (var entity in entities)
            {
                foreach (var entityInvestmentId in entity.investmentIds)
                {
                    if (!mappings.ContainsKey(entityInvestmentId))
                    {
                        mappings.Add(entityInvestmentId, new List<int>());
                    }
                    mappings[entityInvestmentId].Add(entity.Id);
                }
            }
            
            var nodes = new List<object> { };
            var links = new List<object>();
            
            // a invisible root that all connect to - keeps things together
            //nodes.Add(new {name = "Root", value = 0});

            var investments = EntityRepository.Db.Investments.Where(x=> mappings.ContainsKey(x.Id)).ToArray();
            
            foreach (var map in mappings)
            {
                var investment = investments.Single(x => x.Id == map.Key);
                var countLinkedEntities = map.Value.Count;
                var investmentNode = new
                {
                    name = investment.Name, 
                    value = 1
                };
                nodes.Add(investmentNode);
                
                foreach (var child in map.Value)
                {
                    var entity = entities.Single(x => x.Id == child);
                    var entityNode = new
                    {
                        name = entity.Name,
                        
                        // Entity is bigger when its linked to more investments
                        value = mappings.SelectMany(x=>x.Value).Count(x => x == child)
                    };
                    if (!nodes.Contains(entityNode))
                    {
                        nodes.Add(entityNode);
                    }
                    
                    links.Add(new
                    {
                        source = nodes.LastIndexOf(investmentNode), 
                        target = nodes.LastIndexOf(entityNode), 
                        value = 1
                    });
                }
                // add invisible root
                //links.Add(new { source = nodes.LastIndexOf(i), target = 0, value = 0 });
            }
            return new ObjectResult(new { nodes, links });
        }
    }
}