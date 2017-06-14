using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Tests;
using Specialist.Web.Cms.Root.YandexDirect.Logic;

namespace Console {
	public class TestExport {

		protected XElement X(string name, params object[] content) {
			return new XElement(name, content);
		}
		protected XElement X(XName name, params object[] content) {
			return new XElement(name, content);
		}

		protected XAttribute A(string name , object value) {
			value = value ?? string.Empty;
			return new XAttribute(name, value);
		}
		public void Run() {
			var testId = 482;

			var contest = new SpecialistTestDataContext();

			var test = contest.Tests.FirstOrDefault(x => x.Id == testId);

			Images(test);

			Directory.CreateDirectory(testId.ToString());
			var xml = X("Test", A("name", test.Name),  X("Questions", test.TestQuestions.Select(Question)))
				.ToString();
            
			File.WriteAllText(testId + "/test.xml", xml);
		}

		public object Question(TestQuestion q) {
			return X("Question", A("Type", q.Type), Img(q.TestId, "question", q.Id), 
				X("Description",  q.Description), X("Answers", q.TestAnswers.Select(Answer)));
		}

		public XAttribute Img(int testId, string name, int id) {
			if (File.Exists(GetFileName(testId, name, id))) {
				return A("Image", id + ".png");
			}
			return null;
		}
        
		public object Answer(TestAnswer a) {
			return X("Answer", A("IsRight", a.IsRight), Img(a.TestQuestion.TestId, "answer", a.Id), 
				X("Description", a.Description));
		}



		public void Images(Test test) {
			var qIds = test.TestQuestions.Select(x => x.Id).ToList();
			var aIds = test.TestQuestions.SelectMany(x => x.TestAnswers.Select(y => y.Id)).ToList();
			var testId = test.Id;
			var dir = testId.ToString();
			if(Directory.Exists(dir)) Directory.Delete(dir,true);
			Directory.CreateDirectory(dir);
			Directory.CreateDirectory(dir + "/question");
			Directory.CreateDirectory(dir + "/answer");
			LoadImage(qIds, "question", testId);
			LoadImage(aIds, "answer", testId);

		}

		string GetFileName(int testId, string name, int id) {
			return  testId + "/" + name + "/" + id + ".png";
		}

		private void LoadImage(List<int> ids, string name, int testId) {
			ids.AsParallel().ForAll(id => {
				var url = "http://cdn.specialist.ru/Content/File/Test/{0}/{1}.png"
					.FormatWith(name, id);
				var image = Program.GetImage(url);
				if (image != null)
					image.Save(GetFileName(testId, name, id));
			});
		}
	}
}