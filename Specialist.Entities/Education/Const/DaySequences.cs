using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class DaySequences {
		public const string Saturday = "��"; //�������
		public const string Sunday = "��"; //�����������
		public const string Highday = "��";
		
		public static string GetName(string tc) {
			switch(tc)
			{
				case Saturday:
					return "�������";
				case Sunday:
					return "�����������";
				case Highday:
					return "����������� �������� ���";
				default:
					return string.Empty;

			}
		}

		public static readonly List<string> GetAll =
			_.List(Saturday, Sunday, Highday);

		
	}
}