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
    /// An Investment region
    /// </summary>
    public class Region : DbEntityBase, IInvestmentEntity, IDbInvestmentEntityHasInvestments<Region_Investment>
    {
        /// <inheritdoc />
        /// <summary>
        /// The investment Ids associated with this region
        /// </summary>
        [NotMapped] 
        public int[] InvestmentIds => Investments?.Select(x => x.InvestmentID).ToArray() ?? new int[] { };
       
        /// <summary>
        /// The actual Investments
        /// </summary>
        public virtual ICollection<Region_Investment> Investments { get; set; }
    }
}