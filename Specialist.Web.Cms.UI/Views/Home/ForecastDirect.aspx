<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.ViewModel.ForecastDirectVM>" %>
<%@ Import Namespace="Console" %>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Cms.Util" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="DynamicForm.Utils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Helpers" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Прогноз</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Прогноз</h2>

 <% using(Html.BeginForm<HomeController>(c => c.ForecastDirect(null))){ %>
 <p>
       <label>ID компании</label>
       <%= Html.EditorFor(x => x.CampaignId) %>
 </p>
<%= HtmlControls.Submit("Начать формирование") %>
 <% } %>
 
 <h2>Прогнозы</h2>
 <%= Htmls.DefaultList(Model.Files.Select(x => H.Anchor("/temp/forecast/" + x,x).ToString())) %>

</asp:Content>

