using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.Root.Learning.ViewModels {
	public class CertificateValidationVM: IViewModel {
		[DisplayName("ФИО")]
		public string FullName { get; set; }

		[DisplayName("Номер сертификата")]
		public string Number { get; set; }

		public string Title {
			get { return "Проверка подлинности сертификата"; }
		}
	}

}