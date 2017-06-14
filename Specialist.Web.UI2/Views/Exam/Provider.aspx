<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"
	Inherits="System.Web.Mvc.ViewPage<ProviderVM>" %>

<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<%= Images.Entity(Model.Provider).FloatLeft() %>
	<%= Model.Provider.Description %>
	<div class="clear">
	</div>
	
	<% Html.RenderPartial(Views.Shared.Catalog.Vendors,
	       Model.Vendors.OrderBy(e => e.WebSortOrder).ToList()); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="RightColumn" runat="server">
	<%= Htmls2.ChamBegin(true) %>
	    <% Html.RenderAction<PageController>(c => c.NewsFor(Model.Provider)); %>
	<%= Htmls2.BlockEnd() %>
</asp:Content>