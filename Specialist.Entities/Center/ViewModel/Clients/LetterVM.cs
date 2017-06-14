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
using Specialist.Entities.Passport;

namespace Specialist.Entities.Center.ViewModel {
    public class LetterVM : IDataErrorInfo 
    {
        public string YourName { get; set; }
        public string YourPatronymic { get; set; }
        public string YourSurname { get; set; }
        public string YourEmail { get; set; }
        public string YourText { get; set; }
        public string Result { get; set; }

        public User user { get; set; }

        private bool _isStartSearch = false;

        public bool isStartSearch
        {
            get
            {
                return _isStartSearch;
            }
            set
            {
                _isStartSearch = value;
            }
        }

        public string Error { get { return null; } }

         public string this[string propName]
         {
             get
             {
                 if ((propName == "YourName") && string.IsNullOrEmpty(YourName))
                     return "Пожалуйста, введите имя";
                 if ((propName == "YourPatronymic") && string.IsNullOrEmpty(YourPatronymic))
                     return "Пожалуйста, введите отчество";
                 if ((propName == "YourSurname") && string.IsNullOrEmpty(YourSurname))
                     return "Пожалуйста, введите фамилию";
                 if ((propName == "YourEmail") && string.IsNullOrEmpty(YourEmail))
                 {
                     return "Пожалуйста, введите E-mail адрес";
                 }
                 else if ((propName == "YourEmail") && !Regex.IsMatch(YourEmail, ".+\\@.+\\..+"))
                 {
                     return "Пожалуйста, введите правильный E-mail адрес";
                 }

                 return null;
             }
         }

        public void Submit()
        {
            EnsureCurrentlyValid();

            var message = new StringBuilder();
//            var answer = new StringBuilder();

            message.AppendFormat("Имя {0} \n", YourName);
            message.AppendFormat("Отчество {0} \n", YourPatronymic);
            message.AppendFormat("фамилия {0} \n", YourSurname);
            message.AppendFormat("E-mail {0} \n", YourEmail);
            message.AppendFormat(" {0} \n", YourText);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(new MailMessage(
                "info@specialist.ru",
                "job@specialist.ru",
                "Вопрос к консультанту",
                message.ToString()));
/*
            answer.AppendFormat("Здравствуйте, {0} !\n\n", YourName);
            answer.Append("Мы получили Ваш вопрос.\n");
            answer.Append("Ответ на вопрос Вы получите по e-mail в ближайшее время.\n");
            answer.Append("----------------------------------------------------------------------------\n\n");
            answer.Append("Задать любые вопросы по карьере Вы можете на странице Службы трудоустройства http://beta.specialist.ru/Job или по телефону +7 (499) 138-36-48. Ваш консультант Сурикова Маргарита Анатольевна\n\n");
            answer.Append("С уважением,\n");
            answer.Append("Служба трудоустройства центра компьютерного обучения \"Специалист\"\n\n");
            answer.Append("ВНИМАНИЕ! ЭТО ПИСЬМО БЫЛО АВТОМАТИЧЕСКИ ОТПРАВЛЕНО С СЕРВЕРА. ОТВЕТ НА НЕГО ОБРАБОТАН НЕ БУДЕТ. ДЛЯ ОБРАТНОЙ СВЯЗИ ИСПОЛЬЗУЙТЕ АДРЕС job@specialist.ru");

            smtpClient.Send(new MailMessage(
                "info@specialist.ru",
                YourEmail,
                "Ваш вопрос в службу трудоустройства центра \"Специалист\" получен" ,
                answer.ToString()));
            */

            Result = "Письмо отправлено";
        }

        private void EnsureCurrentlyValid()
        {
            var propsToValidate = new[] { "YourName", "YourPatronymic", "YourSurname", "YourEmail" };
            bool IsValid = propsToValidate.All(x => this[x] == null);

            if (!IsValid)
                throw new InvalidOperationException("Невозможно отправить письмо");
        }

    }
}