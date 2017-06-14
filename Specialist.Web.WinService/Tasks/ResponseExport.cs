using System;
using Microsoft.Practices.Unity;
using NLog;
using Specialist.Web.Cms.Services;

namespace Specialist.Web.WinService.Tasks
{
    public class ResponseExport : Task
    {
        [Dependency]
        public IResponseLogicService ResponseLogicService { get; set; }

        public override double Minutes
        {
            get { return 60 * 24; }
        }

        public override void TimerTick()
        {
            ResponseLogicService.ExportToRaws();
            LogManager.GetCurrentClassLogger().Info("Responses is exported");
        }
    }
}