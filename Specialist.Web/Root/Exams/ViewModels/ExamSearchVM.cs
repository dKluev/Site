using System;
using System.Collections.Generic;
using System.ComponentModel;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Exams.ViewModels {
	public class ExamSearchVM:IViewModel {
		public Vendor Vendor { get; set; }

		public List<Exam> Exams { get; set; }

		[DisplayName("Номер экзамена")]
		public string Number { get; set; }


		public string Title {
			get { return "Поиск экзамена " + Vendor.Name; }
		}
	}
}