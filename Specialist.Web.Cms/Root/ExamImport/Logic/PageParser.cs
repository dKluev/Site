using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using PrometricImport;
using SimpleUtils.Common.Extensions;

namespace PrometricGrabber
{
    public class PageParser
    {
        const string selectStart =
            @"<select name=""{0}""";
        const string selectEnd = "</select>";

        const string option = "<option";

        public List<Client> ParseClients(string page)
        {
            var result = GetOptions(page, "LboClients")
                .Select(pair => new Client{Id = pair.Key, Name = pair.Value})
                .ToList();
            foreach (var client in result)
            {
                var ifText = "if (clientID == " + client.Id + ")";
                var clientIfPart = page.Between(ifText, "}");
                var programParts = clientIfPart.Split("document.ClientProgram.LboPrograms");
                foreach (var ifPart in programParts)
                {
                    var m2 = Regex.Match(ifPart,
                        @"new Option\('(.*?)'.*,\s'(.*?)'", RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        client.Programs.Add(int.Parse(m2.Groups[2].Value), 
                            m2.Groups[1].Value);
                    }
                }
            }
            return result;
        }

        private Dictionary<int, string> GetOptions(string page, string name)
        {
            var optionText = page.Between(string.Format(selectStart, name), selectEnd)
                .Split(option);
            var options = new Dictionary<int, string>();
            foreach (var op in optionText)
            {
                var m2 = Regex.Match(op, @"value=""(.*?)"">(.*?)</option>",
                    RegexOptions.Singleline);
                if (m2.Success)
                {
                    options.Add(int.Parse(m2.Groups[1].Value), m2.Groups[2].Value);
                }
            }
            return options;
        }

        public List<ProviderExam> ParseExams(string page)
        {
            var result = new List<ProviderExam>();
            if(page.Contains("No Exams Found"))
                return result;
            var options = GetOptions(page, "lboexamaliases");

            foreach (var pair in options)
            {
                result.Add(new ProviderExam(pair.Value) { Id = pair.Key});
            }

            foreach (var exam in result)
            {
                var ifText = "if (theExam ==  " + exam.Id + ")";
                var all = page.BetweenAll(ifText, "}");
                foreach (var ifPart in all)
                {
                    var m2 = Regex.Match(ifPart,
                        @"theLanguage   ==  '(.*?)'.*writeThePrice\('(.*?)\sUSD", RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        exam.Languages.Add(m2.Groups[1].Value);
						exam.Price = int.Parse(Regex.Replace(m2.Groups[2].Value, 
							@"\.\d\d", ""));
                    }
                }
            }
            return result;
        }

       
    }
}