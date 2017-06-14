using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;

namespace Specialist.Web.Root.Center.ViewModels {
	public class MtsEmployeeFormVM:IViewModel {
		public List<CourseLink> Courses { get; set; }
		public Dictionary<string,List<object>> Dates { get; set; }

		[DisplayName("ФИО")]
		public string FullName { get; set; }



		[DisplayName("Организация")]
		public string Organization { get; set; }

		[DisplayName("Электронный адрес")]
		public string Email { get; set; }

		[DisplayName("Телефон")]
		public string Phone { get; set; }

		[DisplayName("Табельный номер сотрудника")]
		public string Number { get; set; }

		[DisplayName("Курс")]
		public string Course { get; set; }

		[DisplayName("Срок обучения")]
		public string Date { get; set; }

		public string Title { get { return "Обучение для сотрудника МТС"; } }

		public static List<string> CourseTCList = _.List(
			"ФШ1-Л",
			"ДИЗ-А",
			"ОЖИВ-А",
			"ТЕОРЦВ",
			"СКЕТЧ",
			"ОРИС-А",
			"ОРИС2-А",
			"ВЕБ",
			"ЛАНДИЗ",
			"ЛАНДИЗ2-А",
			"ФОТО1-В",
			"ФОТО2",
			"РНР4",
			"ДЖВ3ЕЕ",
			"ОРПМ",
			"ВНЕДУПР-А",
			"ПЭКС",
			"КД2",
			"ВМЖ-Е",
			"ДЕКОР",
			"ИЛ2-Г",
			"ЭКСЕЛЬ3-В"
			);
	}
}
