using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Web.Cms.Root.YandexDirect.Logic {
	public static class Phrases {
		public static List<string> ForCompetitors = new List<string> {
			"����� ���������� �� ���������",
			"����� ���������� ����������",
			"������������� �����",
			"����� �����������",
			"����� 1�"
		};

		public static List<string> YandexCompetitors = _.List(
			"������������� �����",
			"����� �����������",
			"����� 1�",
			"����� �������������� �����",
			"����� 1� �����������",
			"����� 1� �������� � �����",
			"����� 1� �����������",
			"����� 1� �������� � �����"
			); 

		public static List<string> ForWordStat = _.List(
			"�����",
			"��������",
			"����� �����������",
			"������������� �����",
			"����� 1�");
	}
}