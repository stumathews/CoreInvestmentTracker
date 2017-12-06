using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    public class CheckModel : IDbInvestmentEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public string Description { get; set; }
               
        public List<CustomField<string,string>> Fields { get; set; }
    }

    public class CustomField<K,V>
    {
        public K Name { get; set; }
        public V Value { get; set; }
    }
}