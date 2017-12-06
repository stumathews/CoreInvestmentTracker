using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    public class InvestmentInfluenceFactor : IDbInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentInfluenceFactor_Investment>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        public String Influence { get; set; }
        public virtual ICollection<InvestmentInfluenceFactor_Investment> Investments { get; set; }
    }
}