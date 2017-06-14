<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Common.MailList.MailListVM>" %>
<%@ Import Namespace="Specialist.Entities.Profile" %>
<%@ Import Namespace="Specialist.Web.Cms.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Рассылка</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Рассылка</h1>
<% if(Model.SendedPercent.HasValue){ %>
<strong>В процессе - <%= Model.SendedPercent.GetValueOrDefault().ToString("n2") %>%</strong>
<%= Model.LastSendDate.GetValueOrDefault().NotNullString("HH:mm:ss") %>
<% } %>
<% if(!Model.SendedPercent.HasValue || Model.IsStopped){ %>
	<%= H.Form(Url.Action<MailTemplateEntityController>(c => c.SendFor(null)))[
		H.strong["Пробная рассылка"],
		H.input.Type("text").Name("emails").Size(100), H.button["Отправить"]
		] %>
	<h2><%= Model.Template.Name %></h2>
	<%= Model.Template.Description %>
	<br />
	Тематика: 
	<% foreach (var type in Enum.GetValues(typeof(MailListType))) {%>
	<% if((MailListType)type == Model.MailListType) { %>
	<strong><%= type.ToString() %></strong> 
	<% }else{ %>
	<%= type.ToString() %>
	<% } %>

	<% } %>
<%--	<%= H.Form(Url.Action<MailTemplateEntityController>(c => c.SendForAll()))[--%>
<%--		H.Submit("Разослать").Id("send-for-all").Class("confirm-dialog") ] %>--%>
<% }else{ %>
	<%= H.Form(Url.Action<MailTemplateEntityController>(c => c.StopSend()))[
		H.button["Остановить"]
		] %>
<% } %>
</asp:Content>

