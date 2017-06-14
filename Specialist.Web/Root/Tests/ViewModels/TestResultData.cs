using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestResultData {
		public class ComparisonAnswer {
			public List<int> Left { get; set; }
			public List<int> Right { get; set; }
			public ComparisonAnswer() {
				Left = new List<int>();
				Right = new List<int>();
			}

			public override string ToString() {
				return StringUtils.Unsplit(Left.Cast<object>()) + ";" +
					StringUtils.Unsplit(Left.Cast<object>());
			}
			
		}
		public class QuestionAnswer {
			public int QuestionId { get; set; }
			public int? OneAnswer { get; set; }
			public List<int> ManyAnswers { get; set; }
			public List<int> Sort { get; set; }
			public ComparisonAnswer Comparison { get; set; }
			public QuestionAnswer() {
				Comparison = new ComparisonAnswer();
				Sort = new List<int>();
				ManyAnswers = new List<int>();
			}
			public string GetText() {
				if(OneAnswer.HasValue)
					return OneAnswer.Value.ToString();
				if(ManyAnswers.Any())
					return StringUtils.Unsplit(ManyAnswers.Cast<object>());
				if(Sort.Any())
					return StringUtils.Unsplit(Sort.Cast<object>());
				if(Comparison.Left.Any())
					return Comparison.ToString();
				return string.Empty;

			}
		}

		public List<TestResultData.QuestionAnswer> Data { get; set; }
		
	}
}