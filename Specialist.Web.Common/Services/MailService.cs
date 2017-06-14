using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
//using ERBTemplate;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SimpleUtils.Common.Enum;
using SimpleUtils.FluentAttributes.Utils;
using Microsoft.Practices.Unity;
using NLog;
using SimpleUtils.Collections;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Order.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Services.Cms.Const;
using Specialist.Services.Common.Interface;
using Specialist.Services.Common.Utils;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Passport;
using System.Text;
using Specialist.Services.Common.Extension;
using SimpleUtils;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Util;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Logic;
using Specialist.Web.Common.ViewModel;
using Logger = Specialist.Services.Utils.Logger;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Util;
using Specialist.Services.Core.Interface;
using Specialist.Entities.Catalog.Links.Interfaces;
using Htmls = Specialist.Web.Common.Html.Htmls;

namespace Specialist.Services.Common {
	public class MailService : IMailService {
		[Dependency] public IUserService UserService { get; set; }

		[Dependency]
		public MailTemplateService MailTemplateService { get; set; }

		[Dependency]
		public IAuthService AuthService {
			get;
			set;
		}

		[Dependency]
		public IEmployeeService EmployeeService { get; set; }

		[Dependency]
		public IUserSettingsService UserSettingsService {
			get;
			set;
		}

		[Dependency]
		public ICityService CityService { get; set; }
 
		public static MailAddress info = new MailAddress("info@specialist.ru");
		public static MailAddress callback = new MailAddress("callback@specialist.ru");
		public static MailAddress testedit = new MailAddress("testedit@specialist.ru");
		public static MailAddress marketers = new MailAddress("web-marketers@specialist.ru");
		public static MailAddress svidetelstvo = new MailAddress("svidetelstvo@specialist.ru");
		public static MailAddress context = new MailAddress("context@specialist.ru");
		public static MailAddress ptolochko = new MailAddress("ptolochko@specialist.ru");
		public static MailAddress kgral = new MailAddress("kgral@specialist.ru");
		public static MailAddress ashirokov = new MailAddress("ashirokov@specialist.ru");
		public static MailAddress contmanagers = new MailAddress("contmanagers@specialist.ru");
		public static MailAddress ekaranova = new MailAddress("ekaranova@specialist.ru");
		public static MailAddress shneider = new MailAddress("shneider@specialist.ru");
		public static MailAddress corporatesiteorders = new MailAddress("corporatesiteorders@specialist.ru");
		public static MailAddress corporatedepartment = new MailAddress("corporate_department@specialist.ru");
		public static MailAddress devidea = new MailAddress("devidea@specialist.ru");
		public static MailAddress job = new MailAddress("job@specialist.ru");
		public static MailAddress secrko = new MailAddress("secrko@specialist.ru");
		public static MailAddress akavinkina = new MailAddress("akavinkina@specialist.ru");
		public static MailAddress siteOrders = new MailAddress("siteorders@specialist.ru");
		public static MailAddress sberbankPayment = new MailAddress("sberbank-payment@specialist.ru");
		public static MailAddress siteCertOrders = new MailAddress("ordercert@specialist.ru");
		public static MailAddress events = new MailAddress("events@specialist.ru");
		public static MailAddress konkurs = new MailAddress("konkurs@specialist.ru");
		public static MailAddress webgroup = new MailAddress("Web-group@specialist.ru");
		public static MailAddress promocode = new MailAddress("eignatova@specialist.ru");
		public static MailAddress motorina = new MailAddress("motorina@specialist.ru");
		public static MailAddress courseIdea = new MailAddress("courseidea@specialist.ru");
		public static MailAddress site = new MailAddress("site@specialist.ru");
		public static MailAddress testresponse = new MailAddress("testresponse@specialist.ru");
		public static MailAddress secretariat = new MailAddress("secretariat@specialist.ru");
		public static MailAddress zmatisova = new MailAddress("zmatisova@specialist.ru");
		public static MailAddress zayavki = new MailAddress("zayavki@specialist.ru");
		public static MailAddress karpovich = new MailAddress("lara@specialist.ru");
//		public static MailAddress lovkov = new MailAddress("lovkov@specialist.ru");
		public static MailAddress websiteManagers = new MailAddress("Website_Managers@specialist.ru");
		public static MailAddress bt1c = new MailAddress("bt-1c@specialist.ru");

