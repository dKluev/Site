using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using System.Web.Mvc;
using Bing;
using FluentValidation.Internal;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Lms;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Secondary;
using Specialist.Entities.Utils;
using Specialist.Services.Center.ViewModel;
using Specialist.Services.Common.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Lms;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Security;
using Specialist.Web.Pages;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Lms.Const;
using Specialist.Entities.Tests;
using Specialist.Services.Interface;
using Specialist.Services.Tests;
using Specialist.Web.Common.Utils;
using Specialist.Web.Helpers;
using Specialist.Web.Root.Common.Services;
using Specialist.Web.Root.Lms;
using Specialist.Web.Root.Lms.Views;
using Specialist.Web.Root.Profile.Services;
using Specialist.Web.Util;
using SpecialistTest.Web.Core.Mvc.Extensions;
using SubSonic.Extensions;
using Htmls = Specialist.Web.Common.Html.Htmls;


namespace Specialist.Web.Controllers.Common {
	using bh = BootHtmls;
    [Auth(RoleList = Role.Trainer)]
	public class LmsController:ViewController {

		protected override bool IsBootStrap {
			get { return true; }
		}

        [Dependency]
        public IEmployeeVMService EmployeeVMService { get; set; }

        [Dependency]
        public AlbumVideoService AlbumVideoService { get; set; }

        [Dependency]
        public IRepository2<EmployeesCourse> EmployeeCourseService { get; set; }

        [Dependency]
        public TestService TestService { get; set; }

        [Dependency]
        public CourseFileVMService CourseFileVMService { get; set; }

        [Dependency]
        public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

        [Dependency]
        public IRepository2<PiStudentPhoto> StudentPhotoService { get; set; }

		[Dependency]
		public UserTestService UserTestService { get; set; }

        [Dependency]
        public IRepository2<GroupSurvey> GroupSurveyService { get; set; }

        [Dependency]
        public IRepository2<EmployeesCurator> EmployeesCuratorService { get; set; }

        [Dependency]
        public IRepository2<Employee> EmployeeService { get; set; }

        [Dependency]
        public IRepository2<EmployeeContact> EmployeeContactService { get; set; }

        [Dependency]
        public IRepository2<Group> GroupService { get; set; }

		[Dependency]
		public UserTestResultService UserTestResultService { get; set; }

        [Dependency]
        public IRepository2<GroupsCalc> GroupCalcService { get; set; }

        [Dependency]
        public IRepository2<LectureFile> LectureFileService { get; set; }

        [Dependency]
        public IRepository2<Course> CourseRepository { get; set; }

        [Dependency]
        public IRepository2<RatingSubtotal> RatingSubtotalService { get; set; }

        [Dependency]
        public LectureService LectureService { get; set; }

        [Dependency]
        public PiStudentInGroupLectureService PiStudentInGroupLectureService { get; set; }

        [Dependency]
        public IRepository2<PiLecture> PiLectureService { get; set; }

        [Dependency]
        public IRepository2<PiStudentsInGroup> PiStudentsInGroupService { get; set; }

        [Dependency]
        public IRepository2<PiLectureQuestionnaire> PiLectureQuestionnaireService { get; set; }

        [Dependency]
        public IRepository2<PiGroupQuestionnaire> PiGroupQuestionnaireService { get; set; }

        [Dependency]
        public IRepository2<Questionnaire> QuestionnaireService { get; set; }

        [Dependency]
        public IRepository2<QuestionnaireTeachersMark> QuestionnaireTeachersMarkService { get; set; }

        [Dependency]
        public IRepository2<QuestionnaireClassRoomsMark> QuestionnaireClassRoomsMarkService { get; set; }


        [Dependency]
        public IRepository2<EmployeesAbsence> EmployeesAbsenceService { get; set; }

	    public ActionResult Details() {

		    var tagForm = H.Form(Url.Lms().Urls.StudentSearch(null))[
			    H.InputText("name", null).Class("form-control").Style("width:300px;").SetAttr("placeholder", "ФИО"), 
				H.button["Найти"].Class("btn btn-default")].Class("form-inline");
			LectureService.LoadWith(x => x.WebinarLicense);
		    var lecture = GetLectureOrNext(null);
		    var hasWebinar = lecture.GetOrDefault(x => x.WebinarLicense_ID.HasValue);
		    var ahkLink = hasWebinar ? H.br.ToString() + LectureEditView.WebinarBlock(lecture) : null; 
		    var menus = _.List(
				Tuple.Create(Url.Lms().Groups("Группы").ToString(),
			    "Перечень учебных групп преподавателя. Возможность перейти к анкете преподавателя по группе, открыть раскидовку группы, посмотреть результаты анкетирования выпускников."),
			    Tuple.Create(Url.Lms().TimeSheet(null, "Табель").ToString(),
				    "Табель преподавателя."),
			    Tuple.Create(Url.Lms().Lecture(null, "Занятие").ToString(),
				    "Текущее занятие преподавателя - занятие которое идет в настоящий момент. Eсли в настоящий момент нет занятия, то выводится ближайшее занятие ближайшей группы." + ahkLink),
			    Tuple.Create(Url.Lms().Courses("Курсы").ToString(),
				    "Курсы к которым допущен преподаватель"),
			    Tuple.Create(Url.Lms().Curator("Куратор").ToString(), "Ваш куратор"),
			    Tuple.Create(Url.TestEdit().List("Конструктор тестов").ToString(), "Создание тестов для сайта"),
			    Tuple.Create(Url.File().List(1,"Файлы").ToString(), "Файлы преподавателя"),
			    Tuple.Create(Url.Profile().ChangePassword("Сменить пароль").ToString(), "Смена пароля на сайте"),
			    Tuple.Create("Поиск слушателя", tagForm.ToString()));
		    var view = H.div[menus.Select(x => BootHtmls.Panel(x.Item1, x.Item2))];
			return BaseViewWithTitle("Сервисы преподавателя", new PagePart(view.ToString()));
		    
	    }

