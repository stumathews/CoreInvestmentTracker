﻿using System;
using CoreInvestmentTracker.Models.DEL;
using CoreInvestmentTracker.Models.DEL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreInvestmentTracker.Common.ActionFilters
{
    /// <inheritdoc />
    /// <summary>
    /// Provides global logging for controllers
    /// </summary>
    public class GlobalControllerLoggingAttribute : ActionFilterAttribute
    {        
        /// <inheritdoc />
        /// <summary>
        /// Executed while action executing
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
           var log =(IMyLogger) actionContext.HttpContext.RequestServices.GetService(typeof(IMyLogger));
            
           log.Debug(string.Format("Time {2} Request {0} {1}", actionContext.HttpContext.Request.ToString(), actionContext.HttpContext.Request.ToString(), DateTime.UtcNow));
           base.OnActionExecuting(actionContext);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executed after action executed
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var logger = (IMyLogger)actionExecutedContext.HttpContext.RequestServices.GetService(typeof(IMyLogger));
            var user = actionExecutedContext.HttpContext.User;
            logger.Debug(string.Format("{0} Response Code: {1}", actionExecutedContext.HttpContext.Request.ToString(), actionExecutedContext.HttpContext.Response.StatusCode.ToString()));
            base.OnActionExecuted(actionExecutedContext);
        }
    }   
}