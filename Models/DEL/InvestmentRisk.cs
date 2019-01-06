using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    /// <summary>
    /// Investment Risk
    /// </summary>
    public class InvestmentRisk : DbEntityBase, IInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentRisk_Investment>
    {
        [NotMapped]
        public int[] InvestmentIds => Investments?.Select(x => x.InvestmentID).ToArray() ?? new int[] { };

        /// <summary>
        /// Type
        /// </summary>
        public RiskType Type { get; set; }
        /// <summary>
        /// Investments
        /// </summary>
        public virtual ICollection<InvestmentRisk_Investment> Investments { get; set; }
    }
}