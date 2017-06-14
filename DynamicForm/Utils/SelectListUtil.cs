using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.ComponentModel;
using SimpleUtils.ComponentModel.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Common.Extensions;

namespace DynamicForm.Utils
{
    public class SelectListUtil
    {
        public static SelectListItem GetEmptyItem(string text)
        {
            return new SelectListItem {Text = text, Value = string.Empty};
        }

        public static List<SelectListItem> GetSelectItemList<T>(
            IEnumerable<T> source, Func<T, object> textSelector, 
            Func<T, object> valueSelector, string emptyText = null)
        {
            var result = new List<SelectListItem>();
            if(!emptyText.IsEmpty())
                result.Add(GetEmptyItem(emptyText));
            foreach (var item in source)
            {
                result.Add(new SelectListItem { Text = textSelector(item).ToString(), 
                    Value = valueSelector(item).ToString() });
            }
            return result;
        }

        public static List<SelectListItem> GetSelectItemList<T>(
            IEnumerable<T> source)
        {
            var result = new List<SelectListItem>();
            var displayColumn = 
                Config.DescriptionProvider.GetAttribute<DisplayColumnAttribute>(typeof(T))
                    .DisplayColumn;
            var idProperty = LinqToSqlUtils.GetPKPropertyInfo(typeof(T));
            foreach (var item in source)
            {
                result.Add(new SelectListItem
                {
                    Text = item.GetValue(displayColumn).ToString(),
                    Value = idProperty.GetValue(item).ToString()
                });
            }
            return result;
        }
    }
}