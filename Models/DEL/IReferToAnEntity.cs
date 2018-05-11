using CoreInvestmentTracker.Common;

namespace CoreInvestmentTracker.Models.DEL
{
    internal interface IReferToAnEntity
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