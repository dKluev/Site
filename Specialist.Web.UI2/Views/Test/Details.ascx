<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.TestVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Entities.Tests.Consts" %>
<%@ Import Namespace="Specialist.Web.Controllers.Tests" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<% if(Model.Test.Status.In(TestStatus.Edit, TestStatus.Audit)){ %>
<div class="attention2">
	<p>
В данный момент тест находится в стадии редактирования. Просим Вас с пониманием отнестись к этому. 
	</p>
<p>
Как только тест будет обновлен, доступ к нему сразу же возобновится. 
Спасибо за Ваше желание пройти тестирование в Центре «Специалист»! Ждем Вас на нашем сайте!
	
</p>
</div>

<% } %>

<div style="width: 100%;border:1px solid #DCDCDC;">

<table>
	<tbody>
		<tr>
			<td align="center" valign="top">
				<p>
					<%= Images.Image("Test/" + Model.Test.Id + ".png")
	  .Alt(Model.Test.Name).Style("margin-left:10px;margin-right:10px") %>
				</p>

				<% if (Model.Test.Author != null && Model.Test.Author.SiteVisible) { %>
				<p style="text-align: center; font-size: 10px;">
					Автор:<br />
					<%= Html.EmployeeLink(Model.Test.Author) %></p>
				<% } %>
			</td>
			<td valign="top" class="border-test">
				<%= Model.Test.Description %>
				<% if (Model.Courses.Any()) { %>
				<p>
					<span style="color: #a41212"><strong>»</strong></span> <strong>Рекомендуемые курсы:</strong></p>
				<%= Htmls.DefaultList(Model.Courses.Select(s => Html.CourseLink(s))) %>
				<% } %>
<% if(Model.IsActiveCalc){ %>
		<% if(!Model.Test.Certified){ %>
<div style="padding-bottom: 5px;"><b style="color: red"><%= CommonTexts.NoCert %></b></div>
<% } %>

				<%= Model.Test.IsEnglish && !Request.IsAuthenticated ? 
    Url.TestRun().Prerequisite(Model.Test.Id,CourseTC.An1, Images.Button("teststart").ToString()) 
    : Url.TestRun().Details(Model.Test.Id, Images.Button("teststart").ToString())
    %>
		<p><br/></p>
<% } %>
<% if(!Model.Test.CompanyId.HasValue){ %>
				<%= Html.AddThis() %>
<% } %>
			</td>
			<td valign="top" id="stat-block">
				<h4>
					Статистика</h4>
				<table border="0" cellpadding="0" cellspacing="0" width="175">
	<% foreach (var testStat in Model.TestStats) { %>
						<tr>
							<td align="left">
								<div> <%= testStat.Item1 %>:</div>
							</td>
							<td align="right">
								<div> <%= testStat.Item2 %></div>
							</td>
						</tr>
				<% } %>
				</table>
				<p>
					&nbsp;</p>
			</td>
		</tr>
	</tbody>
</table>
</div>
<%--
<% if (Model.Sections.Any()) { %>
<h2>Направления</h2>
<%= Htmls.DefaultList(Model.Sections.Select(s => Url.TestSectionLink(s).ToString())) %>
<% } %>
--%>
<div id="other-tests">
	<% if (Model.PrevTests.Any()) { %>
	<div id="easier-tests">
		<h3>
			Более простые тесты</h3>
		<div class="block_yellow">
			<%= Htmls.DefaultList(Model.PrevTests.Select(s => Url.TestLink(s).ToString())) %>
		</div>
	</div>
	<% } %>
	<% if (Model.NextTests.Any()) { %>
	<div id="harder-tests">
		<h3>
			Более сложные тесты</h3>
		<div class="block_yellow">
			<%= Htmls.DefaultList(Model.NextTests.Select(s => Url.TestLink(s).ToString())) %>
		</div>
	</div>
	<% } %>
</div>
