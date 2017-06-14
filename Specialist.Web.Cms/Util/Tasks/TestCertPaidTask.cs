using System;
using Microsoft.Practices.Unity;
using NLog;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Web.Cms;
using Specialist.Web.Common.Utils.Tasks;
using System.Linq;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Web.WinService.Tasks
{
    public class TestCertPaidTask : TaskWithTimer
    {
        public override double Minutes
        {
            get { return 30; }
        }

        public override void TimerTick()
        {
            var orderService = Cms.MvcApplication.Container.Resolve<IRepository<OrderDetail>>();
            var orderDetails = orderService.GetAll()
                .Where(x => BerthTypes.AllPaidForTestCerts
					.Contains( x.StudentInGroup.BerthType_TC)
					&& x.UserTestId != null
					&& x.Order.MailCount < 2)
                .ToList();

            var mailService = Cms.MvcApplication.Container.Resolve<IMailService>();

            foreach (var orderDetail in orderDetails) {
                mailService.TestCertPaid(orderDetail);
                orderDetail.Order.MailCount = 2;
                orderService.SubmitChanges();
            }

        }
    }
}