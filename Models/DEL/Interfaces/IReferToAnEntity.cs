using CoreInvestmentTracker.Common;

namespace CoreInvestmentTracker.Models.DEL.Interfaces
{
    /// <summary>
    /// An object that refers to an existing Entity via a Owning ID and OwningType
    /// </summary>
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