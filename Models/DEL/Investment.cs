using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    /// <summary>
    /// Represents an investment
    /// </summary>
    public class Investment : IInvestmentEntity
    {
        /// <inheritdoc />
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]                
        public int Id { get; set; }
        /// <inheritdoc />
        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Value proposition
        /// </summary>
        [Display(Name = "Value Proposition")]
        public string ValueProposition { get; set; }

        /// <summary>
        /// Desirability statement
        /// </summary>
        [Display(Name ="Deseriabliity statement")]
        public string DesirabilityStatement { get; set; }

        /// <summary>
        /// Initial investment
        /// </summary>
        [Display(Name = "Initial Investment")]
        [DataType(DataType.Currency)]
        public float InitialInvestment { get; set; }

        /// <summary>
        /// Investment Name
        /// </summary>
        [Required]      
        [Display(Name ="Investment Name")]
        public string Name { get; set; }

        /// <summary>
        /// Investment value
        /// </summary>
        [DataType(DataType.Currency)]
        public float Value { get; set; }

        /// <summary>
        /// Factors
        /// </summary>
        public virtual ICollection<InvestmentInfluenceFactor_Investment> Factors { get; set; }
        /// <summary>
        /// Regions
        /// </summary>
        public virtual ICollection<Region_Investment> Regions { get; set; }
        /// <summary>
        /// Risks
        /// </summary>
        public virtual ICollection<InvestmentRisk_Investment> Risks { get; set; }
        /// <summary>
        /// Groups
        /// </summary>
        public virtual ICollection<InvestmentGroup_Investment> Groups { get; set; }       
        
        /// <summary>
        /// Associated custom entities
        /// </summary>
        public virtual ICollection<CustomEntity_Investment> CustomEntities { get; set; }
        
        /// <inheritdoc />
        public int[] InvestmentIds { get; set; }
    }
}