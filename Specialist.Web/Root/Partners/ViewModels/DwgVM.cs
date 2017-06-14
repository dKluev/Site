using System.Collections.Generic;
using SimpleUtils.Common;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using System.Linq;

namespace Specialist.Web.Root.Partners.ViewModels {
	public class DwgVM {
		public static List<Tuple<string, List<string>>> CourseTCs =
			_.List(
				Tuple.New("Базовые САПР", _.List(
					"АКАД20111",
					"АКАД20112",
					"АКАД20113Д",
					"ЛИСП",
				//	"Т-АКАД-З",
					"ЧЕРЧ"
					)), Tuple.New("Инженерные САПР (машиностроение)", _.List(
						"ИНВ-Г",
						"КОСВ-А",
						"ФЛОВ-А",
						"ФОТВ-А",
						"АС1-Г",
						"СОЛВ1",
						"СОЛВ2",
						"СОЛВ3С",
						"СОЛВ3Д",
						"СОЛВ3Л",
						"ПРИНЖ",
						"ПКАД-А"
						)), Tuple.New("Архитектурные САПР, для промышленного и гражданского строительства", _.List(
							"АРХ1-Б",
							"АРХ2-Б",
							"АРТС",
						//	"Т-АРХАРТ",
						//	"Т-АРХ-Б",
							"МЕП2010",
							"ЦИВИЛ-Б",
							"РЕВ2010",
							"РЕВМЕП"
							)), Tuple.New("3D моделирование, визуализация и анимация", _.List(
								"3ДМ1-И",
								"ФШ3Д",
								"3ДМ2-И",
								"3ДМ3А-В",
								"3ДМ3Б-В",
								"3ДМ4-Б",
								"ВРЭЙ1-А",
								"ВРЭЙ2",
								"МАЯ1-В",
								"ЗБРАШ",
								"РИНОЦ-А",
//								"Т-3ДМ-О",
//								"Т-3ДМР3-Ж",
//								"Т-3ДМР-Н",
//								"Т-3ДМ3-Е",
								"МАЯ1-В",
								"ФШ3Д"
								)), Tuple.New("Сметные расчеты", _.List(
									"СДС",
									"СМЕТ1",
//									"Т-СМЕТ-А",
									"АДМСМЕТ"
									)), Tuple.New("Компьютерная графика", _.List(
										"ФШ1-Д",
										"КД-Д",
										"ФШ2-Д",
										"ИЛ1-И"
										))
				);

		public List<Tuple<string, List<Tuple<CourseLink, List<Group>>>>> Courses { get; set; }
	}
}