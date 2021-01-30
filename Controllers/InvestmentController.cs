using System;
using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Common.ActionFilters;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// The Investment controller.
    /// Investments represent the assets that are valuable to you
    /// </summary>
    [GlobalControllerLogging]
    [Route("api/[controller]")]
    public class InvestmentController : EntityManagedController<Investment>
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="logger">loggin mechanism</param>
        public InvestmentController(IEntityApplicationDbContext<Investment> db, IMyLogger logger) 
            : base(db, logger)
        {
        }

        #region InvestmentBy Methods

        /// <summary>
        /// Get investments By Risk
        /// </summary>
        /// <param name="id">Risk ID</param>
        /// <returns>Investments associated with this risk</returns>
        [HttpGet("ByRisk/{id}")]
        public IActionResult InvestmentByRisk(int id)
        {
            var risk = EntityRepository.GetEntityByType<InvestmentRisk>().SingleOrDefault(r => r.Id == id);
            if (risk == null) return NotFound();
            var investments = risk.Investments;
            return new ObjectResult(investments);
        }

        /// <summary>
        /// Get investments by Factor
        /// </summary>
        /// <param name="id">Factor Id</param>
        /// <returns>Investments associated with this factor</returns>
        [HttpGet("ByFactor/{id}")]
        public IActionResult InvestmentByFactor(int id)
        {
            return new ObjectResult(EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().SingleOrDefault(x => x.Id == id)?.Investments);
        }

        /// <summary>
        /// Get investments by group
        /// </summary>
        /// <param name="id">group id</param>
        /// <returns>Investments associated with this group</returns>
        [HttpGet("ByGroup/{id}")]
        public IActionResult InvestmentByGroup(int id)
        {
            return new ObjectResult(EntityRepository.GetEntityByType<InvestmentGroup>().SingleOrDefault(x => x.Id == id)?.Investments);
        }
        #endregion

        #region Investment Association Methods

        /// <summary>
        /// Gets investments by region
        /// </summary>
        /// <param name="id">region id</param>
        /// <returns>investments associated with this region</returns>
        [HttpGet("ByRegion/{id}")]
        public IActionResult InvestmentByRegion(int id)
        {
            var region = EntityRepository.GetEntityByType<Region>();

            return region != null
                ? new ObjectResult(region.SingleOrDefault(x => x.Id == id)?.Investments)
                : null;
        }

        /// <summary>
        /// Dissosicate a risk with an investment
        /// </summary>
        /// <param name="riskId">risk to dissasociate</param>
        /// <param name="investmentId">investment to dissasociate risk from</param>
        /// <returns>Status Code</returns>
        [HttpPost("DissassociateRisk/{riskId}/{investmentId}")]
        public IActionResult DissassociateRisk(int riskId, int investmentId)
        {
            var riskInvestmentLink = EntityRepository.Db.Find<InvestmentRisk_Investment>(investmentId, riskId);
            EntityRepository.Db.Remove(riskInvestmentLink);
            EntityRepository.SaveChanges();

            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(), "Updates an existing entity", GetUser(), "Dissassociated risk", $"Dissassociated risk '{EntityRepository.GetEntityByType<InvestmentRisk>().Single(r => r.Id == riskId).Name}' with investment.", DateTimeOffset.UtcNow,
                investmentId, EntityType.Investment));
            EntityRepository.Db.SaveChanges();

            return new NoContentResult();
        }

        /// <summary>
        /// Dissosicate a custom entity with an investment
        /// </summary>
        /// <param name="customEntityId">custom entity to dissasociate</param>
        /// <param name="investmentId">investment to dissasociate risk from</param>
        /// <returns>Status Code</returns>
        [HttpPost("DissassociateCustomEntity/{customEntityId}/{investmentId}")]
        public IActionResult DissassociateCustomEntity(int customEntityId, int investmentId)
        {
            var entityInvestmentLink = EntityRepository.Db.Find<CustomEntity_Investment>(investmentId, customEntityId);
            EntityRepository.Db.Remove(entityInvestmentLink);
            EntityRepository.SaveChanges();

            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),
                "Updates an existing entity", GetUser(), "Dissassociated custom entity"
                , $"Dissassociated custom entity '{EntityRepository.GetEntityByType<CustomEntity>().Single(r => r.Id == customEntityId).Name}' with investment.",
                DateTimeOffset.UtcNow,
                investmentId, EntityType.Investment));
            EntityRepository.Db.SaveChanges();

            return new NoContentResult();
        }

        /// <summary>
        /// Dissociate a factor with an investment
        /// </summary>
        /// <param name="factorId">factor id</param>
        /// <param name="investmentId">investment id</param>
        /// <returns>Status code</returns>
        [HttpPost("DissassociateFactor/{factorId}/{investmentId}")]
        public IActionResult DissassociateFactor(int factorId, int investmentId)
        {
            var factorInvestmentLink = EntityRepository.Db.Find<InvestmentInfluenceFactor_Investment>(investmentId, factorId);
            EntityRepository.Db.Remove(factorInvestmentLink);
            EntityRepository.SaveChanges();

            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),"Updates an entity", GetUser(), "Dissassociated factor", $"Dissassociated factor '{EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().Single(f => f.Id == factorId).Name}' with investment.", DateTimeOffset.UtcNow,
                investmentId, EntityType.Investment));
            EntityRepository.Db.SaveChanges();

            return new NoContentResult();
        }

        /// <summary>
        /// Dissociate a group from a investment
        /// </summary>
        /// <param name="groupId">group to dissasoocate</param>
        /// <param name="investmentId">investment</param>
        /// <returns>Status Code</returns>
        [HttpPost("DissassociateGroup/{groupId}/{investmentId}")]
        public IActionResult DissassociateGroup(int groupId, int investmentId)
        {
            var groupInvestmentLink = EntityRepository.Db.Find<InvestmentGroup_Investment>(investmentId, groupId);
            EntityRepository.Db.Remove(groupInvestmentLink);
            EntityRepository.SaveChanges();

            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),"Updates an entity",GetUser(), "Dissassociated group", $"Dissassociated group '{EntityRepository.GetEntityByType<InvestmentGroup>().Single(g => g.Id == groupId).Name}' with investment.", DateTimeOffset.UtcNow,
                investmentId, EntityType.Investment));

            return new NoContentResult();
        }

        /// <summary>
        /// Dissasociates the region from an investment
        /// </summary>
        /// <param name="regionId">region to dissasociate</param>
        /// <param name="investmentId">the investment</param>
        /// <returns>status code</returns>
        [HttpPost("DissassociateRegion/{regionId}/{investmentId}")]
        public IActionResult DissassociateRegion(int regionId, int investmentId)
        {
            var regionInvestmentLink = EntityRepository.Db.Find<Region_Investment>(investmentId, regionId);
            EntityRepository.Db.Remove(regionInvestmentLink);
            EntityRepository.SaveChanges();

            EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),"Updates an entity",GetUser(), "Dissassociated region", $"Dissassociated group '{EntityRepository.GetEntityByType<Region>().Single(r => r.Id == regionId).Name}' with investment.", DateTimeOffset.UtcNow,
                investmentId, EntityType.Investment));

            return new NoContentResult();
        }
        

        /// <summary>
        /// Associates multiple risks with an investment
        /// </summary>
        /// <param name="id">investment</param>
        /// <param name="riskIds">list of risk ids</param>
        /// <returns>status code</returns>
        [HttpPost("AssociateRisks/{id}")]
        public IActionResult AssociateRisks(int id, [FromBody] int[] riskIds)
        {
            foreach (var riskId in riskIds)
            {
                var riskInvestmentLink = new InvestmentRisk_Investment
                {
                    InvestmentID = id,
                    InvestmentRiskID = riskId
                };
                EntityRepository.Db.Add(riskInvestmentLink);
                EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),"Updates an entity",GetUser(), "Associated risk", $"Associated risk '{EntityRepository.GetEntityByType<InvestmentRisk>().Single(r => r.Id == riskId).Name}' with investment.", DateTimeOffset.UtcNow,
                    id, EntityType.Investment));

            }
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Associate custom entity with investments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customEnityIds"></param>
        /// <returns></returns>
        [HttpPost("AssociateCustomEntities/{id}")]
        public IActionResult AssociateCustomEntities(int id, [FromBody] int[] customEnityIds)
        {
            foreach (var customEntityId in customEnityIds)
            {
                var customEntityInvestmentLink = new CustomEntity_Investment
                {
                    InvestmentID = id,
                    CustomEntityId = customEntityId
                };
                EntityRepository.Db.Add(customEntityInvestmentLink);
                EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),
                    "Update an entity", GetUser(), "Associated custom entity",
                    $"Associated custom entity '{EntityRepository.GetEntityByType<CustomEntity>().Single(e => e.Id == customEntityId).Name}' with investment",
                    DateTimeOffset.UtcNow,
                    id, EntityType.Custom));
                
            }
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Associates factors(as checkmodels) with an investment
        /// </summary>
        /// <param name="id">investment</param>
        /// <param name="factorIds"></param>
        /// <returns>status code</returns>
        [HttpPost("AssociateFactors/{id}")]        
        public IActionResult AssociateFactors(int id, [FromBody] int[] factorIds)
        {
            foreach (var factorId in factorIds)
            {
                var factorInvestmentLink = new InvestmentInfluenceFactor_Investment
                {
                    InvestmentID = id,
                    InvestmentInfluenceFactorID = factorId
                };
                EntityRepository.Db.Add(factorInvestmentLink);
                EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),"Updates an entity",GetUser(), "Associated factor", $"Associated factor '{EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().Single(f => f.Id == factorId).Name}' with investment.", DateTimeOffset.UtcNow,
                    id, EntityType.Investment));
            }
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Associate a groups with an investment
        /// </summary>
        /// <param name="id">investment</param>
        /// <param name="groupIDs"></param>
        /// <returns>status code</returns>
        [HttpPost("AssociateGroups/{id}")]
        public IActionResult AssociateGroups(int id, [FromBody] int[] groupIDs)
        {
            foreach (var groupId in groupIDs)
            {
                var groupInvestmentLink = new InvestmentGroup_Investment
                {
                    InvestmentID = id,
                    InvestmentGroupID = groupId
                };
                EntityRepository.Db.Add(groupInvestmentLink);
                EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),"Updates an entity",GetUser(), "Associated group",
                    $"Associated group '{EntityRepository.GetEntityByType<InvestmentGroup>().Single(g => g.Id == groupId).Name}' with investment..",
                    DateTimeOffset.UtcNow, id, EntityType.Investment));
            }
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        
        /// <summary>
        /// Associate regions with an investment
        /// </summary>
        /// <param name="id">investment</param>
        /// <param name="regionIDs"></param>
        /// <returns>status code</returns>
        [HttpPost("AssociateRegions/{id}")]
        public IActionResult AssociateRegions(int id, [FromBody] int[] regionIDs)
        {
            foreach (var regionId in regionIDs)
            {
                var regionInvestmentLink = new Region_Investment
                {
                    InvestmentID = id,
                    RegionID = regionId
                };
                EntityRepository.Db.Add(regionInvestmentLink);
                EntityRepository.Db.RecordedActivities.Add(new RecordedActivity(ActivityOperation.Update.ToString(),"Updates an entity",GetUser(), "Associated region",
                    $"Associated region '{EntityRepository.GetEntityByType<Region>().Single(r => r.Id == regionId).Name}'  with investment..",
                    DateTimeOffset.UtcNow, id, EntityType.Investment));
            }
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }
        #endregion

        #region Graphs Generation Methods

        /// <summary>
        /// Easily Generate investment graphs of entities that implement IInvestmentEntity, IDbInvestmentEntityHasInvestments
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="investmentId"></param>
        /// <param name="aspects"></param>
        /// <returns></returns>
        private IActionResult GenerateGraph<T, T1>(int investmentId, IEnumerable<T> aspects)
            where T : IInvestmentEntity,
                      IDbInvestmentEntityHasInvestments<T1>
            where T1 : IWithAnInvestment
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

            var thisInvestment = EntityRepository.Db.Investments.Find(investmentId);
            var nodes = new List<object> { new { name = thisInvestment.Name, value = 1 } };
            var links = new List<object>();
            
            foreach (var aspect in aspects)
            {
                var lastNode = nodes.Count;
                var otherInvestments = aspect.Investments.Filter(i => !i.Investment.Name.Equals(thisInvestment.Name));
                nodes.Add(new { name = aspect.Name, value = aspect.Investments.Count });
                links.Add(new { source = 0, target = lastNode, value = aspect.Investments.Count });

                foreach (var other in otherInvestments)
                {
                    nodes.Add(new { name = other.Investment.Name, value = 1 });
                    links.Add(new { source = lastNode, target = nodes.Count-1, value = aspect.Investments.Count });
                }
            }
            return new ObjectResult(new { nodes, links });
        }

        /// <summary>
        /// Generates graph data all risks given associated with particular investment
        /// </summary>
        /// <param name="id">The investment Id</param>
        /// <returns>A json repsresentation of the graph data</returns>
        [HttpGet("RisksGraph/{id}")]
        public IActionResult GenerateRisksGraph(int id)
        {
            var investment = EntityRepository.GetOneOrAllEntities()
                .Include(e => e.Risks)
                .ThenInclude(e => e.InvestmentRisk).Single(o => o.Id == id);

            return GenerateGraph<InvestmentRisk, InvestmentRisk_Investment>(id, investment.Risks.Select(r => r.InvestmentRisk));
        }

        /// <summary>
        /// Generates graph data all custom entities given associated with particular investment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("CustomEntitiesGraph/{id}")]
        public IActionResult GenerateCustomEntitiesGraph(int id)
        {
            var investment = EntityRepository.GetOneOrAllEntities()
                .Include(e => e.CustomEntities)
                .ThenInclude(e => e.CustomEntity).Single(o => o.Id == id);
            return GenerateGraph<CustomEntity, CustomEntity_Investment>(id,
                investment.CustomEntities.Select(r => r.CustomEntity));
        }

        /// <summary>
        /// Generates graph data with all factors associated with particular investment
        /// </summary>
        /// <param name="id">Investment Id</param>
        /// <returns>Graph Json</returns>
        [HttpGet("FactorsGraph/{id}")]
        public IActionResult GenerateFactorsGraph(int id)
        {
            var investment = EntityRepository.GetOneOrAllEntities()
                .Include(e => e.Factors)
                .ThenInclude(e => e.InvestmentInfluenceFactor).Single(o => o.Id == id);
            return GenerateGraph<InvestmentInfluenceFactor, InvestmentInfluenceFactor_Investment>(id, investment.Factors.Select(o => o.InvestmentInfluenceFactor));
        }

        /// <summary>
        /// Generates graph data with all groups associated with particular investment
        /// </summary>
        /// <param name="id">investment ID</param>
        /// <returns></returns>
        [HttpGet("GroupsGraph/{id}")]
        public IActionResult GenerateGroupsGraph(int id)
        {
            var investment = EntityRepository.GetOneOrAllEntities()
                .Include(e => e.Groups)
                .ThenInclude(e => e.InvestmentGroup).Single(o => o.Id == id);
            return GenerateGraph<InvestmentGroup, InvestmentGroup_Investment>(id, investment.Groups.Select(o => o.InvestmentGroup));
        }

        /// <summary>
        /// Generates graph data with all regions associated with particular investment
        /// </summary>
        /// <param name="id">investment id</param>
        /// <returns>graph data in json format</returns>
        [HttpGet("RegionsGraph/{id}")]
        public IActionResult GenerateRegionsGraph(int id)
        {
            var investment = EntityRepository.GetOneOrAllEntities()
                .Include(e => e.Regions)
                .ThenInclude(e => e.Region).Single(o => o.Id == id);
            return GenerateGraph<Region, Region_Investment>(id, investment.Regions.Select(r => r.Region));
        }

        #endregion
    }
}