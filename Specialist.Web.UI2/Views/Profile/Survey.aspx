<%@ Import Namespace="DynamicForm" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Secondary.QuestionAnswer>" %>

<%@ Import Namespace="Specialist.Web.Util" %>

<%@ Import Namespace="Specialist.Entities.Utils" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
	<title>Опрос</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<%= Htmls.Title("Опрос") %>
	<% using (Html.BeginForm()) {%>
		<%= Html.HiddenFor(x => x.UserID) %>
		<% Htmls.FormSection(" ", () => {%>
			<%= Html.SelectForWithList(x => x.Question1, SelectListUtil.GetSelectItemList(
		_.List("Нет, все осталось на текущем уровне", 
			"Да, частично", 
			"Да, значительно"))) %>
			<%= Html.ControlFor(x => x.Question2) %>
			<%= Html.SelectForWithList(x => x.Question3, SelectListUtil.GetSelectItemList(
				Enumerable.Range(0,11).Select(x => x.ToString()))) %>
			<%= Html.ControlFor(x => x.Question4) %>
			<%= Html.ControlFor(x => x.Question5) %>
		<% }); %>
		<%= Htmls.Submit("ok") %>
	<% } %>
</asp:Content>