	    public ActionResult Groups() {
			var model = EmployeeVMService.GetGroups();

		    var groupSet = _.List(
			    Tuple.Create("Текущие", model.StartedGroups),
			    Tuple.Create("Предстоящие", model.Groups),
			    Tuple.Create("Прошедшие", model.EndedGroups),
			    Tuple.Create("Семинары", model.SeminarGroups)
			    ).Where(x => x.Item2.Any());
		    var view = bh.Tabs(groupSet.Select(x => Tuple.Create(x.Item1, (object) GroupListView(x.Item2))).ToList());
			return BaseViewWithTitle("Группы", new PagePart(view.ToString()));
		}

	    private TagDiv GroupListView(List<Group> groups) {
		    return bh.Table(H.Head("Курс", "Дата", "Время",
				"Комплекс","Раскидовка","Занятия","Анкеты",
				"Примечания","ЛК группы"),
			    groups.Select(x => H.Row(
				    x.Course_TC + x.SpecialPostfix, 
				    x.DateInterval, x.TimeInterval,x.Complex_TC,
				    H.td[Url.Lms().GroupStudents(x.Group_ID, bh.Icon("th-list"))].Class("td-center"),
				    H.td[Url.Lms().Group(x.Group_ID, bh.Icon("th-large"))].Class("td-center"),
				    H.td[Url.Lms().Questionnaire(x.MegaOrGroupId, bh.Icon("stats"))].Class("td-center"),
				    H.td[Url.Lms().GroupInfo(x.Group_ID, bh.Icon("list-alt"))].Class("td-center"),
				    H.td[Url.Group().Details(x.Group_ID, bh.Icon("link"))].Class("td-center"))
				    ));
	    }


	    public ActionResult Courses() {
	        var model = EmployeeVMService.GetCourses();
		    var files = CourseFileVMService.GetSpecFiles(model.CourseHasVideos.Select(x => x.Item1.CourseTC).ToList())
				.ToDictionary(x => x.CourseTC, x => x.Url);
			var view = bh.Table(H.Head("Код", "Курс", "Ftp", "Записи курса"),
				model.CourseHasVideos.Where(x => x.Item1.IsActive).Select(x => H.Row(
					x.Item1.CourseTC,
					Url.Course().Files(x.Item1.CourseTC, x.Item1.Name), 
					H.Anchor(files.GetValueOrDefault(x.Item1.CourseTC)),
                    Url.Lms().CourseVideos(x.Item1.CourseTC, "Записи")
					)));
			return BaseViewWithTitle("Курсы", new PagePart(view.ToString()));
        }

		public ActionResult GroupStudents(decimal? gId) {
			var lecture = GetLectureOrNext(null);
			if (lecture == null) {
				return LmsNotFound();
			}
			var groupId = gId.GetValueOrDefault(lecture.Group_ID);
			var group = GroupService.GetByPK(groupId);
			var studentsData = GetSigsByMegaOrGroupId(groupId)
				.Select(x => new {x.Student_ID, x.Group.Course_TC, x.Debt}).ToList();
			var studentDebt = studentsData.Where(x => x.Course_TC == group.Course_TC && x.Debt > 0)
				.Select(x => x.Student_ID).ToList();
			var studenCourses = studentsData.DistinctToDictionary(x => x.Student_ID, x => x.Course_TC);
			var studentIds = studentsData.Select(x => x.Student_ID).ToList();
			var groupSurvey = GroupSurveyService.GetAll(x => x.Group.Course_TC == group.Course_TC &&
				studentIds.Contains(x.Student_ID)).ToDictionary(x => x.Student_ID, x => x);
			var view = StudentView(studentIds, studenCourses, groupSurvey, debt: studentDebt).Item1;
			var title = _.List("Раскидовка", group.DateInterval, group.Course_TC).JoinWith(" ");
			return BaseViewWithTitle(title, new PagePart(view.ToString()));
		}

	    public ActionResult StudentSearch(string name) {
		    var studentId = StudentInGroupService.GetAll(x =>
			    ((x.Student.LastName ?? "") + " " + 
				(x.Student.FirstName ?? "") + " " + (x.Student.MiddleName ?? "")).Contains(name)
				&& x.Group.Teacher_TC == User.Employee_TC)
				.OrderByDescending(x => x.StudentInGroup_ID)
				.Select(x => x.Student_ID).FirstOrDefault();
		    return Student(studentId);
	    }


	    public ActionResult Student(decimal studentId) {
		    var notes = GetStudentNotes(studentId, null)
				.GroupByToDictionary(x => x.StudentInGroup_ID, 
				x => x);
			var view = StudentView(_.List(studentId), new Dictionary<decimal, string>(), 
				new Dictionary<decimal, GroupSurvey>(), true, notes);
		    if (!view.Item2.Any()) {
			    return BaseViewWithTitle("Слушатель не найден",
					new PagePart(bh.Info("Попробуйте изменить запрос").ToString()));
		    }
			return BaseViewWithTitle(view.Item2.First().Value.FullName, 
				new PagePart(view.Item1.ToString()));
	    }

	    private List<LmsStudentNote> GetStudentNotes(decimal? studentId, List<decimal> groupIds) {
		    var query = PiStudentInGroupLectureService.GetAll(x =>
			    x.Notes != null);
		    if (studentId.HasValue) {
			    query = query.Where(x => x.PiStudentsInGroup.Student_ID == studentId);
		    }
		    if (groupIds != null && groupIds.Any()) {
			    query = query.Where(x => groupIds.Contains(x.PiStudentsInGroup.Group_ID));
		    }
		    return query
			    .OrderByDescending(x => x.InputDate)
			    .Select(x => new LmsStudentNote {Notes = x.Notes, Student_ID = x.PiStudentsInGroup.Student_ID, LastChanger_TC = x.LastChanger_TC, LastChangeDate = x.LastChangeDate, StudentInGroup_ID = x.PiStudentsInGroup.StudentInGroup_ID})
			    .ToList();
	    }

