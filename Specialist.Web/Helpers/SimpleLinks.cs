using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Entities.Filter;
using Specialist.Entities.Profile.Const;
using Specialist.Services.Core;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using System.Linq;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Controllers.Message;
using SpecialistTest.Web.Core.Mvc;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Group=Specialist.Entities.Context.Group;
using SimpleUtils;
using StringExtension=SimpleUtils.Common.Extensions.StringExtension;
using SimpleUtils.Reflection;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Helpers
{
    public static class SimpleLinks
    {
		public static TagList NewsSubscribeSimple(this UrlHelper Url) {
			return H.l(Images.Main("subscr.jpg"),
				Url.Link<ProfileController>(c => c.Subscribes(), "Подписаться на новости").Class("block1"));
		}

		public static TagTable NewsSubscribeTable(this UrlHelper Url) {
			return H.table[H.tr[H.td[Images.Main("subscr.jpg")],H.td[
				Url.Link<ProfileController>(c => c.Subscribes(), "Подписаться на новости")]]];
		}





        public static string Seminars(this HtmlHelper helper) {
            return helper.ActionLink<CourseController>(c => c.Seminars(),
                "Семинары центра").ToString();
        }

		public static TagA News(this HtmlHelper helper, string title = "Новости") {
			return helper.Url().Link<SiteNewsController>(c => c.List(NewsType.Main, null), title );
		}
		public static TagA Advices(this HtmlHelper helper, string title = "Советы") {
			return helper.Url().Link<CenterController>(c => c.Advices(1), title);
		}
        public static string Consultations(this HtmlHelper helper) {
            return helper.ActionLink<CourseController>(c => c.Consultations(),
                "Консультации").ToString();
        }

        public static string Profile(this HtmlHelper helper) {
            return helper.ActionLink<ProfileController>(c => c.Details(),
                "Профиль").ToString();
        }

        public static string Forum(this HtmlHelper helper) {
            return helper.ActionLink<MessageController>(c => c.Forum(), "Форум").ToString();
        }

		public static TagA BestGraduate(object content) {
			return H.Anchor(SimplePages.FullUrls.BestGraduate, content)
				.Title("Лучший выпускник");
		}

		public static TagA ExcelMaster(object content) {
			return H.Anchor(SimplePages.FullUrls.ExcelMaster, content)
				.Title("Магистр Excel");
		}

		public static TagA RealSpecialist(object content) {
			return H.Anchor(MainMenu.Urls.RealSpecialist, content)
				.Title("Настоящий Специалист");
		}
        public static string TrainerGroups(this HtmlHelper helper) {
            return helper.Url().Lms().Groups("Группы преподавателя").ToString();
        }

        public static string TrainerCourses(this HtmlHelper helper) {
            return helper.Url().Lms().Courses("Курсы преподавателя").ToString();
        }

		public static TagA Card(this HtmlHelper url) {
			return url.Url().Cart().Details("Корзина");
		}

		public static TagA Responses(string content = "Отзывы")
		{
			return H.Anchor(SimplePages.FullUrls.Responses, content);
		}

		public static TagA GroupDiscounts(string content = null) {
			content = content ?? "Все скидки " + CommonTexts.OnDay;
			return H.Anchor(SimplePages.FullUrls.GroupDiscounts, content);
		}

		public static TagA Webinar(string text = "Вебинары")
		{
			return H.Anchor(SimplePages.FullUrls.Webinar, text);
		}
		public static TagA Unlimited(string text = "Безлимитное обучение")
		{
			return H.Anchor(SimplePages.FullUrls.Unlimited, text);
		}
		public static TagA IntraExtra(string text = "Очно-заочное обучение")
		{
			return H.Anchor(SimplePages.FullUrls.IntraExtramural, text);
		}
		public static TagA OpenClasses(string text = "Открытое обучение")
		{
			return H.Anchor(SimplePages.FullUrls.OpenClasses, text);
		}

		public static TagA Testing()
		{
			return H.Anchor(SimplePages.FullUrls.Testing, "Тестирование");
		}

		public static TagA AllDocuments(string content = "Все документы Центра")
		{
			return H.Anchor(SimplePages.FullUrls.DocumentsOff, content);
		}

		public static string PollOption(PollOption pollOption) {
			if(pollOption.Url.IsEmpty())
				return pollOption.Text;
			return H.Anchor(pollOption.Url, pollOption.Text).ToString();
		}

		public static TagA GroupPhotos(int userId) {
			return H.Anchor("/profile/groupphotos/" + userId, "Посмотреть фотографию");
		}
    }
}