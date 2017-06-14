using System.Net.Mail;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Common;
using Specialist.Web.Cms.Core;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;

namespace Specialist.Web.Cms.Controllers {
	public class NewsEntityController: BaseController<News> {
		[Dependency]
		public MailService MailService { get; set; }


	}
}