		const string Br = "<br/>";


		public void SendMisprint(string message) {
			Send(info, contmanagers, message,
				"[Сообщение с сайта]Опечатка",ptolochko);
		}


		public void MessageFromSite(SendMessageVM model, MailAddress to, 
			MailAddress copy = null) {
			var isRus = Linguistics.IsRussian(model.Title) || Linguistics.IsRussian(model.Message);
			model.Title = "[Сообщение с сайта]" + model.Title;
			if (!model.Email.IsEmpty())
				model.Title += ". От " + model.Email;
			if (!model.SenderName.IsEmpty())
				model.Message += Br + model.SenderName.Tag("strong");
			var copyList = copy == null ? new MailAddress[0]: new[] {copy} ;
			if (isRus) {
				Send(info, to, model.Message, model.Title, copyList);
			} else {
				Send(info, ptolochko, model.Message, "[FILTER]" + model.Title);
			}
		}

		private const string MailMaster = "<html><body>{0}</body></thml>";

		public void Send(MailAddress from, MailAddress to, string body, string subject,
			params MailAddress[] cc) {
			Send(from, to, body, subject, null,null, 0, cc);
		}

		public void SendSync(MailAddress from, MailAddress to, string body, string subject,
			params MailAddress[] cc) {
			SendSync(from, to, body, subject, null,null, 0, cc);
		}

		public void SendWithReplyLimit(MailAddress from, MailAddress to, string body, string subject, 
			int replayMinutes, params MailAddress[] cc) {
			Send(from, to, body, subject, null,null, replayMinutes, cc);
		}

		public void SendForResume(MailAddress from, MailAddress to, string body, string subject,
			UploadFile uploadFile) {
			Send(from, to, body, subject, _.List(uploadFile));
		}
		

		public void SendPassport() {
			var user = AuthService.CurrentUser;
			var passportFile = UserImages.GetPassportFileSys(user.UserID);
			if(passportFile.IsEmpty())
				return;
			Send(MailAddress(user), info, "", "[{0}]Копия документа".FormatWith(user.FullName), null, passportFile);
		}
		public void SendJubilee(string message, string fileSys) {
			Send(info, ashirokov, message, "Поздравление с юбилеем", null, fileSys);
		}

		

		public void SendCollectionMcts(string message) {
			Send(info, ekaranova, message, "Заявка Collection MCTS", ptolochko);
		}
		public void SendSeminarRegistration(string message) {
			Send(info, shneider, message, 
				"Регистрация на семинар", corporatedepartment);
		}

		public void SendOrderPaperCatalog(string message) {
			Send(info, info, message, "Заказ бумажного каталога", ptolochko);
		}

		public void SendResponse(string employeeTC, string message) {
			Send(info, contmanagers, message, 
				"[Отзыв]" + AuthService.CurrentUser.FullName, ptolochko);
		}
		public void Send(MailAddress from, MailAddress to, string body, string subject,
			List<UploadFile> uploadFiles, string fileName = null, int replyMinutes = 0,
			params MailAddress[] cc) {
			ThreadPool.QueueUserWorkItem(o => {
				try {
					SendSync(@from, to, body, subject, uploadFiles, fileName, replyMinutes, cc);

				} catch (Exception e) {
					Logger.Exception(e, this + " " + to.Address +
						" " + subject);
				}
			});


		}

