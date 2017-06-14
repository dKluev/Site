namespace Specialist.Web.Util.Routing
{
    public class RoutingParameter
    {
        public string NameInAction { get; set; }

        public string NameInUrl { get; set; }

        public object DefaultValue { get; set; }

        public RoutingParameter(string actionName, string urlName, object defaultValue)
        {
            NameInAction = actionName;
            NameInUrl = urlName;
            DefaultValue = defaultValue;
        }
    }
}