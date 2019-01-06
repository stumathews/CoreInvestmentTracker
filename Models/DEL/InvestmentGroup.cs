using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Investment group
    /// </summary>
    public class InvestmentGroup : DbEntityBase, IInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentGroup_Investment>
    {
        [NotMapped]
        public int[] InvestmentIds => Investments?.Select(i => i.InvestmentID).ToArray() ?? new int[] { };

        /// <summary>
        /// Type
        /// </summary>
        public String Type { get; set; }
        //public ICollection<InvestmentGroup> Groups { get; set; }

        /// <summary>
        /// Investments
        /// </summary>
        public virtual ICollection<InvestmentGroup_Investment> Investments { get; set; }

        /// <summary>
        /// Parent
        /// </summary>
        public virtual InvestmentGroup Parent { get; set; }

        /// <summary>
        /// Children
        /// </summary>
        public virtual ICollection<InvestmentGroup> Children { get; set; }
        
    }
}