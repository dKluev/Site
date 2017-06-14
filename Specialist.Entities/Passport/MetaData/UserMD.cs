using System;
using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using SimpleUtils.FluentAttributes.Core.Interfaces;

namespace Specialist.Entities.Passport.MetaData
{
    public class UserMD: BaseMetaData<User>
    {
        public UserMD() {
	        For(x => x.LastName).Display("Фамилия");
        	For(x => x.EngFullName)
        		.Display("Полное имя на английском")
				.Example("Данные необходимы для получения сертификата международного образца");
	        For(x => x.FirstName).Display("Имя");
	        For(x => x.SecondName).Display("Отчество");
	        For(x => x.BirthDate).Display("Дата рождения").Example("30.06.1980");
            For(x => x.Password)
                .Display("Пароль")
                .UIHint(Controls.Password)
                .Example("Пароль должен быть длиной от 6 знаков");
            For(x => x.Source_ID).Display("Откуда вы о нас узнали");
            For(x => x.WorkBranch_ID).Display("Профессиональная отрасль");
            For(x => x.Metier_ID).Display("Профессиональная категория");
            For(x => x.Student_ID).UIHint(Controls.Hidden);
            For(x => x.Sex).Display("Пол").UIHint("Sex");
            For(x => x.HideCourses).Display("Скрыть пройденные курсы и тесты");
            For(x => x.HideContacts).Display("Не отображать на странице одногруппников");
            For(x => x.Email)
                .Display("E-mail").Example("Нам нужен действующий адрес электронной почты, чтобы держать Вас в курсе скидок и мероприятий и акций партнеров").Requerd();
            For(x => x.MailListSubscribed).Display("Новостная рассылка");
        }
    }
}