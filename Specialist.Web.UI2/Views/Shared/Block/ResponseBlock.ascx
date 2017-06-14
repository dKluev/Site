<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Response>" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<div class="block_comment">
	    <%= Model.PhotoUrl.GetOrDefault(x => H.Anchor(Model.SocialUrl, H.Img(x).FloatLeft())) %>
	<div class="opinion">
		<%= Model.Description %>
	</div>
	<p class="comment_info">
		<% if (!Model.Authors.IsEmpty()) { %>
		<strong class="text_red">Cлушатель:</strong>
		<%= Model.Authors %>
		<% } %>
		<% if (Model.Course != null && Model.Course.IsActive) { %>
		<br />
		<strong class="text_red">Отзыв о курсе:</strong>
		<%= Html.CourseLinkShortName(Model.Course) %>
		<% } %>
		<% if (Model.Employee != null && Model.Employee.SiteVisible) { %>
		<br />
		<strong class="text_red"><%= Model.Employee.IsTrainer ? "Преподаватель" : "Сотрудник" %>: </strong>
		<%= Html.EmployeeEntityLink(Model.Employee) %>
		<% } %>
	</p>
</div>
