<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exam>>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<% if(!Model.Any()) return; %>

<% var hasPrice = Model.Any(e => e.ExamPrice.HasValue); %>
<% var hasMinutes = Model.Any(e => !e.Minutes.IsEmpty()); %>
 <table class="defaultTable">
        <tr>
            <th> ����� </th>
            <th> �������� ����������������� �������� </th>
            <th> ���� </th>
            <% if (hasMinutes){ %>
            <th> ����., ���. </th>
            <% } %>
            <% if (hasPrice){ %>
            <th> �����., ���. </th>
            <th> </th>
            <% } %>
        </tr>
        <% foreach (var exam in Model) { %>
        <tr>
            <td> <%= Html.ExamLink(exam) %> </td> 
            <td> <%= exam.ExamName %> </td> 
            <td> <%= exam.Languages %> </td> 
            <% if(hasMinutes){ %>
            <td> <%= exam.Minutes %> </td> 
            <% } %>
            <% if(hasPrice){ %>
            <td> <%= exam.ExamPrice.MoneyString() %> </td> 
            <td> <%= Html.AddToCart(exam) %> </td>
            <% } %>
        </tr>
        <% } %>
    </table>