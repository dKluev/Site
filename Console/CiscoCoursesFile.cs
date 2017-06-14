using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Extension;
using Specialist.Web.Helpers;

namespace Console {
	public class CiscoCoursesFile {
		Dictionary<string, int> Courses = new Dictionary<string, int> {
{"ИРОНП1",5599},
{"ЦВМН-А",5236},
{"ДЦНИД-А",5248},
{"АЦЕСМ",5193},
{"АМПЛС",5499},
{"АРКЦИС-Б",5651},
{"БГП-А",4807},
{"САНАС",5052},
{"КАПС",5623},
{"ЦИРС1",5931},
{"ЦИРС2",5932},
{"ЦИПТ1-Б",5618},
{"ЦИПТ2-В",5619},
{"СРС1",4789},
{"ЦСЕ",5655},
{"КУВС",5751},
{"ЦВОИС-Б",5620},
{"СВЛАТ",5114},
{"ЦВЛФ",5112},
{"ЦВЛМС",5702},
{"ДЦМДС",5804},
{"ДКАД",5851},
{"ДИЗЦИС",5027},
{"ФАЕРВОЛ-А",5780},
{"ИАЮВС",5758},
{"ИЦНД1-А",5878},
{"ИЦНД2-А",5879},
{"ИКОММ",5881},
{"ИИНС",5782},
{"АЙПИ",5653},
{"ИПС",5625},
{"ИЮВМС",5759},
{"ИЮНЕ",5755},
{"ИЮВН",5513},
{"МКАСТ",5654},
{"ДМЦЕ",2496},
{"МПЛС",5543},
{"МПЛСТ",4175},
{"КЬЮОС-А",5591},
{"ПАТВ1",5685},
{"ПАТВ2",5686},
{"ПАЕТ",5687},
{"РОУТ",5558},
{"РСКАТ6К",5737},
{"СЕНС",5938},
{"СИМОС",5939},
{"СИСАС",5937},
{"СИТКС",5940},
{"СПАДВРОУТ",5796},
{"СПКОР",5797},
{"СПЕДЖ",5798},
{"СПНГН1",5778},
{"СПНГН2",5779},
{"СПРОУТ",5795},
{"СВИЧ",5557},
{"ШУТ",5559},
{"ВОЙС",5622},
{"УКАД",5692},
{"УЕМИ",5492},
{"ВПНРАЗ",5757},

		};

		public void Write() {
			var courseTCs = Courses.Select(x => x.Key).ToList();

			var context = new SpecialistDataContext();
			var courseData = context.Courses.Select(x => new {x.Course_TC, x.UrlName})
				.ToDictionary(x => x.Course_TC, x => x.UrlName);
			var groups = context.Groups.Where(x => courseTCs.Contains(x.Course_TC)).NotBegin()
				.Where(x => x.DateEnd != null)
				.Select(x => new {x.Course_TC, x.DateBeg, 
					x.DateEnd, x.TimeBeg, x.TimeEnd}).OrderBy(x => x.DateBeg).ToList();
			var data = new List<List<string>>();
			foreach (var gr in groups) {
				var en = new CultureInfo("en-US");
				var row = _.List(
					Courses[gr.Course_TC.Trim()].ToString(),
					"Russian",
					"RUS",
					"MOSKVA",
					"GOSPITALNY LANE 4/6 ",
					"MOSKVA",
					"MOSKVA",
					"105005",
					gr.DateBeg.Value.ToString("dd-MMM-yyyy", en),
					gr.DateEnd.Value.ToString("dd-MMM-yyyy", en),
					"ILT",
					"",
					"http://www.specialist.ru/course/" + courseData[gr.Course_TC.Trim()],
					"+7 (495)7804848",
					"",
					"info@specialist.ru",
					"57017");
				data.Add(row);
			}
			File.WriteAllText("cisco.csv", CsvUtil.Render(data));
		}
		 
	}
}