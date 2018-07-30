using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <inheritdoc />
    /// <summary>
    /// Snapshot of a performances for an entity at a given time
    /// </summary>
    public class EntitySnapshot : CustomEntity
    {
        /// <summary>
        /// When the entity's performance data was captured
        /// </summary>
        private DateTime Date { get; set; }
        
        /// <summary>
        /// Each entities' performance in the portfolio
        /// </summary>
        public ICollection<EntityPerformance> Performances { get; set; }
    }
}
