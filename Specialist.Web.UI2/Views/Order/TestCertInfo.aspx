<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Tests.ViewModels.TestCertInfoVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Информация для сертификата тестирования</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%= Htmls.Title("Информация для сертификата тестирования") %>
		<% using (Html.DefaultForm<OrderController>(c => c.TestCertInfo(null))) { %>
            <%= Html.ValidationSummary() %>
            <%= Html.HiddenFor(x => x.IsEngCert) %>
            <%= Html.HiddenFor(x => x.IsPaper) %>
            
        <% if(Model.IsPaper) { %>
            <% Htmls.FormSection("Адрес", () => {%> 
	        <%= Html.SelectFor(x => x.UserAddress.CountryID, Model.Countries) %>
            <%= Html.ControlFor(x => x.UserAddress.City) %>
            <%= Html.ControlFor(x => x.UserAddress.Index) %>
            <%= Html.ControlFor(x => x.UserAddress.Address) %>
            <% }); %> 
        <% } %>
        <% if(Model.IsEngCert) { %>
            <% Htmls.FormSection("Личная информация", () => {%> 
            <%= Html.ControlFor(x => x.User.EngFullName) %>
            <% }); %> 
        <% } %>
            <%= Htmls.Submit("ok") %>
         <% } %>


</asp:Content>
