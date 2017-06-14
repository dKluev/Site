using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using SimpleUtils;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Common.Html
{
    public static class Pager
    {
        private const string PageString = "pageindex=";


       /* public static string GetNumericPager<T>(this HtmlHelper helper, PagedList<T> list)
        {
            return GetNumericPager(helper, null, 
                list.ItemCount, list.PageSize, list.PageIndex);
        }
*/

        public static string GetNumericPager<T>(this HtmlHelper helper, PagedList<T> list)
        {
            return GetNumericPager(helper, list, null);
        }

        public static string GetNumericPagerPretty<T>(this HtmlHelper helper, 
            PagedList<T> list) {
            return GetNumericPager(helper, list, "{0}");
        }


        public static string GetNumericPager<T>(this HtmlHelper helper, PagedList<T> list,
            string format, string allLink = null)
        {
            return GetNumericPager(helper, format,
                list.ItemCount, list.PageSize, list.PageIndex, allLink);
        }

        public static string GetNumericPager(this HtmlHelper helper, string urlFormat, 
                                             int totalRecords, int pageSize, int currentPage, string allLink = null)
        {
            if(urlFormat.IsEmpty())
            {
                var url = helper.ViewContext.HttpContext.Request.Url.PathAndQuery;
                var endPagePartIndex = url.ToLower().EndIndexOf(PageString);
                if (endPagePartIndex < 0)
                {
                    var separator = url.Contains("?") ? "&" : "?";
                    urlFormat = url + separator + PageString + "{0}";
                }
                else
                {
                    var indexAnd = url.IndexOf('&', endPagePartIndex);
                    urlFormat = indexAnd > 0 ? 
                        url.Remove(endPagePartIndex, indexAnd - endPagePartIndex) : 
                        url.Remove(endPagePartIndex);
                    urlFormat = urlFormat.Insert(endPagePartIndex, "{0}");
                }
            }

            var linkFormat = "<a href=\"{0}\">{1}</a>";

            var totalPages = totalRecords / pageSize;
            if (totalRecords % pageSize > 0)
            {
                totalPages++;
            }

            var sb = new StringBuilder();

            if (totalPages <= 1)
                return string.Empty;

            foreach (var keyValuePair in GetPageIndexForShow(currentPage + 1, totalPages))
            {
            
                var linkPageIndex = keyValuePair.Key;


                if (currentPage != linkPageIndex - 1 && keyValuePair.Value)
                {

                    sb.AppendFormat(linkFormat, string.Format(urlFormat, linkPageIndex), linkPageIndex);

                }
                else if (!keyValuePair.Value)
                    sb.AppendFormat(linkFormat, string.Format(urlFormat, linkPageIndex), "...");
                else
                {
                    sb.AppendFormat("<span>{0}</span>", linkPageIndex);
                }

            }

            return "<p class='pages'>" + sb + allLink + "</p>";
        }

        private static Dictionary<int, bool> GetPageIndexForShow(int pageIndex, int pageCount)
        {
            const int pageRange = 5;

            var result = new Dictionary<int, bool>();

            var leftPage = pageIndex - pageRange;
            var rightPage = pageIndex + pageRange;
            if (leftPage < 1)
            {
                leftPage = 1;
                rightPage = leftPage + pageRange * 2;
            }
            else if (rightPage > pageCount)
            {
                rightPage = pageCount;
                leftPage = rightPage - pageRange * 2;
            }

            for (int i = 1; i <= pageCount; i++)
            {
                if (i == 1 || i == pageCount ||
                    (leftPage < i && i < rightPage))
                    result.Add(i, true);
                else if (i == leftPage || i == rightPage)
                    result.Add(i, false);


            }

            return result;

        }

    }
}