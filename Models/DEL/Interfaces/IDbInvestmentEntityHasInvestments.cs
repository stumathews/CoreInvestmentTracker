using System.Collections.Generic;

namespace CoreInvestmentTracker.Models.DEL.Interfaces
{
    /// <summary>
    /// Something that has investments of the specified type
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