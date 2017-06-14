using System;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Utils;

namespace Specialist.Web.Root.Orders.Services {
	public class SberbankService {
		private const string root = "https://securepayments.sberbank.ru";
		private const string sberUrl = root + "/payment/merchants/rbs/payment_ru.html?mdOrder=";
		[Dependency]
		public IRepository2<SberbankOrder> SberbankOrderService { get; set; }
		public string GetUrl(Order order) {
			SberbankOrderService.EnableTracking();
			var sberOrder = SberbankOrderService.FirstOrDefault(x => x.CommonOrderId == order.CommonOrderId);
			Guid sberbankId;
			if (sberOrder != null) {
				sberbankId = sberOrder.SberbankId;
			} else {
				var result = HttpUtils.Get(root + "/payment/rest/register.do",
					PaymentDataCreator.SberbankMerchant(order));
				sberbankId = JsonConvert.DeserializeAnonymousType(result, new {orderId = Guid.Empty, formUrl = ""})
					.orderId;
				if (sberbankId == Guid.Empty) {
					Logger.Exception(new Exception(result), "sberbank " + order.CommonOrderId);
					return null;
				}
				SberbankOrderService.InsertAndSubmit(new SberbankOrder {
					CommonOrderId = order.CommonOrderId,
					SberbankId = sberbankId
				});
			}
			return sberUrl + IdToString(sberbankId);
			
		}


		public int GetAmount(string id, Order order) {
			var param = PaymentDataCreator.SberbankMerchantLogin(order);
			param.Add("orderId", id);
			var result = HttpUtils.Get(root + "/payment/rest/getOrderStatus.do", param);
			var ammount = JsonConvert.DeserializeAnonymousType(result, new {Amount = 0}).Amount;
			return ammount/100;
		}

		private static string IdToString(Guid sberbankId) {
			return sberbankId.ToString("D");
		}
	}
}