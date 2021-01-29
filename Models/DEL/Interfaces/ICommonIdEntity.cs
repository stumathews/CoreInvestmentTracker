using System;

namespace CoreInvestmentTracker.Models.DEL.Interfaces
{
    /// <summary>
    /// All database entities will be commonly basic
    /// </summary>
    public interface ICommonIdEntity
    {
        /// <summary>
        /// The identifier of the entity
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// the name of the entity
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The description of the entity
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// When the entity was created
        /// </summary>
        DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// When the entity was last modified
        /// </summary>
        DateTimeOffset LastModifiedTime { get; set; }
    }
}