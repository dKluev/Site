<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.ViewModel.UpdateDirectVM>" %>
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
	<title>���������� ��������</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2>���������� ��������</h2>

 <% using(Html.BeginForm<HomeController>(c => c.UpdateDirect(null, null))){ %>
 <p>
       <label>ID ��������</label>
       <%= Html.EditorFor(x => x.CampaignId) %>
       <label>ID �������� ��� ���������� ��������� ����� ����������</label>
       <%= Html.EditorFor(x => x.TargetCampaignId) %>
 </p>
<%= HtmlControls.Submit("�������� ��������") %>
 <% } %>
 
 <% if(Model.CampaignId.HasValue){ %>
 
 <h3>�����</h3>
 <%= H.Ul(Model.Created.Select(YandexDirectTextUtils.BannerLink)) %>
 <h3>����������</h3>
 <%= H.Ul(Model.Updated.Select(YandexDirectTextUtils.BannerLink)) %>
 <h3>���������������</h3>
 <%= H.Ul(Model.Archive.Select(YandexDirectTextUtils.BannerLink)) %>
 
 <% } %>

</asp:Content>

