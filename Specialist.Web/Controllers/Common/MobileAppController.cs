using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using Specialist.Web.Common.Mvc.ActionResults;
using Specialist.Web.Root.Services;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.Common.Utils;

namespace Specialist.Web.Controllers.Common {
	public class MobileAppController: Controller {

		public class JsonSectionBase {
			public string type;
			public object id;
//			public string urlName;
			public int courseCount;
			public string name;
			public JsonSectionBase(string type, object id, string urlName, int courseCount, string name) {
				this.type = type;
				this.id = id;
				this.courseCount = courseCount;
//				this.urlName = urlName;
				this.name = name;
			}
		}

		public class JsonSection : JsonSectionBase {
			public JsonSection(string type, object id, string urlName, int courseCount, string name) 
				: base(type,id, urlName, courseCount, name) {
			}
		}

		public class JsonRootSection : JsonSectionBase {
			public List<JsonSection> children;
			public JsonRootSection(string type, object id, string urlName, 
				List<JsonSection> children, int courseCount, string name) : base(type, id, urlName, courseCount, name) {
				this.children = children ?? new List<JsonSection>();
			}
		}


		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService { get; set; }

		[Dependency]
		public ISiteObjectService SiteObjectService { get; set; }

		public JsonSection EntityJson(IEntityCommonInfo x) {
			var tp = x.GetType();
			var type = SiteObject.TypeTableNames[tp];
			var id = LinqToSqlUtils.GetPK(x);
			var count = Counts().GetValueOrDefault(Tuple.Create(type, id.ToString()));
			var name = GetName(type, id);
			return new JsonSection(type, id, x.UrlName, count,name);
		}

		private string GetName(string type, object id) {
			var name = SiteObjectService.GetBy(type, id).GetOrDefault(y => y.Name);
			return name;
		}


		private Dictionary<Tuple<string, string>, int> _counts;
//		private Dictionary<Tuple<string, string>, string> _names;
		public Dictionary<Tuple<string, string>, int> Counts() {
			if (_counts == null) {
				_counts = SiteObjectRelationService.GetAll(x =>
					x.ObjectType == SiteObject.TypeTableNames[typeof (Course)])
					.Where(x => x.Object.IsActive).Select(x =>
						new {x.RelationObject_ID, x.RelationObjectType}).ToList()
					.Select(x => Tuple.Create(x.RelationObjectType, x.RelationObject_ID.ToString()))
					.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
			}
			return _counts;
		}

		//		public Dictionary<Tuple<string, string>, int> Names() {
		//			if (_names == null) {
		//				_names = SiteObjectService.ge;
		//			}
		//			return _names;
		//		}


		public ActionResult Sections() {
			var r = MethodBase.GetCurrentMethod().CacheInFileDay(() => {
				var tree = SiteObjectRelationService.GetAllMenuTree();
				var withoutChildren = _.List(typeof(Course), typeof(Product), typeof(Profession), typeof(SiteTerm));
				var json = tree.Where(x => !withoutChildren.Contains(x.Key.Item1) && x.Value.Any()).Select(x => {
					var type = SiteObject.TypeTableNames[x.Key.Item1];
					var id = x.Key.Item2;
					var count = Counts().GetValueOrDefault(Tuple.Create(type, id.ToString()));
					return new JsonRootSection(type,
						id,
						"",
						x.Value.Select(y => EntityJson(y.Entity)).ToList(),
						count,
						GetName(type, id)
						);
				});
				return JsonConvert.SerializeObject(json);
			});

			return Content(r);
		}
	}
}