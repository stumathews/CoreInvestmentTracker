using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreInvestmentTracker.Models.DEL.Interfaces
{
    /// <summary>
    /// All our investment entities classes have references to other investments that it links to
    /// A name and a description.
    /// </summary>
    public interface IInvestmentEntity : IDbEntity, IHaveInvestments { }

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

    /// <summary>
    /// Basic database entity
    /// </summary>
    public interface IDbEntity : ICommonIdEntity { }

    /// <summary>
    /// Implementation of common functions that a IDbEntity has.
    /// This class promotes reuse of functionality
    /// </summary>
    public abstract class AutoDbEntity : IDbEntity 
    {
        /// <inheritdoc />
        /// <summary>
        /// The identifier of the entity
        /// </summary>
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


    /// <summary>
    /// An entity that has a list of investments
    /// </summary>
    public interface IHaveInvestments
    {
        /// <summary>
        /// Refers to a list of investments
        /// </summary>
        int[] InvestmentIds { get; }
    }
}