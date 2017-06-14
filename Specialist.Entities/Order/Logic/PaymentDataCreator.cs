using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Linq;
using System.Text;
using SimpleUtils;
using System.Linq;
using SimpleUtils.Collections;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context.Const;
using SimpleUtils.Extension;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Order.Logic;
using Specialist.Entities.Utils;
using EnumerableExtension = SimpleUtils.Collections.Extensions.EnumerableExtension;

namespace Specialist.Entities.Context.Logic
{
    public static class PaymentDataCreator {


			public static bool NewRules = false; // DateTime.Today < new DateTime(2016, 07, 01);

    	public const string WebMoneyUrl = // "https://merchant.webmoney.ru/lmi/payment.asp";
		"https://paymaster.ru/Payment/Init";

	    public static string GetTagOrg(Course c) {
		    if (NewRules) {
			    if (c.AuthorizationType_TC == AuthorizationTypes.Microsoft && !c.IsNotAuthorized) {
				    return OurOrgs.Cos;
			    }
			    return OurOrgs.Ru;
		    }
		    if (c.AuthorizationType_TC != null && !c.IsNotAuthorized) {
			    return OurOrgs.Cos;
		    }

		    if (c.IsTheory) {
			    return OurOrgs.Ru;
		    }
		    if (c.CourseDirectionA_TC.In("ÊÃ", "ÁÊÏ", "ÎÔÈÑ")) {
			    return OurOrgs.Ru;
		    }
		    if (c.CourseDirectionA_TC.In("ÈÒÏÐ", "ÏÐÃÑÓÁÄ", "ÑÀÏÐ")) {
			    return OurOrgs.Ru;
		    }
		    if (c.CourseDirectionA_TC == "ÁÓÕ") {
			    return OurOrgs.Ru;
		    }

		    return OurOrgs.Cos;
	    }

	    public static Tuple<string, bool, decimal> GetOurOrg(OrderDetail orderDetail) {
		    var x = GetOurOrgTC(orderDetail);
		    return Tuple.Create(x.Item1, x.Item2, orderDetail.PriceWithDiscount);
	    }

	    public static Tuple<string,bool> GetOurOrgTC(OrderDetail orderDetail) {
		    var complexTC = orderDetail.Group.GetOrDefault(x => x.Complex_TC);
		    if (orderDetail.IsTrack 
				&& orderDetail.Track.OurOrg_TC != null 
				&& orderDetail.Track.OurOrg_TC != OurOrgs.CKO) {
			    return Tuple.Create(orderDetail.Track.OurOrg_TC, false);
		    }
		    if (orderDetail.ReasonForLearning == LearningReasons.Comprehensive || orderDetail.Course.BaseHours <= 8) {
				return Tuple.Create(OurOrgs.Spec, false);
		    }
		    if (complexTC != null) {
			    var classTC = orderDetail.Group.GetOrDefault(x => x.ClassRoom_TC);
			    var hasEducation = LearningReasons.HasEducation.Contains(orderDetail.ReasonForLearning);

			    if (hasEducation) {
				    if (Cities.ClasseRooms.Bm891011.Contains(classTC)) {
					    return Tuple.Create(OurOrgs.Cos, false);
				    }

				    if (Cities.ClasseRooms.OrderRu.Contains(classTC)) {
					    return Tuple.Create(OurOrgs.Ru, false);
				    }

				    if (Cities.Complexes.PlPark.Contains(complexTC)) {
					    return Tuple.Create(OurOrgs.Ru, false);
				    }
				    if (Cities.Complexes.OrderSpec.Contains(complexTC)) {
					    return Tuple.Create(OurOrgs.Spec, false);
				    }
			    }
		    }


		    if (orderDetail.Course.OurOrg_TC != null && orderDetail.Course.OurOrg_TC != OurOrgs.CKO) {
			    return Tuple.Create(orderDetail.Course.OurOrg_TC, false);
		    }
            
			return Tuple.Create(OurOrgs.Spec, false);

/*
			if (orderDetail.Group.GetOrDefault(x => Cities.ClasseRooms.OrderRu.Contains(x.ClassRoom_TC))){
			    return Tuple.Create(OurOrgs.Ru, false);
		    }
			if (orderDetail.Group.GetOrDefault(x => Cities.ClasseRooms.OrderSpec.Contains(x.ClassRoom_TC))){
			    return Tuple.Create(OurOrgs.Spec, false);
		    }
		    if (Cities.Complexes.OrderSpec.Contains(complexTC)) {
    			return Tuple.Create(OurOrgs.Spec,false);
    		}
			if (Cities.Complexes.OrderCos.Contains(complexTC)) {
    			return Tuple.Create(OurOrgs.Cos,false);
    		}
			if (Cities.Complexes.OrderRu.Contains(complexTC)) {
				if (NewRules && complexTC == Cities.Complexes.BS) {
					var c = orderDetail.Course;
				    if (c.AuthorizationType_TC == AuthorizationTypes.Microsoft && !c.IsNotAuthorized) {
					    return Tuple.Create(OurOrgs.Cos,false);
				    }
				}
    			return Tuple.Create(OurOrgs.Ru,false);
    		}
			if (Cities.Complexes.OrderTag.Contains(complexTC)) {
    			return Tuple.Create(GetTagOrg(orderDetail.Course), true);
    		}
*/
	    }

