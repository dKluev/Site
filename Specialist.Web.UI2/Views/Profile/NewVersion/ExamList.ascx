<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Profile.ViewModel.LearningVM>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>


<% if(Model.Student.Exams.Any()) { %> 
    <table class="defaultTable">
        <tr>
            <th>
                Дата сдачи
            </th>
            <th>
                Экзамен
            </th>
        </tr>

    <% foreach (var sig in Model.Student.Exams) { %>
        <tr>
            <td><%= sig.Group.DateBeg.DefaultString() %></td>
            <% if(sig.Exam == null) { %>
            <td><%= sig.Group.Course.Name %></td>
            <% }else{ %>
            <td><%= Html.ExamLinkName(sig.Exam) %></td>
            <% } %>
        </tr>
    <% } %>
    </table>
<% }else{ %>
    Пока ничего нет
<% } %>
