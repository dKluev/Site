<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" 
    Inherits="System.Web.Mvc.ViewPage<EmployeeVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Const" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Center" runat="server">
<% var employee = Model.Employee; %>
	<div id="content" class="longlist">
	    <%= MHtmls.Title(employee.FullName) %>
	   <h3>Контакты</h3> 
        <% Html.RenderPartial(PartialViewNames.Contacts, employee);%>  
        </div>
</asp:Content>