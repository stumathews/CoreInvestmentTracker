using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WinInvestmentTracker.Models.BOLO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace CoreInvestmentTracker.Controllers
{
    /// <summary>
    /// The Investment controller
    /// </summary>
    [GlobalControllerLogging]
    [Route("api/[controller]")]
    public class InvestmentController : EntityManagedController<Investment>
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="logger">loggin mechanism</param>
        public InvestmentController(IEntityApplicationDbContext<Investment> db, IMyLogger logger) 
            : base(db, logger)
        {
        }

        

        /// <summary>
        /// Generates a data for a risk graph
        /// </summary>
        /// <param name="ID">The investment Id</param>
        /// <returns>A json repsresentation of the graph data</returns>
        [HttpGet("RisksGraph/{id}")]
        public IActionResult GenerateRisksGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Risks)
                .ThenInclude(e => e.InvestmentRisk).Single(o => o.ID == ID);

            return GenerateGraph<InvestmentRisk, InvestmentRisk_Investment>(ID, investment.Risks.Select(r => r.InvestmentRisk));
        }

        /// <summary>
        /// Easily Generate investment graphs of entities that implement IDbInvestmentEntity, IDbInvestmentEntityHasInvestments
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="investmentId"></param>
        /// <param name="aspects"></param>
        /// <returns></returns>
        private IActionResult GenerateGraph<T, T1>(int investmentId, IEnumerable<T> aspects)
            where T : IDbInvestmentEntity,
                      IDbInvestmentEntityHasInvestments<T1>
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

            var investment = EntityRepository.db.Investments.Find(investmentId);
            var nodes = new List<object> { new { name = investment.Name, value = 1 } };
            var links = new List<object>();
            var index = 1;
            foreach (var aspect in aspects)
            {
                nodes.Add(new { name = aspect.Name, value = aspect.Investments.Count });
                links.Add(new { source = 0, target = index, value = aspect.Investments.Count });
                index++;
            }
            return new ObjectResult(new { nodes, links });
        }

        /// <summary>
        /// Generates data to show risk information in a  graph
        /// </summary>
        /// <param name="ID">Investment Id</param>
        /// <returns>Graph Json</returns>
        [HttpGet("FactorsGraph/{id}")]
        public IActionResult GenerateFactorsGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Factors)
                .ThenInclude(e => e.InvestmentInfluenceFactor).Single(o => o.ID == ID);
            return GenerateGraph<InvestmentInfluenceFactor, InvestmentInfluenceFactor_Investment>(ID, investment.Factors.Select(o => o.InvestmentInfluenceFactor));
        }

        /// <summary>
        /// Generates data to show group information in a graph
        /// </summary>
        /// <param name="ID">investment ID</param>
        /// <returns></returns>
        [HttpGet("GroupsGraph/{id}")]
        public IActionResult GenerateGroupsGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Groups)
                .ThenInclude(e => e.InvestmentGroup).Single(o => o.ID == ID);
            return GenerateGraph<InvestmentGroup, InvestmentGroup_Investment>(ID, investment.Groups.Select(o => o.InvestmentGroup));
        }

        /// <summary>
        /// Generates graph data that shows regions for investment
        /// </summary>
        /// <param name="ID">investment id</param>
        /// <returns>graph data in json format</returns>
        [HttpGet("RegionsGraph/{id}")]
        public IActionResult GenerateRegionsGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Regions)
                .ThenInclude(e => e.Region).Single(o => o.ID == ID);
            return GenerateGraph<Region, Region_Investment>(ID, investment.Regions.Select(r => r.Region));
        }
        
                
        /// <summary>
        /// Get investments By Risk
        /// </summary>
        /// <param name="id">Risk ID</param>
        /// <returns>Investments associated with this risk</returns>
        [HttpGet("ByRisk/{id}")]
        public IActionResult InvestmentByRisk(int id)
        {
            var risk = EntityRepository.GetEntityByType<InvestmentRisk>().SingleOrDefault(r => r.ID == id);
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
            return new ObjectResult(EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().SingleOrDefault(x => x.ID == id).Investments);
        }

        /// <summary>
        /// Get investments by group
        /// </summary>
        /// <param name="id">group id</param>
        /// <returns>Investments associated with this group</returns>
        [HttpGet("ByGroup/{id}")]
        public IActionResult InvestmentByGroup(int id)
        {
            return new ObjectResult(EntityRepository.GetEntityByType<InvestmentGroup>().SingleOrDefault(x => x.ID == id).Investments);
        }

        /// <summary>
        /// Gets investments by region
        /// </summary>
        /// <param name="id">region id</param>
        /// <returns>investments associated with this region</returns>
        [HttpGet("ByRegion/{id}")]
        public IActionResult InvestmentByRegion(int id)
        {
            return new ObjectResult(EntityRepository.GetEntityByType<Region>().SingleOrDefault(x => x.ID == id).Investments);
        }

        /// <summary>
        /// Dissosicate a risk with an investment
        /// </summary>
        /// <param name="riskID">risk to dissasociate</param>
        /// <param name="investmentID">investment to dissasociate risk from</param>
        /// <returns>Status Code</returns>
        [HttpPost("DissassociateRisk/{riskID}/{investmentID}")]
        public IActionResult DissassociateRisk(int riskID, int investmentID)
        {
            var riskInvestmentLink = EntityRepository.db.Find<InvestmentRisk_Investment>(new object[] { investmentID, riskID });
            EntityRepository.db.Remove(riskInvestmentLink);
            EntityRepository.SaveChanges();

            return new NoContentResult();
        }

        /// <summary>
        /// Dissociate a factor with an investment
        /// </summary>
        /// <param name="factorID">factor id</param>
        /// <param name="investmentID">investment id</param>
        /// <returns>Status code</returns>
        [HttpPost("DissassociateFactor/{factorID}/{investmentID}")]
        public IActionResult DissassociateFactor(int factorID, int investmentID)
        {
            var factorInvestmentLink = EntityRepository.db.Find<InvestmentInfluenceFactor_Investment>(new object[] { investmentID, factorID });
            EntityRepository.db.Remove(factorInvestmentLink);
            EntityRepository.SaveChanges();

            return new NoContentResult();
        }

        /// <summary>
        /// Dissociate a group from a investment
        /// </summary>
        /// <param name="groupID">group to dissasoocate</param>
        /// <param name="investmentID">investment</param>
        /// <returns>Status Code</returns>
        [HttpPost("DissassociateGroup/{groupID}/{investmentID}")]
        public IActionResult DissassociateGroup(int groupID, int investmentID)
        {
            var groupInvestmentLink = EntityRepository.db.Find<InvestmentGroup_Investment>(new object[] { investmentID, groupID });
            EntityRepository.db.Remove(groupInvestmentLink);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Dissasociates the region from an investment
        /// </summary>
        /// <param name="regionID">region to dissasociate</param>
        /// <param name="investmentID">the investment</param>
        /// <returns>status code</returns>
        [HttpPost("DissassociateRegion/{regionID}/{investmentID}")]
        public IActionResult DissassociateRegion(int regionID, int investmentID)
        {
            var regionInvestmentLink = EntityRepository.db.Find<Region_Investment>(new object[] { investmentID, regionID });
            EntityRepository.db.Remove(regionInvestmentLink);
            EntityRepository.SaveChanges();

            return new NoContentResult();
        }
        

        /// <summary>
        /// Associates Risks with an investment where risks are represented as CheckModels
        /// </summary>
        /// <param name="id">investment</param>
        /// <param name="riskIDs"></param>
        /// <returns>status code</returns>
        [HttpPost("AssociateRisks/{id}")]
        public IActionResult AssociateRisks(int id, [FromBody] int[] riskIDs)
        {
            foreach (var riskID in riskIDs)
            {
                var riskInvestmentLink = new InvestmentRisk_Investment
                {
                    InvestmentID = id,
                    InvestmentRiskID = riskID
                };
                EntityRepository.db.Add(riskInvestmentLink);
            }
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Associates factors(as checkmodels) with an investment
        /// </summary>
        /// <param name="id">investment</param>
        /// <param name="factorIDs"></param>
        /// <returns>status code</returns>
        [HttpPost("AssociateFactors/{id}")]        
        public IActionResult AssociateFactors(int id, [FromBody] int[] factorIDs)
        {
            foreach (var factorID in factorIDs)
            {
                var factorInvestmentLink = new InvestmentInfluenceFactor_Investment{
                    InvestmentID = id,
                    InvestmentInfluenceFactorID = factorID
                };
                EntityRepository.db.Add(factorInvestmentLink);
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
            foreach (var groupID in groupIDs)
            {
                var groupInvestmentLink = new InvestmentGroup_Investment
                {
                    InvestmentID = id,
                    InvestmentGroupID = groupID
                };
                EntityRepository.db.Add(groupInvestmentLink);
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
            foreach (var regionID in regionIDs)
            {
                var regionInvestmentLink = new Region_Investment
                {
                    InvestmentID = id,
                    RegionID = regionID
                };
                EntityRepository.db.Add(regionInvestmentLink);
            }
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }
    }
}