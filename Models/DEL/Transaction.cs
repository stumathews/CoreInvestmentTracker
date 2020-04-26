using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreInvestmentTracker.Common;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models.DEL
{
    /// <summary>
    /// Represents a transaction on an investment
    /// </summary>
    public class InvestmentTransaction : DbEntityBase, IInvestmentEntity
    {
        [ForeignKey("Investment_InvestmentId")]
        public int InvestmentId { get; set; }
        /// <summary>
        /// The number of units bought
        /// </summary>
        public int NumUnits { get;set; }

        /// <summary>
        /// The price per unit
        /// </summary>
        public float PricePerUnit { get; set; }

        /// <summary>
        /// ISO code that represents the currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Eg Buy, Sell
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Time that the transaction was made
        /// </summary>
        public DateTimeOffset TransactionDate { get; set; }

        /// <summary>
        /// Refers to a list of investments
        /// </summary>
        [NotMapped]
        public int[] InvestmentIds =>  new int[] {  };

        /// <summary>
        /// Additional cost of this transaction
        /// </summary>
        [DataType(DataType.Currency)]
        public float Commission { get; set; }
    }
}
