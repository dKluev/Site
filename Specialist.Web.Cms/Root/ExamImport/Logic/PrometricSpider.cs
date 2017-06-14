using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using PrometricGrabber;
using Specialist.Entities.Examination.Const;
using Specialist.Services.Examination;

namespace PrometricImport {
    public static class PrometricSpider {
		public const string ExamFolder = "PrometricExam";
        static string rootPrometric = "http://www.register.prometric.com/";
        static string exam = rootPrometric + "Exam.asp";

        public static void DownloadAllExams()
        {
            var text = GetClienProgramText();
            var parser = new PageParser();
            var i = 0;
            var clients = parser.ParseClients(text);
            var clientCount = clients.Count;
            foreach (var client in clients)
            {
                ExamImportService.WriteStatus("Загрузка вендоров",Providers.PrometricExamType,i++, clientCount);
                foreach (var program in client.Programs)
                {
                    try
                    {
                        Request(client.Id, program.Key);
                    }
                    catch
                    {

                    }
                    Thread.Sleep(1000);
                }
              

            }
        }

        public static string GetClienProgramText() {
        	return Specialist.Web.Cms.Properties.Resources.PrometricVendors;
        }

        private static void Request(int clientID, int programID)
        {
            var examRegistration = "ValidateCountry=&CountryID=RUS&StateID=";
            examRegistration +=
                "&ProgramName=a&ClientName=a" +
                    "&ProgramID=" + programID +
                        "&ClientID=" + clientID + " &CountryID=RUS&StateID=";
            var cookies = new CookieCollection();
            cookies.Add(new Cookie("ExamRegistration", examRegistration , "/",
                "www.register.prometric.com"));
            var result = GetRequest(exam, cookies);
			ExamImportService.WriteFile(ExamFolder + "/client" +
                clientID + "program" + programID + ".htm",
                result[1]);
        }

        public static string[] GetRequest(string url, CookieCollection
            cookies)
        {
            if (!url.Contains("http"))
                url = rootPrometric + url;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.CookieContainer = new CookieContainer();
            if (cookies != null)
                request.CookieContainer.Add(cookies);

            using(var response = request.GetResponse()) {
            using(var stream = response.GetResponseStream()) {
            using (var reader = new StreamReader(stream))
            {
                var htmlText = reader.ReadToEnd();
                if (htmlText.Contains("moved"))
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlText);
                    var link = doc.DocumentNode.SelectNodes("//a[@href]").First()
                        .Attributes["href"];
                    return GetRequest(link.Value,
                        request.CookieContainer.GetCookies(request.RequestUri));
                }

                return new[] { url, htmlText };
            }
            	
            }
            	
            }


        }   
    }
}