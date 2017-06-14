using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using Specialist.Web.Const;

namespace Specialist.Web.Core.Views {
	public class GroupInfoView : BaseView<GroupInfo> {

		public override object Get() {
			var isTestAdmin = User.InRole(Role.TestAdmin);
			var sendManagerButton = 
					AjaxForm(Url.GroupTest().Urls.SendGroupTestInfo(Model.Id, true))[
					SaveButton("Отправить письмо менеджеру группы")];
			var selfSendButton = AjaxForm(Url.GroupTest()
				.Urls.SendGroupTestInfo(Model.Id, false))[
					p["Вы можете отправить письмо со все информацией о группе тестирования"],
					SaveButton("Отправить письмо себе")];
			var controls = l(
				div.Class("ui-widget-content ui-corner-all").Style("padding:5px;")[
				sendManagerButton ,isTestAdmin ? selfSendButton : null]);

			if(isTestAdmin) {
				var button = 
					AjaxForm(Url.GroupTest().Urls.GroupInfoComplete(Model.Id))[
					SaveButton("Активировать группу")];
				var button3 = 
					AjaxForm(Url.GroupTest().Urls.RegisterUsers(Model.Group_ID))[
					SaveButton("Зарегистрировать дополнительных слушателей")];
				controls.Add(button);
				controls.Add(button3);
			}
			controls.Add(
				AjaxForm(Url.Action<GroupTestController>(c => c.EditGroupInfo(0,null)))[
					HiddenFor(x => x.Id),
					ControlFor(x => x.Notes),
					SaveButton()
				]);
			return div[controls];
		}
	}
}