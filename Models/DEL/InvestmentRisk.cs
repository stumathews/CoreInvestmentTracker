﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class InvestmentRisk : IInvestmentEntity, IDbInvestmentEntityHasInvestments<InvestmentRisk_Investment>
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public String Description { get; set; }

        [NotMapped]
        public int[] InvestmentIds => Investments?.Select(x => x.InvestmentID).ToArray() ?? new int[] { };

        /// <summary>
        /// Type
        /// </summary>
        public RiskType Type { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Investments
        /// </summary>
        public virtual ICollection<InvestmentRisk_Investment> Investments { get; set; }
    }
}