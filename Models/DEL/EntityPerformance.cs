using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Represents performance metrics about an entity
    /// </summary>
    public class EntityPerformance : CustomEntity
    {
        /// <summary>
        /// List of properties storing the performance values
        /// </summary>
        public ICollection<EntityProperty> Performances { get;set; }
    }
}
