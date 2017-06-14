<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Section>>" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<% foreach (var section in Model) Html.RenderAction<CourseController>(c => c.CourseListFor(section)); %>

