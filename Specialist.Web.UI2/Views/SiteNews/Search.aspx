<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"
	Inherits="System.Web.Mvc.ViewPage<NewsListVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<% Html.RenderPartial(PartialViewNames.NewsList, Model.News); %>

</asp:Content>
