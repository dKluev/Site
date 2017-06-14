using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SimpleUtils.Common.Extensions;

namespace Console {
	public class ProfessionsGenerator {
		private static Dictionary<int, string> professions = new Dictionary<int, string> {
			{1, "Менеджер интернет-проектов"},
			{2, "Контент-менеджер"},
			{3, "Администратор сайта"},
			{4, "Администратор баз данных"},
			{5, "Web-программист"},
			{6, "Web-дизайнер"},
			{7, "HTML-верстальщик"},
			{8, "Программист"},
			{9, "Программист 1С"},
			{10, "Системный администратор"},
			{11, "Системный инженер"},
			{12, "Сетевой администратор"},
			{13, "Сетевой инженер"},
			{14, "Специалист по информационной безопасности"},
			{15, "Оператор ПК"},
			{18, "Логист / Менеджер по логистике"},
			{19, "Менеджер по продажам"},
			{20, "Бухгалтер"},
			{21, "Сметчик"},
			{23, "Верстальщик"},
			{24, "Администратор 1С"},
			{25, "3D-аниматор"},
			{26, "Менеджер по персоналу"},
			{27, "IT-менеджер"},
			{28, "SEO"},
			{29, "Web-программист ASP .NET"},
			{30, "Web-программист PHP"},
			{31, "Администратор Oracle"},
			{32, "Разработчик баз данных"},
			{33, "Администратор корпоративных порталов"},
			{34, "Администратор почтовых систем"},
			{35, "Архитектор"},
			{36, "Кадровый менеджер"},
			{37, "Менеджер"},
			{38, "Видеоинженер"},
			{39, "Дизайнер интерьера"},
			{40, "Инженер Cisco"},
			{41, "Инженер по настройке и ремонту ПК"},
			{42, "Специалист службы поддержки (Helpdesk)"},
			{43, "Монтажник СКС"},
			{44, "Инженер-проектировщик"},
			{45, "Кассир"},
			{46, "Ландшафтный дизайнер"},
			{47, "Менеджер по маркетингу и рекламе"},
			{50, "Офис-менеджер"},
			{51, "Программист Java"},
			{52, "Разработчик бизнес-решений (BI)"},
			{53, "Администратор web-сайтов"},
			{54, "Тестировщик ПО"},
			{55, "Фотограф"},
			{56, "Системный администратор UNIX"},
			{57, "Системный администратор Microsoft"},
			{58, "Секретарь"},
			{59, "Оператор баз данных"},
			{60, "IT-директор (CIO)"},
			{62, "Визуализатор"},
			{63, "Художник"},
			{64, "Flash-аниматор"},
			{65, "Программист PHP"},
			{66, "Социолог"},
			{67, "Дизайнер"},
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