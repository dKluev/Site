using System.Collections.Generic;
using System.Web.Mvc;
using SimpleUtils.Util;

namespace Specialist.Web.Util
{
    public class Month
    {
       

        public static List<SelectListItem> GetAllMoreThen(int monthIndex)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "Нет", Selected = true});

            for (var i = monthIndex; i <= 12; i++)
            {
                var monthName = MonthUtil.GetName(i);             
                result.Add(new SelectListItem{Text = monthName, Value = i.ToString()});

            }
            return result;
        }

       

   


    }
}