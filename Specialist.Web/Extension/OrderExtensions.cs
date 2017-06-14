using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using SimpleUtils.Common.Enum;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Helpers;
using SpecialistTest.Web.Core.Mvc.Extensions;

namespace Specialist.Web.Extension
{
    public static class OrderExtensions
    {
        public static string GetFullDateInfo(this Group group, HtmlHelper helper)
        {
            var result = string.Empty;
            if (group != null)
            {
                result += group.DateInterval + "<br />";
                result += group.DayShift.GetOrDefault(x => x.Name + "<br />");
                result += helper.ComplexLink(group.Complex);
            }
            else
            {
                result = "Уточнить дату позже";
            }
            return result;
        }

        public static string GetSeatNumber(this OrderDetail orderDetail, UrlHelper url) {
	        if(orderDetail.Group_ID == null || orderDetail.IsWebinar 
				|| orderDetail.Group.IsOpenLearning || orderDetail.Group.IsIntraExtramural) {
		        return string.Empty;
	        }
	        var text = "Выбрать";
			if(orderDetail.SeatNumber != null) {
				text = "Место №" + orderDetail.SeatNumber;
			}
	        return
		        url.Link<EditCartController>(c => 
					c.EditSeatNumber(orderDetail.OrderDetailID), text)
					.Class("open-in-dialog not-link").ToString();
        }

        public static string GetStudyType(this OrderDetail orderDetail)
        {
            var result = string.Empty;
            if (orderDetail.PriceType_TC != null)
            {
	            if (PriceTypes.IsIntraExtra(orderDetail.PriceType_TC) 
					|| (orderDetail.Group != null && orderDetail.Group.IsIntraExtramural)) {
                    result += H.Anchor(SimplePages.FullUrls.IntraExtramural, "Очно-заочное обучение");
	            }else if (PriceTypes.IsDistanceOrWebinar(orderDetail.PriceType_TC)
                 || PriceTypes.IsIndividual(orderDetail.PriceType_TC))
                {
                    result += PriceTypes.GetFullName(orderDetail.PriceType.CommonPriceTypeTC);
                }
                else if(orderDetail.Group != null && orderDetail.Group.IsOpenLearning) {
                    result += H.Anchor(SimplePages.FullUrls.OpenClasses, "Открытое обучение");
                	
                }else 
                {
                    result += "Очное обучение ";
                }
                if (PriceTypes.IsBusiness(orderDetail.PriceType_TC))
                {
                    result += Images.Common("eat.gif");
                }
            }

            return result;
        }

        public static List<StepsVM.Step> GetAll(this ViewUserControl<StepsVM> control)
        {
            var model = control.Model;
            var result = new List<StepsVM.Step>();
            foreach (OrderStep orderStep in Enum.GetValues(typeof(OrderStep)))
            {
                if (orderStep == OrderStep.Register && control.Request.IsAuthenticated)
                    continue;
                var step = new StepsVM.Step();
                result.Add(step);
                step.IsCurrent = orderStep == model.OrderStep;
                step.IsPass = model.OrderStep > orderStep;
                var name = EnumUtils.GetDisplayName(orderStep);
                switch (orderStep)
                {
                    case OrderStep.Cart:
                        step.Link = control.Html
                            .ActionLink<CartController>(c => c.Details(), name).ToString();
                        break;
                    case OrderStep.Register:
                        step.Link = name;
                        break;
//                    case OrderStep.Contract:
//                        step.Link = control.Html
//                            .ActionLink<OrderController>(c => c.Contract(), name).ToString();
//                        break;
                    case OrderStep.PaymentTypeChoice:
                        step.Link = name;
                        break;
                }
                if (!step.IsPass)
                    step.Link = name;

            }
            return result;
        }
    }
}