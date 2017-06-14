<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Catalog.MainPageVM>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Site" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<div class="overflow">
	<div class="graduate_opinion">
	    <div class="new_news1" style="padding-top: 1px; padding-bottom: 5px; margin-bottom: 10px;">
         <% Html.RenderPartial(PartialViewNames.Best); %>
	        
	    </div>
        
		<%= Htmls2.ChamBegin() %>
        <% if (!Htmls.IsNewVersion) { %>
        <% Html.RenderPartial(PartialViewNames.ExpressOrder, new ExpressOrderVM()); %> 
        <% } %>

		<% Html.RenderPartial(Views.Shared.Common.Awards); %>
		
                <%= Htmls2.BlockEnd() %>
        <% Html.RenderPartial(Views.Shared.RightColumn.VideoBlock, Model.Videos); %>
        

                <%= Htmls2.ChamBegin() %>
        <% if (!Htmls.IsNewVersion) { %>
           <% Html.RenderPartial(PartialViewNames.OnlineConsultant); %>
        <% } %>
	
	    
		<%= Htmls2.Menu2(H.Anchor(SimplePages.FullUrls.DocumentsOff, "Документы об окончании")) %>
		<p style="text-align: center">
			<% var certType = Model.Documents.Random(); %>
            <% if (certType != null) { %>
			<%= H.Anchor(SimplePages.FullUrls.DocumentsOff,
			        Images.Image(certType.GetType().Name + "/Small/" + certType.UrlName + ".jpg").Alt(certType.Name).ToString())
			        .Title(certType.Name) %>
			<strong>
				<%= SimpleLinks.AllDocuments("Все документы об окончании")
				        .Class("link").Style("color:#1458ae; font-size:12px;") %></strong>

            <% } %>
		</p>

		

<%--	<% Html.RenderAction<PageController>(c => c.SideBanner()); %>--%>
        <div id="poll"></div>
        <%= H.JQuery("controls.loadPoll();") %>
            
		<%= Htmls2.BlockEnd() %>
        
<% if (Htmls.IsNewVersion) { %>

		<%= Htmls2.ChamBegin() %>
<%= Htmls2.Menu2(H.l(H.Anchor(SimplePages.FullUrls.CatalogInfo, Images.Main("blue/download.png").Style("vertical-align:middle; padding:0px; margin:-1px 0 0 0;").Size(18,18)).Title("Скачать каталог курсов"),
    H.Anchor(SimplePages.FullUrls.CatalogInfo, "Скачать каталог курсов").Style("font-size:14px;")
    )) %>
        
        <br/>

<%= Htmls2.Menu2(H.l(H.Anchor(SimplePages.FullUrls.Payment, Images.Main("blue/payments.png").Style("vertical-align:middle; padding:0px; margin:-1px 0 0 0;").Size(18,18)).Title("Формы оплаты"),
    H.Anchor(SimplePages.FullUrls.Payment, "Формы оплаты").Style("font-size:14px;")
    )) %>
        <br/>

<% Html.RenderPartial(Views.Shared.Common.SubscribeBlock); %>


		<%= Htmls2.BlockEnd() %>
        

<% }else{ %>

		<p style="font-size:8px;">
		<%= H.Anchor(SimplePages.FullUrls.Licences,
		        "Лицензия № 029125 на образовательную деятельность Свидетельство № 009965 о гос. аккредитации") %>
		</p>
<% Html.RenderPartial(Views.Shared.Common.SubscribeBlock); %>

		<p>
			<%= Url.Link<SiteNewsController>(c => c.Details(1560, null), Images.Common("widget-install.jpg").ToString())
			        .Title("Установить виджет") %></p>


	<p>
			<%= H.Anchor(SimplePages.FullUrls.CatalogInfo, Images.Common("cat-dwnld1.jpg").ToString())
			        .Title("Скачать каталог курсов") %></p>

<% } %>


	

        
		<%= Htmls2.ChamBegin() %>
        <%= Htmls2.Menu2("Что чаще всего ищут") %>
        
        <% Html.RenderAction<PageController>(c => c.TopSearch()); %>
		<%= Htmls2.BlockEnd() %>
	</div>
	
    <div class="overflow">
        

<%--   <% Html.RenderAction<PageController>(c => c.Banner()); %>--%>
        <%= SiteHtmls.Banners(Model.Banners) %>
    </div>
    <div class="overflow">
    <%= Htmls.HtmlBlock(HtmlBlocks.MainInfo) %>
    </div>
			<div class="groups_on_rebate" id="hot-groups">
			</div>

<%--	<% Html.RenderAction<HomeController>(c => c.News()); %>--%>
	<% Html.RenderPartial(Views.Home.News, Model.News); %>
    <% if (Htmls.IsNewVersion) { %>
	<div class="news_in_main"> 
<div class="h2_over_blue png">
		<h2> <%= SimpleLinks.Responses("Отзывы выпускников") %> </h2>
	</div>
	<div class="block anons_news" style="padding: 15px">
<%= Htmls.HtmlBlock(HtmlBlocks.MainPageResponse) %>
	</div>
</div>

    <% } %>
	<div id="main-response">
	 </div>

	<div style="border-top: 0px none !important;" class="news_in_main">
	    <% if (Htmls.IsNewVersion) { %>
        <%= Htmls.HtmlBlock(HtmlBlocks.AboutCenter) %>
        <% }else{ %>
		<h3>
			<%=HtmlControls.Anchor(MainMenu.Urls.Center, "Центр «Специалист» при МГТУ им. Н.Э.Баумана")%>
		</h3>
		<%=Model.About.Description.Remove(SimplePageConst.Menu)%>
        <% } %>
	</div>
</div>
