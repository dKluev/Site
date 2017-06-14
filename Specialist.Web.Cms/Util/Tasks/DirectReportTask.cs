using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net.Mail;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Facebook;
using Microsoft.Practices.Unity;
using NLog;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Common.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.Cms;
using Specialist.Web.Cms.Root.Socials;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Tasks;
using Specialist.Web.Const;
using Specialist.Web.Helpers;
using Logger = Specialist.Services.Utils.Logger;
using System.Linq;
using System.Xml.Linq;
using Console;
using Specialist.Services.Catalog.Extension;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using Group = Specialist.Entities.Context.Group;

namespace Specialist.Web.WinService.Tasks
{
    public class DirectReportTask : TaskWithTimer
    {

		DirectApiService DirectApiService = new DirectApiService(true);

		int CreateReport(int campainId) {
			return DirectApiService.CreateNewReport(new NewReportInfo {
				CampaignID = campainId,
				StartDate = DateTime.Today.ToString("yyyy-MM-dd"),
				EndDate = DateTime.Today.ToString("yyyy-MM-dd"),
				GroupByColumns = new []{"clPositionType"},
				Filter = new NewReportFilterInfo {
					PageType = "search"
				}
			});
		}

	    public string GetShowSearch(XElement element, string type) {
		    if (element.Elements().Any()) {
			    var t = element.Elements().FirstOrDefault(x => x.Attribute("position_type").Value == type);
			    if (t != null) {
				    return t.Attribute("shows_search").Value;
			    }
		    }
				
		    return "";
	    }

	    public Tuple<int, string, string> ParseReport(string report) {
		    var xml = XDocument.Parse(report);
		    var cId = xml.Root.Element("campaignID").Value;
		    var stat = xml.Root.Element("stat");
		    var premium = GetShowSearch(stat, "premium");
		    var other = GetShowSearch(stat, "other");
		    return Tuple.Create(int.Parse(cId), premium, other);
	    }

	    public List<Tuple<int, string, string>> CreateAndGetReports(List<int> ids) {
		    foreach (var id in ids) {
			    CreateReport(id);
		    }
			var reports = GetReports();
			ClearReports();
		    return reports;
	    }

	    public void ClearReports() {
			var reports = DirectApiService.GetReportList().Select(x => x.ReportID).ToList();
		    foreach (var report in reports) {
			    DirectApiService.DeleteReport(report);
		    }
	    }
		public List<Tuple<int, string, string>> GetReports() {
			while (true) {
				var reportInfos = DirectApiService.GetReportList().ToList();
				if(reportInfos.All(x => x.StatusReport == "Done"))
					return reportInfos.Select(x => ParseReport(HttpUtils.Get(x.Url))).ToList();
				
				Thread.Sleep(20000);
			}
		}


	    public override double Minutes
        {
            get { return 5; }
        }

	    public void SendMail() {
			ClearReports();
	        var allCampains = DirectApiService.GetActiveCompaigns().Where(x => campainIds.Contains(x.Item1));
	        var names = allCampains.ToDictionary(x => x.Item1, x => x.Item2);
	        var rows = names.Keys.ToList().GetRows(5);
		    var reportLines = new List<TagTr>();
		    foreach (var row in rows) {
		        var reports = CreateAndGetReports(row);
		        reportLines.AddRange(reports.Select(x => H.Row(names[x.Item1], x.Item2, x.Item3)));
		    }
			var report = H.table[H.Head("Компания","Премиум","Другие"), reportLines].ToString();
			var mailService = Cms.MvcApplication.Container.Resolve<IMailService>();
			mailService.Send(MailService.info,
			MailService.kgral, report, "Показы Директа");
		    
	    }


        public override void TimerTick() {
	        var hours = _.List(12, 16, 20);
	        var lastDate = GetDate();
	        if (lastDate.HasValue && lastDate.Value.Date < DateTime.Today) {
		        WriteDate();
		        lastDate = null;
	        }
	        var lastHour = lastDate.GetValueOrDefault().Hour;
	        var nextHour = hours.FirstOrDefault(x => x > lastHour);
	        if (nextHour == 0) return;
	        var howHour = DateTime.Now.Hour;
	        if (howHour >= nextHour) {
		        WriteDate();
				SendMail();
	        }
        }

	    private List<int> campainIds = _.List(
		    1766371,
		    11896863,
		    1071640,
		    727856,
		    11576119,
		    851285,
		    920306,
		    736714,
		    855394,
		    840641,
		    5210211,
		    1688161,
		    7979033,
		    779479,
		    911606);

	}
}
