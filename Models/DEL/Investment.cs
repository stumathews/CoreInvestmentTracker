using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoreInvestmentTracker.Models.DEL
{

    /// <summary>
    /// Represents an investment
    /// </summary>
    public class Investment : DbEntityBase, IInvestmentEntity
    {
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
        /// Investment value
        /// </summary>
        [DataType(DataType.Currency)]
        public float Value { get; set; }

        /// <summary>
        /// Factors
        /// </summary>
        public ICollection<InvestmentInfluenceFactor_Investment> Factors { get; set; }
        /// <summary>
        /// Regions
        /// </summary>
        public ICollection<Region_Investment> Regions { get; set; }
        /// <summary>
        /// Risks
        /// </summary>
        public ICollection<InvestmentRisk_Investment> Risks { get; set; }
        /// <summary>
        /// Groups
        /// </summary>
        public ICollection<InvestmentGroup_Investment> Groups { get; set; }       
        
        /// <summary>
        /// Associated custom entities
        /// </summary>
        public ICollection<CustomEntity_Investment> CustomEntities { get; set; }

        /// <summary>
        /// List of transactions
        /// </summary>
        
        public ICollection<InvestmentTransaction> Transactions { get;set; }
        
        /// <inheritdoc />
        public int[] InvestmentIds { get; set; }

        /// <summary>
        /// Currency of the portfolio
        /// </summary>
        [DataType(DataType.Currency)]
        public float Currency { get; set; }
    }
}