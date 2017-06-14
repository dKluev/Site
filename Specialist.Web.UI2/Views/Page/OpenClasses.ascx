<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

	<% Html.RenderAction<CourseController>(c => c.WithOpenClasses()); %>