		public void SendSync(MailAddress from, MailAddress to, string body, string subject, List<UploadFile> uploadFiles,
			string fileName = null, int replyMinutes = 0, params MailAddress[] cc) {
			using (var client = new SmtpClient()) {
				var htmlBody = body.Contains("<html>") ? body : MailMaster.FormatWith(body);
				using (var message =
					new MailMessage(from, to) {
						IsBodyHtml = true,
						BodyEncoding = Encoding.UTF8,
						Subject = subject,
						Body = htmlBody,
					}) {
					if (replyMinutes > 0) {
						var replyDate = DateTime.Now.AddMinutes(replyMinutes);
						message.Headers.Add("X-Message-Flag", "Follow up");
						message.Headers.Add("Reply-By", replyDate.ToString("ddd, dd MMM yyyy HH:mm:ss zz"));
					}

					if (uploadFiles != null) {
						foreach (var uploadFile in uploadFiles) {
							if (uploadFile != null && uploadFile.ContentLength > 0) {
								message.Attachments.Add(new Attachment(uploadFile.Stream, uploadFile.Name));
							}
						}
					}
					if (!fileName.IsEmpty()) {
						var attachment = new Attachment(fileName);
						attachment.Name = Path.GetFileName(fileName);
						message.Attachments.Add(attachment);
					}
					if (cc != null)
						foreach (var addresse in cc) {
							if (addresse != null) {
								message.CC.Add(addresse);
							}
						}
					client.Send(message);
				}
			}
		}

		private MailAddress MailAddress(User user) {
			try {
				return new MailAddress(user.Email, user.FullName);
				
			} catch (Exception e) {
				Logger.Exception(e, this + " wrong email " + user.Email);
			}
			return null;
		}

		public void ExpressOrder(ExpressOrderVM model) {
			if(model.Name.IsEmpty() || model.Contact.IsEmpty()) return;
			var body = "Имя: ".Tag("strong") + model.Name + Br;
			body += "Контакт: ".Tag("strong") + model.Contact + Br;
			if(model.StudentInGroupId > 0)
				body += "Номер заказа: ".Tag("strong") + model.StudentInGroupId + Br;
			var cityTC = UserSettingsService.CityTC;
			if (!cityTC.IsEmpty())
				body += "Город: ".Tag("strong") + CityService
					.FirstOrDefault(x => x.City_TC == cityTC)
					.CityName + Br;
			if (!model.CourseTC.IsEmpty())
				body += "Курс: ".Tag("strong") + model.CourseTC;

			SendWithReplyLimit(info, callback, body, model.CourseTC.IsEmpty()
				? "Срочный заказ" : "Запрос расписания курса", 5);
		}


		public void NewUserMessage(UserMessage message, string privateListLink) {
			var sender = UserService.GetByPK(message.CreatorUserID);
			var receiver = UserService.GetByPK(message.ReceiverUserID);

			var body = sender.FullName + " отправил вам новое сообщение:" + Br +
				message.Text + Br + privateListLink;
			Send(MailAddress(sender), MailAddress(receiver), body, "Новое сообщение");
		}

		public void RegistrationComplete(User user, string couponLink, bool isCityCoupon) {
			var template = MailTemplateService
				.GetTemplate(MailTemplates.RegistrationComplete, user.FullName);
//			var blockId = isCityCoupon ? HtmlBlocks.CityCoupon : HtmlBlocks.RegCoupon;
//			var text = Htmls.AllHtmlBlocks()[blockId];
			var body = TemplateEngine.GetText(template.Description,
				new {
					user.Password, user.Email, 
//					Text = text,
//					CouponImage = CouponUtils.RegistrationIsActive ? H.Img(couponLink) : null
				});
			Send(info, MailAddress(user), body, template.Name);
		}

		public void JobConsultation(User user) {
			var template = MailTemplateService.GetTemplate(MailTemplates.JobConsultation, user.FullName);
			var body = template.Description;
			Send(info, MailAddress(user), body, template.Name);
		}

		public string AddTestResultUtm(string body) {
			var urlPostfix = StringUtils.GetUtmPart("onlinetest", "mail", "2012");
			return Regex.Replace(body, "(href=\".*?)\"", "$1" + urlPostfix + "\"");
		}

