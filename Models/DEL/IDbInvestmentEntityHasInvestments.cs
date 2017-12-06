using System.Collections.Generic;
using WinInvestmentTracker.Models;

namespace CoreInvestmentTracker.Models
{
    public  interface IDbInvestmentEntityHasInvestments<T>
    {
        ICollection<T> Investments { get; set; }
    }
}