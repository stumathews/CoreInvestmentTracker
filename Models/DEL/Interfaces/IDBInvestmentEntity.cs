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
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}