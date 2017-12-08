using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreInvestmentTracker.Models.DEL.Interfaces
{
    /// <summary>
    /// All our entity classes will have an ID that is auto generated.
    /// A name and a description.
    /// </summary>
    public interface IDbInvestmentEntity
    {       
        /// <summary>
        /// The identifier of the entity
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// the name of the entity
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The description of the entity
        /// </summary>
        string Description { get; set; }
        
    }
}