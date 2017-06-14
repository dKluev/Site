<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<List<Employee>>>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


	<div class="tab_content">
            <% foreach (var employees in Model.Data.Cut(2)) { %>
            <div class="tab_2column">
                <% foreach (var employee in employees) { %>
                <div class="branches_block">
                    <% if(employee != null){ %>
                    <%= Images.Employee(employee.Employee_TC) %>
                    <h3> <%= Html.EmployeeLink(employee) %> </h3>
                    <p>
                        <% var icq = employee.EmployeeContacts.FirstOrDefault(ec =>
                       ec.ContactType_ID == ContactTypes.Specialist.Icq)
                       .GetOrDefault(x => x.ContactValue); %>
                    </p>
                   
                    <% } %>
                   
                </div>
                <% } %>
            </div>
            <% } %>
			</div>

</asp:Content>


