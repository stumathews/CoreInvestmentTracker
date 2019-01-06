using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using CoreInvestmentTracker.Common;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Simple text note for an entity
    /// </summary>
    public class InvestmentNote: DbEntityBase, IInvestmentEntity, IReferToAnEntity
    {
        [NotMapped]
        public int[] InvestmentIds => new int[] { };

        /// <inheritdoc />
        /// <summary>
        /// Owning id
        /// </summary>
        public int OwningEntityId { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Type of owning It
        /// </summary>
        public EntityType OwningEntityType  { get; set; }
    }
}