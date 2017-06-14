using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Core;
using Specialist.Web.Core.Logic;
using Specialist.Web.Pages;
using Specialist.Web.Root.Learning.Services;
using Specialist.Web.Root.Profile.ViewModels;
using Specialist.Web.Root.Profile.Views;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Collections.Paging;
using Specialist.Services.Catalog.Extension;

namespace Specialist.Web.Controllers {
	public class OrgProfileController:ViewController {

		[Dependency]
		public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

		[Dependency]
		public IRepository2<CompanyFile> CompanyFileService { get; set; }

		[Dependency]
		public IRepository2<User> UserService { get; set; }

		[Dependency]
		public IRepository2<Company> CompanyService { get; set; }

		[Dependency]
		public GroupCertService GroupCertService { get; set; }

		[Dependency]
		public IRepository2<SpecOrg> SpecOrgService { get; set; }

		[Dependency]
		public IRepository2<StudentInGroupLecture> StudentInGroupLectureService { get; set; }

		[Dependency]
		public IRepository2<Group> GroupService { get; set; }

		[Dependency]
		public IRepository2<Questionnaire> QuestionnaireService { get; set; }

		[Dependency]
		public IRepository2<Student> StudentService { get; set; }

		[Dependency]
		public ICourseVMService CourseVmService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }


		public ActionResult Student(decimal id) {
			var student = StudentService.GetByPK(id);
			var groups = StudentInGroupService.GetAll(x => x.Student_ID == id
				&& x.Org_ID == User.Org_ID).Select(x => x.Group).ToList();
			SetCourses(groups);
			var model = new OrgStudentVM {
				Student = student,
				Groups = groups
			};

			return BaseViewWithModel(new InlineBaseView<OrgStudentVM>(x => 
				new OrgProfileViews(Url).OrgGroups(x.Model.Groups)) , model);
		}

		private void SetCourses(List<Group> groups) {
			foreach (var g in groups) {
				g.Course = CourseService.GetAllCourseNames()
					.GetValueOrDefault(g.Course_TC);
			}
			groups.RemoveAll(x => x.Course == null);
		}

		public ActionResult Group(decimal id) {
			GroupService.LoadWith(x => x.Course);
			var group = GroupService.GetByPK(id);
			var students = StudentInGroupService.GetAll(x => x.Group_ID == id
				&& x.Org_ID == User.Org_ID).Select(x => x.Student).ToList();

			var model = new OrgGroupVM {
				Students = students,
				Group = group
			};
			return BaseViewWithModel(new OrgGroupView(), model);
		}

		public ActionResult GroupSearch(bool today) {
			return BaseViewWithModel(new OrgGroupSearchView(), new OrgGroupSearchVM {
				Current = today,
				AutoSearch = today
			});
		}


		 
		public ActionResult GroupSearchPost(OrgGroupSearchVM model) {
			var sigs = GetOrgSig();
			if(model.StudentId.HasValue)
				sigs = sigs.Where(x => x.Student_ID == model.StudentId.Value);
			var groups = sigs.Select(x => x.Group);
			if(!model.CourseTC.IsEmpty())
				groups = groups.Where(x => x.Course_TC == model.CourseTC);
			if (model.Current) {
				groups = groups.Where(x => x.DateBeg < DateTime.Today && x.DateEnd > DateTime.Today);
			}
			var result = new PagedList<Group>(
				groups.ByDateBegin(model.LeftDataBeg, model.RightDataBeg)
				.Distinct().OrderByDescending(x => x.DateBeg),model.PageIndex,20);
			model.Groups = result;
			SetCourses(result);
			var content = new OrgGroupSearchListView().Init(model,Url).Get().ToString();
			return BaseView(new PagePart(content));
		}


		[AjaxOnly]
		public ActionResult GetStudentsAuto(string term) {
			term = term ?? string.Empty;
			var list = GetOrgSig()
				.Select(x => x.Student).Distinct().Select(x =>  new {Id = x.Student_ID, 
					Name = x.LastName + " " + x.FirstName + " " + x.MiddleName})
					
					.Where(x => x.Name.Contains(term)).Take(20)
				.Select(x => Select2VM.Item(x.Id,x.Name)).ToList();
			return Json(new Select2VM(list), JsonRequestBehavior.AllowGet);
		}

		private IQueryable<StudentInGroup> GetOrgSig() {
			return StudentInGroupService.GetAll(x => x.Org_ID == User.Org_ID 
				&& BerthTypes.AllPaid.Contains(x.BerthType_TC)).OrderByDescending(x => x.StudentInGroup_ID);
		}

		[AjaxOnly]
		public ActionResult GetCoursesAuto(string term) {
			term = term ?? string.Empty;
			var list = GetOrgSig()
				.Select(x => x.Group.Course).Distinct().Select(x =>  new {Id = x.Course_TC, 
					Name = x.WebName})
					
					.Where(x => x.Name.Contains(term)).Take(20)
				.Select(x => Select2VM.Item(x.Id,x.Name)).ToList();
			return Json(new Select2VM(list), JsonRequestBehavior.AllowGet);
		}

