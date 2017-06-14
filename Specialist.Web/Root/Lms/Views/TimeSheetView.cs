using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Lms;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;
using Specialist.Web.Util;

namespace Specialist.Web.Root.Lms.Views {
	using bh = BootHtmls;
	public class TimesheetView : BaseView<TimesheetVM> {
		public override object Get() {

			var webcal = "webcal://" + CommonConst.PureRoot + Url.Rss().Urls.TimeSheetCalendar(User.Employee_TC);
			var view = div[				
				GetScript("/Scripts/Views/Lms/timesheetview.js", "TimesheetView.init", 
				Url.Lms().Urls.TimeSheet(null)),

				h2[MonthUtil.GetName(Model.DateBegin.Month) + " " + Model.DateBegin.Year, 
				Buttons()
				],
				GetLecturesView(),
				LmsViews.SupportInfo,
				Anchor(webcal, "Экспортировать расписание")];
			return view;
		}

		private object Buttons() {

			return Div("btn-group")[
				bh.IconBtn(Url.Lms().Urls.TimeSheet(Model.DateBegin.AddMonths(-1).DefaultString()), "chevron-left"),
				bh.IconBtn("#", "calendar")
				.Data("date-format", "dd.mm.yyyy")
				.Data("date", Model.DateBegin.DefaultString())
				.Id("timesheet-calendar"),
				bh.IconBtn(Url.Lms().Urls.TimeSheet(Model.DateBegin.AddMonths(1).DefaultString()), "chevron-right")
				].Style("margin-left:10px;");
		}


		const string ak = " а.к.";
		private object GetLecturesView() {
			var lectures = Model.Lectures;
			var groups = lectures.GroupBy(x => x.Group_ID).Where(x => x.Count() > 1)
				.Select((x, i) => new {
					x.Key, i
				}).ToDictionary(x => x.Key,
				x => HtmlUtils.Colors.GetValueOrDefault(x.i) ?? "000000");
			var uniqGroups = lectures.Select(x => x.Group_ID).Distinct();
			if (groups.Count == 1 && uniqGroups.Count() == 1) {
				groups.Clear();
			}

			var dateLectures = lectures.OrderBy(x => x.LectureDateBeg)
				.GroupBy(x => x.LectureDateBeg.Date).ToDictionary(x => x.Key, x => x.ToList());

			var weeks = Model.Weeks;
			var rows = weeks.SelectMany(x => _.List(tr[th[""], x.Select(day =>
				th[DateWithDay(day)].Class(day == DateTime.Today ? "today" : ""))])
				.AddFluent(tr[td.Class("day-hours"), x.Select(day =>
						GetRow(dateLectures, groups, day))]));

			var hours = GetHours(lectures.Where(x => IsCurrentMonth(x.LectureDateBeg)).ToList());
			var hoursBlock = HoursBlock(hours);
			var absenceText =
				Model.Absences.OrderBy(x => x.DateFrom).Select(x => x.DateFrom.DefaultWithTimeString() + " - " + x.DateTo.DefaultWithTimeString()).JoinWith(
					", ");
			return H.div[
				table[rows].Class("timesheet-table"), 
				p[Note],
				absenceText.IsEmpty() ? null : 
				  p[span[""].Class("absence-square"), " - Дни вашего отсутствия ({0})".FormatWith(absenceText)].Style("padding-top:5px;"),
				NotesBlock(),
				hoursBlock];
		}

		string Note = "{0} - Спец. {1} - Выезд. {2} - Не Москва".FormatWith(Group.Spec, Group.Viezd, Group.NoMoscow);

		HoursVM GetHours(List<LmsLecture> lectures) {
			var model = new HoursVM();
			lectures.Where(x => IsCurrentMonth(x.LectureDateBeg)).ForEach((l,i) => {
				var hours = EntityUtils.GetLectureHours(l.LectureDateBeg, l.LectureDateEnd, l.Breaks);
				model.Courses[l.Course_TC] = model.Courses.GetValueOrDefault(l.Course_TC) + hours;
				if (l.LectureType_TC == LectureTypes.Individual) {
					model.Ind += hours;
				}
				if (l.Complex == Cities.Complexes.NoMoscow) {
					model.NoMoscow += hours;
				}else if (l.LectureType_TC == LectureTypes.Special) {
					model.Spec += hours;
				}
			});
			return model;
		}

		object NotesBlock() {
			return p[b["Примечание: "], Model.Notes ?? "отсутствует"];
		}

