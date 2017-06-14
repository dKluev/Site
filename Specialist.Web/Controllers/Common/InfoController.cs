using System.Linq;
using System.Web.Mvc;
using Bing;
using Bing.Views;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Core;
using Specialist.Web.Pages;

namespace Specialist.Web.Controllers.Common {
	[Auth(RoleList = Role.Employee)]
	public class InfoController:ViewController {
		[Dependency]
		public IRepository2<CityInfo> CityInfoService { get; set; }
		 
		[Dependency]
		public IRepository2<Lecture> LectureService { get; set; }

		[Dependency]
		public IRepository2<LectureFile> LectureFileService { get; set; }

		public ActionResult CityInfoBlock(int id) {
			var block = CityInfoService.GetValues(id, x => x.Description);
			var model = new BaseVM {
				Title = "Блок города справа",
				RightSide = _.List(new PagePart(Htmls2.Cham(block)))
			};
			return ViewWithBaseVM(model);
		}

		public ActionResult GroupFiles(decimal? groupId) {
			var lectures = LectureService.GetAll(x => x.Group_ID == groupId.GetValueOrDefault()).ToList();
			var lectureIds = lectures.Select(x => x.Lecture_ID).ToList();
			var model = new LectureFilesVM {
				Lectures = lectures,
				GroupId = groupId,
				Files = LectureFileService.GetAll(x => lectureIds.Contains(x.Lecture_ID)).ToList()
			};
			return BaseViewWithModel(new LectureFilesView(), model);
		}
	}
}
