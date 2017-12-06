using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreInvestmentTracker.Common
{
    public enum RiskType
    {
        Company, // risk specific to company
        Market // risk specific to market (systemic risk)
    }
}