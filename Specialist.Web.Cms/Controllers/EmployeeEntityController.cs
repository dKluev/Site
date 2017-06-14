using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.Cms.Core;
using Specialist.Web.Cms.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Helpers;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Cms.Controllers {
	public class EmployeeEntityController : BaseController<Employee> {
		[Dependency]
		public IEmployeeService EmployeeService { get; set; }

		[Dependency]
		public IResponseService ResponseService { get; set; }

		[Dependency]
		public IRepository<Group> GroupService { get; set; }


		public ActionResult TrainersInfo() {
			var model = GetTrainers();
			return View(model);
		}

		private List<TrainerInfoVM> GetTrainers() {
			var trainers = EmployeeService.GetAll(e => e.EmpGroup_TC ==
                EmpGroups.Trainer).ToList();
			var trainerTCs = trainers.Select(x => x.Employee_TC).ToList();
			var responseCounts = ResponseService.GetAll().IsActive()
				.Where(x => x.Type == RawQuestionnaireType.Teacher &&
					trainerTCs.Contains(x.Employee_TC))
				.GroupBy(x => x.Employee_TC)
				.Select(x => new {Key = x.Key.ToUpper(), Count = x.Count()})
				.ToDictionary(x => x.Key, x => x.Count);
			var groupCounts = GroupService.GetAll().PlannedAndNotBegin()
					.Where(x => x.DateBeg < DateTime.Today.AddMonths(6)
						&& trainerTCs.Contains(x.Teacher_TC))
						.GroupBy(x => x.Teacher_TC)
				.Select(x => new {x.Key, Count = x.Count()})
				.ToDictionary(x => x.Key, x => x.Count);
			return trainers.Select(e => new TrainerInfoVM {
				Trainer = e,
				Link = HtmlControls.Anchor(Const.Common.SiteDomain + Html.GetUrlFor(e),
					e.FullName).ToString(),
				ResponseCount =
					responseCounts.GetValueOrDefault(e.Employee_TC),
				HasPhoto = Urls.Employee(e.Employee_TC) != null,
				HasDescription = e.SiteDescription.GetOrDefault(x => x.Length > 50),
				Groups = groupCounts.GetValueOrDefault(e.Employee_TC)
			}
				).Where(x => x.Trainer.SiteVisible || x.Groups > 0).ToList();
		}

		public ActionResult ExportToExcel() {
			var trainers = GetTrainers();
			var table = H.table[
				H.Head(
				"Код",     
				"ФИО",     
				"Описание",
				"Отзывы",  
				"Фото",    
				"Группы",
				"Активный"),
				trainers.Select(p => H.Row(
					p.Trainer.Employee_TC,
					p.Trainer.FullName,
					 p.HasDescription.ToInt(),
					p.ResponseCount,
					 p.HasPhoto.ToInt(),
					  p.Groups, p.Trainer.SiteVisible.ToInt()))];
			return File(new UTF8Encoding().GetBytes(table.ToString()), 
				"application/ms-excel", "trainers.xls");
		}
	}
}