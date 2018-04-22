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
    public class InvestmentNote: IDbInvestmentEntity
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

        /// <summary>
        /// The entity ID for the entity that owns this note, specifically of type OwningEntityType
        /// </summary>
        public int OwningEntityId { get; set; }
        /// <summary>
        /// All notes will be in relation to a particular type
        /// </summary>
        public EntityType OwningEntityType { get; set; }
    }
}