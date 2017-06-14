<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ConsultationListVM>" %>
<%@ Import Namespace="Specialist.Entities.Education.ViewModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
Консультации <br />
<% foreach (var consultation in Model.Consultations) { %>
    <%= consultation.DateBeg %> - <%= consultation.Notes %>
<% } %>

Семинары <br />
<% foreach (var seminar in Model.Seminars) { %>
    <%= seminar.DateBeg %> - <%= seminar.Notes %>
<% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
