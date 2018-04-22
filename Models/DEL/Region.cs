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
    public class Region : IDbInvestmentEntity, IDbInvestmentEntityHasInvestments<Region_Investment>
    {
        /// <summary>
        /// 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Description of region")]        
        public String Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name="Region Name")]
        public String Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Region_Investment> Investments { get; set; }
    }
}