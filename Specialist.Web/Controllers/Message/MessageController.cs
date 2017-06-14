using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcContrib.Filters;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Message;
using Specialist.Entities.Message.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Extension;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Message;
using Specialist.Services.Profile;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Pages;
using Specialist.Web.Util;

namespace Specialist.Web.Controllers.Message {
	public class MessageController : ViewController {
		private TagBuilder image;

		[Dependency]
		public IRepository<UserMessage> UserMessageService { get; set; }

		[Dependency]
		public IRepository2<UserMessage> UserMessageRepository { get; set; }

		[Dependency]
		public IRepository2<StudentInGroup> StudentInGroupRepository { get; set; }

		[Dependency]
		public IMessageSectionService MessageSectionService { get; set; }

		[Dependency]
		public IUserService UserService { get; set; }

		[Dependency]
		public ProfileService ProfileService { get; set; }
		[Dependency]
		public IMailService MailService { get; set; }


		[Dependency]
		public IGroupVMService GroupVMService { get; set; }

		public ActionResult Forum() {
			var section = MessageSectionService.GetAll()
				.FirstOrDefault(ms => ms.SysName == MessageSections.Forum);
			return Section(section.MessageSectionID, null);
		}

		[HandleNotFound]
		public ActionResult Section(int? sectionID, int? pageIndex) {
			var section = MessageSectionService.GetByPK(sectionID);
			if (section == null)
				return null;
			if((section.IsGraduateClubOrChildren) 
				&& !User.GetOrDefault(x => x.InRole(Role.GraduateClubAccess)))
			{
				return BaseView(new PagePart("Доступ только для выпускников"));
			}
			if (section.Children.Any()) {
				var model2 = new SectionListVM {
					MessageSections = section.Children
						.AsQueryable().IsActive().ToList(),
					User = User
				};
				foreach (var messageSection in model2.MessageSections) {
					messageSection.MessageCount = MessageSectionService
						.SectionMessageCounts().GetValueOrDefault(messageSection
							.MessageSectionID);
					messageSection.LastMessageDate = MessageSectionService
						.SectionLastMessageDates().GetValueOrDefault(messageSection
							.MessageSectionID);
				}
				return View(ViewNames.SectionList, model2);
			}
			pageIndex = pageIndex ?? 1;
			var messages = UserMessageService.GetAll()
				.Where(um => um.MessageSectionID == sectionID && um.IsActive)
				.OrderByDescending(um =>
					um.Children.Max(um2 => (DateTime?) um2.CreateDate)
						?? um.CreateDate)
				.ToPagedList(pageIndex.Value - 1);
			var messageIds = messages.Select(x => x.UserMessageID).ToList();
			var messageCounts = UserMessageService
				.GetAll(x => messageIds.Contains(x.UserMessageID))
				.Select(x => new {x.UserMessageID, x.Children.Count}).ToList()
				.ToDictionary(x => x.UserMessageID, x => x.Count);
			var model =
				new SectionMessageListVM {
					Messages = messages,
					MessageCounts = messageCounts,
					Section = section,
				};
			return View(ViewNames.MessageList, model);
		}

		[HandleNotFound]
		public ActionResult Details(long messageID, int? pageIndex) {
			pageIndex = pageIndex ?? 1;
			UserMessageRepository.LoadWith(x => x.CreatorUser, x => x.MessageSection);
			var message = UserMessageRepository.GetByPK(messageID);
			if (message == null || message.ReceiverUserID.HasValue)
				return null;
			var studentIds = _.List(message.CreatorUser.Student_ID.GetValueOrDefault());

			var userMessages = UserMessageRepository.GetAll(x => x.ParentMessageID == messageID)
				.OrderBy(x => x.UserMessageID)
				.ToPagedList(pageIndex.Value - 1);
			studentIds.AddRange(userMessages.Select(x => x.CreatorUser.Student_ID.GetValueOrDefault()));
			studentIds = studentIds.Where(x => x > 0).ToList();
		/*	var	bestStudents = ProfileService.FilterBestGraduate(studentIds);
			var	excelMasterStudents = ProfileService.FilterExcelMaster(studentIds);*/
			var	realStudents = ProfileService.FilterRealGraduate(studentIds);

			var model = new MessageVM {
				Message = message,
				Answers = userMessages
			};
			model.AllMessages.ForEach(x => {
				var studentId = x.CreatorUser.Student_ID.GetValueOrDefault();
				if(studentId > 0) {
					var types = new List<string>();
				/*	if(bestStudents.Contains(studentId)) {
						types.Add(UserMessage.BestGraduate);
					}
					if(excelMasterStudents.Contains(studentId)) {
						types.Add(UserMessage.ExcelMaster);
					}*/
					var real = realStudents.GetValueOrDefault(studentId);
					if(real != null) {
						types.Add(UserMessage.RealSpecialist + real);
					}
					x.BestTypes = types;
				}

			});
			return View(model);
		}


