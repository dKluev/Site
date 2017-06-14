using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.Root.Learning.ViewModels {
	public class CertificateValidationVM: IViewModel {
		[DisplayName("���")]
		public string FullName { get; set; }

		[DisplayName("����� �����������")]
		public string Number { get; set; }

		public string Title {
			get { return "�������� ����������� �����������"; }
		}
	}

}