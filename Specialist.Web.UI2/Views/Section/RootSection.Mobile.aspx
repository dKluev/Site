<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Catalog.ViewModel.SectionVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
	<% var children = Model.SubSections; %>
<%= MHtmls.Back( Url.CoursesLink()) %>

<% if(Model.Section.IsMain && children.Any()) { %>

	<%= MHtmls.LongList(MHtmls.Title(Model.Section.Name),
         Html.Site().MobileSections(
		 children.OrderBy(x => x.WebSortOrder))) %>
<% }else{ %>
<div id="content" class="longlist">
<%= MHtmls.Title(Model.Section.Name) %>	
	<% Html.RenderAction<CourseController>(c => c.CourseListFor(Model.Section)); %>
</div>
<% } %>
<% Html.RenderPartial(Views.Shared.Education.NearestGroupMobile); %>
</asp:Content>
