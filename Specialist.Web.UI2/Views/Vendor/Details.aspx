<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<VendorVM>" %>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% var tabs = VendorVMHelper.GetTabs(Url, Model.Vendor); %>
<%
   var currentTabContent = string.Empty; 
if(Model.CurrentTab == 0)
{
	currentTabContent =
		Images.Entity(Model.Entity).FloatLeft() +
			Model.Entity.Description +
				Html.Action("Banner", "Page") +

					(Model.EntityWithTags.Any()
						? (Html.Partial(PartialViewNames.SubSections,
							Model.EntityWithTags).ToString() 
							+ SiteHtmls.Announces()
							+ H.div.Id("course-lists") )
						: Htmls2.MarkArrow(Model.EntityWithTags
							.Select(x => Html.GetLinkFor(x.Entity))).ToString()
							+ SiteHtmls.Announces() 
				
						) ;
	 
 if(!Model.EntityWithTags.Any()) {
 	currentTabContent += Html.Action("CourseListFor", "Course", new {obj = Model.Vendor});
 } 
}
else if(Model.CurrentTab == 1)
	currentTabContent = Model.GetCertificationDescription(Html) + Html.Action("Banner", "Page");
else
	currentTabContent = 
		Model.Vendor.TestingDescription + Html.Action("Banner", "Page") + H.br + H.br+	
		H.strong[Url.Link<ExamController>(c => c.Search(Model.Vendor.Vendor_ID, null), "Поиск экзамена по номеру")] +
		H.br +
		H.br +
		Html.Partial(PartialViewNames.Testing, Model.Exams);
	%>
<%= Htmls2.Tabs(tabs.ToArray(), new []{currentTabContent},false, 2 , Model.CurrentTab) %>





 <script type="text/javascript">
  	$(function () {
		var vendorId = <%= Model.Vendor.Vendor_ID %>;
		if($("#course-lists").exists())
			lazyContent("#course-lists", 
				'/course'+'/courselistsforvendor?vendorId=' + vendorId,
				"#course-lists");
		$.get('<%= Url.Action<CourseController>(c => c
			.CarouselForEntity(Model.Vendor.GetType().Name, Model.Vendor.Vendor_ID)) %>',
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
	    <% Html.RenderAction<PageController>(c => c.GuideFor(Model.Vendor)); %>
	<%= Htmls2.BlockEnd() %>
    <% Html.RenderAction<PageController>(c => c.VideoFor(Model.Vendor)); %>
	<%= Htmls2.ChamBegin(true) %>
	    <% Html.RenderAction<PageController>(c => c.NewsFor(Model.Vendor)); %>
        <% Html.RenderAction<LocationsController>(c => c.MetroBlock()); %>
	<%= Htmls2.BlockEnd() %>

</asp:Content>

