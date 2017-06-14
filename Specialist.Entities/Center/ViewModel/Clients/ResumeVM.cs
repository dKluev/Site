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
using Specialist.Entities.Profile.ViewModel;

namespace Specialist.Entities.Center.ViewModel
{
//    public class ResumeVM : IViewModel
    public class ResumeVM : IDataErrorInfo
    {
            public UploadFile uploadFile { get; set; }
            public string YourName { get; set; }
            public string YourPatronymic { get; set; }
            public string YourSurname { get; set; }
            public string YourAge { get; set; }
            public string YourSex { get; set; }
            public string YourEducation { get; set; }
            public string YourPosition { get; set; }
            public string YourExperience { get; set; }
            public string YourProfit { get; set; }
            public string YourCurrency { get; set; }
            public string YourCity { get; set; }
            public string YourMetro { get; set; }
            public string YourPeriod { get; set; }
            public string YourEmail { get; set; }
            public string YourTelHome { get; set; }
            public string YourTelJob { get; set; }
            public string YourTelMob { get; set; }
            public string Result { get; set; }
            public bool s1 { get; set; }
            public bool s2 { get; set; }
            public bool s3 { get; set; }
            public bool s4 { get; set; }            
            public bool s5 { get; set; }
            public bool s6 { get; set; }
            public bool s7 { get; set; }
            public bool s8 { get; set; }
            public bool s9 { get; set; }
            public bool s10 { get; set; }
            public bool s11 { get; set; }
            public bool s12 { get; set; }
            public bool s13 { get; set; }
            public bool s14 { get; set; }
           
            public string Error {get {return null;}}

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
                    if ((propName == "YourAge") && string.IsNullOrEmpty(YourAge))
                        return "Пожалуйста, введите возраст";
                    if ((propName == "YourEducation") && string.IsNullOrEmpty(YourEducation))
                        return "Пожалуйста, введите образование";
                    if ((propName == "YourPosition") && string.IsNullOrEmpty(YourPosition))
                        return "Пожалуйста, введите желаемую должность";
                    if ((propName == "YourProfit") && string.IsNullOrEmpty(YourProfit))
                        return "Пожалуйста, введите желаемую зарплату";
                    if ((propName == "YourCity") && string.IsNullOrEmpty(YourCity))
                        return "Пожалуйста, введите город";
                    if ((propName == "s1") && !s1 && !s2 && !s3 && !s4 && !s5 && !s6 && !s7 && !s8 && !s9 && !s10 && !s11 && !s12 && !s13 && !s14)
                        return "Пожалуйста, выберите раздел";
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
               
                var answer = new StringBuilder();

                answer.AppendFormat("Здравствуйте, {0} !\n\n", YourName);
                answer.Append("Благодарим Вас за размещение резюме на сайте www.specialist.ru.\n");
                answer.Append("----------------------------------------------------------------------------\n\n");
                answer.Append("Задать любые вопросы по карьере Вы можете на странице Службы трудоустройства http://beta.specialist.ru/Job или по телефону +7 (499) 138-36-48. Ваш консультант Сурикова Маргарита Анатольевна\n\n");
                answer.Append("С уважением,\n");
                answer.Append("Служба трудоустройства центра компьютерного обучения \"Специалист\"\n\n");
                answer.Append("ВНИМАНИЕ! ЭТО ПИСЬМО БЫЛО АВТОМАТИЧЕСКИ ОТПРАВЛЕНО С СЕРВЕРА. ОТВЕТ НА НЕГО ОБРАБОТАН НЕ БУДЕТ. ДЛЯ ОБРАТНОЙ СВЯЗИ ИСПОЛЬЗУЙТЕ АДРЕС job@specialist.ru");

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(new MailMessage(
                    "info@specialist.ru",
                    YourEmail,
                    "Благодарим за размещение резюме на сайте www.specialist.ru",
                    answer.ToString())); 

                Result = "Резюме отправлено";
            }
            
            private void EnsureCurrentlyValid()
            {
                var propsToValidate = new[] { "YourName", "YourPatronymic", "YourSurname", "YourAge", "YourEducation", "YourPosition", "YourProfit", "YourCity", "s1", "YourEmail" };
                bool IsValid = propsToValidate.All(x => this[x] == null);

                if (!IsValid)
                    throw new InvalidOperationException("Невозможно отправить резюме");
            }
    }
}
