using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Order.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Helpers.Shop;
using SpecialistTest.Web.Core.Mvc;
using SimpleUtils.Extension;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Context.Const;
using Specialist.Web.Common.Extension;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Entities.Context.Extension;

namespace Specialist.Web.Helpers {
	public partial class SiteHtmls: H {
		
			public TagDiv LecturerTwo(Employee t, string urlName) {
				
				return Div("lecturers_2")[
					Div("next_lecturer1")[
						Div("l")[Html.TrainerLink(t,Images.Entity(t).Width(66)),
							Html.TrainerLink(t)],
						Div("r")[
							div.Class("opinion")[
								strong.Class("text_red")["О преподавателе:"], 
								br, H.Raw(StringUtils.GetFirstParagraph(t.Description)),
								t.Description.GetOrDefault(x => 
									Html.TrainerLink(t, b["Читать далее"]))
 								],
							p.Style("margin-left: 10px;")[
							 Url.Link<CenterController>(c => 
								c.CourseTrainers(urlName), "Все преподаватели по курсу")
							]
						]]];
			}


		public TagThead TableHead(params object[] tags) {
			return thead[tr.Class("thead")[
				th.Class("table_c_tl"),
				tags.Where(x => x != null).Select(x => 
					(x is TagTh ? (TagTh)x : th[x]).Rowspan(2)),
				th.Class("table_c_tr")],
				tr[
				th.Class("table_c_bl"),
				th.Class("table_c_br")]];
		}

		public TagTr TableRow(params object[] tags) {
			return tr[td.Class("table_c_tl"),
				tags.Where(x => x != null),
				td.Class("table_c_tr")];
		}

		public TagTable FullTimePrice(CourseBaseVM model) {
			var hasWebinar = model.HasWebinar;
			var hasIntraExtra = model.HasIntraExtra;
			var grayStyle = "color:gray;font-size:12px;";
			Func<int,string> discount = x => "<br/>{0} {1}"
				.FormatWith(span["Экономия до"].Style(grayStyle), span[x + "%"].Class("discount_color"));

			Func<short?,string> discountHead = disc => (model.Course.IsTrackBool || !disc.HasValue ? "" : 
				discount(disc.Value));
			var morningHead = th["с 10:00 до 17:00" + 
				discountHead(model.MorningDiscount)].Id("morning-head");
			var eveningHead = th["Вечер или Выходные" + br + 
				span["Стандартная цена"].Style(grayStyle) ];
			var webinarHead = th[SimpleLinks.Webinar("Вебинар") + 
				discountHead(model.WebinarFinalDiscount)].Id("webinar-head");
			var unlimitHead = th[SimpleLinks.Unlimited()];
			var intraExtraHead = th["Очно-заочно"];
			return table.Class("table")[
				TableHead(" ",
				morningHead, 
				eveningHead,
					hasIntraExtra ? intraExtraHead : null,
					hasWebinar ? webinarHead : null,
					model.UnlimitPrice.HasValue ? unlimitHead : null,
					"Записаться"),
				tbody[
					GetPriceRows(model, hasWebinar, hasIntraExtra, model.MorningDiscount)]
				];
		}

