<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AllCourseListVM>" %>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>

<% var courses = Model.Common.GetFilteredByPriceCourses(); %>
<table class="defaultTable">
	<tr>
		<% if(Model.Common.HasIcons){ %>
		<th style="width:50px;"></th>
		<% } %>
		<th style="text-align: left;font-weight: bold;">
			Курсы обучения
		</th>
		<th class="subtitle">
			Ак.ч
		</th>
		<th>
			Цена
		</th>
		<th>
		    <%= Images.Main("ico_signup2.gif")%>
		</th>
	</tr>
   <% foreach (var courseListItemVM in courses) { %>
   <% var course = courseListItemVM.Course; %>
	<tr>
		<% if(Model.Common.HasIcons){ %>
		<td>
			  <%= course.IsNew ? Images.Main("new.gif").Title("Новый курс") : null %>
			  <%= course.IsHit ? Images.Main("hit.gif").Title("Хит продаж") : null %>
		</td>
		<% } %>
		<td class="td_course"> 
		<%= Html.CourseLink(course) %> 
		</td>

		<td><%= course.BaseHourCalc.ToIntString() %></td>
        <td><%= courseListItemVM.MinFulltimePrice.Item1.MoneyString() %></td>
        <td> <%= Html.AddToCart(course) %> </td>

	</tr>
    <% } %>	
</table>


