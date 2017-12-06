
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace CoreInvestmentTracker.Common.ActionFilters.WebApi
{
    public class GlobalLoggingWebApiAttribute : ActionFilterAttribute
    {        
        //private IMyLogger Logger
        //{
        //    get
        //    {
        //        return (IMyLogger)System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IMyLogger));                                
        //    }
        //}

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //  Logger.Debug(string.Format("{0} WebApi Response Code: {1}", actionExecutedContext.Response, actionExecutedContext.Response.StatusCode.ToString()));
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Logger.Debug(string.Format("WebApi Request {0} {1}", actionContext.Request.Method.ToString(), actionContext.Request.RequestUri.ToString()));
            base.OnActionExecuting(context);
        }
            }
}