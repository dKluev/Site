using System.Security.Policy;
using System.Web.Mvc;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Controllers.Tests;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Const;
using Htmls = SimpleUtils.FluentHtml.Tags.Htmls;
using Specialist.Entities.Catalog.Links.Interfaces;

namespace Specialist.Web.Helpers {
	public static class Links2 {
		public static TagA TestSectionLink(this UrlHelper helper, Section section) {
			return helper.Test().Section(section.UrlName,section.Name);
		}
		public static TagA SectionLink(this UrlHelper helper, Section section) {
			return helper.Section().Details(section.UrlName,section.Name);
		}
		public static TagA CourseGroupLink(this UrlHelper helper, Group @group, 
			object content) {
			return helper.Link<CourseController>(c => c.Group(@group.Group_ID), content);
		}

		public static TagA CoursesLink(this UrlHelper helper, object content = null) {
			return helper.Link<CourseController>(c => c.MainCourses(), content ?? "Курсы");
		}


		public static TagA CourseLink(this UrlHelper helper, Course course, object content = null) {
			if(course == null)
				return new NullTagA();
			if(course.IsTrackBool)
				return helper.Track().Details(course.UrlName,content ?? course.GetName());
			return helper.Course().Details(course.UrlName,content ?? course.GetName());
		}
		public static TagA VideoLink(this UrlHelper helper, Video video) {
			return helper.Center().Video(video.VideoID,
				Linguistics.UrlTranslite(video.Name),video.Name);
		}

		public static TagA NewsListLink(this UrlHelper helper) {
			return helper.SiteNews().List(NewsType.Main, 1, "Новости");
		}

		public static TagA ActionsLink(this UrlHelper helper) {
			return helper.Link<CenterController>(c => c.Actions(), "Акции");
		}

		public static TagA NewsAndActionsLink(this UrlHelper helper) {
			return helper.Link<CenterController>(c => c.NewsAndActions(), "Новости и акции");
		}
		public static TagA Videos(this UrlHelper helper, string content = "Специалист-ТВ") {
			return H.Anchor(SimplePages.FullUrls.SpecialistTV, content);
		}
		public static TagA TestLink(this UrlHelper helper, Test test, object content = null) {
			if(test == null)
				return new NullTagA();
			return helper.Test().Details(test.Id, content ?? test.Name);
		}

		public static object TestActiveOnlyLink(this UrlHelper helper, Test test) {
			if(test.Status== TestStatus.Active)
				return helper.TestLink(test);
			return test.Name;
		}

		public static TagA TestCertificates(this UrlHelper helper) {
			return helper.Order().TestCertificates("Заказы сертификатов");
		}
		public static TagA UserTests(this UrlHelper helper) {
			return helper.Profile().Tests(1, "Мои тесты");
		}

		public static TagA GroupTests(this UrlHelper helper) {
			return helper.GroupTest().List("Результаты тестирований");
		}

		public static TagA AboutCenter(this UrlHelper helper, object content = null) {
			return helper.Center().About(content ?? "О Центре");
		}

		public static TagA Complexes(this UrlHelper helper) {
			return helper.Locations().Complexes("Учебные комплексы");
		}

		public static TagA RealSpecialist(this UrlHelper helper) {
			return helper.Profile().RealSpecialist("Настоящий специалист");
		}
		public static TagA Tests(this UrlHelper helper, string title = "Тесты") {
			return H.Anchor(SimplePages.FullUrls.OnlineTesting, title);
		}



		public static TagA UserTestLink(this UrlHelper helper, UserTest userTest, string content = null) {
			content = content ?? userTest.Test.Name;
			return helper.TestRun().Result(userTest.Id, content);
		}
		public static TagA OrgStudentLink(this UrlHelper helper, Student student) {
			return helper.Link<OrgProfileController>(c => c.Student(student.Student_ID),
				student.FullName);
		}
		public static TagA OrgGroupLink(this UrlHelper helper, Group group, 
			object content = null) {
			return helper.Link<OrgProfileController>(c => c.Group(group.Group_ID),
				content ?? group.GroupNumberTitle);
		}
		public static TagA SendManager(this UrlHelper helper, string employeeTC, string title = "Отправить сообщение") {
			return helper.Link<PageController>(c =>
				c.SendForManager(employeeTC), title);
		}

		public static TagA GroupAttendanceLink(this UrlHelper helper, decimal studentId , 
			decimal groupId) {
			return helper.Link<OrgProfileController>(c => c.Attendance(studentId, groupId),
				"Посещаемость").OpenInUiDialog();
		}

	}
}