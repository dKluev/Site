using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using Microsoft.Practices.Unity;
using NLog;
using Specialist.Entities.Context;
using Specialist.Services.Catalog;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.Cms.Repository;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Cms;
using Specialist.Web.Common.Cms.Interface;
using Specialist.Web.Common.Util;
using Specialist.Web.WinService.Properties;
using Specialist.Web.WinService.Tasks;
using Thread=System.Threading.Thread;

namespace Specialist.Web.WinService
{
    public partial class SiteSupportService : ServiceBase
    {
        readonly UnityContainer _container = new UnityContainer();
        public List<Task> _tasks = new List<Task>();

        public SiteSupportService()
        {
            InitializeComponent();
            UnityRegistrator.RegisterServices(_container);
                _container
                    .RegisterType<IContextProvider, CmsContextProvider>()
                    .RegisterType<IResponseLogicService, ResponseLogicService>()
                    ;
        }

        public void Run()
        {
            var taskTypes = new List<Type> { typeof(ResponseExport) };
            foreach (var taskType in taskTypes)
            {
                var task = _container.Resolve(taskType) as Task;
                task.Start();
                _tasks.Add(task);
            }
        }

        protected override void OnStart(string[] args)
        {
            Run();
            LogManager.GetCurrentClassLogger().Info("Started");
        }

        protected override void OnStop()
        {
            foreach (var task in _tasks)
            {
                task.Stop();
            }
            LogManager.GetCurrentClassLogger().Info("Stoped");
        }
    }
}
