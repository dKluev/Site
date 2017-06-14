using System.Collections.Generic;
using System.Web.Caching;
using SimpleUtils.Common;
using Specialist.Entities.Context;
using Specialist.Web.Common.Utils;
using Specialist.Web.Common.Utils.Files;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using System.Linq;
using Specialist.Services;
using Specialist.Services.Catalog;
using Specialist.Services.Common;
using Specialist.Web.Root.SimpleTests.Logic;

namespace Specialist.Web.Root.Profile.Views {
	public class ClearCacheView:BaseView<Cache> {

		static Dictionary<string,string> _names = new Dictionary<string, string>();
 		static ClearCacheView() {
			_names.Add(CacheUtils.GetCacheKey<MainPageService>(x => x.Get()),"Данные на главной");
 			_names.Add("MainCoursesSections","Курсы закладки");
 			_names.Add("PageMetaServiceGetAllPageMetas","Мета данные страниц");
 			_names.Add("RootSectionsForMain","Левая колонка на главной");

 			_names.Add("SiteObjectRelationServiceGetAllMenuTree","Меню на страницах направлений и вендоров");

 			_names.Add("CourseServiceAllCoursesForList","Курсы во всех списках");
 			_names.Add("CourseServiceSectionCourses","Привязки курсов к направлениям");
 			_names.Add(CacheUtils.GetCacheKey<CourseService>(x => x.GetActiveTrackCourses()),
				"Курсы в треках");

 			_names.Add("CourseVMServiceGetMainCourses","Данные для закладок на странице курсы");

 			_names.Add("GroupServiceGetPlannedAndNotBegin","Все группы на сайте");
 			_names.Add("GroupServiceGetGroupsForCourses","Группы курсов");

 			_names.Add("PriceServiceGetAllCurrent","Все цены на сайте");
 			_names.Add("CityServiceGetAll","Города");
 			_names.Add("HtmlsAllHtmlBlocks","Все Html блоки");
			_names.Add(CacheUtils.GetCacheKey<MailTemplateService>(x => x.List()),"Все шаблоны писем");
			_names.Add(CacheUtils.GetCacheKey<object>(x => Urls.GetCertEmployee()),"Сертификаты преподавателей");
			_names.Add(CacheUtils.GetCacheKey<ImageMetas>(x => x.Descs()),"Описания картинок");
			_names.Add(CacheUtils.GetCacheKey<SimpleTestParser>(x => x.All()),"Все психологические тесты");
			_names.Add(CacheUtils.GetCacheKey<PriceService>(x => x.WebinarDiscouns()),"Скидки вебинаров");
			_names.Add(CacheManager.Announce,"Анонсы групп");
 		}

		public override object Get() {
			var keys = new List<string>();
            var enumerator = Model.GetEnumerator();

            while (enumerator.MoveNext()) {
                keys.Add(enumerator.Key.ToString());
            }
			keys = keys.Where(x => _names.Keys.Contains(x)).ToList();
			keys.Add(CacheManager.Announce);

			return Ul(keys.Select(x =>
				AjaxForm(Url.Action<ProfileController>(z => z.ClearCache(x)))[
				H.Hidden("key", x),
					SaveButton(_names[x])]));
		}
	}
}