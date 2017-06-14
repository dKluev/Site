<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<List<Employee>>>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <% if(!Model.Data.Any()){ %>
			Ничего не найдено
            <% }else{ %>

    <% foreach (var employees in Model.Data.Cut(2)) { %>
    <div class="tab_2column">
        <% foreach (var employee in employees) { %>
        <div class="branches_block">
            <% if(employee != null){ %>
            <%= Images.Employee(employee.Employee_TC).Width(66) %>
            <h3><%= Html.EmployeeLink(employee) %></h3>
            <% } %>
        </div>
        <% } %>
    </div>
    <% } %>

            <% } %>
</asp:Content>