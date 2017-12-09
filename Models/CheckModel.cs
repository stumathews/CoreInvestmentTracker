using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoreInvestmentTracker.Models.DEL.Interfaces;

namespace CoreInvestmentTracker.Models
{
    public class CheckModel : IDbInvestmentEntity
    {
        /// <summary>
        /// Entity ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// name of the Entity
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// If this entity is checked
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// description of this entity
        /// </summary>
        public string Description { get; set; }
               
        /// <summary>
        /// Extra fields
        /// </summary>
        public List<CustomField<string,string>> Fields { get; set; }
    }

    /// <summary>
    /// Represents custom fields that can be associated with something, normally a Checkmodel
    /// </summary>
    /// <typeparam name="K">Key</typeparam>
    /// <typeparam name="V">Value</typeparam>
    public class CustomField<K,V>
    {
        /// <summary>
        /// name of the custom field
        /// </summary>
        public K Name { get; set; }
        /// <summary>
        /// Value of the custom field
        /// </summary>
        public V Value { get; set; }
    }
}