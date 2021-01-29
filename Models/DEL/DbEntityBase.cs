using System;
using System.ComponentModel.DataAnnotations.Schema;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <inheritdoc />
    /// <summary>
    /// Common functionality for entitity classes
    /// </summary>
    public class DbEntityBase : IDbEntity
    {
        /// <inheritdoc />
        /// <summary>
        /// The primary key
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        protected DbEntityBase()
        {
            CreatedTime = DateTimeOffset.UtcNow;
            LastModifiedTime = CreatedTime;
        }

        /// <inheritdoc />
        /// <summary>
        /// The name of the entity
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// The description of the entity
        /// </summary>
        public string Description { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// When the entity was created
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// When the entity was last modified
        /// </summary>
        public DateTimeOffset LastModifiedTime { get; set; }

        /// <summary>
        /// Generic points for this entity
        /// </summary>
        public long Points { get; set; }

        /// <summary>
        /// Generic true/false marker for entity
        /// </summary>
        public bool IsFlagged { get; set; }
    }
}