using System.Net.Mail;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Common;
using Specialist.Entities.Context;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Core;
using System.Linq;
using System.Linq.Dynamic;
using Specialist.Web.Root.Common.MailList;
using SimpleUtils.Extension;
using Specialist.Entities.Passport;

namespace Specialist.Web.Cms.Controllers
{
    public class MailTemplateEntityController: BaseController<MailTemplate>
    {
		[Dependency]
    	public MailListService MailListService { get; set; }

		[Auth(RoleList = Role.AnyContManager)]
		public ActionResult MailList() {
			var model = new MailListVM {
				SendedPercent = MailListService.GetSendedPercent(),
				LastSendDate = MailListService.GetLastSendDate(),
				IsStopped = MailListService.IsStopped
			};
			if(model.SendedPercent.IsNull() || model.IsStopped) {
				var template = MailListService.GetTemplate();
				model.Template = template.V1;
				model.MailListType = template.V2;
			}
			return View(model);
		}

    	[HttpPost]
		public ActionResult SendFor(string emails) {
			var mailTemplate = MailListService.GetTemplate();
			using(var client = new SmtpClient()) {
				foreach (var email in emails.Split(',')) {
					MailListService.Send(client, mailTemplate.V1, email);
				}
			}
    		return RedirectBack();
		}

		[HttpPost]
		public ActionResult SendForAll() {
			MailListService.SendForAll();
			return RedirectBack();
		}
		[HttpPost]
		public ActionResult StopSend() {
			MailListService.StopMailList();
			return RedirectBack();
		}
    }
}
