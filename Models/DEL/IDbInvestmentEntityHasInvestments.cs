using System.Collections.Generic;
using WinInvestmentTracker.Models;

namespace CoreInvestmentTracker.Models
{
    public  interface IDbInvestmentEntityHasInvestments<T>
    {
        /// <summary>
        /// We have investments
        /// </summary>
        ICollection<T> Investments { get; set; }
    }
}