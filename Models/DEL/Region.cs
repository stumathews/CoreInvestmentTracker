using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{    
    public class Region : IDbInvestmentEntity, IDbInvestmentEntityHasInvestments<Region_Investment>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }        
        [Display(Name = "Description of region")]
        public String Description { get; set; }
        [Required]
        [Display(Name="Region Name")]
        public String Name { get; set; }
        public virtual ICollection<Region_Investment> Investments { get; set; }
    }
}