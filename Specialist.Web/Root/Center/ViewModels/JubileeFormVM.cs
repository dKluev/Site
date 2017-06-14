using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;

namespace Specialist.Web.Root.Center.ViewModels {
	public class JubileeFormVM {
		[DisplayName("ФИО")]
		public string FullName { get; set; }

		[DisplayName("Компания")]
		public string CompanyName { get; set; }

		[DisplayName("Поздравление")]
		[UIHint(Controls.TextArea)]
		public string Message { get; set; }

		[DisplayName("Ссылка на видео")]
		public string VideoLink { get; set; }
	}
}
