namespace CoreInvestmentTracker.Models.BOLO
{
    public class HtmlAction
    {
        public HtmlAction(string displayName, string linkTitle, string actionName, string controllerName, object routeValues = null )
        {
            DisplayName = displayName;
            LinkTitle = linkTitle;
            ActionName = actionName;
            ControllerName = controllerName;
            RouteValues = routeValues;
        }

        public string DisplayName{ get; set; }
        public string LinkTitle { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public object RouteValues { get; set; }
    }
}