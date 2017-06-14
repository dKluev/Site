using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Order;
using Specialist.Services.Order.Extension;
using Specialist.Services.Center.Extension;
using Specialist.Services.Common.Extension;
using SimpleUtils.Common.Extensions;

namespace Specialist.Services.Order
{
    public class EditCourseVMService: IEditCourseVMService
    {
        [Dependency]
        public IOrderDetailService OrderDetailService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public ExtrasService ExtrasService { get; set; }

        [Dependency]
        public IDictionariesService DictionariesService {get; set; }

        [Dependency]
        public IPriceService PriceService { get; set;}

        [Dependency]
        public ICityService CityService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        public EditCourseVM GetCourseForEdit(decimal orderDetailID)
        {
        	var currentOrder = OrderService.GetCurrentOrder();
			if(currentOrder == null)
				return null;
            var orderDetail = currentOrder
                .OrderDetails.FirstOrDefault(od => od.OrderDetailID == orderDetailID);
			if(orderDetail == null)
				return null;
            return GetCourseVM(orderDetail, orderDetail.Track_TC != null);
        }

        public EditCourseVM GetCourseForEdit(string trackTC)
        {
        	var currentOrder = OrderService.GetCurrentOrder();
			if(currentOrder == null)
				return null;
        	var orderDetail = currentOrder
                .OrderDetails.FirstOrDefault(od => od.Track_TC == trackTC);
			if(orderDetail == null)
				return null;
            return GetCourseVM(orderDetail);
        }

        private EditCourseVM GetCourseVM(OrderDetail orderDetail)
        {
            return GetCourseVM(orderDetail, false);
        }

        List<City> FilterCityByPrices(List<City> cities, List<PriceView> prices) {
            return cities.Where(c => {
                var priceListType =
                    PriceListTypes.GetPriceListTypeTC(CityService
                        .GetCityPrefix(c.City_TC));
                return
                    prices.Any(
                        p => p.PriceType.PriceListType_TC == priceListType);
            }).ToList();
        }

        private EditCourseVM GetCourseVM(OrderDetail orderDetail, bool isTrackCourse) {
            var courseTC = orderDetail.Track_TC ?? orderDetail.Course_TC;
            var prices = PriceService.GetAllPricesForCourseFilterByCustomerTye(
                courseTC, orderDetail.Order.CustomerType, null);
            var cityTC = Cities.Moscow;
            if (orderDetail.Group != null)
                cityTC = orderDetail.Group.BranchOffice.TrueCity_TC;
            else if (orderDetail.City_TC != null)
                cityTC = orderDetail.City_TC;
            var editCourseVM = new EditCourseVM(CityService.GetPrefixList())
            {
                Cities =
                    FilterCityByPrices(CityService
                        .GetAll().ToList(), prices.ToList()),
                CityTC = cityTC,
                /*PriceListTypes.GetCityTC(orderDetail.PriceType.PriceListType_TC)
                    ?? Cities.Moscow*/
                OrderDetail = orderDetail,
                Prices = prices.OrderBy(p => p.Price).ToList()
            };

            if (PriceTypes.IsDistanceOrWebinar(orderDetail.PriceType_TC))
                editCourseVM.CityTC = Cities.Moscow;
            if(orderDetail.Track_TC == null) {
            	var groups = GroupService
            		.GetGroupsForCourse(courseTC)
            		.ToList();
            	var webinarOnlyGroups = GroupService.WebinarOnly().Where(x => x.Course_TC == courseTC).ToList();
				groups.AddRange(webinarOnlyGroups);
            	editCourseVM.Groups = groups.OrderBy(g => g.DateBeg).ToList();
            }
            else if(isTrackCourse)
            {
                editCourseVM.Groups = GroupService
                    .GetGroupsForCourseByPriceType(orderDetail.Course_TC, 
                        orderDetail.PriceType_TC, orderDetail.City_TC)
                    .OrderBy(g => g.DateBeg).ToList();
            }
            else
            {
                editCourseVM.Groups = new List<Group>();
            }
            editCourseVM.IsBusiness = PriceTypes.IsBusiness(orderDetail.PriceType_TC);
            if(PriceTypes.IsDistanceOrWebinar(orderDetail.PriceType_TC))
                editCourseVM.PriceTypeTC = PriceTypes.Webinar;
	        if (orderDetail.Group.GetOrDefault(x => x.IsIntraExtramural)) {
                editCourseVM.PriceTypeTC = PriceTypes.IntraExtra;
	        }
            if(PriceTypes.IsIndividual(orderDetail.PriceType_TC))
                editCourseVM.PriceTypeTC = PriceTypes.Individual;
            if(editCourseVM.PriceTypeTC == null)
                editCourseVM.PriceTypeTC = string.Empty;

            return editCourseVM;
        }

