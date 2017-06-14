<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Common.Site" %>
<%= Htmls2.Menu2(H.Anchor(SimplePages.FullUrls.Awards,"Наши награды")) %>
<% var images = _.List(
	   Tuple.Create( Urls.Image("SimplePage/lern-certb.jpg"), Urls.Image("SimplePage/lern-cert.jpg"),"LERN"),
	   Tuple.Create( Urls.Image("SimplePage/Microsoft_2010.jpg"), Urls.Image("oldsite/images/News/Microsoft_2010.jpg"),
	   "Лидер Майкрософт в восточной Европе"),
	   Tuple.Create( Urls.Image("News/prava.jpg"), Urls.Image("News/prava-s.jpg"),"Права потребителей"
	   ),
	   Tuple.Create( Urls.Image("SimplePage/raekspert2011.jpg"), Urls.Image("SimplePage/raekspert2011_sm.jpg"),"Рейтинг эксперта")
	); %>
		<div style="margin: 1em 0;height:167px;">
		<%--	<%= H.Anchor(SimplePages.FullUrls.DocumentsOff,
							 Images.Image(certType.GetType().Name + "/Small/" + certType.UrlName + ".jpg").Alt(certType.Name).ToString())
						.Title(certType.Name) %>--%>

	<%= CommonSiteHtmls.Carousel(images.Select(x => 
	H.div.Style("text-align:center;")[H.Anchor(x.Item1, H.Img(x.Item2)
	.Style("max-width:160px;")).Class("fancy-box").Rel("center-awards").Title(x.Item3)]).ToList(), autoPlay:true) %>
			<strong>
				<%= H.Anchor(SimplePages.FullUrls.Awards, "Все награды")
			        	.Class("link").Style("color:#1458ae; font-size:12px;") %></strong>
		</div>