<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<News>>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>


<div class="news_in_main">
    <div class="h2_over_blue png">
        <h2> <%= Url.SiteNews().List(NewsType.Main,1, "Новости обучения и сертификации") %></h2>
    </div>
    <div class="block">
        <div class="anons_news">
		<br />
		<ul class="square1"  style="margin-left:15px;">
        <% Model.ForEach((news,i) => { %>
		  <li> <span class="date"><%= news.PublishDate.ShortString() %></span>  
		  <%= news.IsHot ? (object)H.strong[Url.NewsAnchor(news)] : Url.NewsAnchor(news)  %>
		  </li>
        <% }); %>
		</ul>
		<%= Htmls.HtmlBlock(HtmlBlocks.NewsToolbar) %>
        </div>
		
           
	</div>
	 
 </div>