	    public static string GetOurOrg(Order order, HashSet<string> seminarCourses) {
		    if (order.OrderDetails.Any(x => x.IsDopUsl)) {
			    return OurOrgs.Bt;
		    }
		    if (order.OnlyExam || order.OnlyTestCert) {
			    return OurOrgs.CS;
		    }
		    if (order.HasSchool) {
			    return OurOrgs.Spec;
		    }
		    var orderDetails = order.GetCourseOrderDetails();
		    var parts = ListUtils.Partition(orderDetails, od => !seminarCourses.Contains(od.Course_TC));
		    orderDetails = parts.Item1.Any() ? parts.Item1 : parts.Item2;
		    var orgsData = orderDetails.Select(GetOurOrg).ToList();
		    var orgsWithoutTag = orgsData.Where(x => !x.Item2).Select(x => x.Item1).Distinct().ToList();
		    if (orgsWithoutTag.Count == 1) {
			    return orgsWithoutTag.First();
		    }
		    var tagOrgs = orgsData.Where(x => x.Item2).ToList();
		    if (orgsWithoutTag.Count == 0 && tagOrgs.Any()) {
			    return tagOrgs.OrderByDescending(x => x.Item3).First().Item1;
		    }
    		return OurOrgs.Spec;
	    }
        public static NameValueCollection WebMoney(Order order) {
	        var id = OrderCommonConst.WebMoney.GetValueOrDefault(order.OurOrgOrDefault);
	        if (id == null) return new NameValueCollection();
        	var description = StringUtils.SafeSubstring(order.User.FullName + " " + order.Description,255);
        	return 
                new NameValueCollection
                {
                    {"LMI_MERCHANT_ID", id}, 
                    {"LMI_PAYMENT_AMOUNT", PriceString(order.TotalPriceWithDescount)}, 
                    {"LMI_CURRENCY", "RUB"},
					{"LMI_PAYMENT_SYSTEM", "WebMoney,BankCard"},
                    {"LMI_PAYMENT_NO", order.OrderID.ToString()}, 
                    {"LMI_PAYMENT_DESC", description}, 
                    {"LMI_PAYMENT_DESC_BASE64", 
						StringUtils.EncodeBase64( description)}, 
                    {"LMI_SIM_MODE", "0"}, 
                };
        }


    	public static string PriceString(decimal price) {
			return ((int) price).ToString();
		}

//        public static NameValueCollection YandexMoney(Order order)
//        {
//
//    		var id = OrderCommonConst.YandexMoney.GetValueOrDefault(order.OurOrgOrDefault);
//    		if (id == null) return new NameValueCollection();
//            return
//                new NameValueCollection
//                {
//                    {"scid", id.Item1}, 
//                    {"ShopID", id.Item2}, 
//                    {"CustomerNumber", order.OrderID.ToString()}, 
//                    {"Sum", PriceString(order.TotalPriceWithDescount)}, 
//                    {"CustName", order.User.FullName}, 
//                    {"CustAddr", ""}, 
//                    {"CustEMail",  GetEmail(order)}, 
//                    {"OrderDetails", order.Description}, 
//                };
//        }

	    public static NameValueCollection SberbankMerchantLogin(Order order) {
	        var ourOrgTC = order.OurOrgOrDefault;

    		var id = OrderCommonConst.SberbankMerchant.GetValueOrDefault(ourOrgTC);
    		if (id == null) return null;
		    return
			    new NameValueCollection {
				    {"userName", id.Item1},
				    {"password", id.Item2},
			    };

	    }

	    public static NameValueCollection SberbankMerchant(Order order) {
		    var login = SberbankMerchantLogin(order);
		    if (login == null) {
			    return new NameValueCollection();
		    }
			login.Add(
                new NameValueCollection
                {
                    {"orderNumber", order.CommonOrderId}, 
                    {"amount", PriceString(order.TotalPriceWithDescount * 100)}, 
                    {"currency", "643"}, 
                    {"description", StringUtils.SafeSubstring(order.User.FullName + " " + order.Description, 1000)}, 
					{"expirationDate", DateTime.Now.AddDays(7).ToString("s")},
                    {"returnUrl", "http://www.specialist.ru/order/paymentcomplete"}
                });
		    return login;
	    }

    	private static string GetEmail(Order order) {
    		return order.User.Email.GetOrDefault(x => x.Trim());
    	}

/*
    	public static NameValueCollection Qiwi(Order order) {
    		var id = OrderCommonConst.Qiwi.GetValueOrDefault(order.OurOrgOrDefault);
    		if (id == null) return new NameValueCollection();
        	var data = new NameValueCollection
        	{
        		{"from", id}, 
        		{"txn_id", order.OrderID.ToString()}, 
				{"lifetime", (24*14).ToString()},
				{"check_agt", "false"},
        		{"summ", PriceString(order.TotalPriceWithDescount)}, 
        		{"com", StringUtils.SafeSubstring(order.User.FullName + " " + order.Description,255)}, 
        	};
        	return data;
        }
*/

    	public static NameValueCollection RbkMoney(Order order, string preference = RbkTypes.all) {

    		var shopId = OrderCommonConst.Rbk.GetValueOrDefault(order.OurOrgOrDefault);
    		if (shopId == null) return new NameValueCollection();
        	var data = new NameValueCollection
        	{
        		{"eshopId", shopId}, 
        		{"orderId", order.OrderID.ToString()}, 
        		{"recipientAmount", PriceString(order.TotalPriceWithDescount)}, 
        		{"recipientCurrency", "RUR"}, 
        		{"serviceName", StringUtils.SafeSubstring(order.User.FullName + " " + order.Description,255)}, 
        	};

			var email = GetEmail(order);
			if(!email.IsEmpty())
        		data.Add("user_email", email); 
			if(preference != RbkTypes.all)
        		data.Add("preference", preference); 
        	return data;
        }

    }
}
