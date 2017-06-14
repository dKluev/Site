using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Util
{
    public class SelectListUtil
    {
        public static List<SelectListItem> GetSelectItemList(IEnumerable<string> items) {
        	return GetSelectItemList(items, x => x, x => x);
        }

        public static List<SelectListItem> GetSelectItemList<T>(
            IEnumerable<T> source, Func<T, object> textSelector, 
            Func<T, object> valueSelector, bool withEmpty = false, string emptyText = "Нет", 
				object currentValue = null)
        {
            var result = new List<SelectListItem>();
			if(withEmpty) {
				result.Add(new SelectListItem { Text = emptyText, Value = string.Empty });
			}
            foreach (var item in source) {
	            var value = valueSelector(item).ToString();
	            var listItem = new SelectListItem {
		            Text = textSelector(item).ToString(), Value = value,
					Selected = currentValue != null && value == currentValue.ToString()
	            };
	            result.Add(listItem);
            }
	        return result;
        }
    }
}