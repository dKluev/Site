using System;
using System.Collections.Generic;
using SimpleUtils.Util;
using Specialist.Entities.Catalog.Const;

namespace Specialist.Web.Const {
	public static class CommonTexts {

		public const string Best2016 = "Лучший выпускник 2016";

		public const string TrackCourseDiscountText = "Скидка действует только при единовременной оплате всей комплексной программы";

		public const string DiscountForDay =
			"Данная скидка действительна при заказе и оплате очного обучения и вебинаров сегодня. Запишитесь прямо сейчас со скидкой!";

		public const string TrackName = "Программа повышения квалификации";
		public const string TrackName2 = "Программу повышения квалификации";
		public const string TrackName3 = "Программ повышения квалификации";
		public const string TracksName = "Программы повышения квалификации";

		public const string TrackDiscount = "Вы экономите {0} стоимости {1} курса!";
		public const string OneFreeCourse = "Один курс в подарок!";
		public const string FreeCourse = "Курс в подарок!";
		public const string FullGroupError = "Нельзя вносить в группу более 120 слушателей";
		public const string OpenClasses = "Группа в формате ";
		public const string Discounts = " со скидками";

		public const string NoCert = "После прохождения теста сертификат тестирования не выдается";

		public const string ActionPrefix = "Летние ";

		public const string Phone = "+7 (495) 232-32-16";
		public const string KarpovishPhone = "+7 (903) 700-66-33";

		public const string VacancyPhone = "+7 (495) 780-48-48";

		public static Dictionary<string, string> CourseTexts = new Dictionary<string, string>
	{{CourseTC.Rukpord, "Ваша цель – рост не только профессиональный, но и карьерный? – Пройдите курсы для руководителей!"},
	{CourseTC.English,"Работайте с иностранными партнерами! Или в иностранной фирме. Выбери свой уровень!"}};


	public static string OnDay{get {
			var day = DateTime.Today.Day;
			return " на " + day +
				" " + MonthUtil.GetName(DateTime.Today.Month, true);
		}
		}

		public const string EmptyFirstName = "Не указано";
	}
}