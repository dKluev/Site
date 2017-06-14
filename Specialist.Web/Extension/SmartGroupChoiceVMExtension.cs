//using System.Collections.Generic;
//using System.Web.Mvc;
//using SimpleUtils;
//using SimpleUtils.Collections.Extensions;
//using SimpleUtils.Common.Extensions;
//using Specialist.Entities.Common;
//using Specialist.Entities.Const;
//using Specialist.Entities.Context;
//using Specialist.Entities.Context.ViewModel;
//using System.Linq;
//using Specialist.Web.Util;
//using Specialist.Services.Catalog.Extension;
//
//namespace Specialist.Web.Extension
//{
//    public static class SmartGroupChoiceVMExtension
//    {
//        public static List<SelectListItem> GetWeekDays(
//            this SmartGroupChoiceVM model)
//        {
//            return model.WeekDays.Select(wd =>
//                new SelectListItem
//                {
//                    Selected = model.DayOfWeeks.Contains(wd.DayOfWeek),
//                    Text = wd.Name,
//                    Value = wd.DayOfWeek.ToString(),
//                }).ToList();
//        }
//
//        public static List<SelectListItem> GetComplexList(this SmartGroupChoiceVM model)
//        {
//            return new [] {new SelectListItem{Text = "Нет", Value = string.Empty}}.ToList().
//                AddFluent(SelectListUtil
//                    .GetSelectItemList(model.Complexes, x =>
//                    {
//                        var stations = Metro.GetStations(x.Address).JoinWith(",");
//                        return x.Name + (stations.IsEmpty() ? "" : " (м) ")  
//                            + stations;
//                    }, x => x.Complex_TC));
//        }
//
//        public static List<SelectListItem> GetDayShifts(this SmartGroupChoiceVM model)
//        {
//            return new [] {new SelectListItem{Text = "Нет", Value = string.Empty}}.ToList().
//                AddFluent(SelectListUtil
//                    .GetSelectItemList(model.DayShifts, x => x.Name , x => x.DayShift_TC));
//        }
//
//        
//      /*  public static void SetFullFill(this SmartGroupChoiceVM model, Group group)
//        {
//            model.FullFillComplex = model.FullFillDayShift = model.FullFillWeekDays = true;
//            new[] { group }.AsQueryable().SmartChoice(model);
//             
//        }*/
//    }
//}