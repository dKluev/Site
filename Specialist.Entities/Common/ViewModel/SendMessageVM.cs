using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context;

namespace Specialist.Web.Common.ViewModel
{
    public class SendMessageVM: IViewModel
    {
    	public string Type { get; set; }

    	public string CustomValue { get; set; }

		public const string ForManager = "ForManager";

		public const string Promocode = "Promocode";

    	public const string CourseIdea = "CourseIdea";

    	public const string MobileAppReview = "MobileAppReview";

    	public const string CatalogIdea = "CatalogIdea";

    	public const string TestResponse = "TestResponse";

    	public const string InviteManager = "InviteManager";

    	public const string SendToLeader = "SendToLeader";

    	public const string WebinarSpecial = "WebinarSpecial";

	    public const string CourseTender = "CourseTender";

	    public const string EnglishOrder = "EnglishOrder";

	    public const string DevIdea = "DevIdea";

	    public const string JobVacancy = "JobVacancy";

	    public const string JobManager = "JobManager";

	    public const string CareerDay = "CareerDay";

        [DisplayName("Ваш Email")]
        public string Email { get; set; }

        [DisplayName("Ваше имя")]
        public string SenderName { get; set; }

        [DisplayName("Тема")]
        public string Title { get; set; }

        [DisplayName("Тема")]
        public string EmployeeTC { get; set; }

        [DisplayName("Сообщение")]
        [UIHint(Controls.BigTextArea)]
        public string Message { get; set; }

	    public Employee Employee { get; set; }

    	string IViewModel.Title {
    		get {
    			switch (Type) {
    				case Promocode:
    					return "Заполните форму для получения промокода " 
    						+ "(в сообщении можно указать дополнительную контактную информацию)";
    				case ForManager:
    					return "Отправить сообщение менеджеру";
    				case CourseIdea:
    					return "Идея для нового курса";
    				case MobileAppReview:
    					return "Отзыв о мобильном приложении";
    				case CatalogIdea:
    					return "Предложение по каталогу";
    				case TestResponse:
    					return "Отправить комментарии о тесте";
    				case InviteManager:
    					return "Вызов менеджера на переговоры";
    				case SendToLeader:
    					return "Связь с руководством";
    				case WebinarSpecial:
    					return "Особые условия для организаций, обучающих группу специалистов через вебинар";
    				case CourseTender:
    					return "Обучение через тендер/аукцион";
    				case EnglishOrder:
    					return "Обучение английскому языку";
    				case DevIdea:
    					return "Пожелание разработчикам";
    				case JobVacancy:
    					return "Разместить вакансию в службе трудоустройства центра «Специалист»";
    				case JobManager:
    					return "Отправить сообщение менеджеру службы трудоустройства";
    				case CareerDay:
    					return "Отправить заявку на участие в дне карьеры";
					default:
		    			return "Отправить сообщение вебмастеру";
    			}
    		}
    	}
    }
}