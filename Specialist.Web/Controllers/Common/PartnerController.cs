using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Policy;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Services.Common;
using Specialist.Services.Interface;
using Specialist.Services.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.ActionResults;
using System.Linq;
using System.Text;
using Specialist.Web.Common.Utils;
using Specialist.Web.Root.Partners.ViewModels;
using Specialist.Web.Util;
using Specialist.Web.Helpers;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog.Links.Interfaces;
using SimpleUtils.Extension;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Order;
using Tuple = SimpleUtils.Common.Tuple;
using Specialist.Web.Common.Extension;
using Specialist.Web.Root.Orders.Services;
using Specialist.Services.Order;

namespace Specialist.Web.Controllers.Common {
	public class PartnerController: ExtendedController {

		[Dependency]
		public YmlGenerator YmlGenerator { get; set; }
		[Dependency]
		public UchebaRuGenerator UchebaRuGenerator { get; set; }
		[Dependency]
		public TeachMePleaseGenerator TeachMePleaseGenerator { get; set; }

		[Dependency]
		public X5RetailGenerator X5RetailGenerator { get; set; }

		[Dependency]
		public SverhMarketGenerator SverhMarketGenerator { get; set; }
		[Dependency]
		public GoogleMerchantGenerator GoogleMerchantGenerator { get; set; }

		[Dependency]
		public GoogleRemarketingGenerator GooreGoogleRemarketingGenerator { get; set; }

		[Dependency]
		public SuperJobService SuperJobService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }
		
		[Dependency]
		public IGroupService GroupService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public IOrderService OrderService { get; set; }

		[Dependency]
		public SberbankService SberbankService { get; set; }

        [Dependency]
        public ISpecialistExportService SpecialistExportService { get; set; }



        [Dependency]
		public IRepository2<SberbankOrder> SberbankOrderService { get; set; }
		public ActionResult SberbankCallback(string mdOrder, string orderNumber, string operation, int status) {
            //TODO: Автоматизация кассы https://spoint.specialist.ru/departments/projects/_layouts/listform.aspx?PageType=4&ListId={11B73C8A-2110-40C3-8169-06E92016FE8D}&ID=3530&ContentTypeID=0x010800B6F61EE8E28A8748843BA6EA5E5FB40A
            if (status == 1) {
				var orderId = Guid.Parse(mdOrder);
				if (SberbankOrderService.GetAll(x => x.CommonOrderId == orderNumber
					&& x.SberbankId == orderId).Any()) {
					var order = OrderService.GetByCommonId(orderNumber);
					UpdateOrderPrice(mdOrder, order);
                    /*
                    var sigIdList = order.IsSig ? new List<decimal> { Order.GetIdFromCommon(orderNumber).Item2 } : order.OrderDetails.Select(x => x.StudentInGroup_ID.GetValueOrDefault()).Where(x=>x>0).ToList();
                    foreach (var sigId in sigIdList)
                    {
                        SpecialistExportService.SetDVP(sigId);
                    }
                    */
                    MailService.SberMerchant(order, operation);

				} else {
					Logger.Exception(new Exception(Request.Url.ToString()), "SberbankCallback");
				}
			}
			return null;
		}

		private void UpdateOrderPrice(string mdOrder, Order order) {
			try {
				var amount = SberbankService.GetAmount(mdOrder, order);
				if (amount > 0) {
					order.TotalPriceWithDescount = amount;
				}
			}
			catch (Exception e) {
				Logger.Exception(e, "SberbankCallback");
			}
		}

		public ActionResult YandexMarket() {
			var xml = MethodBase.GetCurrentMethod().CacheDay(() => 
				YmlGenerator.Get(Url).ToString());  

			return new XmlResult(xml);
        }

        public ActionResult GoogleMerchant() {
			var xml = MethodBase.GetCurrentMethod().CacheDay(() => 
				GoogleMerchantGenerator.Get(Url).ToString());  

			return new XmlResult(xml);
        }

        public ActionResult UchebaRu() {
			var xml = MethodBase.GetCurrentMethod().CacheInFileDay(() => 
				UchebaRuGenerator.Get(Url).ToString());  
			return new XmlResult(xml);
        }