		private TagTd GetRow(Dictionary<DateTime, List<LmsLecture>> dateLectures,
			Dictionary<decimal, string> groups, DateTime day) {
			var lecture = dateLectures.GetValueOrDefault(day);
//			if (!IsCurrentMonth(day)) {
//				return td.Class("other-month");
//			} 
			if (lecture == null) {
				return td.Class(GetColorClass(day));
			}
			var lectureDivs = new List<TagA>();
			foreach (var part in lecture) {
				var hours = EntityUtils.GetLectureHours(part.LectureDateBeg,
					part.LectureDateEnd, part.Breaks);
				var calc = Model.Groups[part.Group_ID];
				var lecColor = groups.GetValueOrDefault(part.Group_ID) ?? "000000";
				var lines = _.List(
					part.Course_TC, 
					Model.GroupDates[part.Group_ID],
					part.ClassRoom_TC + " {0:0.##}{1}".FormatWith(hours, ak)
					);
				if (part.Breaks > 0) {
					lines.Add("Перерыв " + part.Breaks);
				}
				var topHeight = GetTopAndHeight(part);
				var linkText = HtmlUtils.ReplaceSpaceWithNbsp(lines.JoinWith("<br/>"));
				lines.Insert(0, part.LectureDateBeg.ToShortTimeString() + " - "
					+ part.LectureDateEnd.ToShortTimeString());
				var titleText = lines.AddFluent("Очные: {0}\nВебинар: {1}"
					.FormatWith(calc.NumOfStudents, calc.NumOfWebinarists)).JoinWith("\n");
				var content = Url.Lms().Lecture(part.Lecture_ID, linkText).Class("lecture")
						.Title(titleText)
						.Data("toggle", "tooltip")
						.Style("border-color: #{0};top: {1}px;height: {2}px"
							.FormatWith(lecColor, topHeight.Item1,topHeight.Item2));
				lectureDivs.Add(content);	
			}

			return td[Div("lecture-wrapper")[lectureDivs]].Class(GetColorClass(day));
		}

		public string GetColorClass(DateTime day) {
			if (IsAbsence(day)) {
				return "absence";
			}
			if (!IsCurrentMonth(day)) {
				return "other-month";
			}
			if (DateUtils.IsWeekend(day) || Model.DayOffList.Contains(day)) {
				return "dayoff";
			}
			if (day == DateTime.Today) {
				return "today";
			}
			return null;
		}

		private double pxPerMinuts = 223.0/(14*60);
		public Tuple<int, int> GetTopAndHeight(LmsLecture lecture) {
			var dateBeg = lecture.LectureDateBeg;
			var minutes = (lecture.LectureDateEnd - dateBeg).TotalMinutes;
			var startMinute = ((dateBeg.Hour - 8)*60 + dateBeg.Minute);
			return Tuple.Create((int)(startMinute *pxPerMinuts) - 1, (int)(minutes*pxPerMinuts) + 2);
		}

		public bool IsAbsence(DateTime date) {
			return Model.Absences.Any(x => x.DateFrom.Date <= date && x.DateTo.Date >= date);
		}

		public static object HoursBlock(HoursVM z) {
			var total = Tuple.Create("Итого", z.Total);
			var ind = z.Ind > 0 ? Tuple.Create("Индивидуально", z.Ind) : null;
			var spec = z.Spec > 0 ? Tuple.Create("Спец", z.Spec) : null;
			var viezd = z.NoMoscow > 0 ? Tuple.Create("Не Москва", z.NoMoscow) : null;
			var hoursText = z.Courses.Select(x => Tuple.Create(x.Key,x.Value)).ToList()
				.AddFluent(_.List(ind,total,ind, spec, viezd).Where(x => x != null));
			var hoursBlock = div[h4["Распределение часов"], bh.TableSmall(hoursText.Select(x => 
				Row(x.Item1, x.Item2.ToString("0.##") + ak)))];
			
			return hoursBlock;
		}

		private const int morningEndHour = 15;

		private string DateWithDay(DateTime x) {
			var isWeekend = DateUtils.IsWeekend(x);
			var text = x.OnlyDM() + " " + MonthUtil.DayNamesShort[x.DayOfWeek] + "";
			var currentMonth = IsCurrentMonth(x);
			var cls = !currentMonth ? "text-muted" : (isWeekend ? "text-danger" : "");
			return b[text].Class(cls).ToString();
		}

		private bool IsCurrentMonth(DateTime x) {
			return Model.DateBegin.Month == x.Month &&
				Model.DateBegin.Year == x.Year;
		}


		public static List<List<LmsLecture>> SplitByTime(List<LmsLecture> list) {
			var part1 = list.Where(x => x.LectureDateBeg.Hour < 12).ToList();
			var part2 = list.Where(x => x.LectureDateBeg.Hour < 16 && x.LectureDateBeg.Hour >= 12).ToList();
			var part3 = list.Where(x => x.LectureDateBeg.Hour >= 16).ToList();
			var morningDay = part1.Any(x => x.LectureDateEnd.Hour >= morningEndHour);
			return morningDay && !part2.Any() ? _.List(part1, part3) : _.List(part1, part2, part3);
		}

	}
}