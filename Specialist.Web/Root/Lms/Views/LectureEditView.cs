using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Lms;
using Specialist.Entities.Lms.Const;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Files;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;

using bh = Specialist.Web.Common.Html.BootHtmls;
namespace Specialist.Web.Root.Lms.Views {
	public class LectureEditView: BaseView<LectureEditVM> {
		public override object Get() {
			var parts = ListUtils.Partition(Model.Students, x => Model.StudentInfos[x.StudentInGroup_ID].IsWebinar);
			var webinarList = parts.Item1;
			var other = parts.Item2;
			var webinarTable = GetTable("Вебинар", webinarList,0,true);
			var otherTable = GetTable("Очное обучение", other,webinarList.Count);
			var form = AjaxForm(Url.Lms().Urls.UpdateLecture(null))[
				HiddenFor(x => x.Lecture.Lecture_ID),
				webinarTable, 
				otherTable,
				Model.LectureEditStatus != LectureEditStatus.Nothing ? (object)bh.SubmitButton("Сохранить")
				.Style("margin-bottom:10px;").Id("button-lecture-edit") : 
				bh.Warning("Сохранить изменения может только преподаватель {0} в день проведения занятия"
				.FormatWith(StringUtils.AngleBrackets(Model.Lecture.Teacher_TC)))].Id("form-lecture-edit");

			var date = Model.Lecture.TrainerComingTime;
			var group = Url.Lms().Group(Model.Lecture.Group_ID, Model.Lecture.Group_ID);
			var comingTime = div[b["Время регистрации: "],
				date.HasValue ? date.Value.ToShortTimeString() : "не определено", 
				b[" Группа: "], group,
				Model.Rating.GetOrDefault(x => b[" Служебный код: "]),
				Model.Rating.GetOrDefault(x => Anchor("#", x.Current).Class("not-link").Title(RatingText.FormatWith(x.Current, x.Max)).Data("toggle", "tooltip"))
				];
			var updateForm = Model.LectureEditStatus == LectureEditStatus.All ? UpdateForm() : null;
			return div[
				GetScript("/Scripts/Views/Lms/lectureeditview.js?v=6", "LectureEditView.init", 
				Url.File().Urls.AddLectureFile(Model.Lecture.Lecture_ID)),
				comingTime, 
				BootHtmls.Collapse(_.List(GetWebinarBlock())) ,
				BootHtmls.Collapse(_.List(updateForm)).Style("margin-top:5px;") ,
				form, 
				BootHtmls.Collapse(_.List(UploadArchive(),FtpBlock())) ,
				br,
				LmsViews.SupportInfo,
				h3[Url.Lms().LectureQuestionnaire(Model.Lecture.Lecture_ID, "Анкета преподавателя")]
				].Class("lecture-edit").Style("font-size:10px;");
		}

		private Tuple<string, object> UpdateForm() {
			var allForm = H.form[
				Model.LastLecture ? 
				l(label["Аттестация"], 
					Select("", "", GetExamMarkList(), z => z.Item2, z => z.Item1,true, "Выбрать").Id("all-exam-mark")) 
					: null,
				label["Тип"],
				Select("", "", TestMethodType.All, z => z.Name, z => z.TC,true, "Выбрать").Id("all-method-type"),
				label["Оценка"], 
				Select("", "", Enumerable.Range(1,5).ToList(), z => z, z => z,true, "").Id("all-mark")
				].Class("form-inline");
			return Tuple.Create("Выставить общую оценку и тип", allForm.As<object>());
		}
		private Tuple<string, object> UploadArchive() {
			var allForm = InputFile("file").Id("input-lecture-file");
			var success = bh.Success("Файл добавлен").Id("lecture-file-success");
			var block = div[success, bh.Danger("").Hide().Id("lecture-file-error"), allForm, 
				Images.Indicator().Hide(),
				p["Zip архив размером не больше {0} мб".FormatWith(LectureFiles.sizeMb)]];
			if (Model.LectureFile == null) {
				success.Hide();
			}
			return Tuple.Create("Архив с проверкой работ слушателей", block.As<object>());
		}
		private Tuple<string, object> FtpBlock() {
			if (!Model.SpecFiles.Any()) {
				return null;
			}
			var content = Model.SpecFiles.Count == 1
				? Anchor(Model.SpecFiles.First().Url) as object
				: bh.TableSmall(Model.SpecFiles.Select(x => Row(x.CourseTC, Anchor(x.Url))));
			return Tuple.Create("Общие материалы по курсу (загрузить с корпоративного фтп)", content);
		}

		string UnlimitPart(LectureEditVM.StudentInfo info, PiStudentInGroupLecture x, string select) {
			if (!info.IsUnlimit ) {
				return null;
			}

			if (!Model.ShowUnlimit) {
				return " (БО)";
			}
			var options = _.List(
				Tuple.Create("True", "Идентификация пройдена"),
				Tuple.Create("False", "Идентификация не пройдена"));
			var selectControl =
				Select(select,
					x.IsRecognized.HasValue ? x.IsRecognized.Value.ToString() : "",
					options, z => z.Item2, z => z.Item1, true, "")
					.SetDisabled(Model.LectureEditStatus != LectureEditStatus.All)
					.Style("width:180px;").Class("form-control");
			return " (БО " + (info.HasPhoto ? Url.Lms().StudentPhoto(info.StudentID, "фото")
				.Data("toggle", "lightbox").Data("type", "image").Data("title", info.FullName).ToString() : "без фото") + ") " + selectControl;
		}

