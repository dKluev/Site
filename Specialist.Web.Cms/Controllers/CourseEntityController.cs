using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Mvc;
using BLToolkit.Mapping;
using Microsoft.Practices.Unity;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Const;
using Specialist.Web.Cms.Core;
using Specialist.Services.Common.Extension;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Cms.Logic;
using Specialist.Web.Cms.Logic.Responses;
using Specialist.Web.Cms.Services;
using Specialist.Web.Cms.Util;
using Specialist.Web.Common.Html;
using Specialist.Web.Helpers;
using SimpleUtils.Common.Extensions;
using SpecialistTest.Web.Core.Mvc.Extensions;
using SimpleUtils.Collections.Extensions;


namespace Specialist.Web.Cms.Controllers
{
    public class CourseEntityController : BaseController<Course>
    {
        [Dependency]
        public IRepository<SiteObject> SORepository { get; set; }

        [Dependency]
        public IRepository2<CoursesChain> CourseChainService { get; set; }
		[Dependency]
		public ISiteObjectRelationService SORelationService { get; set; }

        [Dependency]
        public IContextProvider ContextProvider { get; set; }

        [Dependency]
        public CourseEntityService CourseEntityService { get; set; }

		[Dependency]
		public IRepository<Group> GroupService { get; set; }

		[Dependency]
		public IRepository<UserWork> UserWorkService { get; set; }
		[Dependency]
		public IRepository<Section> SectionService { get; set; }

		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService { get; set; }

		[Dependency]
		public IRepository<EntityStudySet> EntityStudySetService { get; set; }
        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Sorting")]
        public ActionResult SortingPost(string RelationObject_ID,
            string RelationObjectType)
        {
            return RedirectToAction("Sorting", new
            {
                RelationObject_ID,
                RelationObjectType
            });
        }

        public ActionResult Sorting(string RelationObject_ID,
            string RelationObjectType)
        {
            var courses = GetCourses(
                RelationObject_ID, 
                RelationObjectType);

            return View(courses);
        }

	

