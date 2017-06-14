using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Console;
using Newtonsoft.Json.Linq;
using SimpleUtils.Collections;
using SimpleUtils.Extension;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Utils;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using Specialist.Web.Cms.Util;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Tasks;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Services.Catalog.Extension;
using Microsoft.Practices.Unity;

namespace Specialist.Web.WinService.Tasks {
	public class YandexCompetitorsTasks : TaskWithTimer {

		public override double Minutes {
			get { return 24*60; }
		}

		public override void TimerTick() {
			var db = YandexParser.GetYandexCompetitorDB();
			if(db.Items.Count > 0 && db.Items.First().Date == DateTime.Today)
				return;
			Task.Factory.StartNew(() => {
				for (int i = 0; i < Phrases.YandexCompetitors.Count; i++) {
					var competitor = YandexParser.GetYandexCompetitors(i);
					db.Add(competitor);
					Thread.Sleep(60000);
				}
				db.Save();
			});
		}

	}
}