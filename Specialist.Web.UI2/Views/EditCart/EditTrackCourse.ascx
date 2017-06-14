<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EditCourseVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>

<div id="selectCourseBin">
    <h4>Курс:</h4>
	<p> <%= Html.CourseLink(Model.OrderDetail.Course) %> </p>
<% using (Html.BeginForm<EditCartController>(c => c.EditCourse(null))) {%>
    <%= Html.HiddenFor(x => x.OrderDetail.OrderDetailID) %>
    <%= Html.HiddenFor(x => x.IsTrackCourse) %>
    <% Html.RenderPartial(Views.EditCart.GroupList, Model.GetGroupListVM(Model.CityTC, Model.OrderDetail.IsWebinar)); %> 
    <div><%= Images.Submit("ok") %></div> 
<% } %>

</div>