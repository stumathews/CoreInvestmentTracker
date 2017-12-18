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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int ID { get; set; }
        public String Description { get; set; }
        public String Symbol { get; set; }

        [Display(Name = "Value Proposition")]
        public String ValueProposition { get; set; }

        [Display(Name ="Deseriabliity statement")]
        public String DesirabilityStatement { get; set; }

        [Display(Name = "Initial Investment")]
        [DataType(DataType.Currency)]
        public float InitialInvestment { get; set; }

        [Required]      
        [Display(Name ="Investment Name")]
        public String Name { get; set; }

        [DataType(DataType.Currency)]
        public float Value { get; set; }

        public virtual ICollection<InvestmentInfluenceFactor_Investment> Factors { get; set; }
        public virtual ICollection<Region_Investment> Regions { get; set; }
        public virtual ICollection<InvestmentRisk_Investment> Risks { get; set; }
        public virtual ICollection<InvestmentGroup_Investment> Groups { get; set; }        
    }
}