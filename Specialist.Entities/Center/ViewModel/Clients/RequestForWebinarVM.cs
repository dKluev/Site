using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Context.Const;
using System.ComponentModel;

namespace Specialist.Entities.Center.ViewModel
{
    public class RequestForWebinarVM : IDataErrorInfo
    {
        public string Result { get; set; }
        public string YourName { get; set; }
        public string YourEmail { get; set; }
        public string YourText { get; set; }
        
        public string Error { get { return null; } }

            public string this[string propName]
            {
                get
                {
                    if ((propName == "YourName") && string.IsNullOrEmpty(YourName))
                        return "Пожалуйста, введите имя";
                    if ((propName == "YourEmail") && string.IsNullOrEmpty(YourEmail))
                        return "Пожалуйста, введите Ваш телефон или E-mail";
                    if ((propName == "YourText") && string.IsNullOrEmpty(YourText))
                        return "Пожалуйста, введите информацию о курсе";
                    return null;
                }
            }

            public void Submit()
            {
                EnsureCurrentlyValid();
                var message = new StringBuilder();

                message.AppendFormat("Имя: {0} \n", YourName);
                message.AppendFormat("Телефон(e-mail): {0} \n", YourEmail);
                message.AppendFormat("Курс, пожелания к заказу: {0} \n", YourText);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(new MailMessage(
                    "info@specialist.ru",
                    "dinzis@specialist.ru",
                    "Заявка на вебинар",
                    message.ToString()));

                Result = "Заявка отправлена";
            }
            
            private void EnsureCurrentlyValid()
            {
                var propsToValidate = new[] { "YourName", "YourEmail", "YourText" };
                bool IsValid = propsToValidate.All(x => this[x] == null);

                if (!IsValid)
                    throw new InvalidOperationException("Невозможно отправить заявку");
            }
    }
}
