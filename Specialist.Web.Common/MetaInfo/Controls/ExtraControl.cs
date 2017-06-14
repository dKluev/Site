using System.Web.Routing;

namespace SimpleUtils.FluentAttributes.Core.Controls
{
    public abstract class ExtraControl
    {
        public string DisplayName { get; set; }

        public virtual string Name { get { return this.GetType().Name; } }

        public virtual string ImageName { get { return Name; } }

        public ExtraControl(string displayName)
        {
            DisplayName = displayName;
        }

        public abstract RouteValueDictionary GetRouteValues(object entity);
        
    }
}