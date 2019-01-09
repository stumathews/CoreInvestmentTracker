using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <inheritdoc />
    /// <summary>
    /// Types of user defined entities
    /// </summary>
    public class CustomEntityType : DbEntityBase
    {
        /// <summary>
        /// Specific datatype of the custom entity type
        /// </summary>
        public EntityType DataType { get; set; }
    }
}
