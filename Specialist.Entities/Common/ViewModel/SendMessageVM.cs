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

        [DisplayName("��� Email")]
        public string Email { get; set; }

        [DisplayName("���� ���")]
        public string SenderName { get; set; }

        [DisplayName("����")]
        public string Title { get; set; }

        [DisplayName("����")]
        public string EmployeeTC { get; set; }

        [DisplayName("���������")]
        [UIHint(Controls.BigTextArea)]
        public string Message { get; set; }

	    public Employee Employee { get; set; }

    	string IViewModel.Title {
    		get {
    			switch (Type) {
    				case Promocode:
    					return "��������� ����� ��� ��������� ��������� " 
    						+ "(� ��������� ����� ������� �������������� ���������� ����������)";
    				case ForManager:
    					return "��������� ��������� ���������";
    				case CourseIdea:
    					return "���� ��� ������ �����";
    				case MobileAppReview:
    					return "����� � ��������� ����������";
    				case CatalogIdea:
    					return "����������� �� ��������";
    				case TestResponse:
    					return "��������� ����������� � �����";
    				case InviteManager:
    					return "����� ��������� �� ����������";
    				case SendToLeader:
    					return "����� � ������������";
    				case WebinarSpecial:
    					return "������ ������� ��� �����������, ��������� ������ ������������ ����� �������";
    				case CourseTender:
    					return "�������� ����� ������/�������";
    				case EnglishOrder:
    					return "�������� ����������� �����";
    				case DevIdea:
    					return "��������� �������������";
    				case JobVacancy:
    					return "���������� �������� � ������ ��������������� ������ �����������";
    				case JobManager:
    					return "��������� ��������� ��������� ������ ���������������";
    				case CareerDay:
    					return "��������� ������ �� ������� � ��� �������";
					default:
		    			return "��������� ��������� ����������";
    			}
    		}
    	}
    }
}