<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MainCoursesVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Message" %>
<%@ Import Namespace="Specialist.Web.Common.Logic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <% Html.RenderAction<CenterController>(c => c.ActionsBlock()); %>
 <% Html.RenderAction<PageController>(c => c.Banner()); %>
   <% Html.RenderAction<CourseController>(c => c.MainCoursesSections()); %>

	  <% Html.RenderAction<CourseController>(c => c.NearestCourses()); %>
	  
	<div class="docs_graduation">
	    <%= Htmls.HtmlBlock(HtmlBlocks.MainCoursesDocuments) %>
	</div>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title> <%= HtmlTitleCreator.Get(null)?? "Выберите свой курс в «Специалистe» при МГТУ им.Баумана" %></title>
</asp:Content>

<asp:Content ContentPlaceHolderID="RightColumn" runat="server">
	
  <%= Url.Link<CourseController>(c => c.IsNew(),Images.Main("all-new-courses.jpg").Style("padding-bottom:10px;")) %>

<%= Htmls2.ChamBegin(true) %>
	
	<div id="hot-groups">
	    <% Html.RenderAction<PageController>(c => c.HotGroupsFor(null)); %>
	</div>
<%= Htmls2.BlockEnd()%>

</asp:Content>
