using System.Collections.Generic;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Catalog;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;
using System.Net.Mail;
using Specialist.Entities.Tests;
using Specialist.Web.Common.ViewModel;

namespace Specialist.Services.Common.Interface
{
    public interface IMailService {
        void NewUserMessage(UserMessage message, string privateListLink);
        void RestorePassword(User user);
        void SendMisprint(string message);
        void RegistrationComplete(User user, string couponLink, bool isCityCoupon);
        void JobConsultation(User user);
        void OrderComplete(Entities.Context.Order order, bool sendToUser);
        void SendForResume(MailAddress from, MailAddress to, string body, string subject, UploadFile uploadFile);
        void MessageFromSite(SendMessageVM model, MailAddress to, MailAddress copy = null);
        void ExpressOrder(ExpressOrderVM model);
        void SendCompetitionRequest(CompetitionVM model);
        void SeminarComplete(GroupSeminar seminar, string complexLink);
        void OrderInfoForManager(Entities.Context.Order order,
            string confirmInfo, bool notComplete = false);
        void OrderStarted(Entities.Context.Order order);
    	void SendForOrgManager(string message, string title);

    	void Send(MailAddress from, MailAddress to, string body, string subject,
    		params MailAddress[] cc);

    	void SendPassport();
    	void NewGroupTest(User user);
    	void GroupTestInfo(IEnumerable<User> users, IEnumerable<TagA> tests, string managerEmail);
    	void TestAudit(Test test, TagA viewLink, string email, bool isReturn  = false);
    	void SendPollAnswer(Poll poll, string answer);
    	void SendJubilee(string message, string fileSys);
    	void TestResult(User user, TagA test, List<TagA> courses, UserTest userTest);
    	void TestCertPaid(Entities.Context.OrderDetail orderDetail);
    	void SendCollectionMcts(string message);
    	void TestComplete(Test test);
    	void TestCertFull();
    	void SendChangeNameRequest(string link, User oldName, User newName);
	    void SendSeminarRegistration(string message);
	    void SendResponse(string employeeTC, string message);
	    void SendOrderPaperCatalog(string message);
	    void PaperCatalogSubscribe(User user);
	    void NewGroupPhoto(User user, string link);
	    void OrderUnlimit();
	    void SberMerchant(Entities.Context.Order order, string operation);

	    void SendSync(MailAddress from, MailAddress to, string body, string subject,
		    params MailAddress[] cc);

	    void SendSync(MailAddress from, MailAddress to, string body, string subject, List<UploadFile> uploadFiles,
		    string fileName = null, int replyMinutes = 0, params MailAddress[] cc);

	    void SimpleRegistration(SimpleRegUser user, string url);
    }
}