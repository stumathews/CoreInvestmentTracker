using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    public class InvestmentRisk : IDbInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentRisk_Investment>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public String Description { get; set; }
        public RiskType Type { get; set; }
        public String Name { get; set; }
        public virtual ICollection<InvestmentRisk_Investment> Investments { get; set; }
    }
}