using System;
using System.Text;
using System.Web;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Root.Common.Services;

namespace Specialist.Web.Common.Logic {
	public class PageMetaTags {
		public static string Get(object model) {
			var url = HttpContext.Current.Request.Url.AbsolutePath;
			return Get(url, model);
		}


		public static string Get(string url, object model = null) {
			url = url.ToLower();
			string description = null;
			string keywords = null;
			var service = new PageMetaService();
			var pageMeta = service.GetFor(url);
			var image = string.Empty;
			if (pageMeta != null) {
				description = pageMeta.Description;
				keywords = pageMeta.Keywords;
			}
			if (model is NewsVM) {
				var newsVM = model.As<NewsVM>();
				description = description ?? StringUtils.RemoveTags(newsVM.News.ShortDescription);
				image = Urls.Image("News/Small/" + newsVM.News.SmallImage);
			}else if (model is CourseVM) {
				var courseVM = model.As<CourseVM>();
				var course = courseVM.Course;
				var meta = GetCourseMeta(course);
				description = description ?? meta.Item1 ;
				image = meta.Item2;
			}else if (model is MobileCourseVM) {
				var courseVM = model.As<MobileCourseVM>();
				var course = courseVM.Course;
				var meta = GetCourseMeta(course);
				description = description ?? meta.Item1 ;
				image = meta.Item2;
			}else if (model is AboutTrainerVM) {
				var aboutTrainerVM = model.As<AboutTrainerVM>();
				description = description ??
					StringUtils.RemoveTags(StringUtils.GetFirstParagraph(aboutTrainerVM.Description.FirstPart));
				image = Urls.Image("Employee/" + aboutTrainerVM.Employee.Employee.Employee_TC.ToLower() + ".jpg");
			}else if (pageMeta == null && model is ExamVM) {
				var exam = model.As<ExamVM>().Exam;
				description = "Экзамен № {0} {1} в Специалисте"
					.FormatWith(exam.Exam_TC, exam.ExamName);
				keywords = "Экзамен, {1}, {0}, Специалист"
					.FormatWith(exam.Exam_TC, exam.ExamName);

			}
			return GetMetaTags(keywords, description, image);
		}

		private static Tuple<string, string> GetCourseMeta(Course course) {
			var meta = Tuple.Create(StringUtils.RemoveTags(StringUtils.GetFirstParagraph(course.Description)),
				Urls.Image("Course/" + course.UrlName + ".jpg"));
			return meta;
		}

		public static string GetMetaTags(string keywords, string description, string image) {
			var result = new StringBuilder();
			if (!keywords.IsEmpty())
				result.AppendLine(
					"<meta name='keywords' content='" + keywords + "'/>");
			if (!description.IsEmpty()) {
				result.AppendLine("<meta name='description' content='" + description + "'/>");
			}
			result.AppendLine("<meta name='og:description' content='" + description + "'/>");
			if(!image.IsEmpty())
				result.AppendLine("<meta name='og:image' content='" + image + "'/>");
			return result.ToString();
		}
	}
}