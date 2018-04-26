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
    public class Investment : IDbInvestmentEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]                
        public int Id { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public String Symbol { get; set; }
        /// <summary>
        /// Value proposition
        /// </summary>
        [Display(Name = "Value Proposition")]
        public String ValueProposition { get; set; }

        /// <summary>
        /// Desirability statement
        /// </summary>
        [Display(Name ="Deseriabliity statement")]
        public String DesirabilityStatement { get; set; }

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
        public String Name { get; set; }

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

        public int[] investmentIds { get; set; }
    }
}