		public ActionResult Group(decimal groupID) {
			var message = GroupVMService.GetOrCreateGroupRootMessage(groupID);
			return RedirectToAction(() => Details(message.UserMessageID, 1));
		}

		[HandleNotFound]
		public ActionResult AddAnswer(long messageID) {
			var userMessage = UserMessageService.GetByPK(messageID);
			if (userMessage == null)
				return null;
			var model = new AddAnswerVM {
				Message = userMessage,
				CannotAddMessageToClub = CheckCannotAddMessageToClub(userMessage.MessageSection)
			};
			return View(model);
		}

		private const int messageMaxLength = 8000;

		bool CheckCannotAddMessageToClub(MessageSection section) {
			return section != null && section.IsGraduateClubOrChildren && 
				!User.InRole(Role.ForumAdmin | Role.Trainer | Role.Employee | Role.ContentManager) && 
				!StudentInGroupRepository.GetAll(x => 
				x.Student_ID == User.Student_ID && !CourseTC.SemSrt.Contains(x.Group.Course_TC) 
				&& BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC)).Any();
		}

		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		[ModelStateToTempData]
		public ActionResult AddAnswer(AddAnswerVM model) {
			if (model.IsLoad == EditMessageVM.LoadImage) {
				model.Message = UserMessageService.GetByPK(model.Message.UserMessageID);
				return ProcessImage(model);
			}
			var message = new UserMessage {
				ParentMessageID = model.Message.UserMessageID,
				IsActive = true,
				CreatorUserID = User.UserID,
				Text = model.Description
			};
			var parent = UserMessageService.GetAll(x =>
				x.UserMessageID == model.Message.UserMessageID 
				&& x.IsAnswered).FirstOrDefault();
			if(parent != null) {
				parent.IsAnswered = false;
			}

			InsertAndSubmit(message);

			return RedirectToAction(() => Details(model.Message.UserMessageID, 1));
		}

		private void CheckMaxLength(UserMessage message) {
			message.Text = StringUtils.SafeSubstring(message.Text, messageMaxLength);
		}

		[Authorize]
		public ActionResult AddMessage(int sectionID) {
			var messageSection = MessageSectionService.GetByPK(sectionID);
			return View(new EditMessageVM {
				MessageSection = messageSection,
				CannotAddMessageToClub = CheckCannotAddMessageToClub(messageSection)
			});
		}

		[Auth(RoleList = Role.Employee)]
		public ActionResult Edit(long messageId) {
			var message = UserMessageService.GetByPK(messageId);
			CheckPermission(message);

			return View(ViewNames.AddMessage, new EditMessageVM {
				MessageId = message.UserMessageID,
				Description = message.Text,
				MessageTitle = message.Title,
			});
		}

		private void CheckPermission(UserMessage message) {
			if (User.UserID != message.CreatorUserID && !User.InRole(Role.Admin))
				throw new PermissionException("message access");
		}

		[HttpPost]
		[ValidateInput(false)]
		[Auth(RoleList = Role.Employee)]

		public ActionResult EditPost(EditMessageVM model) {
			if (model.IsLoad == EditMessageVM.LoadImage) {
				return ProcessImage(model, ViewNames.AddMessage);
			}

			var message = UserMessageService.GetByPK(model.MessageId);
			CheckPermission(message);
			message.Title = model.MessageTitle;
			message.Text = model.Description;
			CheckMaxLength(message);
			UserMessageService.SubmitChanges();
			if(message.Parent.GetOrDefault(x => x.GroupID) > 0)
				return RedirectToAction(() => Group(message.Parent.GroupID.Value));
			return RedirectToAction(() => Section(
				message.MessageSectionID ?? message.Parent.MessageSectionID, 1));
		}


		[Authorize]
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult AddMessage(EditMessageVM model) {
			if (model.IsLoad == EditMessageVM.LoadImage) {
				model.MessageSection = MessageSectionService.GetByPK(
					model.MessageSection.MessageSectionID);
				return ProcessImage(model);
			}

			if (FluentValidate(model)) {
				var messageID = SaveMessage(model);
				return RedirectToAction(() => Details(messageID, 1));
			}
			model.MessageSection = MessageSectionService.GetByPK(
				model.MessageSection.MessageSectionID);

			return View(model);
		}

