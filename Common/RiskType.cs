using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// 
    /// </summary>
    public enum RiskType
    {
        /// <summary>
        /// risk specific to company
        /// </summary>
        Company,
        /// <summary>
        /// risk specific to market (systemic risk)
        /// </summary>
        Market
    }
}