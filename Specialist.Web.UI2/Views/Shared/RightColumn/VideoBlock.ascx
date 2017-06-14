<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.Video>>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Site" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<% if (!Model.Any())return; %>

<% if (Htmls.IsNewVersion) { %>
	<%= Htmls2.ChamBegin() %>

<%= Htmls2.Menu2(Url.Videos()) %>


		<%= H.Div("spectv_bg")[Model.Take(3).Select(video => H.l(
	H.h4[ Url.Center().Video(video.VideoID, video.UrlTitle, video.Name).Style("color:#1a405e;")], Htmls2.YouTube(video.YouTubeID, video.Name)))] %>

<p><strong>
    <%= Url.Videos("Все видео").Style("color:#1458ae; font-size:12px;").Class("link") %>
</strong></p>
	
		<%= Htmls2.BlockEnd() %>

	


<% }else { %>

<div class="specialist-tv">
	<h2>
		<%= Url.Videos().Style("color:#0A5292;") %> </h2>
		<div class="spectv_bg">

		<%= CommonSiteHtmls.Carousel(Model.ToList().GetRows(3).Select(videos => videos.Select(video => H.l(
	Htmls2.YouTube(video.YouTubeID, video.Name),H.h3[ Url.Center()
	.Video(video.VideoID, video.UrlTitle, video.Name).Style("color:#000066;")])).ToList())) %>
	</div>
</div>



<% } %>
