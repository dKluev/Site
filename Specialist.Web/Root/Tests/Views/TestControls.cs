using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using System.Xml.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using SpecialistTest.Web.Core.Mvc;
using Specialist.Web.Const;

namespace Specialist.Web.Core.Views {
	public class TestControls : BaseView {


		public static object TestFileUpload(object fileControl, 
			string uploadUrl, 
			string deleteUrl, 
			string controlUrl, bool isNew) {
			if(isNew)
				return "Перед загрузкой файла нажмите сохранить";

			return l(p[string.Format("Файл формата {0} размером не больше {1} Mb", 
				Urls.TestFileExts.JoinWith(", "), UserImages.MaxTestFileSize.MBytes) ],
				div.Class("test-image-content")[
					div.Class("test-image-buttons")[div.Class("upload-button")["Загрузить"]
					],
					div.Class("test-image-control")[fileControl],br,
					button.Class("delete-button")["Удалить"]],
				GetScript("/Scripts/Views/TestEdit/testFileUpload.js", "initTestFileUpload",
				uploadUrl, deleteUrl, controlUrl , Urls.TestFileExts)
				);
		}
		public static IHtmlTag QuestionFileView(int questionId, bool small, bool ts = false) {
			var file = UserImages.GetTestQuestionFileSys(questionId);
			return GetTestFileView(file, small, ts);
		}

		public static IHtmlTag AnswerFileView(int answerId, bool small, bool ts = false) {
			var file = UserImages.GetTestAnswerFileSys(answerId);
			return GetTestFileView(file, small, ts);
		}

		public static object ModulePercentsView(Dictionary<int, int> percents, List<TestModule> testModules) {
			return div[p["Если все проценты равны нулю, выборка по модулям будет равномерной"], 
				testModules.Select((m,i) => p[strong[m.Name],br,
				Hidden("ModulePercents[" + i + "].Key", m.Id ),
				InputText("ModulePercents[" + i + "].Value", 
				percents.GetValueOrDefault(m.Id).NotNullString()).Style("width:40px;")," %"])] ;
		}

		public static IHtmlTag GetTestFileView(string file, bool small, bool ts) {
			if (file == null)
				return null;
			var url = Urls.SysToWeb(file);
			if(ts)
				url = url + "?r=" + (DateTime.Now - new DateTime(2010, 1, 1)).TotalMilliseconds;
			if (Path.GetExtension(file) == Urls.Swf) {
					var embed = new XElement("embed");
				embed.SetAttributeValue("type", "application/x-shockwave-flash");
				embed.SetAttributeValue("src", url);
				embed.SetAttributeValue("pluginspage", "http://get.adobe.com/flashplayer/");
					var tagObject = @object.Classid("clsid:d27cdb6e-ae6d-11cf-96b8-444553540000")[
					param.Name("movie").Value(url),embed];

			/*	var tagObject = @object.Type("application/x-shockwave-flash").Data(url)[
					param.Name("movie").Value(url)];*/
				if (small)
					tagObject.Width(200);
				return tagObject;
			}
			var control = Img(url);
			if (small)
				control.Width(200);
			else {
				control.Style("max-width:100%;");
			}
			return control;
		}

		public static object StatsLinks(UrlHelper url, decimal groupId, bool isUsers) {
			var link = isUsers
				? url.GroupTest().PlanTestQuestionStats(groupId,
					"Показать статистику вопросов")
				: url.GroupTest().PlanTestUserStats(groupId, "Показать статистику слушателей");
				
			return b[link];
		}

		public override object Get() {
			throw new System.NotImplementedException();
		}
	}
}