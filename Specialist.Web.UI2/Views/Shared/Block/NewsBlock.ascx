<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Center.ViewModel.NewsBlockVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="SimpleUtils.Linq.Data.LInq" %>
<% if(!Model.News.Any()) return; %>

<%= Htmls2.Menu2("Новости") %>


<div class="block_chamfered_in v_news">

<% Model.News.ForEach((news,i)=> { %>

        <h3><%= Html.NewsLink(news, news.Title) %></h3>
    <p>
        <%= StringUtils.GetShortText(news.ShortDescription) %></p>
    	<p class="date"><%= news.PublishDate.DefaultString() %></p>
<% }); %>
<%--		<%= Htmls2.Rss(Url.Link<RssController>(c => c.News(), "RSS").Class("rss_a")) %>--%>
<p>
	<% if(Model.Entity != null && !(Model.Entity is SimplePage) && Model.News.Count == CommonConst.NewsCount){ %>
	<%= Url.Link<SiteNewsController>(c => c.Search(Model.Entity.GetType().Name.ToLowerInvariant(), 
	(int)LinqToSqlUtils.GetPK(Model.Entity), 1), "Все новости раздела").Class("all") %>

	<% }else{ %>
	<%= Html.News("Все новости").Class("all") %>

	<% } %>
	</p>

</div>