	    private Tuple<TagDiv, Dictionary<decimal, Student>> StudentView(List<decimal> studentIds, 
			Dictionary<decimal, string> studenCourses, Dictionary<decimal, GroupSurvey> groupSurvey, 
			bool hideName = false, Dictionary<decimal, List<LmsStudentNote>> notes = null, 
			List<decimal> debt = null) {
			var isSameCourse = studenCourses.Select(x => x.Value).Distinct().Count() <= 1;
		    var sigs = StudentInGroupService.GetAll(x => studentIds.Contains(x.Student_ID)
			    && BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC)
			    && !CourseTC.AllSpecialWithoutHalf.Contains(x.Group.Course_TC))
				.Select(x => new {
					x.StudentInGroup_ID,
				    x.Group.Course_TC,
				    CourseName = x.Group.Course.Name,
				    x.Group.DateBeg,
				    x.Group.DateEnd,
					Consultant = x.Consultant.LastName + " " + x.Consultant.FirstName + " " + x.Consultant.MiddleName,
				    x.Student,
				    x.Group.Teacher.LastName,
				    x.Group.Teacher.FirstName,
				    x.Group.Teacher.MiddleName,
					OrgName = x.Org.FullName,
				    x.Group.Hours,
				    ComplexName = x.Group.Complex.Name,
				    x.BerthType_TC
			    }).OrderByDescending(x => x.DateEnd).ToList();
		    var studentSigs = sigs.GroupBy(x => x.Student.Student_ID).ToList();
		    var students = sigs.Select(x => x.Student).DistinctToDictionary(x => x.Student_ID, x => x);


		    var view = H.div[studentSigs.Select(x => new {groups = x,student = students[x.Key]})
				.OrderBy(x => StringUtils.OnlyLetters(x.student.FullName)).Select(x => {
			    var student = x.student;
			    var rows = x.groups.OrderByDescending(g => g.DateBeg).SelectMany(g => {
				    var row =_.List(H.Row(g.Course_TC,
					    g.CourseName,
					    g.DateBeg.DefaultString(),
					    g.DateEnd.DefaultString(),
					    _.List(g.LastName, g.FirstName, g.MiddleName).JoinWith(" "),
					    (int) g.Hours,
					    g.ComplexName,
					    g.BerthType_TC,
					    g.Consultant,
					    g.OrgName
					    ));
				    var note = notes != null ? notes.GetValueOrDefault(g.StudentInGroup_ID) : null;
				    if (note != null) {
					    var noteTable = bh.Collapse(_.List(
							Tuple.Create("Примечания", LmsViews.NotesView(note).As<object>())));
						row.Add(H.tr[H.td[noteTable].Colspan(10)]);
				    }
				    return row;
			    }).ToList();
				var hasDebt = debt != null && debt.Contains(student.Student_ID) 
					? H.span[" (долг) "].Class("text-danger").ToString() : null;
			    return H.div[hideName ? null : H.h3[Url.Lms().Student(student.Student_ID, student.FullName) 
					+ hasDebt + (isSameCourse ? "" : " ({0})".FormatWith(studenCourses[x.groups.Key]))],
				    groupSurvey.GetValueOrDefault(x.groups.Key).GetOrDefault(y => H.div[
					    y.Reply1.GetOrDefault(z => H.p[H.b[GroupSurvey.Replay1], H.br, z]),
						    y.Reply2.GetOrDefault(z => H.p[H.b[GroupSurvey.Replay2], H.br, z])
					    ]),
				    bh.Table(H.Head("Код", "Курс", "Начало", "Окончание", "Преподаватель", "Ак. часы",
					    "Комплекс", "Место в группе", "Менеджер", "Организация"),
					    rows)];
		    })];
		    return Tuple.Create(view, students);
	    }


	    public ActionResult TimeSheet(string d) {
			var date = d.GetOrDefault(x => (DateTime?)DateTime.Parse(x));
			date = DateUtils.FirstMonthDay(date ?? DateTime.Today);
			var weeks = DateUtils.GetMonthWeeksByTwo(date.Value);
		    var allDays = weeks.SelectMany(x => x);
		    var dateBegin = allDays.First();
			var dateEnd = allDays.Last().AddDays(1);

			var absences = EmployeesAbsenceService.GetAll(x => 
				x.Employee_TC == User.Employee_TC && x.IsRestriction
				&& ((x.DateFrom <= dateEnd && x.DateFrom >= dateBegin)  
				|| (x.DateTo <= dateEnd && x.DateTo >= dateBegin))).ToList();			
			var lectures = LectureService.GetLmsLectures(dateBegin, dateEnd, User.Employee_TC);
			var groupIds = lectures.Select(x => x.Group_ID).ToList();
			var groups = GroupCalcService.GetAll(x => groupIds.Contains(x.Group_ID)).ToList()
				.ToDictionary(x => x.Group_ID, x => x);
			var groupDates = GroupService.GetAll(x => groupIds.Contains(x.Group_ID))
				.Select(x => new {x.Group_ID,x.DateBeg,x.DateEnd}).ToList()
				.ToDictionary(x => x.Group_ID, x => Specialist.Entities.Context.Group.DateIntervalShort(
					x.DateBeg,x.DateEnd));
		    var notes = EmployeeService.GetValues(User.Employee_TC, x => x.Notes);
			var model = new TimesheetVM {
				DateBegin = date.Value,
				Lectures = lectures,
				Weeks = weeks,
				Absences = absences,
				Notes = notes,
				Groups = groups,
				DayOffList = CalendarService.DayOffList(),
				GroupDates = groupDates
			};
			return BaseViewWithModel(new TimesheetView(), model);
		}




	    List<TagTr> GetAbsence(DateTime date, List<EmployeesAbsence> list, bool end = false) {
		    var employeesAbsences = list.Where(x => end ? x.DateFrom > date : x.DateTo < date).ToList();
			employeesAbsences.ForEach((x,i) => list.Remove(x));
		    return employeesAbsences.Select(a => H.Row("{0} - {1}".FormatWith(
				DateWithDay(a.DateFrom), DateWithDay(a.DateTo)), H.td["Период вашего отсутствия"].Colspan(3)))
						.ToList();
	    }


