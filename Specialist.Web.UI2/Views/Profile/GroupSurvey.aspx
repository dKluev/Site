<%@ Import Namespace="DynamicForm" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Secondary.GroupSurvey>" %>

<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Util" %>

<%@ Import Namespace="Specialist.Entities.Utils" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>Опрос</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<%= Htmls.Title("Опрос перед началом обучения") %>
    <%= H.p[H.b["Курс: "],StringUtils.AddTargetBlank(Html.CourseLink(Model.Group.Course))] %>
    <%= H.p[H.b["Преподаватель: "], StringUtils.AddTargetBlank(Html.EmployeeLink(Model.Group.Teacher))] %>
	<% using (Html.BeginForm()) {%>
		<%= Html.HiddenFor(x => x.Student_ID) %>
		<%= Html.HiddenFor(x => x.Group_ID) %>
		<% Htmls.FormSection(" ", () => {%>
			<%= Html.ControlFor(x => x.Reply1) %>
			<%= Html.ControlFor(x => x.Reply2) %>
		<% }); %>
		<%= Htmls.Submit("ok") %>
	<% } %>
</asp:Content>
