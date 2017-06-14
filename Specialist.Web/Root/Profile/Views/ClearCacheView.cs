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
			_names.Add(CacheUtils.GetCacheKey<MainPageService>(x => x.Get()),"������ �� �������");
 			_names.Add("MainCoursesSections","����� ��������");
 			_names.Add("PageMetaServiceGetAllPageMetas","���� ������ �������");
 			_names.Add("RootSectionsForMain","����� ������� �� �������");

 			_names.Add("SiteObjectRelationServiceGetAllMenuTree","���� �� ��������� ����������� � ��������");

 			_names.Add("CourseServiceAllCoursesForList","����� �� ���� �������");
 			_names.Add("CourseServiceSectionCourses","�������� ������ � ������������");
 			_names.Add(CacheUtils.GetCacheKey<CourseService>(x => x.GetActiveTrackCourses()),
				"����� � ������");

 			_names.Add("CourseVMServiceGetMainCourses","������ ��� �������� �� �������� �����");

 			_names.Add("GroupServiceGetPlannedAndNotBegin","��� ������ �� �����");
 			_names.Add("GroupServiceGetGroupsForCourses","������ ������");

 			_names.Add("PriceServiceGetAllCurrent","��� ���� �� �����");
 			_names.Add("CityServiceGetAll","������");
 			_names.Add("HtmlsAllHtmlBlocks","��� Html �����");
			_names.Add(CacheUtils.GetCacheKey<MailTemplateService>(x => x.List()),"��� ������� �����");
			_names.Add(CacheUtils.GetCacheKey<object>(x => Urls.GetCertEmployee()),"����������� ��������������");
			_names.Add(CacheUtils.GetCacheKey<ImageMetas>(x => x.Descs()),"�������� ��������");
			_names.Add(CacheUtils.GetCacheKey<SimpleTestParser>(x => x.All()),"��� ��������������� �����");
			_names.Add(CacheUtils.GetCacheKey<PriceService>(x => x.WebinarDiscouns()),"������ ���������");
			_names.Add(CacheManager.Announce,"������ �����");
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