<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Center.ViewModel.NewsVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
<%= MHtmls.Back(Url.NewsListLink()) %>
<div id="content" class="longlist">
<%= MHtmls.Title(Model.News.Name) %>	
<%= Model.News.Description %>
</div>

</asp:Content>
