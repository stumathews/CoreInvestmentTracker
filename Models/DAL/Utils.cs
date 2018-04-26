using System;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DAL
{
    public sealed class Utils
    {
        /// <summary>
        /// Utility function to change the type
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T1 ChangeType<T1>(object obj)
        {
            return (T1)Convert.ChangeType(obj, typeof(T1));
        }
    }
}