	    private TagDiv GetLecturesView(List<LmsLecture> lectures, List<EmployeesAbsence> absences) {
		    var dateLectures = lectures.OrderBy(x => x.LectureDateBeg)
				.GroupByToDictionary(x => x.LectureDateBeg.Date, x => x);
			string ak = " а.к.";
		    var allRows = dateLectures.SelectMany(x => {
			    TagTr row;
			    if (x.Value == null) {
				    row = H.tr[H.td[DateWithDay(x.Key)], H.td, H.td, H.td];
			    } else {
				    var parts = TimesheetView.SplitByTime(x.Value);
				    row = H.tr[H.td[DateWithDay(x.Key)],
					    parts.Select((z,i) => H.td.Colspan(i == 0 ? 4 - parts.Count : 1)[z.Select(l =>  {
						    var hours = EntityUtils.GetLectureHours(l.LectureDateBeg, l.LectureDateEnd, l.Breaks);
						    return Url.Lms().Lecture(l.Lecture_ID,
							    _.List(l.LectureDateBeg.ToShortTimeString() + " - " + l.LectureDateEnd.ToShortTimeString(),
								    l.Course_TC, l.GroupDateBeg.OnlyDM(), l.ClassRoom_TC, "({0:0.##}{1})".FormatWith(hours,ak))
								    .JoinWith(" ")).Class("btn btn-default").Style("width:100%;");
					    })])];
					    
				    }
//			    var rows = GetAbsence(x.Key, absences);
//			    rows.Add(row);
			    return _.List(row.Class(x.Key == DateTime.Today ? "info" : null));
		    }).ToList();
		    if (absences.Any()) {
			    allRows.AddRange(GetAbsence(DateTime.MaxValue, absences));
		    }
		    var table = bh.Table(H.Head("Дата", "Утро", "День", "Вечер"), allRows);
		    return H.div[table];
	    }


	    private static string DateWithDay(DateTime x) {
		    var isWeekend = DateUtils.IsWeekend(x);
		    var text = x.DefaultString() + " (" + MonthUtil.DayNames[x.DayOfWeek] + ")";
		    return H.b[isWeekend ? H.span[text].Class("text-danger").ToString() : text].ToString();
	    }

	    public ActionResult Group(decimal groupId) {
		    var group = GroupService.GetByPK(groupId);
		    var finalGroupId = group.MegaOrGroupId;
			
			var view = GetLecturesView(LectureService.GetLectures(x => x.Group_ID == finalGroupId), 
				new List<EmployeesAbsence>());
			return BaseViewWithTitle("Группа {0} {1}"
				.FormatWith(group.DateInterval, group.Course_TC), 
				new PagePart(view.ToString()));
		}

	    public ActionResult GroupInfo(decimal groupId) {
		    var groupIds = GroupService.GetAll(x => x.MegaGroup_ID.GetValueOrDefault(x.Group_ID) == groupId)
			    .Select(x => x.Group_ID).ToList();
		    var notes = GetStudentNotes(null, groupIds)
				.GroupByToDictionary(x => x.Student_ID, x => x);
		    object view;
		    if (!notes.Any()) {
				view = bh.Info("Примечаний нет");
		    } else {
			var students = StudentInGroupService.GetAll(x => 
			    groupIds.Contains(x.Group_ID)).Select(x => x.Student).ToList();
		    view = students.OrderBy(x => x.FullName).Select(x => 
				new {student = x,notes = notes.GetValueOrDefault(x.Student_ID)}).Where(x => x.notes != null)
				.Select(x => H.div[
			    H.h3[x.student.FullName], LmsViews.NotesView(x.notes)]);
			    
		    }
			return BaseViewWithTitle("Примечания группы {0}".FormatWith(groupId), 
				new PagePart(H.div[view].ToString()));

	    }


		public ActionResult Lecture(decimal? lectureId) {
			try {
				LectureService.LoadWith(x => x.WebinarLicense);
				var lecture = GetLectureOrNext(lectureId);
				if (lecture == null) {
					return LmsNotFound();
				}
				if (GroupService.GetValues(lecture.Group_ID, x => x.Teacher_TC) == User.Employee_TC) {
					UpdateTrainerComingTime(lecture);
				} else {
					if (!User.InRole(Role.Admin)) {
						return Content("Информация не доступна");
					}
				}
				var groupId = lecture.Group_ID;
				var group = GroupService.GetByPK(groupId);
				var model = new LectureEditVM();
				var lecId = lecture.Lecture_ID;
				var lectureFile = LectureFileService.FirstOrDefault(x => x.Lecture_ID == lecId);
				model.WebinarExists = LectureService.GetValues(lecId, x => x.WebinarLicense_ID).HasValue;
				model.Group = group;
				model.Lecture = lecture;
				model.Rating = GetRating(group);
				model.Students = GetPiStudentInGroupLectures(lecId);
				model.LectureFile = lectureFile;
				model.LectureEditStatus = GetLectureEditStatus(lecId);
				var studentData = GetSigsByMegaOrGroupId(groupId)
					.Select(x => new {
						x.StudentInGroup_ID,
						x.Student.LastName,
						x.Student.FirstName,
						x.Student.MiddleName,
						x.Group.Course_TC,
						x.Group_ID,
						x.Student_ID,
						x.PriceType_TC,
						x.BerthType_TC,
						x.Student.StudentEmails.FirstOrDefault().Email
					}).ToList();
				var unlimitStudents = studentData.Where(x => StudentInGroup.IsUnlimitSig(x.PriceType_TC, x.BerthType_TC))
					.Select(x => x.Student_ID).ToList();
				var studentWithPhoto = StudentPhotoService.GetAll(x => unlimitStudents.Contains(x.Student_ID))
					.Select(x => x.Student_ID).ToList();
				var groupIds = studentData.Select(x => x.Group_ID).Distinct().ToList();
				var examMarks = GetPiSigsByMegaOrGroupId(groupIds).Select(x => 
					new {x.StudentInGroup_ID, x.FinalExamMark_TC})
					.ToDictionary(x => x.StudentInGroup_ID, x => x.FinalExamMark_TC);
				if (model.LectureEditStatus == LectureEditStatus.All 
					&& !model.IsDpCons 
					&& examMarks.All(x => x.Value == null)) {
					foreach (var key in examMarks.ToList()) {
						examMarks[key.Key] = FinalExamMarks.Pass;
					}
				}
				model.StudentInfos = studentData
						.ToDictionary(x => x.StudentInGroup_ID, 
						x => new LectureEditVM.StudentInfo(
							_.List(x.LastName, x.FirstName, x.MiddleName), 
							x.Email,
							x.PriceType_TC,
							x.Course_TC,
							x.Student_ID,
							examMarks[x.StudentInGroup_ID],
							studentWithPhoto.Contains(x.Student_ID),
							x.BerthType_TC
							));
				var courseTCs = model.StudentInfos.Select(x => x.Value.CourseTC).Distinct().ToList();
				model.SpecFiles = CourseFileVMService.GetSpecFiles(courseTCs).OrderBy(x => x.CourseTC).ToList();
				model.LastLecture = IsLastOrSecondLecture(groupId, lecId, groupIds.Count > 1);
				model.FirstLecture = IsFirstLecture(groupId, lecId);
				return BaseViewWithModel(new LectureEditView(), model);
			}
			catch (SqlException e) {
				Logger.Exception(e, User);
				return LmsConnectionError();
			}
		}

