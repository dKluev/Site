using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Center.ViewModels {
	public class SeminarRegisterFormVM:IViewModel {
		[DisplayName("ФИО")]
		public string FullName { get; set; }

		[DisplayName("Название компании")]
		public string CompanyName { get; set; }

		[DisplayName("Электронный адрес")]
		public string Email { get; set; }

		[DisplayName("Телефон")]
		public string Phone { get; set; }

		[DisplayName("Должность")]
		public string Position { get; set; }

		[DisplayName("Область деятельности")]
		public string Region { get; set; }

		[DisplayName("Кто в вашей компании отвечает за обучение сотрудников")]
		public string StudyManger { get; set; }

		[DisplayName("Направление обучения, которое Вам наиболее интересно")]
		public string Section { get; set; }

		[DisplayName("На какие курсы Вы вероятно запишите сотрудников")]
		public string Courses { get; set; }

		[DisplayName("Какое количество сотрудников Вы планируете обучить")]
		public string HowMany { get; set; }

		[DisplayName("Откуда Вы узнали о мероприятии")]
		public string WhereAbout { get; set; }

		public Group Group { get; set; }

		public string Title { get { return "Регистрация на семинар: " + 
			(Group != null ? Group.Title : "") ; } }
	}
}