        public void Update(EditCourseVM model)
        {
            var newDetail = model.OrderDetail;
            var order = OrderService.GetCurrentOrder();
            var orderDetail = order.OrderDetails
                .First(od => od.OrderDetailID == newDetail.OrderDetailID);
            
            if(model.IsTrackCourse)
                UpdateTrackGroup(newDetail, orderDetail);
            else if (orderDetail.Track_TC != null)
            {
                UpdateTrackPrices(order, orderDetail.Track_TC, model.PriceTypeTC,
                    newDetail, model.CityTC);
            }
            else
                UpdateCourse(model, newDetail, orderDetail);
			orderDetail.ClearDiscount();
	        orderDetail.SeatNumber = null;
        	var extrases = ExtrasService.GetFor(orderDetail).Select(x => x.Extras_ID);
        	var notAvailableExtrases = orderDetail.OrderExtras.Where(
        		od => !extrases.Contains(od.Extras_ID)).ToList();
        	for (int i = 0; i < notAvailableExtrases.Count; i++) {
        		var notAvailable = notAvailableExtrases[i];
        		orderDetail.OrderExtras.Remove(notAvailable);
        	}

        	OrderService.SubmitChanges();
        }

        public void UpdateTrack(List<OrderDetail> orderDetails)
        {
            var order = OrderService.GetCurrentOrder();
            var trackTC = order.OrderDetails
                .First(od => od.OrderDetailID == orderDetails.First().OrderDetailID)
                .Track_TC;
            var priceTypeTC = GetMaxPriceType(order, orderDetails);
            UpdateTrackPrices(order, trackTC, priceTypeTC, null, Cities.Moscow);
            foreach (var newDetail in orderDetails)
            {
                var orderDetail = order.OrderDetails
                    .First(od => od.OrderDetailID == newDetail.OrderDetailID);

                UpdateTrackGroup(newDetail, orderDetail);
            }
            OrderService.SubmitChanges();
        }

        private string GetMaxPriceType(Entities.Context.Order order, 
            List<OrderDetail> newOrderDetails)
        {
            var orderDetailIDs = newOrderDetails.Select(od => od.OrderDetailID);
            var trackDetails = order.OrderDetails.Where(od => 
                orderDetailIDs.Contains(od.OrderDetailID));

            var prices = new List<PriceView>();
            foreach (var orderDetail in trackDetails)
            {
                orderDetail.Group_ID = newOrderDetails.FirstOrDefault(
                    od => od.OrderDetailID == orderDetail.OrderDetailID).Group_ID;
                if(orderDetail.Group_ID == null)
                    continue;

                var group = GroupService.GetPlannedAndNotBegin()
                    .ByPrimeryKey(orderDetail.Group_ID);
                var priceTypeTC = OrderService.GetPriceTypeForGroup(
                    group, false, order.CustomerType);
                var price = PriceService.GetAllPricesForCourse(orderDetail.Course_TC,
                    orderDetail.Track_TC).FirstOrDefault(p => p.PriceType_TC == priceTypeTC);
                if(price != null)
                    prices.Add(price);
            }
            return prices.AsQueryable().SelectMax(p => p.Price).PriceType_TC;
        }



        private void UpdateTrackGroup(OrderDetail newDetail, OrderDetail orderDetail)
        {
            orderDetail.Group_ID = newDetail.Group_ID;
	        orderDetail.SeatNumber = null;

        }

