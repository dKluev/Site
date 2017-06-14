using System.Net.Mail;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Common;
using Specialist.Web.Cms.Core;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;

namespace Specialist.Web.Cms.Controllers {
	public class MarketingActionEntityController: BaseController<MarketingAction> {
		[Dependency]
		public MailService MailService { get; set; }
		protected override void AfterAdded(MarketingAction entity) {
			if (entity.IsActive) {
				SendMail(entity);
			}
		}

		private bool isActive = false;

		protected override void BeforeUpdate(MarketingAction obj) {
			isActive = obj.IsActive;
		}

		protected override void AfterUpdate(MarketingAction obj) {
			if (!isActive && obj.IsActive) {
				SendMail(obj);
			}
		}

		private void SendMail(MarketingAction obj) {
			MailService.NewMarketingAction(obj, Url.Center().MarketingAction(obj.UrlName, obj.Name)
				.AbsoluteHref().ToString());
		}
	}
}