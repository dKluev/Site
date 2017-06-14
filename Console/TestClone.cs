using System.Data.Linq.Mapping;
using System.IO;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Tests;
using SimpleUtils.Common.Extensions;

namespace Console {
	public class TestClone {
		private int TestId = 771;

		public void Start() {
			var contest = new SpecialistTestDataContext();
			var qIds = contest.TestQuestions.Where(x => x.TestId == TestId)
				.Select(x => x.Id).ToList();
			var answers = contest.GetTable<CloneAnswer>()
				.Where(x => qIds.Contains(x.QuestionId)).ToList();
			var dir = TestId.ToString();
			if(Directory.Exists(dir)) Directory.Delete(dir);
			Directory.CreateDirectory(dir);
			answers.AsParallel().ForAll(answer => {
				var url = "http://cdn.specialist.ru/Content/File/Test/Answer/{0}.jpg"
					.FormatWith(answer.SourceId);
				var image = Program.GetImage(url);
				if(image != null)
					image.Save(TestId + "/" + answer.Id + ".jpg");
				
			});

		}
		[TableAttribute(Name="SpecialistWeb.dbo.TestAnswers")]
		public class CloneAnswer {
			[Column]
			public int Id { get; set; }
			[Column]
			public int QuestionId { get; set; }
			[Column]
			public int? SourceId { get; set; }
		}
	}
}