<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<SectionVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="SimpleUtils.Linq.Data.LInq" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%= Images.Entity(Model.Section).FloatLeft() %>
<%= Model.Description.FirstPart %>
    <% Html.RenderAction<PageController>(c => c.Banner()); %>
    
    <% if(Model.Section.IsMain || Model.Section.RelationsAsMenu) { %>
        <% Html.RenderPartial(PartialViewNames.SubSections, Model.EntityWithTags); %>
		<%= Model.Description.SecondPart %>
		<%= SiteHtmls.Announces() %>
        <% if(Model.Section.Section_ID == Sections.Student){ %>
         <% Html.RenderAction<CenterController>(c => c.ActionsBlock()); %>
        <% } %>
		<div id="course-lists"></div>
    <% }else{ %>
	<% if (Model.EntityWithTags.Any()) { %>
    <ul class="mark_arrow2">
        <% foreach (var entityWithTag in Model.EntityWithTags.OrderBy(e => e.Entity.WebSortOrder)) { %>
            <li><%= Html.GetLinkFor(entityWithTag.Entity)%></li>
        <% } %>
    </ul>
	<% } %>
		<%= SiteHtmls.Announces() %>
		<% Html.RenderAction<CourseController>(c => c.CourseListFor(Model.Section)); %>
    <% } %>

	<% if (Model.Section.IsMain) { %>
    <% Html.RenderPartial(PartialViewNames.UserWorks); %>

	<% } %>
	

 <script type="text/javascript">
  	$(function () {
		var sectionId = <%= Model.Section.Section_ID %>;

		if($("#course-lists").exists())
			lazyContent("#course-lists", 
				'/course'+'/courselistsforsection?sectionId=' + sectionId,
				"#course-lists");
		$.get('/Course/CoursesForCarousel/' + sectionId,
			function (html) {
				var nearestCourses = $("div.nearest-courses").html(html);
				if(html){
					$("h2.announces-header").show();
					controls.initCarousel(nearestCourses);
				}
			});

  	});
  </script>
   
</asp:Content>
<asp:Content ContentPlaceHolderID="RightColumn" runat="server">
<%= Htmls2.ChamBegin(true) %>
    <% Html.RenderAction<PageController>(c => c.GuideFor(Model.Section)); %>
	  <% Html.RenderPartial(PartialViewNames.HotGroupBlock, Model.Announces); %>
    <% Html.RenderAction<PageController>(c => c.UserWorksFor(Model.Section)); %>
	




<%= Htmls2.BlockEnd() %>
    <% Html.RenderAction<PageController>(c => c.VideoFor(Model.Section)); %>
<%= Htmls2.ChamBegin(true) %>
    <% Html.RenderAction<PageController>(c => c.NewsFor(Model.Section)); %>
	  <%= Htmls2.Menu2("Направления", "grey") %>

	  <div class="block_chamfered_in">
	     <% Html.RenderAction<SectionController>(c => c.MainPageSections()); %> 

		        <p>
            <%= HtmlControls.Anchor(MainMenu.Urls.MainCourses ,
                                       "Все направления").Class("all") %></p>
	  </div>

    <% Html.RenderAction<LocationsController>(c => c.MetroBlock()); %>
    <% var professions = Model.Professions; %>
    <% if (Model.Section.Section_ID == Sections.Marketing && professions.Any()) { %>
    	<%= Htmls2.Menu2("Профессии") %>
        <%= Html.Site().Professions(Model.Professions) %>
    <% } %>
<%= Htmls2.BlockEnd() %>
</asp:Content>