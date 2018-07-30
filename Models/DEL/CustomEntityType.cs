using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Types of user defined entities
    /// </summary>
    public class CustomEntityType : IDbEntity
    {
        /// <inheritdoc />
        /// <summary>
        /// The identifier of the entity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// the name of the entity
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The description of the entity
        /// </summary>
        public string Description { get; set; }
    }
}
