using System.Web.Mvc;
using System.Web.Routing;
using Specialist.Web.Common.Mvc;

namespace Specialist.Web.Common.Mvc
{
    public static class TagBuilderExtension
    {
        public static TagBuilder Attr(this TagBuilder builder, object htmlAttribute)
        {
            builder.MergeAttributes(new RouteValueDictionary(htmlAttribute));
            return builder;
        }

        public static TagBuilder ChangeAttr(this TagBuilder builder, string name, 
            string value)
        {
            if (builder.Attributes.ContainsKey(name))
                builder.Attributes[name] = value;
            else
                builder.Attributes.Add(name, value);
            return builder;
        }



        public static TagBuilder Class(this TagBuilder builder, string name)
        {
            builder.AddCssClass(name);
            return builder;
        }

        public static TagBuilder Style(this TagBuilder builder, string style)
        {
            builder.Attributes.Add("style", style);
            return builder;
        }

        public static TagBuilder Id(this TagBuilder builder, string id)
        {
            builder.Attributes.Add("id", id);
            return builder;
        }

        public static TagBuilder Size(this TagBuilder builder, int? width, int? height)
        {
            if(width.HasValue)
                builder.MergeAttributes(
                    new RouteValueDictionary(new { width }));
            if(height.HasValue)
                builder.MergeAttributes(
                    new RouteValueDictionary(new { height }));
            return builder;
        }

        public static TagBuilder Size(this TagBuilder builder, int size) {
             builder.Attr(new { size });
            return builder;
        }

        public static string GetString(this TagBuilder builder, bool innerHtml)
        {
            if (innerHtml)
                return builder.InnerHtml;
            return builder.ToString();
        }

    }
}