		public void TestResult(User user, TagA test, List<TagA> courses, UserTest userTest) {
			if(user == null)
				return;
			var templateName = MailTemplates.TestResult;
			if(userTest.IsPass)
				templateName = MailTemplates.TestResultSuccess;
			var template = MailTemplateService.GetTemplate(templateName,user.FullName);
			var body = TemplateEngine.GetText(template.Description,
				 new {
					 Course = courses.Select(x => x.ToString()).JoinWith(", "),
					 Test = test.ToString()
				 });
			Send(info, MailAddress(user), AddTestResultUtm(body), template.Name);
		}



		public void OrderComplete(Entities.Context.Order order, bool sendToUser) {
			var user = AuthService.CurrentUser;
			var isCredit = order.PaymentType_TC == PaymentTypes.AlphaCredit;
			var template = MailTemplateService.GetTemplate(isCredit ? MailTemplates.OrderCompleteCredit : MailTemplates.OrderComplete, user.FullName);
			var orderDescription = GetOrderDescription(order);
			orderDescription += HtmlControls.Anchor(CommonConst.SiteRoot +
				 "/order/paymenttypechoice?orderid=" + order.OrderID) + Br;
			if (order.PaymentType_TC == PaymentTypes.SberBank) {
				orderDescription += "Квитанция:" + Br +
					HtmlControls.Anchor(CommonConst.SiteRoot +
					"/order/receipt?orderid=" + order.OrderID) + Br +
					"После оплаты необходимо подтвердить оплату обучения на странице:" + Br +
				 HtmlControls.Anchor(CommonConst.SiteRoot +
				  "/order/confirm?orderid=" + order.OrderID) + Br;
			}

			var managerTC = order.Manager_TC ?? Employees.MainManager;
			var manager = EmployeeService.AllEmployees()[order.Manager_TC];

			var body = TemplateEngine.GetText(
				template.Description, new {
					orderDescription,
					Manager = H.Anchor("/manager/" + managerTC.ToLower(), 
						manager.LastName + " " + manager.FirstName).AbsoluteHref().ToString()
				});
			if (sendToUser) {
				Send(info, MailAddress(order.User), body, template.Name);
			}

			OrderInfoForManager(order);

		}

		public void OrderStarted(Entities.Context.Order order) {
			var orderDetail = order.OrderDetails.FirstOrDefault(x => !x.IsTestCert);
			if(orderDetail == null)
				return;
			var courseData = orderDetail.Course;
			var groupData = orderDetail.Group;

			var user = order.User;
			var managerData = EmployeeService.AllEmployees()[Employees.GetKarpovich()];
			var manager = H.Anchor("/manager/" + managerData.Employee_TC.ToLower(),
				managerData.FullName).AbsoluteHref().ToString();
			var managerPhoto = Images.Employee(managerData.Employee_TC).ToString();

			var userName = user.FullName;
			var template = MailTemplateService.GetTemplate(MailTemplates.OrderStarted, userName);
			
			var orderDescription = H.Anchor("/course/" + courseData.UrlName.ToLower(),
				courseData.GetName()).AbsoluteHref().ToString();
			var certType = string.Empty;
			if(courseData.AuthorizationType_TC == AuthorizationTypes.Microsoft) {
				certType = "Международный сертификат Microsoft";
			}
			else {
				certType = courseData.CourseCertificates
					.FirstOrDefault().GetOrDefault(x => x.CertType.CertTypeName);
			}
			if(!certType.IsEmpty())
				certType = " и " + certType;
			var complexInfo = string.Empty;
			var trainerInfo = string.Empty;
			if(groupData != null && groupData.Teacher != null) {
				var trainerLink = H.Anchor("/trainer/" + 
					groupData.Teacher.Employee_TC.ToLower(),
				groupData.Teacher.FullName).AbsoluteHref().ToString();
				var complexData = groupData.Complex;
				var complexLink = H.Anchor("/locations/complex/" +
					complexData.UrlName, complexData.Name).ToString();

				trainerInfo = "Ваш курс будет вести {0}. {1} <br/> {2}".FormatWith(trainerLink,
					StringUtils.GetFirstParagraph(groupData.Teacher.SiteDescription),
					H.Anchor("/trainer/{0}/trainer-responses/1".FormatWith(
					groupData.Teacher.Employee_TC.ToLower()),
				"Отзывы по преподавателю").AbsoluteHref());
				complexInfo = " или непосредственно в учебный комплекс {0} по адресу {1} {2}"
					.FormatWith(complexLink, complexData.Address, 
					H.Anchor(Urls.GetComplexMap(complexData.UrlName), "[Схема проезда]"));

				orderDescription += (". Дата начала занятий: {0}. " +
					"Место проведения обучения: УК {1}. Дни и часы занятий: {2} {3}").FormatWith(
						groupData.DateBeg.DefaultString(), complexLink, groupData.DaySequence, groupData.TimeInterval);
			}
			var body = TemplateEngine.GetText(
				template.Description, new {
					orderDescription,
					manager,
					trainerInfo,
					complexInfo,
					managerPhoto,
					certType
				});
			var title = TemplateEngine.GetText(template.Name, new{UserName = user.FirstSecondName});
			body = StringUtils.AddUtm(body, "email", "email", "unfinished_orders");
			Send(new MailAddress(managerData.FirstSpecEmail), MailAddress(order.User), body, title, websiteManagers);
		}