	    private bool IsLastOrSecondLecture(decimal groupId, decimal lecId, bool isMega) {
		    return isMega 
				? LectureService.GetAll(x => x.Group_ID == groupId)
			    .OrderBy(x => x.LectureDateBeg).Select(x => x.Lecture_ID)
			    .First() != lecId
				: LectureService.GetAll(x => x.Group_ID == groupId)
			    .OrderByDescending(x => x.LectureDateBeg).Select(x => x.Lecture_ID)
			    .First() == lecId;
	    }

	    private bool IsFirstLecture(decimal groupId, decimal lecId) {
		    return LectureService.GetAll(x => x.Group_ID == groupId).OrderBy(x => x.LectureDateBeg)
			    .Select(x => x.Lecture_ID)
			    .First() == lecId;
	    }

	    private Lecture GetLectureOrNext(decimal? lectureId) {
		    var lecture = lectureId.HasValue
			    ? LectureService.GetByPK(lectureId.Value)
			    : LectureService.GetAll(x => x.Teacher_TC == User.Employee_TC &&
				    (x.LectureDateBeg > DateTime.Now || (x.LectureDateBeg < DateTime.Now && x.LectureDateEnd > DateTime.Now)))
				    .OrderBy(x => x.LectureDateBeg).FirstOrDefault();
		    return lecture;
	    }

	    private IQueryable<StudentInGroup> GetSigsByMegaOrGroupId(decimal groupId) {
		    return StudentInGroupService.GetAll(x => 
			    x.Group.MegaGroup_ID.GetValueOrDefault(x.Group_ID) == groupId);
	    }
	    private IQueryable<PiStudentsInGroup> GetPiSigsByMegaOrGroupId(List<decimal> groupIds) {
		    return PiStudentsInGroupService.GetAll(x => groupIds.Contains(x.Group_ID));
	    }

	    private ActionResult LmsNotFound() {
		    return BaseViewWithTitle("404",new PagePart(BootHtmls.Warning("Страница не найдена").ToString()));
	    }
	    private ActionResult LmsConnectionError() {
		    Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			return BaseViewWithTitle("Ошибка соединения",
			new PagePart(BootHtmls.Danger(
				"Проблема со связь с базой Специалист. Попробуйте повторить операцию через несколько минут или обратитесь в Технический отдел " + 
				H.Email("support@specialist.ru")).ToString()));
	    }

	    private List<PiStudentInGroupLecture> GetPiStudentInGroupLectures(decimal lectureId) {
		    var students = PiStudentInGroupLectureService.GetAll(x => x.Lecture_ID == lectureId
				&& BerthTypes.AllPaid.Contains(x.PiStudentsInGroup.BerthType_TC)).ToList();
			
		    return students;
	    }

	    void UpdateTrainerComingTime(Lecture lecture) {
		    try {
			    if (lecture.TrainerComingTime == null 
					&& DateTime.Now > lecture.LectureDateBeg.AddMinutes(-30)) {
				    PiLectureService.EnableTracking();
				    var piLecture = PiLectureService.GetByPK(lecture.Lecture_ID);
				    piLecture.TrainerComingTime = DateTime.Now;
					EntityUtils.UpdateLastChangerAndDate(piLecture, User.Employee_TC);
					PiLectureService.SubmitChanges();
			    }
		    }
		    catch (Exception e) {
			    Logger.Exception(e,User);
		    }

	    }

	    void CheckUpdateLecturePermissionAndDate(decimal lectureId) {
		    if (GetLectureEditStatus(lectureId) == LectureEditStatus.Nothing) {
			    throw new PermissionException();
		    }
	    }

	    private LectureEditStatus GetLectureEditStatus(decimal lectureId) {
//#if DEBUG 
//		    return true;
//#endif
		    var values = LectureService.GetValues(lectureId, 
				x => new {x.Teacher_TC, x.LectureDateBeg});
		    var nextMonth = values.LectureDateBeg.AddMonths(1);
		    var maxEditDate = new DateTime(nextMonth.Year, nextMonth.Month, 2);
		    if (values.Teacher_TC != User.Employee_TC || DateTime.Today > maxEditDate) {
			    return LectureEditStatus.Nothing;
		    }

		    if (values.LectureDateBeg.Date == DateTime.Today) {
			    return LectureEditStatus.All;
		    }
			return LectureEditStatus.OnlyNotes;
	    }

