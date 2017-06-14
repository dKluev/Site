using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Utils;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils;
using Htmls = Specialist.Web.Common.Html.Htmls;

namespace Specialist.Web.Root.SimpleTests.Logic {
	public class SimpleTestParser {

		public SimpleTest Parse(int id, SimpleTest.Blocks blocks) {
			var pairs = GetPairs(Htmls.HtmlBlock(blocks.Questions));
			var questions = pairs.Aggregate(new List<SimpleTest.Question>(),
			   (sl, t) => {
				   if (t.Item1 == q)
					   sl.Add(new SimpleTest.Question(t.Item2));
				   else if (t.Item1 == a)
					   sl.Last().Answers.Add(new SimpleTest.Answer {
						   Text = t.Item2
					   });
				   else if (t.Item1 == i)
					   sl.Last().Image = t.Item2;
				   return sl;
			   });

			var results = GetPairs(Htmls.HtmlBlock(blocks.Result)).Aggregate(new List<SimpleTest.Result>(),
			   (sl, t) => {
				   if (t.Item1 == r)
					   sl.Add(new SimpleTest.Result(){Text = t.Item2});
				   else if (t.Item1 == c)
					   sl.Last().Courses = t.Item2;
				   return sl;
			   });
			return new SimpleTest(id) {
				Name = blocks.Name,
				Description = Htmls.HtmlBlock(blocks.Description),
				Questions = questions,
				Results = results
			};

		}


		public Dictionary<int, SimpleTest> All() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				return SimpleTest.List.ToDictionary(x => x.Key, x => Parse(x.Key, x.Value));
			});
		}
		const string q = "q:";
		const string a = "a:";
		const string i = "i:";
		const string r = "r:";
		const string c = "c:";

		public List<Tuple<string, string>> GetPairs(string text) {
			var prev = ' ';
			var all = _.List(q, a, i, r,c);
			var cText = new StringBuilder();
			var cKey = "";
			var result = new List<Tuple<string, string>>();
			foreach (var ch in text) {
				var newKey = new string(new []{prev, ch});
				if (all.Contains(newKey)) {
					if (cText.Length > 1) {
						result.Add(Tuple.Create(cKey, cText.ToString().Trim()));
					}
					cText.Clear();
					cKey = newKey;
					prev = ' ';
				}
				else {
					cText.Append(prev);
					prev = ch;
				}
			}
			if (cText.Length > 0) {
				result.Add(Tuple.Create(cKey, cText.ToString()));
			}
			return result;
		}

	}
}