		public void TestCertFull() {
			Send(info, siteCertOrders,"Необходимо очистить группу",
				"Группа сертификатов тестирования заполнена", ptolochko, svidetelstvo);
		}

		public void TestCertPaid(Entities.Context.OrderDetail orderDetail) {
			var template = MailTemplateService.GetTemplate(MailTemplates.TestCertPaid, 
				orderDetail.Order.User.FullName);
			var body = TemplateEngine.GetText(
				template.Description, new {
					TestCert = H.Anchor("/order/testcertificates",
						orderDetail.UserTest.Test.Name).AbsoluteHref(),
				});
			Send(info, MailAddress(orderDetail.Order.User), body, template.Name);
			Send(info, siteCertOrders, GetOrderCmsAnchor(orderDetail.Order).ToString()
				, "Сертификат оплачен");
		}


		private string GetOrderDescription(Entities.Context.Order order, bool isFull = false) {
			var orderDescription = order.GetDescription(isFull) + Br;

			if (order.IsOrganization)
				orderDescription +=
					"Интернет счет: ".Tag("strong") + order.InvoiceNumber + Br;

			orderDescription +=
				"Итого к оплате: ".Tag("strong")
				+ order.TotalPriceWithDescount.MoneyString()
					+ " руб. ";
			return orderDescription;
		}

	/*	public void OrderConfirm(Entities.Context.Order order) {

		}

		public void MailListSubscribe() {
			var template = GetTemplate(MailTemplates.MailSubscribe);
			var user = AuthService.CurrentUser;
			var body = template.Description;
			Send(info, MailAddress(user), body, template.Name);
		}

		public void MailSubscribe(bool newspaper, bool catalog) {
			if (!(newspaper || catalog))
				return;
			var user = AuthService.CurrentUser;
			var managerBody = newspaper ? "Газета" + Br : "";
			managerBody += newspaper ? "Каталог" + Br : "";
			managerBody += GetUserInfo(user);
			managerBody += "Адрес: ".Tag("strong") + user.AddressDescription;

			Send(info, amitrofanova, managerBody, "Новая подписка");
		}*/

		public void SendForOrgManager(string message, string title) {
			var user = AuthService.CurrentUser;
			var subject = user.FullName + ": " + (title ?? "сообщение");
			var body = message + Br +
				GetUserInfo(user);
			Send(info, zayavki, body, subject);
		}

		public void OrderUnlimit() {
			var user = AuthService.CurrentUser;
			var userInfo = GetUserInfo(user);
			Send(info, siteOrders, userInfo, "[Заказ БО] " + user.FullName, websiteManagers);
		}

