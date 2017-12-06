using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CoreInvestmentTracker.Common
{
    public class ReflectionUtilities
    {
        public static Object SetPropertyValue(Object entity, string propertyName, string propertyValue)
        {
            PropertyInfo propInfo = entity.GetType().GetProperty(propertyName);
            if (propInfo != null)
            {
                Type t = propInfo.PropertyType;
                object d;
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (string.IsNullOrEmpty(propertyValue))
                        d = null;
                    else
                        d = Convert.ChangeType(propertyValue, t.GetGenericArguments()[0]);
                }
                else if (t == typeof(Guid))
                {
                    d = new Guid(propertyValue);
                }
                else
                {
                    d = Convert.ChangeType(propertyValue, t);
                }

                propInfo.SetValue(entity, d, null);
            }
            return entity;
        }
    }
}