        private void UpdateCourse(EditCourseVM model, OrderDetail newDetail,
            OrderDetail orderDetail) 
        {
            if (orderDetail.Order.IsOrganization && newDetail.Count > 0) {
            	orderDetail.Count = newDetail.Count;
            }
			orderDetail.OrgStudents = newDetail.OrgStudents;

            var prices = PriceService.GetAllPricesForCourse(orderDetail.Course_TC,
                orderDetail.Track_TC).AsQueryable();

            string resultPriceTypeTC = null;
            if (!model.CityTC.IsEmpty())
                orderDetail.City_TC = model.CityTC;
            if (PriceTypes.IsDistance(model.PriceTypeTC))
            {
                resultPriceTypeTC = model.PriceTypeTC;
                orderDetail.Group_ID = null;
            }
            if(PriceTypes.IsIndividual(model.PriceTypeTC)) {
                resultPriceTypeTC = model.PriceTypeTC;
                orderDetail.Group_ID = null;
            }
            if (model.PriceTypeTC == string.Empty)
            {
                orderDetail.Group_ID = newDetail.Group_ID;
                if (orderDetail.Group_ID.HasValue)
                {
                    var group = GroupService.GetByPK(newDetail.Group_ID);
                    resultPriceTypeTC = OrderService.GetPriceTypeForGroup(group,
                        model.IsBusiness, orderDetail.Order.CustomerType);
                }
                else
                {
                    var groupPrices = prices.Where(p => !p.IsDistance && !p.IsIndividual).AsQueryable();
                    var maxGroupPrice = groupPrices.GetDefault();
                    if (maxGroupPrice != null)
                    {
                        resultPriceTypeTC = maxGroupPrice.PriceType_TC;
                    }
                }
            }

            if (PriceTypes.Webinars.Contains(model.PriceTypeTC))
            {
                resultPriceTypeTC = model.PriceTypeTC;
                orderDetail.Group_ID = newDetail.Group_ID;
            }


            if (resultPriceTypeTC != null)
            {
                orderDetail.PriceType_TC = resultPriceTypeTC;
                orderDetail.Price = prices.First(p => p.PriceType_TC == resultPriceTypeTC)
                    .Price;
            }
            else
            {
                var defaultPrice = prices.GetDefault();
                orderDetail.PriceType_TC = defaultPrice.PriceType_TC;
                orderDetail.Price = defaultPrice.Price;
            }
            
        }

        private void UpdateTrackPrices(Entities.Context.Order order, string trackTC, string priceTypeTC, OrderDetail newDetail, string cityTC)
        {

            var trackDetails = order.OrderDetails.Where(od => od.Track_TC == trackTC);
        	string pricePrefix = null;
			if(cityTC != null)
                pricePrefix = PriceTypes.GetPrefix(CityService.GetCityPrefix(cityTC));
            
            foreach (var orderDetail in trackDetails)
            {
                if (newDetail != null) {
                	if (order.IsOrganization && newDetail.Count > 0) 
						orderDetail.Count = newDetail.Count;
					orderDetail.OrgStudents = newDetail.OrgStudents;
                }
                if (priceTypeTC == string.Empty)
                    priceTypeTC =
                        pricePrefix + (orderDetail.Order.IsOrganization
                            ? PriceTypes.Corporate
                            : PriceTypes.PrivatePersonWeekend);
                var allPricesForCourse = 
                    PriceService.GetAllPricesForCourse(orderDetail.Course_TC,
                        orderDetail.Track_TC).AsQueryable();
                var price = allPricesForCourse.FirstOrDefault(p => p.PriceType_TC ==
                        priceTypeTC);
                if (price == null)
                    price = allPricesForCourse.GetDefault();

                orderDetail.Group_ID = null;
	            orderDetail.SeatNumber = null;
                orderDetail.PriceType_TC = priceTypeTC;
                orderDetail.City_TC = cityTC;
                orderDetail.Price = price.Price;

            }
        }
       
    }
}
