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
    /// Investment group
    /// </summary>
    public class InvestmentGroup : IDbInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentGroup_Investment>
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }

        [NotMapped]
        public int[] investmentIds => Investments?.Select(i => i.InvestmentID).ToArray() ?? new int[] { };

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