using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.PlannedTests.ViewModels {
	public class PlanTestQuestionStatsVM:IViewModel {

		public decimal GroupId { get; set; }

		public List<Tuple<int,int>> QuestionStats { get; set; }
		public Dictionary<int,string> QuestionNames { get; set; }

		public string Title { get { return "Статистика вопросов группы " + GroupId; } }
	}
}