using SimpleUtils.Util;

namespace Specialist.Web.Common.Html
{
    public static class StringExtension
    {
        public static string Quote(this string str)
        {
            return "\"" + str + "\"";
        }

     

        private const string Href = "href=\"";
        public static string LinkAddFragmentCyrillic(this string html, string fragment) {
            return html.LinkAddFragment(Linguistics.UrlTranslite(fragment));
        }

        public static string LinkAddFragment(this string html, string fragment) {
           
            var hrefIndex = html.IndexOf(Href) + Href.Length + 1;
            html = html.Insert(html.IndexOf("\"", hrefIndex), "#" + fragment);
            return html;
        }


        public static string ClearFromImg(this string str)
        {
            var objRegEx = new System.Text.RegularExpressions.Regex("<[/]?img.*>");

            return objRegEx.Replace(str, "");
        }

        public static string Tag(this string str, string tag)
        {
            return string.Format("<{1}>{0}</{1}>", str, tag);
        }
    }
}