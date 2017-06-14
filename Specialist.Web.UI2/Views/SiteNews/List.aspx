<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"
	Inherits="System.Web.Mvc.ViewPage<NewsListVM>" %>

<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<%= Htmls2.BorderBegin() %>
	<ul class="bookmarks">
		<% foreach (var newsType in NewsType.All.Where(x => !x.HideFromTabs)) { %>
		<% if (newsType.NewsTypeID == Model.Type.NewsTypeID) { %>
		<li class="active">
			<% } else { %>
			<li>
				<% } %>
				<%= Html.ActionLink<SiteNewsController>(c => 
					c.List(newsType.UrlName, null), newsType.Name) %>
			</li>
			<% } %>
            <li><%= Url.SiteNews().All(null, "Архив новостей") %></li>
	</ul>
	<div class="tab_content">
	<% Html.RenderPartial(PartialViewNames.NewsList, Model.News); %>
	</div>
	<%=Htmls.BorderEnd %>
</asp:Content>
