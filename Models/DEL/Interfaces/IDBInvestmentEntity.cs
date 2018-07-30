using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreInvestmentTracker.Models.DEL.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// All our investment entities classes have references to other investments that it links to
    /// A name and a description.
    /// </summary>
    public interface IInvestmentEntity : IDbEntity, IHaveInvestments
    {  
    }

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
    }

    /// <summary>
    /// Basic database entity
    /// </summary>
    public interface IDbEntity : ICommonIdEntity
    {       

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