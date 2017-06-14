<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<VacancyListVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% foreach(var vacancy in Model.Vacancies){ %>
    <p class="vacancy">
        <span class="date">
 <%--        <%= vacancy.PublishDate.DefaultString() %> --%> </span><br/>
        <%= Html.VacancyLink(vacancy) %>
    </p>
<% } %>

<%= Html.GetNumericPagerPretty(Model.Vacancies) %>

</asp:Content>


