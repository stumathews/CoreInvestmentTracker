using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreInvestmentTracker.Common
{
    /// <summary>
    /// Provides global logging for controllers
    /// </summary>
    public class GlobalControllerLoggingAttribute : ActionFilterAttribute
    {        
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
           var Log =(IMyLogger) actionContext.HttpContext.RequestServices.GetService(typeof(IMyLogger));
            
           Log.Debug(string.Format("Request {0} {1}", actionContext.HttpContext.Request.ToString(), actionContext.HttpContext.Request.ToString()));
           base.OnActionExecuting(actionContext);
        }
        
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var Log = (IMyLogger)actionExecutedContext.HttpContext.RequestServices.GetService(typeof(IMyLogger));
            Log.Debug(string.Format("{0} Response Code: {1}", actionExecutedContext.HttpContext.Request.ToString(), actionExecutedContext.HttpContext.Response.StatusCode.ToString()));
            base.OnActionExecuted(actionExecutedContext);
        }
    }   
}