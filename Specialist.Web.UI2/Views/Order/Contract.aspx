<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<ContractVM>" %>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Order.Const" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.8/themes/smoothness/jquery-ui.css" type="text/css" />
<title><%= Model.Title %></title>
	<style type="text/css">
	.ui-button { margin-left: -1px; }
	.ui-button-icon-only .ui-button-text { padding: 0.35em; } 
	.ui-autocomplete-input { margin: 0; padding: 0.45em 0 0.47em 0.45em; }
	.uicombobox {width:600px;}
	</style>

</asp:Content>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%--        <% if (Model.Cart.Order.NotOnlyWebinar) {%>--%>
<%--                <%= Model.GetOffer(SimplePages.Offers.Universal) %>--%>
<%--        <% } %>--%>
<%--        <% if(Model.Cart.Order.GetHasWebinar()){ %>--%>
<%--                <%= Model.GetOffer(SimplePages.Offers.Webinar) %>--%>
<%--                <%= Model.GetOffer(SimplePages.Offers.WebinarRu) %>--%>
<%--        <% } %>--%>



</asp:Content>