        public virtual ActionResult CourseNames(string q)
        {
            var names = Repository.GetAll().IsActive()
                .Where(c => (c.Course_TC + " - " + c.Name).Contains(q))
                .Select(c => new {name = c.Course_TC + " " + c.Name , id =c.Course_TC})
                .Take(10);
            return Json(names, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Course> GetCourses(string RelationObject_ID,
            string RelationObjectType) {
            IQueryable<Course> courseQuery = null;
            if(RelationObjectType == null)
            {
                courseQuery = new List<Course>().AsQueryable();//Repository.GetAll();
            }
            else
            {
                
                var relationObjectType = ContextProvider.GetTypeByTableName(
                    RelationObjectType);
               /* if (obj.RelationObject_ID is string[])
                    obj.RelationObject_ID = (obj.RelationObject_ID as string[]).First();*/
                var relationObjectID = 
                    LinqToSqlUtils.CorrectPKType(RelationObject_ID, relationObjectType);
                courseQuery = SiteObjectService
                    .GetByRelationObject<Course>(relationObjectType, relationObjectID);
                ViewData["SiteObject"] = SORepository.GetAll().First(so =>
                    so.ID.Equals(relationObjectID) 
                    && so.Type == RelationObjectType);


            }

            return courseQuery.Where(c => c.IsActive)
                .OrderBy(c => c.WebSortOrder)
                .Select(c => new { c.Course_TC, c.Name })
                .ToList()
                .Select(c => new Course
                {
                    Course_TC = c.Course_TC,
                    Name = c.Name
                });
        }

        public ActionResult UpdateSorting(List<string> changedTCList,
            List<string> allTCList)
        {
            if(changedTCList != null)
            {
                var context = new SpecialistDataContext();
                var courses = context.Courses.Where(c => c.IsActive)
                    .OrderBy(c => c.WebSortOrder)
                    .ToList();
                new CourseSorting(courses, changedTCList, allTCList).Update();
                context.SubmitChanges();
                Thread.Sleep(2000);
            }
            return Json("done");
        }

		/*[Auth(RoleList = Role.AnyContManager)]
		public ActionResult RelationSorting(string RelationObject_ID,
	 string RelationObjectType)
		{
			if (RelationObject_ID == null)
				return View(new List<Course>());
			var objId =
				LinqToSqlUtils.CorrectPKType(RelationObject_ID,  ContextProvider.GetTypeByTableName(
				  RelationObjectType));

			var relations = GetCourseRelations(RelationObjectType, (int)objId);
			var courses = GetCourses(
				RelationObject_ID,
				RelationObjectType).OrderBy(c => relations.First(r => 
					r.Object_ID.Equals(c.Course_TC)).RelationOrder);



			return View(courses);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionName("RelationSorting")]
		[Auth(RoleList = Role.AnyContManager)]
		public ActionResult RelationSortingPost(string RelationObject_ID,
			string RelationObjectType)
		{
			return RedirectToAction("RelationSorting", new
			{
				RelationObject_ID,
				RelationObjectType
			});
		}

		[Auth(RoleList = Role.AnyContManager)]
		public ActionResult UpdateRelationSorting(List<string> courseTCList,
			int? objId, string objType)
		{
			var relations = 
				GetCourseRelations(objType, objId.Value);
			for (short i = 0; i < courseTCList.Count; i++) {
				var courseTC = courseTCList[i];
				var relation = relations.FirstOrDefault(x => x.Object_ID.Equals(courseTC));
				if (relation != null)
					relation.RelationOrder = i;
			}

			SORelationService.SubmitChanges();

			return Json("ok");
		}

    	private List<SiteObjectRelation> GetCourseRelations(string objType, int objId) {
    		return SORelationService.GetByRelation(objType, objId, typeof(Course))
    			.ToList();
    	}*/

		[Dependency]
		public IResponseService ResponseService { get; set; }

		public IEnumerable<T> EnumMap<T>(IEnumerable<object> list) {
			return list.Select(x => Map.ObjectToObject<T>(x));
		} 

		public ActionResult CourseInfo() {
			var courseTCs = GroupService.GetAll().PlannedAndNotBegin().NotSpecial().NotSeminars()
				//.Where(x => x.DateBeg < DateTime.Now.AddDays(14))
				.Where(x => x.PrintAnnounce)
				.OrderBy(x => x.DateBeg)
				.Select(g => g.Course_TC)
				.ToList();

			var courseSources = Repository.GetAll(x => courseTCs.Contains(x.Course_TC))
				.Select(c => new {
					c.Course_TC,
					c.UrlName,
					c.Name,
					c.AnnounceDescription
				}).ToList().OrderBy(x => courseTCs.IndexOf(x.Course_TC));
			var courses = EnumMap<Course>(courseSources).ToList();
			var model = new JsTableVM {
				Title = "Курсы",
				Columns = {{"", null},{"Код", null}, {"Курс", null}, {"Анонс", null}, {"Отзывы", null}},
				Rows = courses.Select(x => _.List(
					(object)Images.Entity(x),
					x.Course_TC,
					Html.CourseLinkAnchor(x.UrlName, x.Name).AbsoluteHref(),
					x.AnnounceDescription,
					ResponseService.GetAllForCourse(x.Course_TC).Count()
					))
			};
			return View(ViewNames.JsTable, model);

		}

		object CourseLink(string courseTC) {
			return Url.Link<CourseEntityController>(c => c.Edit(courseTC, null),
				courseTC).As<object>();
		}

		public ActionResult CourseNewVersions() {

			var courses = CourseEntityService.GetNewVersionCourses();
			var model = new JsTableVM {
				Title = "Новые версии курсов",
				Columns = {{"Код", null}, {"Есть группы", null}, {"Старый код",null}},
				Rows = courses.Select(x => _.List(
					CourseLink(x.Item1),
					CmsHtmls.CheckIcon(x.Item2),
					CourseLink(x.Item3)
					))
			};
			return View(ViewNames.JsTable, model);

		}
		public ActionResult WithoutGuide() {
			var courseTCs = SORelationService.GetRelation(typeof(Guide),
    			Enumerable.Empty<object>(), typeof (Course))
    			.Where(x => x.Object.IsActive && x.RelationObject.IsActive)
				.Select(x => x.RelationObject_ID.ToString()).ToList();
			return CourseTable("Курсы без путеводителей", c => !courseTCs.Contains(c.Course_TC));
		}


		public ActionResult WithoutResponse() {
			var courseTCs = ResponseService.GetAll(x => x.IsActive 
				&& x.Type == RawQuestionnaireType.CourseComment)
				.GroupBy(x => x.Course_TC).Where(x => x.Any()).Select(x => x.Key).Distinct().ToList();
			var allActiveCourseTCs = Repository.GetAll(x => x.IsActive).Select(x => x.Course_TC).ToList();
			var showTCs = allActiveCourseTCs.Except(courseTCs);
			return CourseTable("Курсы без отзывов", c => showTCs.Contains(c.Course_TC));
		}

		public ActionResult WithoutTag(int count) {
			var courseTCs = SiteObjectRelationService.GetRelation(typeof(Course),
				Enumerable.Empty<object>())
				.GroupBy(x => x.Object_ID)
				 .Where(x => x.Count() == count)
				 .Select(x => x.Key.ToString()).Distinct().ToList();

			return CourseTable("Курсы без связей", 
				c => courseTCs.Contains(c.Course_TC));
		}

		public ActionResult WithoutDescription() {
			return CourseTable("Курсы без описания", c => c.Description == null);
		}

		public ActionResult WithoutChain() {
			var parentCourseTCs = CourseChainService.GetAll().Select(x => x.Course_TC)
				.Distinct().ToList();
			return CourseTable("Курсы без цепочек", c =>
				!parentCourseTCs.Contains(c.ParentCourse_TC));
		}

		private ActionResult CourseTable(string title,
			Expression<Func<Course, bool>> selector) {
			var courses = OnlyActiveCourses(Repository.GetAll(selector));
			return CourseTableByCourses(title, courses);
		}

		private static List<Course> OnlyActiveCourses(IQueryable<Course> courses) {
			var courses2 = courses.Where(c => c.IsActive &&
				c.IsTrack != true).ToList();
			return courses2;
		}

		private ActionResult CourseTableByCourses(string title, List<Course> courses) {
			var courseTCs = courses.Select(x => x.Course_TC).ToList();
			var groups =
				GroupService.GetAll().PlannedAndNotBegin()
					.Where(x => x.DateBeg < DateTime.Today.AddMonths(6)
						&& courseTCs.Contains(x.Course_TC)).GroupBy(x => x.Course_TC)
					.Select(x => new {
						x.Key, Count = x.Count()
					})
					.ToDictionary(x => x.Key, x => x.Count);
			var model = new JsTableVM {
				Title = title,
				Columns = { { "Код", null }, { "Название", null }, { "Группы", null } },
				Rows = courses.Select(x => _.List(
					CourseLink(x.Course_TC),
					Html.CourseLinkAnchor(x.UrlName, x.WebName).AbsoluteHref(),
					groups.GetValueOrDefault(x.Course_TC)
					))
			};
			return View(ViewNames.JsTable, model);
		}


		public ActionResult Update() {
			return View(ViewNames.CommonPage,
				new PageVM {
					Title = "Обновление курса",
					Html = new ResponseUpdateView().Get(Url)
				});
		}


		public ActionResult TagsReport(string id) {
			var courseTCs = StringUtils.SafeSplit(id);

			var courses = Repository.GetAll(x => courseTCs.Contains(x.Course_TC)).ToList();

			var model = new JsTableVM {
				Title = "Привязки курсов",
				Columns = {{"Код", null}, 
					{"Название", null}, 
					{"Новый", null}, 
					{"Привязки", null},
					{"Количество привязок", null}},
				Rows = courses.Select(x => {
					var courseTags = GetCourseTagLinks(x);
					return _.List(
						(object)x.Course_TC,
						Html.CourseLinkAnchor(x).AbsoluteHref(),
						x.IsNew ? "да" : "нет",
						courseTags.JoinWith("<br/>"),
						courseTags.Count);
				})
			};
			return View(ViewNames.JsTable, model);
		}

		List<string> GetCourseTagLinks(Course c) {
			var x1 = SiteObjectService.GetSingleRelation<Vendor>(c).ToList()
				.Select(x => H.Anchor(Html.GetUrlFor(x), x.Name));
			var x2 = SiteObjectService.GetSingleRelation<Section>(c).ToList()
				.Select(x => H.Anchor(Html.GetUrlFor(x), x.Name));
			var x3 = SiteObjectService.GetSingleRelation<Product>(c).ToList()
				.Select(x => H.Anchor(Html.GetUrlFor(x), x.Name));
			var x4 = SiteObjectService.GetSingleRelation<Profession>(c).ToList()
				.Select(x => H.Anchor(Html.GetUrlFor(x), x.Name));
			var x5 = SiteObjectService.GetSingleRelation<SiteTerm>(c).ToList()
				.Select(x => H.Anchor(Html.GetUrlFor(x), x.Name));
			return x1.Concat(x2).Concat(x3).Concat(x4).Concat(x5)
				.Select(x => x.AbsoluteHref().ToString()).ToList();
		}

		[HttpPost]
		public ActionResult Update(ResponseUpdateView model) {
			model.ToCourseTC = model.ToCourseTC.ToUpperInvariant();
			if (!Repository.GetAll().Any(x => x.Course_TC == model.ToCourseTC)) {
				ShowMessage("Курс с кодом {0} не существует".FormatWith(model.ToCourseTC));
				return RedirectBack();
			}
			var responses = ResponseService.GetAll(x => x.Course_TC == model.FromCourseTC).ToList();
			var responseCount = responses.Count;
			foreach (var response in responses) {
				response.Course_TC = model.ToCourseTC;
			}
			ResponseService.SubmitChanges();
			Func<string, string> comma = x => "," + x + ",";

			var workCount = UpdateUserWorks(model, comma);
			var sectionCount = UpdateSections(model, comma);
			UpdateEntityStudySets(model, comma);
			var tagCount = UpdateRelations(model);
			var courseTagCount = UpdateCourseRelations(model);
			SORelationService.SubmitChanges();

			var message = (
				"Обновлено. Отзывов {0}, работ слушателей {1}, не анонс {4}, привязки {2} и курса {3}, ")
				.FormatWith(responseCount, workCount, tagCount,courseTagCount, sectionCount);
			ShowMessage(message);
			return RedirectBack();
		}


		private int UpdateSections(ResponseUpdateView model, Func<string, string> comma) {
			var sections = SectionService.GetAll().Select(uw => new {
				courseTCs = "," + uw.NotAnnounce.Replace(" ", "") + ",",
				uw
			})
				.Where(x => x.courseTCs.Contains(comma(model.FromCourseTC))).ToList();
			foreach (var section in sections) {
				var newCourseTCs = section.courseTCs.Replace(comma(model.FromCourseTC),
					comma(model.ToCourseTC));
				section.uw.NotAnnounce = newCourseTCs.Trim(',');
			}
			SectionService.SubmitChanges();
			return sections.Count;
		}

		private int UpdateUserWorks(ResponseUpdateView model, Func<string, string> comma) {
			var userWorks = UserWorkService.GetAll().Select(uw => new {
				courseTCs = "," + uw.Course_TC.Replace(" ", "") + ",",
				uw
			})
				.Where(x => x.courseTCs.Contains(comma(model.FromCourseTC))).ToList();
			foreach (var userWork in userWorks) {
				var newCourseTCs = userWork.courseTCs.Replace(comma(model.FromCourseTC),
					comma(model.ToCourseTC));
				userWork.uw.Course_TC = newCourseTCs.Trim(',');
			}
			UserWorkService.SubmitChanges();
			return userWorks.Count;
		}

		private void UpdateEntityStudySets(ResponseUpdateView model, Func<string, string> comma) {
			var entitySets = EntityStudySetService.GetAll().Select(uw => new {
				courseTCs = "," + uw.CourseTCList.Replace(" ", "") + ",",
				uw
			})
				.Where(x => x.courseTCs.Contains(comma(model.FromCourseTC))).ToList();
			foreach (var entitySet in entitySets) {
				var newCourseTCs = entitySet.courseTCs.Replace(comma(model.FromCourseTC),
					comma(model.ToCourseTC));
				entitySet.uw.CourseTCList = newCourseTCs.Trim(',');
			}
			UserWorkService.SubmitChanges();
		}
		private int UpdateRelations(ResponseUpdateView model) {
			var courseTableName = LinqToSqlUtils.GetTableName(typeof (Course));
			var relations = SORelationService.GetAll(x => 
				x.RelationObject_ID.Equals(model.FromCourseTC) 
				&& x.RelationObjectType == courseTableName).ToList();
			foreach (var relation in relations) {
				relation.RelationObject_ID = model.ToCourseTC;
			}

			var count = relations.Count;
			return count;
		}
		private int UpdateCourseRelations(ResponseUpdateView model) {
			var courseTableName = LinqToSqlUtils.GetTableName(typeof (Course));
			var relations = SORelationService.GetAll(x => 
				x.Object_ID.Equals(model.FromCourseTC) 
				&& x.ObjectType == courseTableName).ToList();
			foreach (var relation in relations) {
				relation.Object_ID = model.ToCourseTC;
			}

			var count = relations.Count;
			return count;
		}
	}
}