		static Dictionary<string,string> SberOperationNames = new Dictionary<string, string> {
			{"approved", "Холдирование суммы"},
			{"deposited", "Оплачено"},
			{"reversed", "Отмена"},
			{"refunded", "Возврат"},
		};
		public void SberMerchant(Entities.Context.Order order, string operation) {
			var operationName = SberOperationNames.GetValueOrDefault(operation) ?? operation;
			var body = H.b[operationName] + Br +
				H.b["Email: "] + order.User.Email + Br +
				GetOrderDescription(order, true) + Br +
				GetOrderCmsAnchor(order) + Br;
			SendSync(info, sberbankPayment, body,"[ПАО Сбербанк] {0} {1} {2}"
				.FormatWith(operationName, order.User.FullName, order.CommonOrderId), ptolochko);
			if (!order.User.Email.IsEmpty()) {
				body = H.b["Описание покупки: "] + order.GetDescription(false) + Br +
					H.b["Стоимость покупки: "] + order.TotalPriceWithDescount.MoneyString() + " RUB" + Br +
					H.b["Номер заказа: "] + order.CommonOrderId + Br +
					H.b["Дата платежа: "] + DateTime.Now.DefaultString() + Br +
					H.b["Результат: "] + operationName + Br + Br + 
					"Это письмо сгенерировано автоматически и не требует ответа.";
				try {
					Send(info, new MailAddress(order.User.Email), body, 
						"Оплата заказа {0} на сайте www.specialist.ru".FormatWith(order.CommonOrderId));
				}
				catch (Exception ex) {
					Logger.Exception(ex, "sber mail");
				}
				
			}
		}

		public void OrderInfoForManager(Entities.Context.Order order,
			string confirmInfo = null, bool notComplete = false) {
			try {
				var user = order.User;
				var userInfo = GetUserInfo(user);
				var socialUrl = order.SocialUrl;
				var body = userInfo +
					(order.PromoCode.IsEmpty() ? "" : "Промокод: ".Tag("strong") + order.PromoCode + Br) +
					(socialUrl.IsEmpty() ? "" : "Ссылка на соц. сеть для скидки: ".Tag("strong") + socialUrl + Br) +
					GetOrderDescription(order,true) + Br + 
					GetOrderCmsAnchor(order) + Br;
				var subject = string.Empty;
				if (notComplete) {
					subject = "[Незавершенный заказ] " + user.FullName;
				} else if (!confirmInfo.IsEmpty()) {
					subject = "[Подтверждение оплаты] " + user.FullName;
					body += OrderConfirmVM.ConfirmInfoName.Tag("strong") + Br +
						confirmInfo;
				} else {
					subject = "[{0}][{1}][Заказ] {2}".FormatWith(order.PaymentType_TC, 
						order.OurOrgOrDefault, user.FullName);
				}
				if (!order.IsOrganization) {
					var managerTc = order.Manager_TC;
					var email = GetEmployeeMail(managerTc);
					if (order.PaymentTypeIsSet && order.OrderDetails.Any(x => x.IsDopUsl)) {
						Send(info, siteOrders, body, subject, bt1c, email);
					}else if(order.OrderDetails.Any(x => x.IsTestCert)) {
						Send(info, siteOrders, body, subject, siteCertOrders, email);
					}else {
						Send(info, siteOrders, body, subject, email);
					}
				} else {
					Send(info, secrko, body, subject);
					Send(info, corporatesiteorders, body, subject);

				}

			} catch (Exception e) {
				Logger.Exception(e, "orderId = " + order.OrderID);
				throw;
			}
		}

		private MailAddress GetEmployeeMail(string employeeTC) {
			return new MailAddress(EmployeeService.AllEmployees()[employeeTC].FirstSpecEmail);
		}

		private static TagA GetOrderCmsAnchor(Entities.Context.Order order) {
			return order.IsSig ? null : H.Anchor(CmsUrls.Order + order.OrderID);
		}

		private static TagA GetTestCmsAnchor(Test test) {
			return H.Anchor(CmsUrls.Test + test.Id);
		}
		private string GetUserInfo(User user) {
			return user.MailDescription;
		}

