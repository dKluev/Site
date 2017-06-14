using System;
using System.Reflection;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Tests;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Helpers;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Core.Views {
	public class CourseSocialLinkView:BaseView<CourseSocialLinkVM> {
		public override object Get() {
			var url = Links.CourseLinkAnchor(null, Model.Course).AbsoluteHref().GetHref();
			var text =
				("Чтобы принять участие в семинаре со скидкой {0}, " +
				"поделитесь ссылкой на него с друзьями в социальных сетях")
					.FormatWith(Htmls2.DiscountText("50%"));
			return div[
				JavaScript().Src("/Scripts/Views/Course/seminarsociallink.js?v=3"),
				Div("attention")[text, br, url],
				h3["Добавьте ссылку на Вашу публикацию в социальной сети:"],
				form.Action(Url.Cart().Urls.AddCourseWithSocialLinkPost(null))[
				InputText("socialurl","").Id("social-link-control").Style("width:400px;margin-bottom:10px;"),br,
				Submit("Добавить в корзину").Id("social-submit-button").SetDisabled(true)]];
		}
	}
}