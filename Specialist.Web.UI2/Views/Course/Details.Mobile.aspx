<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.ViewModel.MobileCourseVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
  <% if(Model.Section != null){ %>
	<%= MHtmls.Back(Url.SectionLink(Model.Section)) %>
<% } %>	
<% var course = Model.Course; %>
<div id="content" class="longlist">
  <h2  class="coursename"><%= course.GetName() %></h2>
  <p>Продолжительность - <%= course.BaseHours.ToIntString() %> ак. ч.</p>
  <% if(Model.HasPPPrice){ %>
  <%= Html.AddToCartMobile(c => c.AddCourse(course.Course_TC, null)) %>
  <% } %>
  <p><%= course.AnnounceDescription %> </p>
	  <div id="social-buttons"></div>
	
	<%= H.JavaScript().Src("/scripts/socials/otherbuttons.js?v=4") %>
</div>

<%= Html.Site().MobileCourseGroups(Model.Groups) %>

	

</asp:Content>
