using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class InvestmentInfluenceFactor : DbEntityBase, IInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentInfluenceFactor_Investment>
    {
        [NotMapped]
        public int[] InvestmentIds => Investments?.Select(i => i.InvestmentID).ToArray() ?? new int[] { };

        /// <summary>
        /// Influence
        /// </summary>
        public string Influence { get; set; }
        /// <summary>
        /// Investments
        /// </summary>
        public virtual ICollection<InvestmentInfluenceFactor_Investment> Investments { get; set; }
    }
}