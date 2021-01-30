using System.Collections.Generic;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreInvestmentTracker.Controllers
{
    /// <summary>
    /// An entity that refers or belongs to another entity
    /// such as notes for an investment or a custom entities that belong/refer to a investment
    /// </summary>
    public class RefersToEntityFunctions
    {
        private NotesController _notesController;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="notesController"></param>
        public RefersToEntityFunctions(NotesController notesController)
        {
            _notesController = notesController;
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
            var items = _notesController.EntityRepository.GetOneOrAllEntities().Where(o => o.OwningEntityId == owningEntityId 
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
            var entity = _notesController.EntityRepository.Db.Find<InvestmentNote>(owningEntityId, (EntityType)owningEntityType, id);
            if (entity == null) return _notesController.NotFound();
            _notesController.EntityRepository.Db.Remove(entity);
            _notesController.EntityRepository.SaveChanges();
            return new NoContentResult();

        }
    }
}