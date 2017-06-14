
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Timers;
using Microsoft.Practices.Unity;
using NLog;
using Specialist.Entities.Context;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.Common.Cms.Interface;
using Specialist.Web.WinService.Properties;

namespace Specialist.Web.WinService.Tasks
{
    public class SomeTask: Task
    {
      

        [Dependency]
        public IBlockCreator BlockCreator { get; set; }

        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }


        public override double Minutes
        {
            get { return 60 * 24; }
        }

        public override void TimerTick()
        {
            Check();
            LogManager.GetCurrentClassLogger().Info("check");
        }

        private static void UpdateActiveByActualDate()
        {
            var context = new SpecialistWebDataContext();
            var banners = context.Banners
                .Where(b => b.ActualDate < DateTime.Today && b.IsActive);
            foreach (var banner in banners)
            {
                banner.IsActive = false;
            }
            context.SubmitChanges();

        }


        private void Check()
        {

            UpdateActiveByActualDate();
            var blockWithoutContentCount = BlockCreator.GetBlocksWithoutContent().Count;
            var outdatedRelationCount = SiteObjectService.GetRelationWithNotActive().Count;
            if (blockWithoutContentCount > 0 || outdatedRelationCount > 0)
                SendMail(CreateBody(blockWithoutContentCount, outdatedRelationCount));

        }
        private readonly string _withoutContent =
            Settings.Default.CmsUrl + "/Block/WithoutContent";
        private readonly string _siteObjectRelationLink =
            Settings.Default.CmsUrl + "/SiteObjectRelation/OutdatedList";


        private string CreateBody(int blockWithoutContentCount, int outdatedRelationCount)
        {
            var result = string.Empty;
            if (blockWithoutContentCount > 0)
                result +=
                    string.Format("<a href={1}>Блоки без контента [{0}]</a><br/>",
                        blockWithoutContentCount, _withoutContent);
            if (outdatedRelationCount > 0)
                result += string.Format("<a href={1}>Устаревшие связи [{0}]</a>",
                    outdatedRelationCount, _siteObjectRelationLink);
            return result;

        }

        private void SendMail(string body)
        {
            /*   var sclient = new SmtpClient();
            sclient.Host = "ms-a-0001.specialist.ru";
            sclient.Credentials = new NetworkCredential("ptolochko", "saoPRY");
            sclient.Port = 25;

            foreach (var email in Settings.Default.EmailList)
            {
                var from = new MailAddress("cms@specialist.ru", "CMS", Encoding.UTF8);
                var to = new MailAddress(email);
                var msg = new MailMessage(from, to);
                msg.IsBodyHtml = true;
                msg.Body = body;
                msg.BodyEncoding = Encoding.UTF8;
                msg.Subject = "CMS message.";
                sclient.Send(msg);
            }*/


        }
    }
}