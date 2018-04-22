using CoreInvestmentTracker.Models.DEL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// Stuff to do with shalow copying objects
    /// </summary>
    public static class ShallowCopy
    {
        /// <summary>
        /// Merges two objects but skips specified property names
        /// </summary>
        /// <typeparam name="T">type of both items</typeparam>
        /// <param name="old">old item</param>
        /// <param name="new">ne witem</param>
        /// <param name="except">skipp these property names in merge</param>
        /// <returns></returns>
        public static void Merge<T>(T old, T @new, string[] except)
        {
            var newProperties = TypeDescriptor.GetProperties(typeof(T)).Cast<PropertyDescriptor>();
            var oldProperties = TypeDescriptor.GetProperties(old).Cast<PropertyDescriptor>();
            
            foreach (var oldProperty in oldProperties)
            {                
                var property = oldProperty;
                var newProperty = newProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (except.Contains(newProperty.Name)) continue;
                if (newProperty != null)
                {
                    oldProperty.SetValue(old, Convert.ChangeType(newProperty.GetValue(@new), oldProperty.PropertyType));
                }
            }
        }
        /// <summary>
        /// Merges two objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static T MergeObjects<T>(T obj1, T obj2)
        {
            var objResult = Activator.CreateInstance(typeof(T));

            var allProperties = typeof(T).GetProperties().Where(x => x.CanRead && x.CanWrite);
            foreach (var pi in allProperties)
            {
                object defaultValue;
                if (pi.PropertyType.IsValueType)
                {
                    defaultValue = Activator.CreateInstance(pi.PropertyType);
                }
                else
                {
                    defaultValue = null;
                }
                
                var value = pi.GetValue(obj2, null);

                if (value != defaultValue)
                {
                    pi.SetValue(objResult, value, null);
                }
                else
                {
                    value = pi.GetValue(obj1, null);

                    if (value != defaultValue)
                    {
                        pi.SetValue(objResult, value, null);
                    }
                }
            }
            return (T)objResult;
        }
    }
}
