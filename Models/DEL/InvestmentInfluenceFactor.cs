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
    /// 
    /// </summary>
    public class InvestmentInfluenceFactor : IDbInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentInfluenceFactor_Investment>
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public String Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }

        [NotMapped]
        public int[] investmentIds => Investments?.Select(i => i.InvestmentID).ToArray() ?? new int[] { };

        /// <summary>
        /// Influence
        /// </summary>
        public String Influence { get; set; }
        /// <summary>
        /// Investments
        /// </summary>
        public virtual ICollection<InvestmentInfluenceFactor_Investment> Investments { get; set; }
    }
}