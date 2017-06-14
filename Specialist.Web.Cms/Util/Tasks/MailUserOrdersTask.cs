using System;
using Microsoft.Practices.Unity;
using NLog;
using Specialist.Entities.Context;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Web.Cms;
using Specialist.Web.Common.Utils.Tasks;
using System.Linq;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Web.WinService.Tasks
{
    public class MailUserOrdersTask : TaskWithTimer
    {
        public override double Minutes
        {
            get { return 60 * 24; }
        }

        public override void TimerTick()
        {
            var orderService = Cms.MvcApplication.Container.Resolve<IRepository<Order>>();

            var orders = orderService.GetAll()
                .Where(x => x.PaymentType_TC == null)
                .Where(x => x.MailCount == 0)
                .Where(x => x.UpdateDate.Date == DateTime.Today.AddDays(-1))
                .ToList().Where(x => x.TotalPriceWithDescount > 0).ToList();

            var mailService = Cms.MvcApplication.Container.Resolve<IMailService>();

            foreach (var order in orders) {
                mailService.OrderStarted(order);
                order.MailCount += 1;
                orderService.SubmitChanges();
            }

        }
    }
}