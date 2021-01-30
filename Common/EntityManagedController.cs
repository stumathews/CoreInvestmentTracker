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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreInvestmentTracker.Common
{
    /// <inheritdoc />
    /// <summary> 
    /// A controller that has access the the strongly typed entity type specified through the EntityRepository memeber.
    /// Also contains the common controller Actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GlobalControllerLogging]
    public class EntityManagedController<T> : BaseEntityControllerFunctionality<T> where T : class, IInvestmentEntity, new()
    {
        /// <summary>
        /// Logging facility. This is resolved by dependency injection
        /// </summary>
        private readonly IMyLogger Logger;

        /// <summary>
        /// Access to te underlying store of entities for this T type of managed entity controller. This is resolved by depedency injection.
        /// </summary>
        /// <summary>
        /// Constructor for dependency injection support
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="db"></param>
        public EntityManagedController( IEntityApplicationDbContext<T> db, IMyLogger logger) : base(db, logger)
        {
            Logger = logger;
        }


        /// <summary>
        /// Generically generates a series of datapoints representing for all or one of the entitiy types.
        /// This includes the linking of it to other entity types by joining up all the resulting entities by common investments
        /// </summary>
        /// <returns></returns>
        [HttpGet("GenerateEntityInvestmentsGraphFor/{entityId}")]
        public IActionResult GenerateEntityInvestmentsGraphFor(int entityId)
        {
            return GenerateSharedEntityGraphDataFor(entityId);
        }

        /// <summary>
        /// Generically generates a series of datapoints representing for all or one of the entitiy types.
        /// This includes the linking of it to other entity types by joining up all the resulting entities by common investments
        /// </summary>
        /// <returns></returns>
        [HttpGet("GenerateSharedInvestmentsGraphDataForAll"), Authorize]
        public IActionResult GenerateSharedGraphDataForAll()
        {
            return GenerateSharedEntityGraphDataFor();
        }

        private IActionResult GenerateSharedEntityGraphDataFor(int? entityId = null)
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

            var entities = EntityRepository.GetOneOrAllEntities(withChildren: true, specificId: entityId).ToArray();

            // inventmentId,List<EntityId>
            var mappings = new Dictionary<int, List<int>>();
            foreach (var entity in entities)
            {
                foreach (var entityInvestmentId in entity.InvestmentIds)
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
            
            var investments = from id in mappings.Keys
                              join investment in EntityRepository.Db.Investments on id equals investment.Id
                              select investment;

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
                var lastNodeIndex = nodes.Count - 1;
                foreach (var child in map.Value)
                {
                    var entity = entities.Single(x => x.Id == child);
                    var entityNode = new
                    {
                        name = entity.Name,

                        // Entity is bigger when its linked to more investments
                        value = mappings.SelectMany(x => x.Value).Count(x => x == child)
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
                //links.Add(new { source = lastNodeIndex, target = 0, value = 10 });
                
            }

            return new ObjectResult(new {nodes, links});
        }

        
    }
}