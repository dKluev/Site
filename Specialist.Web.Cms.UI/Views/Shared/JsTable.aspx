<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.Core.ViewModel.JsTableVM>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Cms.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title><%= Model.Title %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1><%= Model.Title %></h1>
<% Html.RenderPartial(PartialViewNames.JsTableControl); %>
</asp:Content>