		private bool CheckMessageImage() {
			if (Request.Files.Count == 0)
				return true;
			var file = Request.Files[0];
			if (file.ContentLength == 0)
				return true;
			if (!file.FileName.ToLower().EndsWith(Urls.PhotoExt))
				ModelState.AddModelError("Image", "Фото только в формате jpg");
			if (file.ContentLength > UserImages.ForumMaxImageSize.Bytes)
				ModelState.AddModelError("Image", "Слишком большой файл");
			return ModelState.IsValid;
		}

		[Auth(RoleList = Role.ForumAdmin | Role.Trainer)]
		[HttpPost]
		public ActionResult Delete(long messageID) {
			var message = UserMessageService.GetByPK(messageID);
			if (User.InRole(Role.ForumAdmin) || (User.InRole(Role.Trainer) && message.CreatorUserID == User.UserID)) {
				UserMessageService.DeleteAndSubmit(message);
				if (message.ParentMessageID.HasValue)
					return RedirectToAction(() =>
						Details(message.ParentMessageID.Value, 1));
				if (message.MessageSectionID.HasValue)
					return RedirectToAction(() =>
						Section(message.MessageSectionID.Value, 1));
				
			}

			return RedirectBack();
		}

		[Auth(RoleList = Role.ForumAdmin)]
		public ActionResult AnsweredToggle(long messageID) {
			var message = UserMessageService.GetByPK(messageID);
			message.IsAnswered = !message.IsAnswered;
			UserMessageService.SubmitChanges();
			return RedirectBack();
		}

		private ActionResult ProcessImage(CreateMessageVM model, string viewName = null) {
			if (CheckMessageImage()) {
				var imageUrl = UserImages.SaveMessageImage(Request.Files, User.UserID);
				if (!imageUrl.IsEmpty()) {
					var value = ModelState["Description"].Value;
					image = HtmlControls.Image(imageUrl);
					ModelState["Description"].Value =
						new ValueProviderResult(
							new[] {model.Description + image},
							value.AttemptedValue + image, null);
				}
			}
			if (viewName.IsEmpty())
				return View(model);
			return View(viewName, model);
		}

		private long SaveMessage(EditMessageVM model) {
			var message = new UserMessage {
				CreatorUserID = User.UserID,
				IsActive = true,
				MessageSectionID = model.MessageSection.MessageSectionID,
				Title = model.MessageTitle,
				Text = model.Description,
			};

			InsertAndSubmit(message);
			return message.UserMessageID;
		}

		[Authorize]
		public ActionResult PrivateList(int receiverID, int? pageIndex) {
			pageIndex = pageIndex ?? 1;
			var sender = AuthService.CurrentUser;
			var receiver = UserService.GetByPK(receiverID);
			var userID = sender.UserID;
			var messages =
				UserMessageService.GetAll()
					.Where(um => (um.CreatorUserID == userID
						&& um.ReceiverUserID == receiverID)
							|| (um.CreatorUserID == receiverID
								&& um.ReceiverUserID == userID))
					.OrderByDescending(um => um.CreateDate)
					.ToPagedList(pageIndex.Value - 1);
			var model =
				new PrivateMessageListVM {
					Sender = sender,
					Receiver = receiver,
					Messages = messages,
				};
			return View(model);
		}

		[HttpPost]
		[Authorize]
		public ActionResult PrivateList(PrivateMessageListVM model) {
			var sender = User;
			var receiver = UserService.GetByPK(model.Receiver.UserID);
			if (!model.SendMessage.IsEmpty()) {
				var message = new UserMessage {
					Text = model.SendMessage,
					CreatorUserID = sender.UserID,
					ReceiverUserID = model.Receiver.UserID,
				};
				InsertAndSubmit(message);
				if (receiver.Employee_TC != null) {
					MailService.NewUserMessage(message,
						Html.ActionLinkWithDomain<MessageController>(
							c => c.PrivateList(sender.UserID, null),
							"Отправить сообщение через сайт"));
				}

				ShowMessage(
					"Ваше сообщение Вашему персональному менеджеру успешно отправлено." +
						" Мы обязательно ответим на Ваше сообщение.");
			}

			return RedirectToAction(() => PrivateList(receiver.UserID, 1));
		}

		private void InsertAndSubmit(UserMessage message) {
			CheckMaxLength(message);
			UserMessageService.InsertAndSubmit(message);
		}

		/*
		[AjaxOnly]
		public ActionResult MessageBlock() {
			var messages =
				UserMessageService.GetAll()
					.Where(m => m.MessageSectionID == MessageSections.LearningId)
					.OrderByDescending(um => um.CreateDate).Take(3);
			return View(messages);
		}
*/
	}
}