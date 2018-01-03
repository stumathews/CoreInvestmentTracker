using System.Collections.Generic;

namespace CoreInvestmentTracker.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  interface IDbInvestmentEntityHasInvestments<T>
    {
        /// <summary>
        /// We have investments
        /// </summary>
        ICollection<T> Investments { get; set; }
    }
}