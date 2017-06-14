<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.ViewModel.TrackVM>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Logic" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Specialist.Web.Util"%>
<%@ Import Namespace="MvcContrib.Unity"%>
<%@ Import Namespace="Specialist.Web.Helpers"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% var title = HtmlTitleCreator.Get(Model); %>
<%= Htmls.MetaTitle(title) %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	 <% if (Model.Course.IsNew || Model.Course.IsHit){ %> 
	<div style="float:left;margin-right: 1em;clear: right;">
		 <%= Images.Main("{0}-course.png"
			.FormatWith(Model.Course.IsNew ? "new" : "hit")) %>
	</div>
	<% } %>
    <%= Htmls.Title((Model.Course.IsDiplom ? Images.Main("diplom.gif").Style("vertical-align:middle; padding-right:7px;").Title("Дипломные программы") : null) + Model.Course.GetName()) %>
	<div class="float_left" style="text-align: center;">
	  <%= Images.Entity(Model.Course) %>
	<br />
	<% if(Model.Prices.Any()) { %>
	<div style="margin-top:10px;">
	    
        <% if(Model.CompleteCourseCount >= 50) { %>
            <p style="text-align:center;font-size:10px;">
            Эту <%= Model.Course.IsDiplom ? "Дипломную программу" : CommonTexts.TrackName2 %><br />
             в нашем Центре успешно закончили <br />
            <span class="discount_color"><%= Model.CompleteCourseCount %></span> человек!
            </p>
        <% } %>

        <%= Html.AddToCart(x => x.AddCourse(Model.Course.Course_TC, null), true)%>
	</div>
	<% } %>
	</div>

    <%= Model.Course.Description %>

    
    <% if (Model.Course.IsSchool) { %>
        <%= Htmls.HtmlBlock(HtmlBlocks.TrackSchool) %>
    <% } else { %>
        <%= Model.Course.IsIntraExtraTrack || Model.Course.IsDiplom ? null : Htmls.HtmlBlock(HtmlBlocks.TrackText) %>
        <%= Model.Course.IsDiplom ? Htmls.HtmlBlock(HtmlBlocks.TrackDiplom) : null %>
	<% } %>
    
    <%= Htmls.AddThis(Html) %>

<div class="clear"></div>
    <% Html.RenderPartial(PartialViewNames.CoursePrices, Model); %>
 <% Html.RenderAction<CourseController>(c => c.CourseListFor(Model.Course)); %>


	<!--useful <%= Model.Course.Course_TC %>-->
    
		<% Html.RenderPartial(Views.Shared.Catalog.CourseDocuments, Model); %>


</asp:Content>

<asp:Content ContentPlaceHolderID="RightColumn" runat="server">

	<%= Htmls2.ChamBegin(true) %>
	    <% Html.RenderAction<PageController>(c => c.GuideFor(Model.Course)); %>
		   <% Html.RenderPartial(PartialViewNames.MainNews, true);%>
	<%= Htmls2.BlockEnd() %>

</asp:Content>
