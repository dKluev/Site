<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.ViewModel.DirectBannerPricesVM>" %>
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
	<title>���� ����������</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>���� ����������</h2>

 <% using(Html.BeginForm<HomeController>(c => c.DirectBannerPrices(null,null, null))){ %>
 <p>
       <label>ID ��������</label>
       <%= Html.EditorFor(x => x.CompanyId) %>
 </p>
 <p>
       <label>ID ����������</label>
       <%= Html.EditorFor(x => x.BannerId) %>
 </p>
 <p>
       <label>API �����</label>
       <%= Html.EditorFor(x => x.Token) %>
        <%= H.Anchor(YandexAuthService.authUrl, "�������� �����") %>
 </p>
<%= HtmlControls.Submit("��������") %>
 <% } %>
 
 <% if(Model.BannerId.HasValue){ %>
 
<%= H.div[Model.Prices.Select(x => H.p[H.h3[x.Phrase], H.p[x.Prices.Select(z => z + " ")]])] %>
 
 <% } %>

</asp:Content>

