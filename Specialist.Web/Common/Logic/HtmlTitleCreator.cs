using System.Web;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Root.Common.Services;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Common.Logic {
	public class HtmlTitleCreator {
		public static string Get(object model) {
			var url = HttpContext.Current.Request.Url.AbsolutePath.ToLowerInvariant();
			var pageMeta = new PageMetaService().GetFor(url);
			if (pageMeta != null && !pageMeta.Title.IsEmpty())
				return pageMeta.Title;
			if(model == null)
				return null;
			string title = GetTitle(model as dynamic);
			if(!title.IsEmpty())
				return title;
			var viewModel = model as IViewModel;
			if (viewModel != null) {
				return viewModel.Title;
			}
			return title;
		}

		public static string GetPageTitle() {
			var url = HttpContext.Current.Request.Url.AbsolutePath.ToLowerInvariant();
			var pageMeta = new PageMetaService().GetFor(url);
			if (pageMeta != null)
				return pageMeta.PageTitle;
			return null;

		}
		private static string GetTitle(object obj) {
			return null;
		}

		private static string GetTitle(VendorVM model) {
			return Htmls.CommonTitle(model.Vendor.Name);
		}

		private static string GetTitle(ExamVM model) {
			return "Экзамен № {0} {1} в Специалисте".FormatWith(model.Exam.Exam_TC, model.Exam.ExamName);
		}
		private static string GetTitle(CourseBaseVM model) {
			return model.HtmlTitle;
		}

		private static string GetTitle(Profession model) {
			return Htmls.CommonTitle(model.Name);
		}
		private static string GetTitle(Product model) {
			return Htmls.CommonTitle(model.Name);
		}
		private static string GetTitle(SiteTerm model) {
			return Htmls.CommonTitle(model.Name);
		}
		private static string GetTitle(SectionVM model) {
    			return
					(model.Section.Name.ToLowerInvariant().Contains("курсы") ? "" : 
					"Курсы ") + model.Section.Name + " в Специалисте";
		}
	}
}