using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SimpleUtils.Common.Extensions;

namespace Console {
	public class ProfessionsGenerator {
		private static Dictionary<int, string> professions = new Dictionary<int, string> {
			{1, "�������� ��������-��������"},
			{2, "�������-��������"},
			{3, "������������� �����"},
			{4, "������������� ��� ������"},
			{5, "Web-�����������"},
			{6, "Web-��������"},
			{7, "HTML-�����������"},
			{8, "�����������"},
			{9, "����������� 1�"},
			{10, "��������� �������������"},
			{11, "��������� �������"},
			{12, "������� �������������"},
			{13, "������� �������"},
			{14, "���������� �� �������������� ������������"},
			{15, "�������� ��"},
			{18, "������ / �������� �� ���������"},
			{19, "�������� �� ��������"},
			{20, "���������"},
			{21, "�������"},
			{23, "�����������"},
			{24, "������������� 1�"},
			{25, "3D-��������"},
			{26, "�������� �� ���������"},
			{27, "IT-��������"},
			{28, "SEO"},
			{29, "Web-����������� ASP .NET"},
			{30, "Web-����������� PHP"},
			{31, "������������� Oracle"},
			{32, "����������� ��� ������"},
			{33, "������������� ������������� ��������"},
			{34, "������������� �������� ������"},
			{35, "����������"},
			{36, "�������� ��������"},
			{37, "��������"},
			{38, "������������"},
			{39, "�������� ���������"},
			{40, "������� Cisco"},
			{41, "������� �� ��������� � ������� ��"},
			{42, "���������� ������ ��������� (Helpdesk)"},
			{43, "��������� ���"},
			{44, "�������-�������������"},
			{45, "������"},
			{46, "����������� ��������"},
			{47, "�������� �� ���������� � �������"},
			{50, "����-��������"},
			{51, "����������� Java"},
			{52, "����������� ������-������� (BI)"},
			{53, "������������� web-������"},
			{54, "����������� ��"},
			{55, "��������"},
			{56, "��������� ������������� UNIX"},
			{57, "��������� ������������� Microsoft"},
			{58, "���������"},
			{59, "�������� ��� ������"},
			{60, "IT-�������� (CIO)"},
			{62, "������������"},
			{63, "��������"},
			{64, "Flash-��������"},
			{65, "����������� PHP"},
			{66, "��������"},
			{67, "��������"},
		};

		static string String(Tuple<string,string> t) {
			return t.Item1 + ";" + t.Item2;
		}


		public void Run() {
			var result = new List<Tuple<Tuple<string, string>, List<Tuple<string, string>>>>();
			var document = XDocument.Load("catalog.xml");
			var categories = document.Root.Elements();
			foreach (var p in professions) {
				var superProf = categories.Select(x => {
					var name = x.Element("name").Value.ToLower();
					var ourName = p.Value.ToLower();
					return new {
						Contains = name.Contains(ourName) || ourName.Contains(name),
						Equal = name == ourName,
						E = x
					};
				}).OrderByDescending(x => x.Equal).Where(x => x.Contains || x.Equal)
					.Select(x => Tuple.Create(x.E.Element("id").Value,
						x.E.Element("name").Value)).ToList();

				result.Add(Tuple.Create(Tuple.Create(p.Key.ToString(), 
					p.Value), superProf));
			}
			var text = result.Select(x => string.Join(",", String(x.Item1),
				x.Item2.Select(String).JoinWith(","))).JoinWith(Environment.NewLine);
			File.WriteAllText("result.txt", text);

			System.Console.Read();
		}
	}
}