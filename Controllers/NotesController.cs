using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CoreInvestmentTracker.Controllers
{
    /// <summary>
    /// Controller for Notes
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class NotesController : EntityManagedController<InvestmentNote>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="logger"></param>
        public NotesController(IEntityApplicationDbContext<InvestmentNote> db, IMyLogger logger) 
            : base(db, logger)
        {

        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="owningEntityID"></param>
        /// <param name="owningEntityType"></param>
        /// <returns>item</returns>
        [HttpGet("{owningEntityID}/{owningEntityType}")]
        public IEnumerable<InvestmentNote> GetOwningentityNotes(int owningEntityID, int owningEntityType)
        {
            var items = EntityRepository.Entities.Where(o => o.OwningEntityId == owningEntityID 
                                                          && o.OwningEntityType == (EntityType)owningEntityType);
            return items.ToList();
        }

        /// <summary>
        /// Deletes and Entity
        /// </summary>
        /// <param name="noteType">Type of note</param>
        /// <param name="id">The id of the entity to delete</param>
        /// <returns>NoContentResult</returns>
        [HttpDelete("{owningEntityId}/{owningEntityType}/{ID}")]
        public IActionResult Delete(int owningEntityId, int owningEntityType, int ID)
        {
            var entity = EntityRepository.db.Find<InvestmentNote>(owningEntityId, (EntityType)owningEntityType, ID);
            if (entity == null)
            {
                return NotFound();
            }
            EntityRepository.db.Remove(entity);
            EntityRepository.SaveChanges();
            return new NoContentResult();
        }
    }
}