﻿using CoreInvestmentTracker.Models.DEL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using CoreInvestmentTracker.Models.DEL;
using Microsoft.AspNetCore.Http;

namespace CoreInvestmentTracker.Common
{

    /// <summary>
    /// Types of entity
    /// </summary>
    public enum EntityType
    {
        /// <summary>
        /// Represents a unknown entity type
        /// </summary>
        None,
        /// <summary>
        /// Investment
        /// </summary>
        Investment,
        /// <summary>
        /// Group
        /// </summary>
        InvestmentGroup,
        /// <summary>
        /// Risk
        /// </summary>
        InvestmentRisk,
        /// <summary>
        /// Factor
        /// </summary>
        InvestmentInfluenceFactor,
        /// <summary>
        /// Region
        /// </summary>
        Region,

        /// <summary>
        /// User
        /// </summary>
        User,

        /// <summary>
        /// Note
        /// </summary>
        Note,

        /// <summary>
        /// Activity
        /// </summary>
        Activity,

        /// <summary>
        /// Custom entity
        /// </summary>
        Custom,

        /// <summary>
        /// Cusotm Entity type
        /// </summary>
        CustomType,

        /// <summary>
        /// Number
        /// </summary>
        Number,

        /// <summary>
        /// String
        /// </summary>
        String,

        /// <summary>
        /// List of strings
        /// </summary>
        ListOfStrings,

        /// <summary>
        /// List of numbers
        /// </summary>
        ListOfNumbers,

        /// <summary>
        /// An investment transaction
        /// </summary>
        InvestmentTransaction



    }

    public enum ActivityOperation
    {
        Create,
        Read,
        Update,
        Delete
    }

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

    

    public static class StaticUtilities
    {
        public static void ExtractBodyAsString(HttpRequest request)
        {
            var body = new StreamReader(request.Body);
            var requestBody = body.ReadToEnd();
        }
        public static string ToStr(this CustomEntity entity)
        {
            return $"Name={entity.Name}, Desc={entity.Description}, Type={entity.GetType()}, OwningEntityId={entity.OwningEntityId}, IsCustomEntity={!NotCustomEntityTheOwningEntity(entity)}";
        }

        public static string GetOwningEntityType(this CustomEntity entity)
        {
            return NotCustomEntityTheOwningEntity(entity)
                ? entity.OwningEntityType.ToString()
                : entity.OwningCustomEntity.Name;
            
        }

        /// <summary>
        /// Determine if there is a custom entity type
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool NotCustomEntityTheOwningEntity(CustomEntity e)
        {
            return (e.OwningCustomEntity?.Id == 0 || string.IsNullOrEmpty(e.OwningCustomEntity?.Name));
        }
    }

}