
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CoreInvestmentTracker.Common
{
    public class ClearCustomRedirectsAttribute : ActionFilterAttribute
    {
        //private IMyLogger Logger
        //{
        //    get
        //    {
        //        return DependencyResolver.Current.GetService<IMyLogger>();
        //    }
        //}
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Logger.Debug($"Removing custom redirects (ReturnAction) stored in TempData.");
            //filterContext.Controller.TempData.Remove("ReturnAction");
            //filterContext.Controller.TempData.Remove("ReturnController");
            //filterContext.Controller.TempData.Remove("ReturnRouteValues");
            base.OnActionExecuted(filterContext);
        }
    }
}