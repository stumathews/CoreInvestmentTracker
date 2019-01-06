using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Represents an custom entity that the user can define
    /// </summary>
    public class CustomEntity : DbEntityBase, IInvestmentEntity, IReferToAnEntity, 
                                IReferToACustomEntity,  IDbInvestmentEntityHasInvestments<CustomEntity_Investment>
    {
        /// <inheritdoc />
        /// <summary>
        /// Type that this entity belongs to
        /// </summary>
        public CustomEntityType CustomEntityType { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The refered owning custom entity, if entity type = custom
        /// </summary>
        public CustomEntity OwningCustomEntity { get; set; }
        
        /// <inheritdoc />
        /// <summary>
        /// Entities that relate to this entity
        /// </summary>
        public ICollection<CustomEntity> Associations { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// This allows this custom entity to be owned by a non-custom entity
        /// </summary>
        public int OwningEntityId { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// This allows this custom entity to be owned by a non-custom entity
        /// </summary>
        public EntityType OwningEntityType { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// We have investments
        /// </summary>
        public ICollection<CustomEntity_Investment> Investments { get; set; }
        
        /// <summary>
        /// Convienient way to expose investments ids so we can get them generically using IHaveInvestments
        /// </summary>
        [NotMapped]
        public int[] InvestmentIds => Investments?.Select(x => x.InvestmentID).ToArray() ?? new int[] { };
    }

    /// <summary>
    /// Refers to a custom entity
    /// </summary>
    public interface IReferToACustomEntity
    {
        /// <summary>
        /// The Owing custom entity
        /// </summary>
        CustomEntity OwningCustomEntity { get; set; }

        /// <summary>
        /// Type that this entity belongs to
        /// </summary>
        CustomEntityType CustomEntityType { get; set; }

        /// <summary>
        /// Entities that relate to this entity
        /// </summary>
        ICollection<CustomEntity> Associations { get; set; }
    }
}
