<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>


<% Html.RenderAction<PageController>(c => c.NearestGroupMobile()); %>