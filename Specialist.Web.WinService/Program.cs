using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.ServiceProcess;
using System.Text;
using NLog;

namespace Specialist.Web.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
//                new SiteSupportService().Run();
                var servicesToRun = new ServiceBase[] 
        			{ 
        				new SiteSupportService() 
        			};
                ServiceBase.Run(servicesToRun);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().ErrorException(ex.Message, ex);
            }
            
        }
    }
}
