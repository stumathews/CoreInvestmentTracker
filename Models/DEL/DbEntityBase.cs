using System;
using System.ComponentModel.DataAnnotations.Schema;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <inheritdoc />
    /// <summary>
    /// Common functionality
    /// </summary>
    public class DbEntityBase : IDbEntity
    {
        /// <inheritdoc />
        /// <summary>
        /// The identifier of the entity
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
    }
}