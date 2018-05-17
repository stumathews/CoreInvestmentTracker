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
    public class InvestmentNote: IInvestmentEntity, IReferToAnEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Title of the note
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Contents of the note
        /// </summary>
        public string Description { get; set; }

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