		private TagList GetPriceRows(CourseBaseVM model, 
			bool hasWebinar, bool hasIntraExtra, short? monDiscount) {
			var unlimitPrice = model.UnlimitPrice;
			var price = model.GetPrice(PriceTypes.PrivatePersonWeekend);
			var orgPrice = model.GetPrice(PriceTypes.Corporate);
			var intraExtraPrice = model.GetPrice(PriceTypes.IntraExtra);
			var intraExtraOrgPrice = model.GetPrice(PriceTypes.IntraExtraOrg);
			var orgTitle = "Организации";
			if (CourseTC.MsVoucher.Contains(model.Course.Course_TC)) {
				orgTitle += br.ToString() + span["(Данный курс можно",br,"оплатить ваучерами ",br,
					Anchor("/news/2297", "Software Assurance"), ")"]
						.Style("font-size:10px;color:black;font-weight:normal;");
			}
			var ppTitle = "Частные лица";
			if (CourseTC.CiscoPrepay.Contains(model.Course.Course_TC)) {
				ppTitle += br.ToString() + span["По предоплате"]
						.Style("font-size:10px;color:grey;");
			}

			var webinarPrice = model.GetPrice(PriceTypes.Webinar);
			Func<decimal?, string> getFirstPayment = x => x.HasValue 
				? (OrderDetail.FloorToFifty(x.Value*CommonConst.FirstPaymentPercent)).MoneyString()
			    : null;
			var firstPayment = model.Course.IsTrackBool ?
				TableRow(td.Class("td_entrant")[Anchor(SimplePages.FullUrls.FirstPaymentNews,
					"Первый взнос (для частных лиц)").Style("color:red;")],
					td[getFirstPayment(price)],
					td[getFirstPayment(price)],
					hasIntraExtra ? td[getFirstPayment(intraExtraPrice)] : null,
					hasWebinar ? td[getFirstPayment(webinarPrice)] : null,
					unlimitPrice.HasValue ? td : null,
					td.Class("last_td")).Style("color:red;") : null;
			return
				l(
				TableRow(td.Class("td_entrant")[ppTitle],
				td[Htmls2.DiscountPrice(monDiscount, price)],
					td[price.MoneyString()],
				hasIntraExtra ? td[intraExtraPrice.MoneyString()] : null,
					hasWebinar ? td[GetWebinarPriceBlock(model)] : null,
					unlimitPrice.HasValue ? td[
					SimpleLinks.Unlimited(unlimitPrice > 0 ? unlimitPrice.MoneyString() : "Бесплатно")] : null, 
					td.Class("last_td")[
						Html.AddToCart(model.Course, priceTypeTC: PriceTypes.PrivatePersonWeekend)]),
				TableRow(td.Class("td_entrant")[orgTitle],
				td[orgPrice.MoneyString()],
					td[orgPrice.MoneyString()],
					hasIntraExtra ? td[intraExtraOrgPrice.MoneyString()] : null,
					hasWebinar ? td[model.GetPrice(PriceTypes.WebinarOrg).MoneyString()] : null,

					unlimitPrice.HasValue ? td[""] : null, 
					td.Class("last_td")[
						Html.AddToCart(model.Course, priceTypeTC: PriceTypes.Corporate)]),
				firstPayment
				);
		}

		private static string GetWebinarPriceBlock(CourseBaseVM model) {

			var price = model.GetPrice(PriceTypes.Webinar);
			if (model.WebinarFinalDiscount > 0) {
				var postfix = model.WebinarDiscount > 0
					? "<br/>скидка {0}% {1}".FormatWith(model.WebinarFinalDiscount,
						DateTime.Today <= new DateTime(2015, 12, 31) ? "до 31 декабря" : "")
					: "";
				return Htmls2.DiscountPrice(model.WebinarFinalDiscount, price) + postfix;
			}
			return price.MoneyString();
		}


		public TagTable DistancePrice(CourseBaseVM Model) {

			var heads = Model.DistanceTypes.Select(x =>
				PriceTypes.GetFullName(x.PriceType_TC)).ToList()
				.AddFluent("Записаться").Cast<object>().ToArray();

			var values = Model.DistanceTypes.Select(x =>
				td[Model.GetPrice(x.PriceType_TC).MoneyString()]).ToList()
				.AddFluent(
				td.Class("last_td")[Html.AddToCart(Model.Course, priceTypeTC:
						Model.DistanceTypes.FirstOrDefault()
						.GetOrDefault(y => y.PriceType_TC))]).Cast<object>().ToArray();

			return table.Class("table")[
				TableHead(heads),
				tbody[
					TableRow(values)
					]
				];
		}

		public object AddCourseButton(CourseBaseVM model) {
			return model.HasPPPrice ? Html.AddToCart(x => x.AddCourse(model.Course.Course_TC, null), true) : null;
		}

	}
}