using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    public interface IReferToAnEntity : IDbEntity
    {
        /// <summary>
        /// The entity ID for the entity that owns this note, specifically of type OwningEntityType
        /// </summary>
        int OwningEntityId { get; set; }

        /// <summary>
        /// All notes will be in relation to a particular type
        /// </summary>
        EntityType OwningEntityType { get; set; }
    }
}