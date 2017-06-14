using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Web.Root.Center.ViewModels {
	public class CollectionMctsFormVM {
		[DisplayName("ФИО")]
		public string FullName { get; set; }

		[DisplayName("Электронный адрес")]
		public string Email { get; set; }

		[DisplayName("Телефон")]
		public string Phone { get; set; }

		[DisplayName("Название компании")]
		public string CompanyName { get; set; }

		[DisplayName("Город")]
		public string City { get; set; }

/*
		[DisplayName("Дата сдачи экзамена")]
		public string Date { get; set; }

		[DisplayName("Учебный центр")]
		public string Complex { get; set; }
*/
	}
}
