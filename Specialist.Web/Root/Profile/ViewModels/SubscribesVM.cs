using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SimpleUtils.FluentAttributes.Attributes;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Passport;
using Specialist.Web.Common.Mvc.Binders;

namespace Specialist.Entities.Profile.ViewModel {
	public class SubscribesVM : IViewModel {
		public User User { get; set; }

		public string Title {
			get { return "��� ��������"; }
		}


		[UIHint("PaperCatalog")]
		[DisplayName("�������� �������")]
		public SubscribeType Subscribes { get; set; }

		[UIHint("FlagEnum")]
		[DisplayName("������������ ��������")]
		public MailListType MailListTypes { get; set; }
	}
}