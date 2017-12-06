using System.Collections.Generic;
using CoreInvestmentTracker.Models.BOLO;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace WinInvestmentTracker.Models.BOLO
{
    public class ParentChildEntityWithHtmlActions<TEntity1, TEntity2> : ParentChildEntity<TEntity1, TEntity2>
        where TEntity1 : class, IDbInvestmentEntity
        where TEntity2 : class, IDbInvestmentEntity
    {
        public IEnumerable<HtmlAction> HtmlActions { get; set; }
    }
}