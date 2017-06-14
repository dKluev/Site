<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exam>>" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<% if(!Model.Any()) return; %>
<p class="signs">
    <%= Images.Common("blue_bin.gif")%> - Записаться</p>
<% var hasOnePrice = Model.Any(x => x.ExamPrice.HasValue); %>
<table class="marketplace">
    <tr>
        <th> Номер экзамена </th>
        <th> Экзамен </th>
        <% if(hasOnePrice){ %>
        <th>
            <%= Images.Common("blue_bin.gif")%>
        </th>
        <% } %>
        <th>
            Номер курса
        </th>
        <th>
            Курсы для подготовки
        </th>
        <th>
            <%= Images.Common("blue_bin.gif")%>
        </th>
    </tr>
    <% Model.ForEach((exam, j) => { %>
    <% var courses = exam.Courses.Where(c => !c.IsTrackBool && c.IsActive); %>
    <% var rowSpan = courses.Any() ? courses.Count() : 1; %>
    <tr>
        <td class="step n<%= j + 1 %>" rowspan="<%= rowSpan %>">
            
            <%= exam.Exam_TC %>
        </td>
        <td rowspan="<%= rowSpan %>">
            <%= Html.ExamLinkName(exam) %>
        </td>
        <% if (hasOnePrice) { %>
        <td class="add-to-cart" rowspan="<%= rowSpan %>">
            <%= Html.AddToCart(exam) %>
        </td>
        <% } %>
        <% courses.ForEach((course, i) => { %>
        <% if (i > 0) { %>
        <tr>
            <% } %>
            <td class="step n<%= j + 1 %>">
                <%= course.Course_TC %>
            </td>
            <td>
                <%= Html.CourseLinkShortName(course) %>
            </td>
            <td>
                <%= Html.AddToCart(course) %>
            </td>
        </tr>
        <% }, () => { %>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <% }); %>
   
    <% }); %>
</table>
