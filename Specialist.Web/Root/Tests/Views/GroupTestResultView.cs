using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using System.Linq;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Const;
using Specialist.Web.Helpers;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Core.Views {
	public class GroupTestResultView : BaseView<GroupTestResultVM> {
		public override object Get() {
			return l(
				Model.GroupUserTestsList.Select(x => 
					div[GroupTestView(x.GroupTest), 
					UserTestsView(Url, x.UserTests)]), 
					br,
					Url.GroupTest().DownloadResult(Model.GroupInfo.Id, 
					"Скачать результаты") );
		}

		object GroupTestView(GroupTest groupTest) {
			return div.Style("margin:10px 0px;")[strong["Тест:"], Url.TestLink(groupTest.Test)];
		}

		public static object UserTestsView(UrlHelper url, List<UserTest> userTests) {
			return table.Class("defaultTable")[Head("ФИО", "Время", "Дата","Статус"),
				userTests.Select(x => Row(
					x.User.FullName,
					TimeSpan.FromSeconds(x.Time).ToString("mm\\:ss"),
					x.RunDate.DefaultWithTimeString(),
					url.UserTestLink(x, UserTestStatus.GetName(x.Status)).OpenInUiDialog())
					)];
		}
	}
}