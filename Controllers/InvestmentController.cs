using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WinInvestmentTracker.Models.BOLO;
using Microsoft.EntityFrameworkCore;


namespace CoreInvestmentTracker.Controllers
{
    [GlobalLogging]
    public class InvestmentController : EntityManagedController<Investment>
    {
        public InvestmentController(IEntityApplicationDbContext<Investment> db, IMyLogger logger) 
            : base(db, logger)
        {
        }

        public IActionResult GenerateRisksGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Risks)
                .ThenInclude(e => e.InvestmentRisk).Single(o=>o.ID == ID);

            return GenerateGraph<InvestmentRisk, InvestmentRisk_Investment>(ID, investment.Risks.Select(r=>r.InvestmentRisk));
        }

        /// <summary>
        /// Easily Generate investment graphs of entities that implement IDbInvestmentEntity, IDbInvestmentEntityHasInvestments
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="investmentId"></param>
        /// <param name="aspects"></param>
        /// <returns></returns>
        private IActionResult GenerateGraph<T,T1>(int investmentId, IEnumerable<T> aspects) 
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

            var investment = EntityRepository.Entities.Find(investmentId);
            var nodes = new List<object> { new { name = investment.Name, value = 1 } };
            var links = new List<object>();
            var index = 1;
            foreach (var aspect in aspects)
            {
                nodes.Add(new { name = aspect.Name, value = aspect.Investments.Count });
                links.Add(new { source = 0, target = index, value = aspect.Investments.Count });
                index++;
            }
            return Json(new { nodes, links });
        }



        public IActionResult GenerateFactorsGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Factors)
                .ThenInclude(e => e.InvestmentInfluenceFactor).Single(o => o.ID == ID);
            return GenerateGraph<InvestmentInfluenceFactor,InvestmentInfluenceFactor_Investment>(ID, investment.Factors.Select(o=>o.InvestmentInfluenceFactor));
        }

        public IActionResult GenerateGroupsGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Groups)
                .ThenInclude(e => e.InvestmentGroup).Single(o => o.ID == ID);
            return GenerateGraph<InvestmentGroup, InvestmentGroup_Investment>(ID, investment.Groups.Select(o=>o.InvestmentGroup));
        }

        public IActionResult GenerateRegionsGraph(int ID)
        {
            var investment = EntityRepository.Entities
                .Include(e => e.Regions)
                .ThenInclude(e => e.Region).Single(o => o.ID == ID);
            return GenerateGraph<Region, Region_Investment>(ID, investment.Regions.Select(r=>r.Region));
        }

        public IActionResult SelectFactors()
        {
            var checkModels = EntityRepository.
                GetEntityByType<InvestmentInfluenceFactor>().
                Select(o => new CheckModel
                {
                    ID = o.ID,
                    Name = o.Name,
                    Checked = false
                }
            ).ToList();
            
            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Factors", createActionControllerName:"Factor", createActionName: "Create", redirectToControllerName:"Investment", redirectToAction:"SelectFactors");
            return View("SelectItems", checkModels);
        }

        /// <summary>
        /// Provides a new action URL in Viewbag.ActionUrl that SelectItems view pulls out and exposes
        /// This is used so that we can use the same generic SelectItems view and CheckmModels but
        /// customize it depending on the underlying type of items the select items view is showing.
        /// We usually want a 'Create' button at the bottom, going to the Create action of the underlying type that we are
        /// selecting so that we can create a new one and when we are done we want to be redirected back to where we started after the
        /// 'Create' action is done for that item but by default the create action redirects to 'Details'. Here we can ovveride that.
        /// </summary>
        /// <param name="checkItemsViewTitle"></param>
        /// <param name="createActionControllerName"></param>
        /// <param name="createActionName"></param>
        /// <param name="redirectToControllerName"></param>
        /// <param name="redirectToAction"></param>
        private void AddCustomCreateAndCustomCreateRedirect(string checkItemsViewTitle, string createActionControllerName, string createActionName, string redirectToControllerName, string redirectToAction, object routeValues = null)
        {
            ViewBag.Title = checkItemsViewTitle;
            ViewBag.CustomActionUrl = Url.Action(createActionName, createActionControllerName, null, null);
            ViewBag.CustomActionName = "Create new " + createActionControllerName.ToLower();


            // This will force any Create actions to go to the following controller/action instead.
            OverrideCreateActionRedirect(redirectToControllerName, redirectToAction, routeValues);
        }

        /// <summary>
        /// "Create" actions usually redirect to the "Details" page unless we say otherwise here
        /// </summary>
        /// <param name="returnControllerName">controller to redirect to</param>
        /// <param name="returnAction">action in controller to redirect to</param>
        private void OverrideCreateActionRedirect(string returnControllerName, string returnAction, object routeValues = null)
        {
            TempData["ReturnAction"] = returnAction;
            TempData["ReturnController"] = returnControllerName;
            TempData["ReturnRouteValues"] = routeValues;
        }

        [HttpPost]
        [ClearCustomRedirects]
        public IActionResult SelectFactors(List<CheckModel> checkModels)
        {
            TempData["factorIDs"] = checkModels.Where(o=>o.Checked).Select(o=>o.ID).ToArray();
            return RedirectToAction("SelectRisks");
        }

        public IActionResult SelectRisks()
        {
            var checkModels = EntityRepository.
               GetEntityByType<InvestmentRisk>().
               Select(o => new CheckModel
               {
                   ID = o.ID,
                   Name = o.Name,
                   Checked = false
               }
           ).ToList();

            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Risks", createActionControllerName: "Risk", createActionName: "Create", redirectToControllerName: "Investment", redirectToAction: "SelectRisks");
            return View("SelectItems", checkModels);
        }

        [HttpPost]
        [ClearCustomRedirects]
        public IActionResult SelectRisks(List<CheckModel> checkModels)
        {
            TempData["riskIDs"] = checkModels.Where(o => o.Checked).Select(o => o.ID).ToArray();
            return RedirectToAction("SelectRegions");
        }

        public IActionResult SelectRegions()
        {
            var checkModels = EntityRepository.
              GetEntityByType<Region>().
              Select(o => new CheckModel
              {
                  ID = o.ID,
                  Name = o.Name,
                  Checked = false
              }
          ).ToList();
            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Regions", createActionControllerName: "Region", createActionName: "Create", redirectToControllerName: "Investment", redirectToAction: "SelectRegions");
            return View("SelectItems", checkModels);
        }

        [HttpPost]
        [ClearCustomRedirects]
        public IActionResult SelectRegions(List<CheckModel> checkModels)
        {
            TempData["regionIDs"] = checkModels.Where(o => o.Checked).Select(o => o.ID).ToArray();
            return RedirectToAction("SelectGroups");
        }

        public IActionResult SelectGroups()
        {
            var checkModels = EntityRepository.
              GetEntityByType<InvestmentGroup>().
              Select(o => new CheckModel
              {
                  ID = o.ID,
                  Name = o.Name,
                  Checked = false,
                  Fields = new List<CustomField<string, string>> {
                      new CustomField<string, string> { Name = nameof(o.Type), Value = o.Type },
                      new CustomField<string, string> { Name = nameof(o.ID), Value = o.ID.ToString() },
                  }
              }
          ).ToList();
            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Groups", createActionControllerName: "Group", createActionName: "Create", redirectToControllerName: "Investment", redirectToAction: "SelectGroups");
            return View("SelectItems", checkModels);
        }

        [HttpPost]
        [ClearCustomRedirects]
        public IActionResult SelectGroups(List<CheckModel> checkModels)
        {
            TempData["groupIDs"] = checkModels.Where(o => o.Checked).Select(o => o.ID).ToArray();
            return RedirectToAction("Summary");
        }

        public IActionResult Summary()
        {
            var investment = TempData.Peek("investment");
            return View(investment);
        }

        [HttpPost]
        public IActionResult Summary(string outcome)
        {
            if (outcome.Equals("Cancel"))
                return RedirectToAction("Index");
            var investment = (Investment)TempData["investment"];

            var factorIDs = (int[])TempData["factorIDs"];
            var factors = factorIDs.Select(f => EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().Find(f));
            investment.Factors = factors.Select(f => new InvestmentInfluenceFactor_Investment
            {
                InvestmentInfluenceFactor = f,
                Investment = investment
            }).ToList();

            var risksIDs = (int[])TempData["riskIDs"];
            var risks = risksIDs.Select(r => EntityRepository.GetEntityByType<InvestmentRisk>().Find(r));
            investment.Risks = risks.Select(r => new InvestmentRisk_Investment
            {
                InvestmentRisk = r,
                Investment = investment
            }).ToList();

            var groupIDs = (int[])TempData["groupIDs"];
            var groups = groupIDs.Select(g => EntityRepository.GetEntityByType<InvestmentGroup>().Find(g));
            investment.Groups = groups.Select(g=> new InvestmentGroup_Investment
            {
                InvestmentGroup = g,
                Investment = investment
            }).ToList();

            var regionIDs = (int[])TempData["regionIDs"];
            var regions = regionIDs.Select(g => EntityRepository.GetEntityByType<Region>().Find(g));
            investment.Regions = regions.Select(r => new Region_Investment
            {
                Region = r,
                Investment = investment
            }).ToList();

            EntityRepository.Entities.Add(investment);
            EntityRepository.SaveChanges();


            return RedirectToAction("Details", investment);
        }

        [HttpPost]
        public override IActionResult Create(Investment entity)
        {
            try
            {
                TempData["investment"] = entity;
                return RedirectToAction("SelectFactors");
            }
            catch
            {
                return View(entity);
            }
        }


        public IActionResult InvestmentByRisk(int id)
        {
            var risk = EntityRepository.GetEntityByType<InvestmentRisk>().SingleOrDefault(r => r.ID == id);
            var risks = risk.Investments;
            ViewBag.ExtraTitle = $"By Investment Risk: {risk.Name}";
            return View("Index", risks);


        }

        public IActionResult InvestmentByFactor(int id)
        {
            return View("Index", EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().SingleOrDefault(x => x.ID == id).Investments);
        }

        public IActionResult InvestmentByGroup(int id)
        {
            return View("Index", EntityRepository.GetEntityByType<InvestmentGroup>().SingleOrDefault(x => x.ID == id).Investments);
        }

        public IActionResult InvestmentByRegion(int id)
        {
            return View("Index", EntityRepository.GetEntityByType<Region>().SingleOrDefault(x => x.ID == id).Investments);
        }

        public IActionResult DissassociateRisk(int riskID, int investmentID)
        {
            var investment = EntityRepository.Entities.Find(investmentID);
            var risk = investment.Risks.First(r => r.InvestmentRiskID == riskID);
            investment.Risks.Remove(risk);
            EntityRepository.SaveChanges();

            return RedirectToAction("Details", investment);
        }

        public IActionResult DissassociateFactor(int factorID, int investmentID)
        {
            var investment = EntityRepository.Entities.Find(investmentID);
            var factor = investment.Factors.First(f => f.InvestmentInfluenceFactorID == factorID);
            investment.Factors.Remove(factor);
            EntityRepository.SaveChanges();

            return RedirectToAction("Details", investment);
        }

        public IActionResult DissassociateGroup(int groupID, int investmentID)
        {
            var investment = EntityRepository.Entities.Find(investmentID);
            var group = investment.Groups.First(g => g.InvestmentGroupID == groupID);
            investment.Groups.Remove(group);
            EntityRepository.SaveChanges();

            return RedirectToAction("Details", investment);
        }

        public IActionResult DissassociateRegion(int regionID, int investmentID)
        {
            var investment = EntityRepository.Entities.Find(investmentID);
            var region = investment.Regions.First(r => r.RegionID == regionID);
            investment.Regions.Remove(region);
            EntityRepository.SaveChanges();

            return RedirectToAction("Details", investment);
        }

        public IActionResult AssociateRisk(int id)
        {
            var investment = EntityRepository.Entities.Find(id);
            var model = new ParentChildEntity<CheckModel, Investment>
            {
                Parent = investment,
                Children = EntityRepository.GetEntityByType<InvestmentRisk>().Select(risk => new CheckModel
                {
                    ID = risk.ID,
                    Name = risk.Name,
                    Description = risk.Description,
                    Checked = false
                }).ToList()
            };
            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Risks", createActionControllerName: "Risk", createActionName: "Create", redirectToControllerName: "Investment", redirectToAction: "AssociateRisk", routeValues: new { Id = id });
            return View("SelectItemsWithParent", model);
        }

        [HttpPost]
        [ClearCustomRedirects]
        public IActionResult AssociateRisk(int id, List<CheckModel> Children)
        {
            var investment = EntityRepository.Entities.Find(id);
            var riskIDs = Children.Where(o => o.Checked).Select(o => o.ID);
            foreach (var ID in riskIDs)
            {
                var risk = EntityRepository.GetEntityByType<InvestmentRisk>().Find(ID);
                investment.Risks.Add(new InvestmentRisk_Investment { Investment = investment, InvestmentRisk = risk });
            }
            EntityRepository.SaveChanges();


            return RedirectToAction("Details", investment);
        }


        public IActionResult AssociateFactor(int id)
        {
            var investment = EntityRepository.Entities.Find(id);
            var model = new ParentChildEntity<CheckModel, Investment>
            {
                Parent = investment,
                Children = EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().Select(risk => new CheckModel
                {
                    ID = risk.ID,
                    Name = risk.Name,
                    Description = risk.Description,
                    Checked = false
                }).ToList()
            };
            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Factors", createActionControllerName: "Factor", createActionName: "Create", redirectToControllerName: "Investment", redirectToAction: "AssociateFactor", routeValues: new { Id = id });
            return View("SelectItemsWithParent", model);
        }

        [HttpPost]
        [ClearCustomRedirects]
        public IActionResult AssociateFactor(int id, List<CheckModel> Children)
        {
            var investment = EntityRepository.Entities.Find(id);
            var factorIDs = Children.Where(o => o.Checked).Select(o => o.ID);
            foreach (var ID in factorIDs)
            {
                var factor = EntityRepository.GetEntityByType<InvestmentInfluenceFactor>().Find(ID);
                investment.Factors.Add(new InvestmentInfluenceFactor_Investment { Investment = investment, InvestmentInfluenceFactor = factor });
            }
            EntityRepository.SaveChanges();


            return RedirectToAction("Details", investment);
        }

        public IActionResult AssociateGroup(int id)
        {
            var investment = EntityRepository.Entities.Find(id);
            var model = new ParentChildEntity<CheckModel, Investment>
            {
                Parent = investment,
                Children = EntityRepository.GetEntityByType<InvestmentGroup>().Select(group => new CheckModel
                {
                    ID = group.ID,
                    Name = group.Name,
                    Description = group.Description,
                    Checked = false,

                }).ToList()
            };
            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Groups", createActionControllerName: "Group", createActionName: "Create", redirectToControllerName: "Investment", redirectToAction: "AssociateGroup", routeValues: new { Id = id });
            return View("SelectItemsWithParent", model);
        }

        [HttpPost]
        [ClearCustomRedirects]
        public IActionResult AssociateGroup(int id, List<CheckModel> Children)
        {
            var investment = EntityRepository.Entities.Find(id);
            var groupIDs = Children.Where(o => o.Checked).Select(o => o.ID);
            foreach (var ID in groupIDs)
            {
                var group = EntityRepository.GetEntityByType<InvestmentGroup>().Find(ID);
                investment.Groups.Add(new InvestmentGroup_Investment { Investment = investment, InvestmentGroup = group });
            }
            EntityRepository.SaveChanges();


            return RedirectToAction("Details", investment);
        }
        //
        public IActionResult AssociateRegion(int id)
        {
            var investment = EntityRepository.Entities.Find(id);
            var model = new ParentChildEntity<CheckModel, Investment>
            {
                Parent = investment,
                Children = EntityRepository.GetEntityByType<Region>().Select(risk => new CheckModel
                {
                    ID = risk.ID,
                    Name = risk.Name,
                    Description = risk.Description,
                    Checked = false
                }).ToList()
            };
            AddCustomCreateAndCustomCreateRedirect(checkItemsViewTitle: "Regions", createActionControllerName: "Region", createActionName: "Create", redirectToControllerName: "Investment", redirectToAction: "AssociateRegion", routeValues: new { Id = id });
            return View("SelectItemsWithParent", model);
        }

        [HttpPost]
        [ClearCustomRedirects]
        [GlobalLoggingAttribute]
        public IActionResult AssociateRegion(int id, List<CheckModel> Children)
        {
            var investment = EntityRepository.Entities.Find(id);
            var regionIDs = Children.Where(o => o.Checked).Select(o => o.ID);
            foreach (var ID in regionIDs)
            {
                var region = EntityRepository.GetEntityByType<Region>().Find(ID);
                investment.Regions.Add(new Region_Investment { Investment = investment, Region = region });
            }
            EntityRepository.SaveChanges();


            return RedirectToAction("Details", investment);
        }

        [GlobalLoggingAttribute]
        public IActionResult ShowGraph()
        {
            Logger.Debug("ShowGraph");
            return View();
        }
    }
}