        public ActionResult TeachMePlease() {
//			return new XmlResult(TeachMePleaseGenerator.Get(Url).ToString());
			var data = MethodBase.GetCurrentMethod().CacheInFileDay(() => 
				TeachMePleaseGenerator.Get(Url).ToString());  
			return new XmlResult(data);
        }


        public ActionResult X5RetailGroup(string id) {
//			return new XmlResult(X5RetailGenerator.Get(Url).ToString());
	        if (_.HashSet("csv", "xml", "xlsx").Contains(id)) {
		        var key = "PartnerController-X5RetailGroup" + id;
		        var fileName = "courses_{0}.".FormatWith(DateTime.Today.DefaultString());

		        switch (id) {
			        case "xml":
				        return new XmlResult(CacheUtils.Get(key, () => X5RetailGenerator.Get(Url)));
			        case "csv":
				        return File(Encoding.GetEncoding(1251).GetBytes(CacheUtils.Get(key, () => X5RetailGenerator.Csv(Url))),
					        "text/csv", fileName + "csv");
			        case "xlsx":
				        return File(Convert.FromBase64String(CacheUtils.Get(key, () => X5RetailGenerator.Xlsx(Url))),
					        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + "xlsx");
		        }

	        }
	        return NotFound();
        }

        public ActionResult GoogleRemarketing() {
			var xml = MethodBase.GetCurrentMethod().CacheDay(() => 
				GooreGoogleRemarketingGenerator.Get(Url));  

			return Content(xml);
        }
        public ActionResult SverhMarket() {
			var xml = MethodBase.GetCurrentMethod().CacheDay(() => 
				SverhMarketGenerator.Get(Url).ToString());  
			return new XmlResult(xml);
        }

		[OutputCache(Duration = 60 * 60, VaryByParam = "categoryId")]
		public ActionResult SuperJob(int categoryId) {
			var professions = SuperJobService.GetProfessions(categoryId);
			if(professions.IsEmpty())
				return null;
			var courseTCs = CourseService.GetCourseTCListFor(typeof (Profession),
				professions.Cast<object>()).Random(5);
			var courses = CourseService.GetCourseLinkList(courseTCs)
				.Where(c => c.IsTrack == false).ToList();
			if(!courses.Any())
				return null;
			var links = H.l(H.div["Рекомендуемыe курсы"],
				H.ul[
				courses.Select(c => H.li[
					H.Anchor(CommonConst.SiteRoot + "/course/" + c.UrlName, c.GetShortName())
					.FluentUpdate(a => a.SetAttributeValue("target", "_blank"))
					])]); 
			return Content(links.ToString());
		}


		[OutputCache(Duration = 8 * 60 * 60, VaryByParam = "none")]
		public ActionResult Dwg() {
			var model = new DwgVM();
			model.Courses = DwgVM.CourseTCs.Select(c => Tuple.New(c.V1, GetDwgCourses(c.V2))).ToList();
			return View(model);
		}

		[OutputCache(Duration = 8 * 60 * 60, VaryByParam = "None")]
		public ActionResult DwgCourses() {
            return new XmlResult(GetDwgXml());
		}

		private List<SimpleUtils.Common.Tuple<CourseLink, List<Group>>> GetDwgCourses(List<string> courseTCs) {
			var courses = CourseService.GetCourseLinkList(courseTCs).ToList()
				.OrderBy(c => courseTCs.IndexOf(c.CourseTC));
			return courses.Select(c => Tuple.New(c, GroupService.GetGroupsForCourse(c.CourseTC).Take(5).ToList()))
				.ToList();
		}

		private XElement X(string name, params object[] content) {
			return new XElement(name, content);
		}

		private XAttribute A(string name , object value) {
			return new XAttribute(name, value);
		}

		public XDocument GetDwgXml() {
			var courses = 
				GetDwgCourses(DwgVM.CourseTCs.SelectMany(x => x.V2).Distinct().ToList())
				.Where(x => x.V2.Any())
				.OrderBy(x => x.V2.First().DateBeg).Take(5);
            var yaml =
             new XDocument(
             X("courses", courses.Select(c => X("course", 
				 A("url", 
				 Html.CourseLinkAnchor(c.V1.UrlName, c.V1.GetName()).AbsoluteHref().Attribute("href").Value),
				 A("name", c.V1.GetName()),
				 A("date", c.V2.First().DateBeg.DefaultString())))) 
             );
            return yaml;
        }

	}
}