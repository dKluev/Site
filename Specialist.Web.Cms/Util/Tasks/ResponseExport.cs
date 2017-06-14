using System;
using Microsoft.Practices.Unity;
using NLog;
using Specialist.Web.Cms;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Utils.Tasks;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Web.WinService.Tasks
{
    public class ResponseExportTask : TaskWithTimer
    {
        public override double Minutes
        {
            get { return 60 * 24; }
        }

        public override void TimerTick()
        {
            Cms.MvcApplication.Container
				.Resolve<IResponseLogicService>().ExportToRaws();
        }
    }
}