		public void RestorePassword(User user) {
			var template = MailTemplateService.GetTemplate(MailTemplates.RestorePassword, user.FullName);
			var body = TemplateEngine.GetText(template.Description,
				new {
					user.Password,
				});
			Send(info, MailAddress(user), body, template.Name);
		}

		public void SendCompetitionRequest(CompetitionVM model) {
			var user = AuthService.CurrentUser;
			var body = GetUserInfo(user) + model.Request;
			Send(info, konkurs, body, "Заявка на конкурс: " + model.Competition.Name,
				_.List(model.UploadFile));
		}

		public void NewGroupTest(User user) {
		
			var template = MailTemplateService.GetTemplate(MailTemplates.NewGroupTest, user.FullName);
			Send(info, ptolochko, template.Description, template.Name);
		}

		public void TestAudit(Test test, TagA viewLink, string checkerTC, bool isReturn = false) {
			var newTest = checkerTC.IsEmpty();
			string prefix;
			if (newTest) {
				if (test.HasChecker) {
					prefix = "[Тест после исправления]";
				} else {
					prefix = "[Тест от преподавателя]";
				}
			} else {
				prefix = "[Назначена проверка теста]";
			}
			var author = EmployeeService.AllEmployees()[test.Author_TC];
			var authorEmail = new MailAddress(author.FirstSpecEmail);
			var body = string.Empty;
			
			if (isReturn) {
				prefix = "[Тест возвращен на редактирование]";
				authorEmail = null;
			} else {
				if (!newTest) {
					body += "После проверки, напишите в ответ на это письмо ваши замечания по тесту.";
				}
			}
			body += H.br + viewLink.ToString() + H.br + "Автор теста: " + author.FullName;
			if (test.HasChecker) {
				var checker = EmployeeService.AllEmployees()[test.Checker_TC];
				body += H.br + "Проверяющий: " + checker.FullName;
			}
			var subject = prefix + test.Name;
			var main = newTest ? testedit : GetEmployeeMail(checkerTC);
			Send(testedit, main, body , subject, authorEmail, newTest ? null : testedit );
		}

		public void TestComplete(Test test) {
			var subject = "[Разместить новый тест на сайте]{0}".FormatWith(test.Name);
			Send(testedit, contmanagers,GetTestCmsAnchor(test).ToString(), subject, 
				testedit);
		}
		public void GroupTestInfo(IEnumerable<User> users, IEnumerable<TagA> tests, string managerEmail) {
			var user = AuthService.CurrentUser;
			var template = MailTemplateService.GetTemplate(MailTemplates.GroupTestInfo, user.FullName);
			var userPart = users.Select(x => _.List(x.FullName, " Логин: ".Tag("strong"), x.Email,
				" Пароль: ".Tag("strong"), x.Password).JoinWith(" ")).JoinWith(Br);
			var body = TemplateEngine.GetText(template.Description, new {GroupInfo = userPart + 
				Br + H.strong["Тесты"] + Br +
				tests.Select(x => x.ToString()).JoinWith(Br)}) ;
			var to = managerEmail.IsEmpty()
				? MailAddress(user)
				: new MailAddress(managerEmail);
			Send(info, to, body, template.Name);
		}

		public void SeminarComplete(GroupSeminar seminar, string complexLink) {
			var user = AuthService.CurrentUser;
			var body =
				GetSeminarInfo(seminar, complexLink) +
				GetUserInfo(user) +
				(user.IsCompany ? "Организация" : "Частное лицо");
			if(!seminar.Group.IsSeminar || seminar.Group.IsCareerDay)
				Send(info, events, body, "Заявка на участие в мероприятии "
					+ seminar.Group.DateBeg.DefaultString());

			SeminarCompleteForUser(user, seminar, complexLink);

		}

		private string GetSeminarInfo(GroupSeminar seminar, string complexLink) {
			var seminarInfo = "Семинар " + seminar.Group.Title
				+ " состоится: " + 
					seminar.Group.DateBeg.DefaultString() + " " + seminar.Group.TimeInterval;
			if(!seminar.Group.WebinarExists)
				seminarInfo += Br + "Место: " + complexLink + Br 
					+ seminar.Group.Complex.GetOrDefault(x => x.Address);
			return seminarInfo;
		}

