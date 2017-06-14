<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEntityCommonInfo>" %>
<%@ Import Namespace="SimpleUtils.Linq.Data.LInq" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Logic" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title> <%= HtmlTitleCreator.Get(null) ?? Htmls.CommonTitle(Model.Name) %> </title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	
<% var product = Model as Product; %>
<% if(product != null && product.Product_ID == 118){ %>
<%= Htmls.Title("Курсы Photoshop. Графический редактор Фотошоп для начинающих и профессионалов.") %>

<% }else{ %>
    <% var title = Model is Profession
           ? "{0}: подготовка по специальности".FormatWith(Model.Name)
           : StringUtils.CoursesPrefix(Model.Name); %>
<%= Htmls.Title(title) %>

<% } %>


<%= Images.Entity(Model).FloatLeft() %>
<%= Model.Description %>

<% if(ViewBag.Seminars != null){ %>
<h2>Пробные вебинары</h2>
	<% Html.RenderPartial(Views.Shared.Education.SeminarList,(object)ViewBag.Seminars); %>
<% } %>
<%= SiteHtmls.Announces() %>
<% Html.RenderAction<PageController>(c => c.Banner()); %>
<% Html.RenderAction<CourseController>(c => c.CourseListFor(Model)); %>




<script type="text/javascript">
  	$(function () {
	
		$.get('<%= Url.Action<CourseController>(c => c
			.CarouselForEntity(Model.GetType().Name, LinqToSqlUtils.GetPK(Model))) %>',
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
    <% Html.RenderAction<PageController>(c => c.GuideFor(Model)); %>
    <% Html.RenderAction<PageController>(c => c.HotGroupsFor(Model)); %>
    <% Html.RenderAction<PageController>(c => c.UserWorksFor(Model)); %>
	<%= Htmls2.BlockEnd() %>
    <% Html.RenderAction<PageController>(c => c.VideoFor(Model)); %>
	<%= Htmls2.ChamBegin(true) %>
    <% Html.RenderAction<PageController>(c => c.NewsFor(Model)); %>
    <% Html.RenderAction<LocationsController>(c => c.MetroBlock()); %>
	<%= Htmls2.BlockEnd() %>
</asp:Content>
