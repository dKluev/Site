<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SimpleUtils.Collections.Paging.PagedList<News>>" %>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<% foreach (var news in Model) { %>
		<div class="block_action">
			<div class="action_img">

				<% if(news.YouTubeId != null){ %>
					<%= Htmls2.YouTube(news.YouTubeId, news.Title)    %>
				<% }else{ %>
					<%= Images.NewsSmall(news)%>
				<% } %>
			</div>
          	<div class="action_text">

            <h3>
                <%= Html.NewsLink(news) %></h3>
        	<p>
        	    <%= news.ShortDescription %>
        	</p>
				<p class="details">
        	    <%= Html.NewsLinkRead(news) %> </p>
			<p class="date">
                <%= news.PublishDate.DefaultString() %></p>
			</div>
		</div>
    <% } %>
		<%= Url.Link<RssController>(c => c.News(), Images.Common("rss.gif").ToString()) %>
        <%= Html.GetNumericPager(Model, "{0}", Url.Link<SiteNewsController>(c => c.All(null), "Все").ToString()) %> 
