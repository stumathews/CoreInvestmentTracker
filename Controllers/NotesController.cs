using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models;
using CoreInvestmentTracker.Models.DAL.Interfaces;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CoreInvestmentTracker.Common.ActionFilters;

namespace CoreInvestmentTracker.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for Notes
    /// </summary>
    [Route("api/[controller]")]
    [GlobalControllerLogging]
    public class NotesController : EntityManagedController<InvestmentNote>
    {
        /// <inheritdoc />
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
        /// <param name="owningEntityId"></param>
        /// <param name="owningEntityType"></param>
        /// <returns>item</returns>
        [HttpGet("{owningEntityId}/{owningEntityType}")]
        public IEnumerable<InvestmentNote> GetAllNotesFor(int owningEntityId, int owningEntityType)
        {
            var items = EntityRepository.GetAllEntities().Where(o => o.OwningEntityId == owningEntityId 
                                                          && o.OwningEntityType == (EntityType)owningEntityType);
            return items.ToList();
        }

        /// <summary>
        /// Deletes and Entity
        /// </summary>
        /// <param name="id">The id of the entity to delete</param>
        /// <param name="owningEntityId">The id of the type you want the note for</param>
        /// <param name="owningEntityType">The type of the owning entity</param>
        /// <returns>NoContentResult</returns>
        [HttpDelete("{owningEntityId}/{owningEntityType}/{id}")]
        public IActionResult Delete(int owningEntityId, int owningEntityType, int id)
        {
            var entity = EntityRepository.Db.Find<InvestmentNote>(owningEntityId, (EntityType)owningEntityType, id);
            if (entity == null) return NotFound();
            EntityRepository.Db.Remove(entity);
            EntityRepository.SaveChanges();
            return new NoContentResult();

        }
    }
}