		public ActionResult StatusUpdate() {
			return BaseViewWithModel(new OrgStatusUpdateView(), new OrgStatusUpdateVM());
		} 

		[Authorize]
		public ActionResult StatusUpdatePost(OrgStatusUpdateVM model) {
			if(!User.IsCompany) {
				ModelState.AddModelError("", "Вы должны быть зарегистрированы как компания");
				return ErrorJson();
			}
			var org = SpecOrgService.FirstOrDefault(x => x.WebKeyword == model.Code);
			if(org == null) {
				ModelState.AddModelError("", "Код не существует");
				return ErrorJson();
			}
			UserService.EnableTracking();
			var user = UserService.GetByPK(User.UserID);
			user.Org_ID = org.Org_ID;
			UserService.SubmitChanges();
			AuthService.RefreshUser();
			return OkJson();

		}

		public ActionResult NextCourseOrder(decimal studentId, decimal groupId) {
			var parentCourseTC = GroupService.GetValues(groupId, x =>
				x.Course.ParentCourse_TC);
			var courseTCs = CourseService.GetNextCourseTCs(_.List(parentCourseTC));
			var courses = CourseService.GetByPK(courseTCs).ToList();

			var model = new OrgNextCoursesVM {
				Courses = courses
			};
			return BaseViewWithModel(new OrgNextCoursesView(), model);
		}

		public ActionResult Questionnaire(decimal studentId, decimal groupId) {
			var sigId = GetSigId(studentId, groupId);

			var q = QuestionnaireService.FirstOrDefault(x => x.StudentInGroup_ID == sigId);
			var model = new OrgQuestionnaireVM {Questionnaire = q};

			return BaseViewWithModel(new OrgQuestionnaireView(), model);
		}

		private decimal GetSigId(decimal studentId, decimal groupId) {
			return StudentInGroupService.GetAll(x =>
				x.Student_ID == studentId && x.Group_ID == groupId && x.Org_ID == User.Org_ID)
				.Select(x => x.StudentInGroup_ID).FirstOrDefault();
		}

		public ActionResult Attendance(decimal studentId, decimal groupId) {
			var sigId = GetSigId(studentId, groupId);
			var lectures = StudentInGroupLectureService.GetAll(x =>
				x.StudentInGroup_ID == sigId).ToList();
			GroupCertService.CreateOrExistsEng(sigId, null, false);
			var model = new GroupAttendanceVM {
				GroupId = groupId,
				Lectures = lectures,
				SigId = sigId,
				HasGroupCert = GroupCertService.IsCertExist(sigId,true)
			};
			return BaseViewWithModel(new GroupAttendanceView(), model);
		}

		public ActionResult RealSpecialist() {
			StudentInGroupService.LoadWith(x => x.Load(z => z.Student)
				.And<Student>(z => z.StudentCalc).And<StudentCalc>(z => z.StudentClabCard));
			var students = GetOrgSig().Where(x => x.Student.StudentCalc.ВestClabCard_ID.HasValue)
				.Select(x => x.Student).Distinct().ToList();
			var model = new OrgRealSpecialistVM {
				Students = students.OrderBy(x => x.LastName).ToList()
			};
			return BaseViewWithModel(new OrgRealSpecialistView(), model);
		}

		[Auth(RoleList = Role.Employee)]
		public ActionResult Files(int id) {
			var files = CompanyFileService.GetAll(x => x.CompanyID == id).ToList();
			var name = CompanyService.GetValues(id, x => x.CompanyName);
			var model = new OrgFileListVM {
				CompanyFiles = files,
				CompanyName = name
			};
			return BaseViewWithModel(new OrgFileListView(), model);
		}

		[Auth(RoleList = Role.Employee)]
		public ActionResult List() {
			var companies = UserService.GetAll(x => x.Org_ID.HasValue).Select(x => x.Company)
				.Where(x => x != null).ToList();
			var model = new OrgListVM {
				Companies = companies
			};
			return BaseViewWithModel(new OrgListView(), model);
			
		}

		public ActionResult DownloadOrders() {
			var data = GetOrgSig().Where(x => x.Charge > 0).Select(x => new {
				x.Group.DateBeg,
				x.Group.DateEnd,
				x.Student.FirstName,
				x.Student.LastName,
				x.Student.MiddleName,
				x.Group.Course.WebName,
				x.Charge
			}).ToList().OrderByDescending(x => x.DateBeg).Select(x => _.List(
				x.DateBeg.DefaultString(),
				x.DateEnd.DefaultString(),
				x.LastName,
				x.FirstName,
				x.MiddleName,
				x.WebName,
				((int)x.Charge).ToString()
				)).ToList();

			data.Insert(0, _.List("Дата начала", "Дата окончания", "Фамилия", "Имя","Отчество","Курс","Стоимость"));
			return File(StringUtils.Encoding1251.GetBytes(CsvUtil.Render(data)), 
				"text/csv", "orders.csv");
		}
	}
}