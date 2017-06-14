using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using System.Linq;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Const;
using Specialist.Web.Helpers;
using SimpleUtils.Extension;

namespace Specialist.Web.Core.Views {
	public class TestCertificatesView : BaseView<TestCertificatesVM> {
		public override object Get() {

			return l(
				Model.OrderDetails.Any() ? (object)
				table.Class("defaultTable")[Head("Тест", "Статус"),
					Model.OrderDetails.Select(x => Row(
						Url.UserTestLink(x.UserTest),
						GetStatus(x)	
					))] : strong["На данный момент у вас нет заказанных сертификатов тестирования."],
					br, strong[
					Anchor(SimplePages.FullUrls.TestCertOrder, "Порядок заказа сертификатов")]);
		}

		public object GetStatus(OrderDetail orderDetail) {
			if(orderDetail.StudentInGroup != null 
				&& BerthTypes.AllPaidForTestCerts
					.Contains(orderDetail.StudentInGroup.BerthType_TC))
				if(orderDetail.Params.Type == TestCertType.Image) {
					var isRusEng = orderDetail.Params.Lang == TestCertLang.RusEng;
					var links = _.List(Url.Link<OrderController>(c =>
						c.DownloadTestCertificate(orderDetail.OrderDetailID, false), 
						isRusEng ? "Русский" : "Скачать"));
					if(isRusEng) {
						links.Add(
						Url.Link<OrderController>(c =>
							c.DownloadTestCertificate(orderDetail.OrderDetailID, true), "Английский"));
						
					}
					return links;
				}else {
					if(orderDetail.StudentInGroup.IsReported) {
						return "Отправлен";
					}
					return "Обрабатывается";
				}
			return "Проверка оплаты";
		}
	}
}