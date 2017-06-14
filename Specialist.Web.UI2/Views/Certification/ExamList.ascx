<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exam>>" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<% if(!Model.Any()) return; %>
<p class="signs">
    <%= Images.Common("blue_bin.gif")%> - ����������</p>
<% var hasOnePrice = Model.Any(x => x.ExamPrice.HasValue); %>
<table class="defaultTable">
    <tr>
        <th> ����� �������� </th>
        <th> ������� </th>
        <% if(hasOnePrice){ %>
        <th>
            <%= Images.Common("blue_bin.gif")%>
        </th>
        <% } %>
        <th>
            ����� ��� ����������
        </th>
        <th>
            <%= Images.Common("blue_bin.gif")%>
        </th>
    </tr>
    <% foreach(var exam in Model){ %>
    <% var courses = exam.Courses.Where(c => !c.IsTrackBool && c.IsActive); %>
    <% var rowSpan = courses.Count() == 0 ? 1 : courses.Count(); %>
    <tr>
        <td rowspan="<%= rowSpan %>">
            <%= Html.ExamLink(exam) %>
        </td>
        <td rowspan="<%= rowSpan %>">
            <%= exam.ExamName %>
        </td>
        <% if(hasOnePrice){ %>
        <td rowspan="<%= rowSpan %>">
            <%= Html.AddToCart(exam) %>
        </td>
        <% } %>
        <% courses.ForEach((course, i) => {%>
        <% if(i > 0){ %>
        <tr>
            <% } %>
            <td>
                <%=Html.CourseLinkShortName(course)%>
            </td>
            <td>
                <%=Html.AddToCart(course)%>
            </td>
        </tr>
        <% }, () => { %>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <% }); %>
   
    <% } %>
</table>
