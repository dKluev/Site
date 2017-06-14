<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.PrerequisiteTestVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<table>
	<tbody>
		<tr>
			<td align="center" valign="top">
				<p>
					<%= Images.Entity(Model.Course)
	  .Alt(Model.Test.Name).Style("margin-left:10px;margin-right:10px") %>
				</p>
	<% if (Model.Test.Author != null && Model.Test.Author.SiteVisible) { %>
				<p style="text-align: center; font-size: 10px;">
					Автор:<br />
					<%= Html.EmployeeLink(Model.Test.Author) %></p>
				<% } %>

	</td>
			<td valign="top">
			    <% if(Model.CoursePrerequisite.Text.IsEmpty()){ %>
			Данный тест предназначен для оценки Вашей предварительной подготовки к прохождению выбранного Вами курса. Наличие предварительной подготовки является залогом Вашего успешного обучения. Если Вы не обладаете знаниями, достаточными для прохождения теста на 80%, мы рекомендуем Вам получить предварительную подготовку. Только после этого Вы сможете качественно обучиться на выбранном курсе.
				<% }else{ %>
                <%= Model.CoursePrerequisite.Text %>
                <% } %>
<% if(Model.PrerequisiteCourses.Any()){ %>
				<p>
					<span style="color: #a41212"><strong>»</strong></span> <strong>Для успешной сдачи теста, необходимы знания по следующим курсам:</strong></p>
<%= Htmls.DefaultList(Model.PrerequisiteCourses.Select(x => Html.CourseLink(x))) %>
				<% } %>
<div style="padding-bottom: 5px;"><b style="color: red"><%= CommonTexts.NoCert %></b></div>
<% if(Model.Test.IsActive){ %>
				<%= Url.TestRun().Prerequisite(Model.Test.Id,Model.Course.Course_TC, 
	Images.Button("teststart").ToString()) %>
<% } %>
			</td>

</table>


