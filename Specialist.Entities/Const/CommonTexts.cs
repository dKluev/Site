using System;
using System.Collections.Generic;
using SimpleUtils.Util;
using Specialist.Entities.Catalog.Const;

namespace Specialist.Web.Const {
	public static class CommonTexts {

		public const string Best2016 = "������ ��������� 2016";

		public const string TrackCourseDiscountText = "������ ��������� ������ ��� �������������� ������ ���� ����������� ���������";

		public const string DiscountForDay =
			"������ ������ ������������� ��� ������ � ������ ������ �������� � ��������� �������. ���������� ����� ������ �� �������!";

		public const string TrackName = "��������� ��������� ������������";
		public const string TrackName2 = "��������� ��������� ������������";
		public const string TrackName3 = "�������� ��������� ������������";
		public const string TracksName = "��������� ��������� ������������";

		public const string TrackDiscount = "�� ��������� {0} ��������� {1} �����!";
		public const string OneFreeCourse = "���� ���� � �������!";
		public const string FreeCourse = "���� � �������!";
		public const string FullGroupError = "������ ������� � ������ ����� 120 ����������";
		public const string OpenClasses = "������ � ������� ";
		public const string Discounts = " �� ��������";

		public const string NoCert = "����� ����������� ����� ���������� ������������ �� ��������";

		public const string ActionPrefix = "������ ";

		public const string Phone = "+7 (495) 232-32-16";
		public const string KarpovishPhone = "+7 (903) 700-66-33";

		public const string VacancyPhone = "+7 (495) 780-48-48";

		public static Dictionary<string, string> CourseTexts = new Dictionary<string, string>
	{{CourseTC.Rukpord, "���� ���� � ���� �� ������ ����������������, �� � ���������? � �������� ����� ��� �������������!"},
	{CourseTC.English,"��������� � ������������ ����������! ��� � ����������� �����. ������ ���� �������!"}};


	public static string OnDay{get {
			var day = DateTime.Today.Day;
			return " �� " + day +
				" " + MonthUtil.GetName(DateTime.Today.Month, true);
		}
		}

		public const string EmptyFirstName = "�� �������";
	}
}