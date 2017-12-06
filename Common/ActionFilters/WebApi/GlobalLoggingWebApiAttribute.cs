
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public GlobalLoggingWebApiAttribute(ILogger logger)
        {

        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            _logger.LogInformation(string.Format("{0} WebApi Response Code: {1}", actionExecutedContext.HttpContext.Response, actionExecutedContext.HttpContext.Response.StatusCode.ToString()));
            base.OnActionExecuted(actionExecutedContext);
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            _logger.LogInformation(string.Format("WebApi Request {0} {1}", actionContext.HttpContext.Request.Method.ToString(), actionContext.HttpContext.Request.ToString()));
            base.OnActionExecuting(actionContext);
        }
    }

}