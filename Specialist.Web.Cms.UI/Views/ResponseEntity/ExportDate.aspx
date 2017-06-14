<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DateTime>>" %>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Ёкспорт отзывов</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2> Ёкспорт отзывов </h2>
<% foreach (var yearDates in Model.GroupBy(d => d.Year)) { %>
<div class="ui-corner-all ui-widget-content" style="padding:5px">
    <h3><%= yearDates.Key %></h3>
    <% foreach (var monthDates in yearDates.GroupBy(d => d.Month)) { %>
        <h4><%= MonthUtil.GetName(monthDates.Key) %></h4> |
        <% foreach (var dates in monthDates.GroupBy(d => d.Day)) { %>
            <% var date = dates.First(); %>
            <%= Html.ActionLink<ResponseEntityController>(c => c.Export(date), 
                date.Day.ToString()) %> (<%= dates.Count() %>) |
        <% } %>
        <br />
    <% } %>
</div>
<% } %>
<br />


</asp:Content>

