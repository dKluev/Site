using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Center.ViewModels {
	public class OrgCatalogFormVM:IViewModel {

		[DisplayName("Название компании")]
		public string CompanyName { get; set; }

		[DisplayName("ФИО получателя")]
		public string FullName { get; set; }

		[DisplayName("Должность")]
		public string Position { get; set; }

		[DisplayName("Индекс")]
		public string Index { get; set; }

		[DisplayName("Адрес доставки")]
		public string Address { get; set; }

		[DisplayName("Электронный адрес")]
		public string Email { get; set; }

		[DisplayName("Телефон для подтверждения доставки")]
		public string Phone { get; set; }

		[DisplayName("Кол-во каталогов")]
		public string Count { get; set; }

		[DisplayName("Решение по обучению принимаю я")]
		public bool IamStudyManager { get; set; }

		[DisplayName("ФИО")]
		public string SmFullName { get; set; }

		[DisplayName("Телефон")]
		public string SmPhone { get; set; }

		[DisplayName("Email")]
		public string SmEmail { get; set; }

		public string Title { get { return "Заказ бесплатного каталога курсов"; } }
	}
}