	    [HttpPost]
	    public ActionResult UpdateLecture(LectureEditVM model) {
		    try {
			    PiStudentInGroupLectureService.context.CommandTimeout = 180;
			    var editStatus = GetLectureEditStatus(model.Lecture.Lecture_ID);
			    if (editStatus == LectureEditStatus.Nothing) {
				    throw new PermissionException();
			    }
			    PiStudentInGroupLectureService.EnableTracking();
			    PiStudentsInGroupService.EnableTracking();
			    var students = GetPiStudentInGroupLectures(model.Lecture.Lecture_ID);
			    var groupId = LectureService.GetValues(model.Lecture.Lecture_ID, x => x.Group_ID);
			    var groupIds = GetSigsByMegaOrGroupId(groupId).Select(x => x.Group_ID).Distinct().ToList();
			    var lastLecture = IsLastOrSecondLecture(groupId, model.Lecture.Lecture_ID, groupIds.Count > 1);
			    List<PiStudentsInGroup> sigs = null;
			    if (lastLecture) {
				    sigs = GetPiSigsByMegaOrGroupId(groupIds).ToList();
			    }
			    using (var ts = new TransactionScope()) {
				    PiStudentInGroupLectureService.EditBegin(User.Employee_TC);
				    foreach (var newStudent in model.Students) {
					    var oldStudent = students.FirstOrDefault(x =>
						    x.StudentInGroupLecture_ID == newStudent.StudentInGroupLecture_ID);
					    if (oldStudent != null) {
						    if (editStatus == LectureEditStatus.All) {
							    oldStudent.Update(newStudent,
								    x => x.IsPresent,
								    x => x.Mark,
								    x => x.TestMethodType_TC,
								    x => x.Lateness,
								    x => x.Departure,
								    x => x.IsRecognized,
								    x => x.Notes);
						    }
						    else if (editStatus == LectureEditStatus.OnlyNotes) {
							    oldStudent.Update(newStudent, x => x.Notes);
						    }
						    oldStudent.LastChanger_TC = User.Employee_TC;
						    if (IsStudentChanged(oldStudent.StudentInGroupLecture_ID)) {
							    oldStudent.LastChangeDate = DateTime.Now;
						    }
						    if (lastLecture) {
							    var sig = sigs.FirstOrDefault(x => x.StudentInGroup_ID == oldStudent.StudentInGroup_ID);
							    if (sig != null) {
								    sig.FinalExamMark_TC = newStudent.FinalExamMark_TC;
							    }
						    }
					    }
				    }
				    PiStudentInGroupLectureService.SubmitChanges();
				    PiStudentInGroupLectureService.EditFinish();
				    ts.Complete();
			    }
			    PiStudentsInGroupService.SubmitChanges();
			    ShowMessage("Данные сохранены");
			    return RedirectBack();
		    } catch (SqlException e) {
			    Logger.Exception(e, User);
			    return LmsConnectionError();
		    }
	    }

	    public bool IsStudentChanged(decimal id) {
		    var student =
			    PiStudentInGroupLectureService.context.GetChangeSet().Updates.OfType<PiStudentInGroupLecture>()
				    .FirstOrDefault(x => x.StudentInGroupLecture_ID == id);
		    return student != null;

	    }

	    public ActionResult LectureQuestionnaire(decimal lectureId) {
		    var lecture = PiLectureQuestionnaireService
			    .FirstOrDefault(x => x.Lecture_ID == lectureId) ?? 
				new PiLectureQuestionnaire {
					Lecture_ID = lectureId
				};
		    var groupId = GetGroupId(lectureId);
		    var group  = PiGroupQuestionnaireService
			    .FirstOrDefault(x => x.Group_ID == groupId) ?? 
				new PiGroupQuestionnaire() { Group_ID = groupId};
		    var model = new LectureQuestionnaireVM {
			    Lecture = lecture,
			    Group = group,
				HasPermission = GetLectureEditStatus(lectureId) == LectureEditStatus.All
		    };
		    return BaseViewWithModel(new LectureQuestionnaireView(), model);
	    }

	    private decimal GetGroupId(decimal lectureId) {
		    return LectureService.GetValues(lectureId, x => x.Group_ID);
	    }

	    [HttpPost]
	    public ActionResult LectureQuestionnairePost(LectureQuestionnaireVM model) {
			PiLectureQuestionnaireService.EnableTracking();
			PiGroupQuestionnaireService.EnableTracking();
		    CheckUpdateLecturePermissionAndDate(model.Lecture.Lecture_ID);
		    var lecture = model.Lecture;
		    if (lecture.LectureQuestionnaire_ID > 0) {
			    var oldLecture = PiLectureQuestionnaireService.GetByPK(lecture.LectureQuestionnaire_ID);
			    if (oldLecture.Lecture_ID != lecture.Lecture_ID) {
				    throw new PermissionException();
			    }
			    oldLecture.Update(lecture,
				    x => x.ClassRoomLetter,
				    x => x.ClassRoomComment,
				    x => x.EquipmentLetter,
				    x => x.EquipmentComment,
				    x => x.FeedingLetter,
				    x => x.FeedingComment
				    );
		    }
		    else {
			    lecture.Employee_TC = User.Employee_TC;
			    PiLectureQuestionnaireService.Insert(lecture);
		    }
		    var group = model.Group;
		    if (group.GroupQuestionnaire_ID > 0) {
			    var oldGroup = PiGroupQuestionnaireService.GetByPK(group.GroupQuestionnaire_ID);
			    if (oldGroup.Group_ID != group.Group_ID) {
				    throw new PermissionException();
			    }
			    oldGroup.Update(group,
				    x => x.AdministrationLetter,
				    x => x.AdministrationComment,
				    x => x.DocumentsLetter,
				    x => x.DocumentsComment,
				    x => x.ScheduleLetter,
				    x => x.ScheduleComment
				    );
		    }
		    else {
			    group.Employee_TC = User.Employee_TC;
			    PiGroupQuestionnaireService.Insert(group);
		    }
			EntityUtils.UpdateLastChangerAndDate(model.Lecture, User.Employee_TC);
			EntityUtils.UpdateLastChangerAndDate(model.Group, User.Employee_TC);
			PiLectureQuestionnaireService.SubmitChanges();
			PiGroupQuestionnaireService.SubmitChanges();
		    ShowMessage("Данные сохранены");
		    return RedirectBack();
	    }

