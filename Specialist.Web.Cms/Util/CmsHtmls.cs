using System.Web.Mvc;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Cms.Util
{
    public static class CmsHtmls
    {
        public static TagBuilder Span()
        {
            return new TagBuilder("span");
        }

        public static TagBuilder Icon()
        {
            var span = Span();
            span.AddCssClass("ui-icon");
            return span;
        }

        public static string ContentWithIcon(string icon, string content)
        {
            return "<table><tr><td width='20px'>" + icon +
                "</td><td>" + content + "</td></tr></table>";
        }

        public static string Form(string action, string content)
        {
            return string.Format(
                "<form style='display:table;' action={0} method=\"post\">{1}</form>",
                action.Quote(), content);
        }

        public static string DeleteButton(string action)
        {
            return Form(action, "<input onclick=\"return confirmDelete();\" " +
                " class='ui-icon ui-icon-trash' title='Удалить'  type=\"submit\">");
        }

        public static string CheckIcon(bool b) {
            return b ? Icons.Check.ToString() : string.Empty;
        }

        public static class Icons
        {
            public static TagBuilder Edit
            {
                get
                {
                    var span = CmsHtmls.Icon();
                    span.AddCssClass("ui-icon-pencil");
                    return span;
                }
            }

            public static TagBuilder Table
            {
                get
                {
                    var span = CmsHtmls.Icon();
                    span.AddCssClass("ui-icon-calculator");
                    return span;
                }
            }

             public static TagBuilder Check
            {
                get
                {
                    var span = CmsHtmls.Icon();
                    span.Attributes.Add("style", "margin:auto");
                    span.AddCssClass("ui-icon-check");
                    return span;
                }
            }
           

            public static TagBuilder Add
            {
                get
                {
                    var span = CmsHtmls.Icon();
                    span.AddCssClass("ui-icon-plusthick");
                    return span;
                }
            }

			public static TagBuilder Help
            {
                get
                {
                    var span = CmsHtmls.Icon();
                    span.AddCssClass("ui-icon-help");
					span.Attributes.Add("style", "display:inline-block;");
                    return span;
                }
            }
        }

    }
}