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
using System.Threading.Tasks;
//using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace CoreInvestmentTracker.Common
{    
    [GlobalLogging]
    /// <summary>
    /// A controller that has access the the strongly typed entity type specified through the EntityRepository memeber.
    /// Also contains the common controller Actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityManagedController<T> : Controller where T : class, IDbInvestmentEntity
    {
        //[Dependency]
        protected IMyLogger Logger { get; set; }
                
        /// <summary>
        /// Access to te underlying store of entities
        /// </summary>
        //[Dependency]
        

        public readonly IEntityApplicationDbContext<T> EntityRepository;
        public EntityManagedController(IEntityApplicationDbContext<T> entityApplicationDbContext)
        {
            EntityRepository = entityApplicationDbContext;
        }
        
        /// <summary>
        /// Primarily used to update entities using x-editable post backs
        /// </summary>
        /// <param name="name">name of changed entity property</param>
        /// <param name="value">value of the property</param>
        /// <param name="pk">the primary key of the entity</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(string name, string value, int pk)
        {
            var candidate = EntityRepository.Entities.Find(pk);
            ReflectionUtilities.SetPropertyValue(candidate, name, value);
            EntityRepository.SaveChanges();
                        
            return StatusCode(StatusCodes.Status200OK);
        }

        
        /// <summary>
        /// Returns the Index view for this controller
        /// </summary>
        /// <returns>Index view</returns>
        public virtual IActionResult Index()
        {
            return View(EntityRepository.Entities.ToList());
        }

        /// <summary>
        /// Returns a parial view of the iddex view wihtout any layout scaffoldfing
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult IndexViewRaw()
        {
            // No layout stuff is wth view - maybe to embedded into another view
            // no head footer and css and javascript
            return PartialView("Index", EntityRepository.Entities.ToList());
        }

        /// <summary>
        /// Show create view for this entity
        /// </summary>
        /// <returns>Create view</returns>
        public virtual IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>view details of the entity</returns>
        [HttpPost]
        public virtual IActionResult Create(T entity)
        {
            try
            {
                EntityRepository.Entities.Add(entity);
                EntityRepository.SaveChanges();
                
                // We'll support a custom redirect if we've got one
                var returnAction = (string)TempData["ReturnAction"];
                var returnController = (string)TempData["ReturnController"];
                var returnRouteValues = TempData["ReturnRouteValues"];
                if (returnAction != null && returnController != null)
                {
                    Logger.Debug($"Using custom direct to '{returnAction}' via controller {returnController}");
                    return RedirectToAction(returnAction, returnController, returnRouteValues);
                }
                return RedirectToAction("Details", entity);
            }
            catch
            {
                return View(entity);
            }
        }

        /// <summary>
        /// Show Details page for the entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View of the details page for this entity</returns>
        public virtual IActionResult Details(int id)
        {
            return View(EntityRepository.Entities.Find(id));
        }

        /// <summary>
        /// Shows the delete view for this entity
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>View of the delete view for this entity</returns>
        public virtual IActionResult Delete(int ID)
        {
            var candidate = EntityRepository.Entities.Find(ID);
            return View(candidate);
        }

        /// <summary>
        /// Deletes this entity
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        /// <returns>View showing a list of all the entities</returns>
        [HttpPost]
        public IActionResult Delete(T entity)
        {
            var candidate = EntityRepository.Entities.Find(entity.ID);
            EntityRepository.Entities.Remove(candidate);
            EntityRepository.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}