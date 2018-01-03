﻿using CoreInvestmentTracker.Models.DEL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionUtilities
    {
        /// <summary>
        /// Sets a specific property on a object using reflection.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <returns>the object, modified</returns>
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