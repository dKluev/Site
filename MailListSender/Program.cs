using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Web.Common.Util;
using Specialist.Web.Root.Common.MailList;

namespace MailListSender {
	class Program {
		static void Main(string[] args) {

			var container = new UnityContainer();
			UnityRegistrator.RegisterServices(container);

			var service = container.Resolve<MailListService>();
			var mailTemplate = service.GetTemplate();
			using(var client = new SmtpClient()) {
				foreach (var email in 
					"ptolochko@specialist.ru,ptolochko@mail.ru".Split(',')) {
					service.Send(client, mailTemplate.V1, email);
				}
			}
			service = container.Resolve<MailListService>();
			Console.WriteLine("current = " + service.CurrentUser.GetValueOrDefault());
			Console.WriteLine(service.CurrentUser.HasValue 
				? "Продолжить рассылку?" : "Рассылать?");
			Console.ReadKey();
			Console.WriteLine(service.GetSendedPercent().GetValueOrDefault()
				.ToString("n2") + "%");
			service.SendForAll();
			Console.WriteLine("done");
			Console.ReadKey();
		}
	}
}
