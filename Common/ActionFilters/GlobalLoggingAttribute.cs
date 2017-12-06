

using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreInvestmentTracker.Common
{
    public class GlobalLoggingAttribute : ActionFilterAttribute
    {
        //private IMyLogger Log
        //{
        //    get
        //    {
        //        return DependencyResolver.Current.GetService<IMyLogger>();
        //    }
        //}
    
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
           //Log.Debug(string.Format("Request {0} {1}", actionContext.HttpContext.Request.HttpMethod.ToString(), actionContext.HttpContext.Request.Url.ToString()));
           base.OnActionExecuting(actionContext);
        }
        
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {           
            //Log.Debug(string.Format("{0} Response Code: {1}", actionExecutedContext.HttpContext.Request.Url.ToString(), actionExecutedContext.HttpContext.Response.StatusCode.ToString()));
            base.OnActionExecuted(actionExecutedContext);
        }
    }   
}