<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SimplePage>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%= MHtmls.Back(Url.AboutCenter()) %>

<div id="content" class="longlist">
<%= MHtmls.Title("О Центре:") %>
<%= Model.Description %>

	  <div id="social-buttons"></div>
	
	<%= H.JavaScript().Src("/scripts/socials/otherbuttons.js?v=4") %>
	</div>

<% Html.RenderPartial(Views.Shared.Education.NearestGroupMobile); %>