namespace CoreInvestmentTracker.Models.DEL.Interfaces
{
    /// <summary>
    /// All our investment entities classes have references to other investments that it links to
    /// A name and a description.
    /// </summary>
    public interface IInvestmentEntity : IDbEntity, IHaveInvestments { }
}