		object ShowWhenEdit(object x) {
			var disable = Model.LectureEditStatus != LectureEditStatus.All;
			return disable ? null : "&nbsp;" + x;
		}

		public const string RatingText =
			"Ваш рейтинг сейчас по результатам предыдущих групп этого курса \"{0}\"\n(лучший показатель по курсу \"{1}\").\nРейтинг можно повысить (смотрите \"Инструкцию для преподавателей\"\nили обращайтесь за разъяснениями к своему куратору).\nВсе в Ваших руках! Желаем Вам удачи и благодарных слушателей!";

		private TagDiv GetTable(string title, List<PiStudentInGroupLecture> students, int offset,
			bool webinar = false) {
			var disable = Model.LectureEditStatus != LectureEditStatus.All;
			var checkboxPostfix = webinar ? "" : "1";
			if (students.Any()) {
				var data = new {Students = new PiStudentInGroupLecture()};
				var isSameCourse = Model.StudentInfos.Select(x => x.Value.CourseTC).Distinct().Count() <= 1;
				var table =  
					bh.Table(H.Head("#", "Слушатель", "Присут." + ShowWhenEdit(InputCheckbox("", "").Class("all-present" + checkboxPostfix).Title("Присутствие")), "Оценка", "Тип оценки",
					"Опоздание (мин)", "Уход (мин)", Model.LastLecture ? "Аттестация" : null, "Примечание"),
					students.Select(x => {
						var info = Model.StudentInfos[x.StudentInGroup_ID];
						return new {
							Student = x,
							info = info
						};
					})
						.OrderBy(x => x.info.FullName).Select((kk, idx) => {
							var i = idx + offset;
							var x = kk.Student;
							var info = kk.info;
							var megaGroupCourse = isSameCourse ? "" : " ({0})".FormatWith(info.CourseTC);
							var row = _.List<object>(
								i + 1, H.CleanRaw(
								Url.Lms().Student(info.StudentID, Raw(info.FullName)).Class("student-fullname").ToString() +
									Hidden(data.For(y => y.Students.StudentInGroupLecture_ID, i),
										x.StudentInGroupLecture_ID) + megaGroupCourse
									+ span[ info.Email.GetOrDefault(y => " (" + HtmlControls.MailTo(y) + ")") ].Class("student-email") 
									+ UnlimitPart(info, x, data.For(z => z.Students.IsRecognized, i))),
								Div("checkbox")[
									InputCheckbox(data.For(z => z.Students.IsPresent, i), "true")
										.SetChecked(x.IsPresent && x.Truancy.HasValue)
										.SetDisabled(disable)
										.Style("width:40px;margin-right:auto;margin-left:auto;")
										.Class("present-control" + checkboxPostfix),
									Hidden(data.For(z => z.Students.IsPresent, i), "false")
									],
								Select(data.For(y => y.Students.Mark, i), 
									x.Mark.HasValue ? x.Mark.Value.ToString("n0") : "",
									Enumerable.Range(1,5).ToList(), z => z, z => z, true, "")
									.Style("width:60px;").Class("form-control mark-control")
									.SetDisabled(disable),
								Select(data.For(y => y.Students.TestMethodType_TC, i), x.TestMethodType_TC,
									TestMethodType.All, z => z.Name, z => z.TC, true, "Выбрать")
									.Style("width:100px;").Class("form-control method-type-control")
									.SetDisabled(disable),
								InputNumber(data.For(y => y.Students.Lateness, i), x.Lateness).Style("width:60px;")
									.Class("form-control").SetDisabled(disable),
								InputNumber(data.For(y => y.Students.Departure, i), x.Departure)
									.Class("form-control").Style("width:60px;").SetDisabled(disable) 
								);
							if (Model.LastLecture) {
								var list = GetExamMarkList();
								row.Add(
								Select(data.For(y => y.Students.FinalExamMark_TC, i), info.FinalExamMark_TC,
								   list, z => z.Item2, z => z.Item1, true, "Выбрать")
									.Style("width:100px;").Class("form-control exam-mark-control")
									.SetDisabled(disable));
							}
							row.Add( InputText(data.For(y => y.Students.Notes, i), x.Notes)
									.SetDisabled(Model.LectureEditStatus == LectureEditStatus.Nothing)
									.Class("form-control text2area").Size(300).Style("width:300px;") );
							return Row(row.ToArray());
						}));
				return div[h4[title], table];
			}
			return null;
		}

		private List<Tuple<string, string>> GetExamMarkList() {
			var list = Model.IsDpCons
				? FinalExamMarks.MarkList
				: FinalExamMarks.PassList;
			return list;
		}

		private Tuple<string, object> GetWebinarBlock() {
			var lecture = Model.Lecture;
			if (!(Model.WebinarExists && Model.LectureEditStatus == LectureEditStatus.All)) {
				return null;
			}
		    var webinarBlock = WebinarBlock(lecture);
			return Tuple.Create("Подключение к вебинару", webinarBlock);
	    }

		public static object WebinarBlock(Lecture lecture) {
			var lp = lecture.WebinarLicense;
			var webinarBlock = LmsViews.UrlWithPassword(lecture.WebinarURL, lp.LicenseLogin, lp.LicensePassword);
			return webinarBlock;
		}
	}
}