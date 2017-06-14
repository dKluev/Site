using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Center.ViewModels {
	public class PaperCatalogFormVM:IViewModel {
		[DisplayName("ФИО")]
		public string FullName { get; set; }

		[DisplayName("Электронный адрес")]
		public string Email { get; set; }

		public string Title { get { return "Заказ печатного экземпляра каталога"; } }
	}
}
