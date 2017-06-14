using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Common.Const;

namespace Specialist.Web.Root.SimpleTests.Logic {
	public class SimpleTest:IViewModel {

		public class Blocks {
			public int Description { get; set; }
			public int Questions { get; set; }
			public int Result { get; set; }
			public string Name { get; set; }
			public Blocks(string name, int description, int questions, int result) {
				Name = name;
				Description = description;
				Questions = questions;
				Result = result;
			}
		}

		public static Dictionary<int, Blocks> List = new Dictionary<int, Blocks> {
			{1, new Blocks("Кто же Вы - Есенин, Ньютон или Леонардо Да Винчи?",
				HtmlBlocks.Tests.Newton, HtmlBlocks.Tests.NewtonQuestions, HtmlBlocks.Tests.NewtonResults)},
			{2, new Blocks("Диагностика творческого потенциала и креативности",
				HtmlBlocks.Tests.Creative, HtmlBlocks.Tests.CreativeQuestions,
				HtmlBlocks.Tests.CreativeResults)},
		}; 
		public class Answer {
			public string Text { get; set; }
		}

		public class Result {
			public string Text { get; set; }
			public string Courses { get; set; }
		}
		public class Question {
			public Question(string text) {
				Text = text;
				Answers = new List<Answer>();
			}

			public string Image { get; set; }

			public string Text { get; set; }

			public List<Answer> Answers { get; set; }
		}

		public SimpleTest(int id) {
			Id = id;
		}
		public int Id { get; set; }
		public string Name { get; set; }

		public string Description { get; set; }

		public List<Question> Questions { get; set; }

		public List<Result> Results { get; set; }

		public string Title { get { return "Тест " + Name; } }
	}
}
