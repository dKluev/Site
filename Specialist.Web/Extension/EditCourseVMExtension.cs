using System.Collections.Generic;
using System.Web.Mvc;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Order.Const;
using Specialist.Entities.ViewModel;
using SimpleUtils;
using Specialist.Web.Util;
using System.Linq;

namespace Specialist.Web.Extension
{
    public static class EditCourseVMExtension
    {
        public static List<SelectListItem> GetAllCity(this EditCourseVM model)
        {
            return SelectListUtil.GetSelectItemList(model.Cities,
                x => x.CityName,
                x => x.City_TC);
               /* .AddItem(new SelectListItem
                {
                    Text = "Дистанционное обучение",
                    Value = Cities.Distance,
                });*/
        }

        
        public static List<SelectListItem> GetAllGroups(this EditCourseVM model, 
            string cityTC)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectGroupItem
                           {
                               Text = "Уточнить дату позже",
                               Value = ((int)Groups.NotChoiceGroupID).ToString()
                           });
            foreach (var group in model.GetGroups(cityTC))
            {
                result.Add(new SelectGroupItem
                               {
                                   Selected = group.Group_ID == model.OrderDetail.Group_ID,
                                   Text =
                                       (group.DateBeg)
                                       .NotNullToString("dd.MM.yyyy") + " " + 
                                       group.DayShift.Name + " " +
                                       group.Complex.Name,
                                   Value = group.Group_ID.ToString(),
                                   DayShift = group.DayShift_TC
                           });
            }
            return result;
        }
    }
}