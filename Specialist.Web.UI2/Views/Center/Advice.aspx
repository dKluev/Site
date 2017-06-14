<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<AdviceVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h3> <%= Html.EmployeeLink(Model.Advice.Employee) %>: <%= Model.Advice.Name %></h3>
<%= Images.Employee(Model.Advice.Employee_TC).FloatLeft() %>
<%= Model.Advice.Description %>
<%= Htmls.AddThis(Html) %>

</asp:Content>