	    object QTable(List<Tuple<string, string, decimal>> items, Dictionary<decimal, string> students) {
		    var indexes = students.Select(x => x.Key).ToList();
		    var showLetter = items.Select(x => x.Item1).Any(x => !x.IsEmpty());
		    if (!showLetter && items.All(x => x.Item2.IsEmpty())) {
			    return H.p["Ответов нет"];
		    }
		    var notes = bh.Table(items.Select(x => {
			    var student =H.td[H.span["Слушатель " + (indexes.IndexOf(x.Item3) + 1) ]]
					.Style("width:120px;");
			    return showLetter ? H.Row(student, H.td[x.Item1].Style("width:30px;"), x.Item2) : H.Row(student, x.Item2);
		    } ));
		    return notes;
		}

		public ActionResult Questionnaire(decimal groupId) {
			var group = GroupService.GetByPK(groupId);
			var questionnaiers = QuestionnaireService.GetAll(x => 
				x.StudentInGroup.Group.MegaGroup_ID.GetValueOrDefault(x.StudentInGroup.Group.Group_ID) 
					== groupId).ToList();
			object view;
			if (!questionnaiers.Any()) {
				view = LmsViews.NoData();
			}
			else {
			var teacherMarks = QuestionnaireTeachersMarkService.GetAll(x => 
				x.Questionnaire.StudentInGroup.Group.MegaGroup_ID.GetValueOrDefault(x.Questionnaire.StudentInGroup.Group.Group_ID) 
				== groupId && x.Teacher_TC == User.Employee_TC).ToList();
			var roomMarks = QuestionnaireClassRoomsMarkService.GetAll(x => 
				x.Questionnaire.StudentInGroup.Group.MegaGroup_ID.GetValueOrDefault(x.Questionnaire.StudentInGroup.Group.Group_ID) == groupId).ToList();
			var letters = questionnaiers.SelectMany(x =>
				_.List(x.SkillsLetter, x.CourseLetter, x.RecordQualityLetter, x.OrganizingLetter)
					.Select(y => Tuple.Create(x.Questionnaire_ID, y)));
			var students = letters.GroupBy(x => x.Item1).ToDictionary(x => x.Key,
				x => x.Select(z => z.Item2.ToString()).JoinWith(" "));
			var recomends = questionnaiers.Select(x => x.RecommendationMark).Where(x => x.HasValue).ToList();
			var minus = recomends.Count(x => x <= 6);
			var plus = recomends.Count(x => x >= 9);
			var dvk = questionnaiers.Count == 0 ? 0 : ((plus - minus)*100)/questionnaiers.Count;
			var items = _.List(
				Tuple.Create("Как бы Вы оценили полученные Вами на курсе знания и приобретенные навыки?",
					questionnaiers.Select(x => Tuple.Create(x.SkillsLetter.NotNullString(), x.SkillsComment, x.Questionnaire_ID))),
				Tuple.Create("Как бы Вы оценили курс?",
					questionnaiers.Select(x => Tuple.Create(x.CourseLetter.NotNullString(), x.CourseComment, x.Questionnaire_ID))),
				Tuple.Create("Как бы Вы оценили запись курса?",
					questionnaiers.Select(x => Tuple.Create(x.RecordQualityLetter.NotNullString(), x.RecordQualityComment,
						x.Questionnaire_ID))),
				Tuple.Create("Как бы Вы оценили работу Центра по организации Вашего обучения и выдачу документов?",
					questionnaiers.Select(x => Tuple.Create(x.OrganizingLetter.NotNullString(), x.OrganizingComment,
						x.Questionnaire_ID))),
				Tuple.Create("Что Вам больше всего понравилось в пройденном курсе?",
					questionnaiers.Select(x => Tuple.Create((string)null,x.ExpectationComment,x.Questionnaire_ID))),
				Tuple.Create("Есть ли у Вас знакомые, которым Вы могли бы порекомендовать обучиться в нашем Центре?",
					questionnaiers.Select(x => Tuple.Create((string)null,x.AdministrationComment,x.Questionnaire_ID))),
				Tuple.Create("Что нам следовало бы сделать по-другому, чтобы Ваше впечатление о Центре было бы лучше?",
					questionnaiers.Select(x => Tuple.Create((string)null,x.StudentNotes,x.Questionnaire_ID))),
				Tuple.Create("Как бы Вы оценили Вашего преподавателя?",
					teacherMarks.Select(x => Tuple.Create(x.TeacherLetter.NotNullString(), x.Notes,x.Questionnaire_ID))),
				Tuple.Create("Как бы Вы оценили класс и технику (качество вебинара)?",
					roomMarks.Select(x => Tuple.Create(x.ClassRoomLetter.NotNullString(), x.Notes,x.Questionnaire_ID))),
				Tuple.Create("Какова вероятность, что Вы порекомендуете обучение в нашем Центре другу или коллеге?",
					questionnaiers.Select(x => Tuple.Create(x.RecommendationMark.NotNullString(), "", x.Questionnaire_ID))));
			view = H.div[
				items.Select(x => H.div[H.h3[x.Item1], QTable(x.Item2.ToList(), students)] ), 
				dvk != 0 ? H.h3["ДВК - " + dvk + "%"] : null];
				
			}
			return BaseViewWithTitle("Анкеты группы {0} {1}"
				.FormatWith(group.DateInterval, group.Course_TC), new PagePart(view.ToString()));

		}

	    private void WebinarLauncherPermission(decimal lectureId) {
		    if (IsNotUserLecture(lectureId)) {
			    throw new PermissionException();
		    }
		    
	    }

