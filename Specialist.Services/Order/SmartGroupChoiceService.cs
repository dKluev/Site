//using System;
//using System.Collections.Generic;
//using Microsoft.Practices.Unity;
//using SimpleUtils.Collections.Extensions;
//using SimpleUtils.Common.Extensions;
//using SimpleUtils.Linq.Data.LInq;
//using SimpleUtils.LinqToSql;
//using Specialist.Entities.Common;
//using Specialist.Entities.Context;
//using Specialist.Entities.Context.Const;
//using Specialist.Entities.Context.ViewModel;
//using Specialist.Services.Center.Extension;
//using Specialist.Services.Interface;
//using Specialist.Services.Interface.Center;
//using Specialist.Services.Interface.Order;
//using System.Linq;
//using SimpleUtils;
//using Specialist.Services.Catalog.Extension;
//using SimpleUtils.Util;
//using SimpleUtils.Extension;
//using Specialist.Services.Order.Extension;
//
//namespace Specialist.Services.Order
//{
//    public class SmartGroupChoiceService : ISmartGroupChoiceService
//    {
//        [Dependency]
//        public IOrderService OrderService { get; set; }
//
//        [Dependency]
//        public IGroupService GroupService { get; set; }
//
//        [Dependency]
//        public IPriceService PriceService { get; set; }
//
//        [Dependency]
//        public IDayShiftService DayShiftService { get; set; }
//
//        [Dependency]
//        public IComplexService ComplexService { get; set; }
//
//        [Dependency]
//        public IUserSettingsService UserSettingsService { get; set; }
//
//        public SmartGroupChoiceVM Get(string trackTC)
//        {
//            var model = new SmartGroupChoiceVM();
//            model.TrackTC = trackTC;
//            Init(model);
//            model.DayOfWeeks = new List<DayOfWeek>();
//            return model;
//        }
//
//        private void Init(SmartGroupChoiceVM model)
//        {
//            var order = OrderService.GetCurrentOrder();
//            model.OrderTrack = new OrderTrack
//            {
//                OrderDetails = order.OrderDetails.Where(od => od.Track_TC == model.TrackTC)
//                    .ToList(),
//            };
//            var complexes = new List<Complex>();
//            var dayShifts = new List<DayShift>();
//            var dayOfWeeks = new List<DayOfWeek>();
//            foreach (var orderDetail in model.OrderTrack.OrderDetails)
//            {
//                var groups = GroupService.GetPlannedAndNotBegin().
//                    ByCourse(orderDetail.Course_TC).ByCity(UserSettingsService.CityTC);
//                complexes.AddRange(groups.Select(g => g.Complex).Distinct());
//                dayShifts.AddRange(groups.Select(g => g.DayShift).Distinct());
//                var courseDayOfWeeks = GroupService.GetAll().PlannedAndNotBegin()
//                    .ByCourse(orderDetail.Course_TC).ByCity(UserSettingsService.CityTC)
//                    .SelectMany(g => g.Lectures).Select(l => l.LectureDateBeg.DayOfWeek)
//                    .Distinct();
//                dayOfWeeks.AddRange(courseDayOfWeeks);
//                orderDetail.Group_ID = null;
//            }
            //            complexes = ComplexService.GetAll().PublishInSite().ToList();
//            model.Complexes = complexes.DistinctByPK().ToList();
//            model.DayShifts = dayShifts.DistinctByPK().ToList();
//            model.WeekDays = dayOfWeeks.Distinct().Select(dow => new WeekDay(dow))
//
//                .ToList();
//            if(model.DayOfWeeks == null)
//                model.DayOfWeeks = new List<DayOfWeek>();
//            model.DayShifts = DayShiftService.GetCurrent();
//
//        }
//
//        public SmartGroupChoiceVM SetGroups(SmartGroupChoiceVM model)
//        {
//            Init(model);
//            switch (model.Type)
//            {
//                case SmartChoiceType.Fast:
//                    Fast(model);
//                    break;
//                case SmartChoiceType.Economic:
//                    Economic(model);
//                    break;
//                case SmartChoiceType.Custom:
//                    Custom(model);
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//            model.IsResult = true;
//            return model;
//        }
//
//        private void Fast(SmartGroupChoiceVM model)
//        {
//            var orderDetails = model.OrderTrack.OrderDetails.OrderBy(od => od.OrderDetailID);
//            var selectedGroups = new List<Group>();
//            foreach (var orderDetail in orderDetails)
//            {
//                var courseGroups = GroupService.GetGroupsForCourse(orderDetail.Course_TC)
//                    .ByCity(UserSettingsService.CityTC).OrderBy(g => g.DateEnd);
//                foreach (var group in courseGroups)
//                {
//                    if(selectedGroups.All(g => !g.IsCross(group)))
//                    {
//                        selectedGroups.Add(group);
//                        orderDetail.Group_ID = group.Group_ID;
//                        break;
//                    }
//                }
//            }
//        }
//
//        private void Economic(SmartGroupChoiceVM model)
//        {
//            var orderDetails = model.OrderTrack.OrderDetails.OrderBy(od => od.OrderDetailID);
//            var price = PriceService
//                .GetAllPricesForCourse(model.OrderTrack.Track.Course_TC, null)
//                .ByCity(UserSettingsService.CityTC)
//                .SelectMin(p => p.Price);
//            var priceTypeTC = price.GetOrDefault(x => x.PriceType_TC);
//            SetOrderByDateGroups((pd, od) => GroupService.GetGroupsForCourseByPriceType(
//                    od.Course_TC, priceTypeTC, UserSettingsService.CityTC)
//                    .Where(g => g.DateBeg > pd)
//                    .SelectMin(g => g.DateBeg), orderDetails);     
//        }
//
//        private void Custom(SmartGroupChoiceVM model)
//        {
//            var orderDetails = model.OrderTrack.OrderDetails.OrderBy(od => od.OrderDetailID);
//            SetOrderByDateGroups((pd, od) => GroupService.GetAll().SmartChoice(model,
//                    od.Course_TC, pd, UserSettingsService.CityTC), orderDetails);
//        }
//
//        private void SetOrderByDateGroups(Func<DateTime, OrderDetail, Group> groupSelector, 
//            IOrderedEnumerable<OrderDetail> orderDetails) {
//          /*  var previousDateEnd = DateTime.Today;
//            foreach (var orderDetail in orderDetails)
//            {
//                var group = groupSelector(previousDateEnd, orderDetail);
//                if (group != null)
//                {
//                    orderDetail.Group_ID = group.Group_ID;
//                    if (group.DateEnd.HasValue || orderDetail.Course.Weeks.HasValue)
//                        previousDateEnd = group.DateEnd ?? group.DateBeg.Value
//                            .AddDays((int)orderDetail.Course.Weeks * 7);
//                }
//            }*/
//        }
//    }
//}