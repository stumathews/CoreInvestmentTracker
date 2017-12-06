using System.Collections.Generic;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace WinInvestmentTracker.Models.BOLO
{
    public class ParentChildEntity<TEntity1, TEntity2>
        where TEntity1 : class, IDbInvestmentEntity
        where TEntity2 : class, IDbInvestmentEntity
    {
        public List<TEntity1> Children { get; set; }
        public TEntity2 Parent { get; set; }
    }

    
}