	    private bool IsNotUserLecture(decimal lectureId) {
		    return LectureService.GetValues(lectureId, x => x.Group.Teacher_TC) != User.Employee_TC;
	    }

	    public ActionResult WebinarLauncher(decimal? lectureId) {

/*
		    if (IsNotUserLecture(lectureId)) {
			    return BaseViewWithTitle("Нет доступа",new PagePart(
					BootHtmls.Warning("Эту лекцию ведет другой преподаватель").ToString()));
		    }
*/
			LectureService.LoadWith(x => x.WebinarLicense);
		    var lecture = GetLectureOrNext(lectureId);
			if(lecture == null || lecture.WebinarLicense == null)
				return LmsNotFound();
		    var lp = lecture.WebinarLicense;
		    var id = StringUtils.ParseInt(StringUtils.GetRegGroupValue(lecture.WebinarURL, @"join/(\d+)"));
		    var script = TemplateEngine.GetText(Properties.Resources.WebinarLauncherTamplate,
			 new {WebinarId = id, Login = lp.LicenseLogin, Password = lp.LicensePassword});
		    return File(Encoding.UTF8.GetBytes(script), "text/ahk", 
				"Launcher{0}.ahk".FormatWith(lecture.Lecture_ID));
	    }

		public ActionResult Curator() {
		    var curTC = EmployeesCuratorService.FirstOrDefault(x => 
				x.Emp_TC == User.Employee_TC 
				&& (x.DateFrom < DateTime.Today || x.DateFrom.Equals(null)) 
				&& (x.DateTo > DateTime.Today || x.DateTo.Equals(null))).GetOrDefault(x => x.Сurator_TC);
			var view = string.Empty;
		    if (curTC == null) {
			    view = BootHtmls.Warning("У вас нет куратора").ToString();
		    } else {
		    var phone = EmployeeContactService.FirstOrDefault(x => 
				x.EmployeeFK_TC == curTC && x.ContactType_ID == ContactTypes.Specialist.Phone);
		    var curator = EmployeeService.GetByPK(curTC);
		    view = H.div[Images.Employee(curator.Employee_TC).Style("float:left;margin-right:10px;"),
			    H.h3[curator.FullName], 
				phone.GetOrDefault(x => H.h4[phone.ContactValue]),
				H.h4[H.Email(curator.FirstEmail)]].ToString();
		    }
		    return BaseViewWithTitle("Куратор", new PagePart(view));
	    }

	    public ActionResult StudentPhoto(decimal id) {
		    var photo = StudentPhotoService.GetByPK(id).Photo.ToArray();
			return File(photo, "image/jpg");	    
	    }

	    public ActionResult GroupTestResults(decimal id) {
		    var date = GroupService.GetValues(id, x => x.DateBeg);
			var sigData = StudentInGroupService.GetAll(x => x.Group_ID == id 
				&& !x.Track_TC.Equals(null)).Select(x => new { x.Track_TC, x.Student_ID}).ToList();
		    var trackTCs = sigData.Select(x => x.Track_TC).Distinct().ToList();
		    var studentIds = sigData.Select(x => x.Student_ID).ToList();
			UserTestService.LoadWith(x => x.User, x => x.Test);
		    var testIds = trackTCs.SelectMany(x => TestService.CourseTests()
			    .GetValueOrDefault(x) ?? new List<Test>()).Select(x => x.Id).ToList();
			var userTests = UserTestService.GetAll(x => testIds.Contains(x.TestId)
			&& studentIds.Contains(x.User.Student_ID.GetValueOrDefault())).ToList();
			var data = UserTestResultService.GetResultData(_.List(userTests),true);
			return File(Encoding.GetEncoding(1251).GetBytes(CsvUtil.Render(data)), 
				"text/csv", "TestResults.csv");
	    }

	    public ActionResult CourseVideos(string courseTC) {
	        var model = EmployeeCourseService.GetAll(x => 
				x.Course_TC == courseTC && !Equals(x.BroadcastingURL,null) && x.Employee_TC == User.Employee_TC && x.IsActive)
				.Select(x => new {x.BroadcastingURL, x.WbnRecLogin, x.WbnRecPwd}).FirstOrDefault();
		    if (model == null) {
			    return NotFound();
		    }
		    TagDiv view;
		    if (StringUtils.IsVimeoUrl(model.BroadcastingURL)) {
			    var albumId = StringUtils.GetVimeoAlbumId(model.BroadcastingURL);
			    var videoIds = AlbumVideoService.GetVideos(albumId);
			    view = SiteHtmls.VimeoPlayers(videoIds, model.WbnRecPwd).Style("width:640px;margin:10px auto;");
		    } else {
			    view = H.div[LmsViews.UrlWithPassword(model.BroadcastingURL, model.WbnRecLogin, model.WbnRecPwd)];
		    }
			 
			return BaseViewWithTitle("Видеозаписи курса " + courseTC.ToUpper(), 
				new PagePart(view.ToString()));
	    }


	    public LectureEditVM.Ratings GetRating(Group g) {
		    var ratingCourseTC = CourseRepository.GetValues(g.Course_TC, c => c.RatingCourse_TC);
		    var rating = RatingSubtotalService.GetAll(x =>
			    x.RatingCourse_TC == ratingCourseTC && x.Teacher_TC == g.Teacher_TC)
			    .GroupBy(x => 1).Select(y => y.Sum(z => z.Score)/y.Sum(z => z.Quantity)).FirstOrDefault();
		    if (rating > 0) {
			    var maxRating = RatingSubtotalService.GetAll(x =>
				    x.RatingCourse_TC == ratingCourseTC)
				    .GroupBy(x => x.Teacher_TC).Max(y => y.Sum(z => z.Score)/y.Sum(z => z.Quantity));
			    
			    return new LectureEditVM.Ratings {
				   Current = TeacherRatings.GetLetter(rating),
				   Max = TeacherRatings.GetLetter(maxRating)
			    }; 
		    }
		    return null;
	    }


	}
}
