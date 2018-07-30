using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// A property associated with an entity
    /// </summary>
    public class EntityProperty : CustomEntity
    {
        /// <summary>
        /// Value of the property
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// String, Int etc - loosly defined
        /// </summary>
        public string FormatType { get; set; }
    }
}
