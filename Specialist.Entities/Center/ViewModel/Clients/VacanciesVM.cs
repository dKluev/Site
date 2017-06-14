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
    //    public class ResumeVM : IViewModel
    public class VacanciesVM : IDataErrorInfo
    {
        public string YourPosition { get; set; }
        public string YourCity { get; set; }
        public string YourText { get; set; }
        public string YourCompany { get; set; }
        public string YourFIO { get; set; }
        public string YourPos { get; set; }
        public string YourTel { get; set; }
        public string YourEmail { get; set; }
        public string YourAgeSince { get; set; }
        public string YourAgeTill { get; set; }
        public string YourEducation { get; set; }
        public string YourExperience { get; set; }
        public string YourSex { get; set; }
        public string YourBusy { get; set; }
        public string YourSchedule { get; set; }
        public string YourLang { get; set; }
        public string YourLangLevel { get; set; }
        public string YourProfit { get; set; }
        public string YourCurrency { get; set; }
        public string YourMetro { get; set; }
        public string YourPeriod { get; set; }
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

        public string Error { get { return null; } }

        public string this[string propName]
        {
            get
            {
                if ((propName == "YourPosition") && string.IsNullOrEmpty(YourPosition))
                    return "Пожалуйста, введите должность";
                if ((propName == "YourEducation") && string.IsNullOrEmpty(YourEducation))
                    return "Пожалуйста, введите образование";
                if ((propName == "YourProfit") && string.IsNullOrEmpty(YourProfit))
                    return "Пожалуйста, введите зарплату";
                if ((propName == "YourCity") && string.IsNullOrEmpty(YourCity))
                    return "Пожалуйста, введите город";
                if ((propName == "YourText") && string.IsNullOrEmpty(YourText))
                    return "Пожалуйста, введите текст свого объявления";
                if ((propName == "s1") && !s1 && !s2 && !s3 && !s4 && !s5 && !s6 && !s7 && !s8 && !s9 && !s10 && !s11 && !s12 && !s13 && !s14)
                    return "Пожалуйста, выберите раздел";
                if ((propName == "YourCompany") && string.IsNullOrEmpty(YourCompany))
                    return "Пожалуйста, введите название компании";
                if ((propName == "YourFIO") && string.IsNullOrEmpty(YourFIO))
                    return "Пожалуйста, введите свои фамилию и инициалы";
                if ((propName == "YourPos") && string.IsNullOrEmpty(YourPos))
                    return "Пожалуйста, введите свою должность";
                if ((propName == "YourTel") && string.IsNullOrEmpty(YourTel))
                    return "Пожалуйста, введите свой телефон";
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
            var answer = new StringBuilder();

            message.Append("Требования \n");
            
            message.AppendFormat("Должность {0} \n", YourPosition);
            message.AppendFormat("Возраст от {0} ", YourAgeSince);
            message.AppendFormat(" до {0} \n", YourAgeTill);
            message.AppendFormat("Образование {0} \n", YourEducation);
            message.AppendFormat("Опыт работы {0} \n", YourExperience);
            message.AppendFormat("Пол {0} \n", YourSex);
            message.AppendFormat("Занятость {0} \n", YourBusy);
            message.AppendFormat("График работы {0} \n", YourSchedule);
            message.AppendFormat("Иностранный язык {0} ", YourLang);
            message.AppendFormat(" уровень {0} \n", YourLangLevel);
            message.AppendFormat("Заработная плата {0} ", YourProfit);
            message.Append(YourCurrency);
            message.Append(" в месяц \n");
            message.AppendFormat("Город {0} \n", YourCity);
            message.AppendFormat("Ближайшая станция метро {0} \n", YourMetro);
            message.AppendFormat("Текст объявления {0} \n", YourText);

            message.Append("Раздел ");
            if (s1) message.Append("Веб-технологии");
            if (s2) message.Append("Системное администрирование");
            if (s3) message.Append("Программирование");
            if (s4) message.Append("Бухгалтерия / Финансы");
            if (s5) message.Append("Дизайн, графика, верстка, 3D");
            if (s6) message.Append("Кадры/управление персоналом");
            if (s7) message.Append("Административный персонал");
            if (s8) message.Append("Проектирование");
            if (s9) message.Append("Техническое обслуживание ПК, HelpDesk");
            if (s10) message.Append("Складское хозяйство / Логистика / ВЭД");
            if (s11) message.Append("Продажи / Закупки");
            if (s12) message.Append("Информационная безопасность");
            if (s13) message.Append("Маркетинг / Реклама / PR");
            if (s14) message.Append("Разное");
            message.Append("\n");

            message.AppendFormat("Срок публикации {0} \n", YourPeriod);
            message.Append("Контактная информация \n");
            message.AppendFormat("Компания {0} \n", YourCompany);
            message.AppendFormat("Фамилия И.О. {0} \n", YourFIO);
            message.AppendFormat("Должность {0} \n", YourPos);
            message.AppendFormat("Телефон {0} \n", YourTel);
            message.AppendFormat(" E-mail {0} \n", YourEmail);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(new MailMessage(
                "info@specialist.ru",
                "job@specialist.ru",
                "Вакансии",
                message.ToString()));

            answer.AppendFormat("Здравствуйте, {0} !\n\n", YourFIO);
            answer.Append("Благодарим Вас за размещение вакансии на сайте www.specialist.ru.\n");
            answer.Append("----------------------------------------------------------------------------\n\n");
            answer.Append("Задать любые вопросы по карьере Вы можете на странице Службы трудоустройства http://beta.specialist.ru/Job или по телефону +7 (499) 138-36-48. Ваш консультант Сурикова Маргарита Анатольевна\n\n");
            answer.Append("С уважением,\n");
            answer.Append("Служба трудоустройства центра компьютерного обучения \"Специалист\"\n\n");
            answer.Append("ВНИМАНИЕ! ЭТО ПИСЬМО БЫЛО АВТОМАТИЧЕСКИ ОТПРАВЛЕНО С СЕРВЕРА. ОТВЕТ НА НЕГО ОБРАБОТАН НЕ БУДЕТ. ДЛЯ ОБРАТНОЙ СВЯЗИ ИСПОЛЬЗУЙТЕ АДРЕС job@specialist.ru");

            smtpClient.Send(new MailMessage(
                "info@specialist.ru",
                YourEmail,
                "Благодарим за размещение вакансии на сайте www.specialist.ru",
                answer.ToString()));

            Result = "Заявка на вакансию отправлена";

        }

        private void EnsureCurrentlyValid()
        {
            var propsToValidate = new[] { "YourPosition", "YourEducation", "YourProfit", "YourCity", "YourText", "s1", "YourCompany", "YourFIO", "YourPos", "YourTel", "YourEmail" };
            bool IsValid = propsToValidate.All(x => this[x] == null);

            if (!IsValid)
                throw new InvalidOperationException("Невозможно отправить вакансию");
        }
    }
}