		public void SeminarCompleteForUser(User user, GroupSeminar seminar, string complexLink) {
			if (seminar.Group.Group_ID == 223973) {
				var mailTemplate = MailTemplateService.GetTemplate(MailTemplates.AdminSeminar, user.FullName);
				Send(info, MailAddress(user), mailTemplate.Description, mailTemplate.Name);
				return;
			}
			if (seminar.Group.Group_ID == 241944) {
				var mailTemplate = Htmls.AllHtmlBlocks()[HtmlBlocks.Seminar241944];
				Send(info, MailAddress(user), mailTemplate.Item2, mailTemplate.Item1);
				return;
			}
			var template = MailTemplateService.GetTemplate(MailTemplates.SeminarComplete, user.FullName);
			var seminarLink =
				"Ссылка для подключения к семинару будет выслана Вам по электронной почте не позднее, чем за 15 минут до начала семинара.";
			var body = TemplateEngine.GetText(template.Description,
				new {
					SeminarInfo = GetSeminarInfo(seminar, complexLink),
					SeminarName = seminar.Group.Title,
					SeminarLink = seminarLink
				});

			Send(info, MailAddress(user), body, template.Name);

		}

		public void SendPollAnswer(Poll poll, string answer) {
			if(!answer.IsEmpty())
			Send(info, motorina, answer, "Ответ на отпрос: " + poll.Name);
		}

		public void SendChangeNameRequest(string link, User oldName, User newName) {
			var body = "old: " + oldName.FullName + Br
				+ "new: " + newName.FullName + Br + link;
			Send(info, ptolochko, body, "Запрос на смену ФИО " + oldName.FullName);
		}



		public void PaperCatalogSubscribe(User user) {
			var address = user.GetAddress();
			if (address != null) {
				var type = (SubscribeType) user.Subscribes;
				var names = Enum.GetValues(typeof (SubscribeType)).Cast<SubscribeType>()
					.Where(x => x != SubscribeType.None).Where(x => type.HasFlag(x))
					.Select(x => EnumUtils.GetDisplayName(x)).JoinWith(Br);
				var body =  names + Br +
					user.FullName + Br +
					address.FullAddress;
				Send(info, secretariat, body, "Подписка на каталог", zmatisova);
			}
		}

		public void NewGroupPhoto(User user, string link) {
			var template = MailTemplateService.GetTemplate(MailTemplates.NewGroupPhoto, user.FullName);
			var body = TemplateEngine.GetText(template.Description, new {
				PhotoLink = link
			});
			Send(info, MailAddress(user), body, template.Name);
			
		}

		private static MailAddress[] ForNewEntity = new [] {
			webgroup,
			new MailAddress("corporate_department@SPECIALIST.RU"),
			new MailAddress("people_dep@SPECIALIST.RU"),
			new MailAddress("Filial_admins@SPECIALIST.RU"),
			new MailAddress("Economicdepartment@SPECIALIST.RU")
		};

		public void NewMarketingAction(MarketingAction entity, string link) {

			var desc = H.strong["Срок акции: "] + entity.DateInterval + H.br +
				entity.Description + H.br + link;
			
			Send(info, context, desc, "[Акция]" + entity.Name, ForNewEntity);
			
		}
		public void NewNews(News entity) {

			var desc = H.strong["Дата публикации: "] + entity.PublishDate.DefaultString() + H.br +
				entity.Description;
			Send(info, context, desc, "[Новость]" + entity.Title, ForNewEntity);
			
		}


		public void SimpleRegistration(SimpleRegUser user, string url) {

			var template = MailTemplateService.GetTemplate(MailTemplates.SimpleReg, user.Name);
			var body = TemplateEngine.GetText(template.Description, new {
				Url = url
			});
			Send(info, new MailAddress(user.Email), body, template.Name);
		}


	}
}
