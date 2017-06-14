<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>

<div class="header1">
	<!--noindex-->
    <div class="registration">
        <div class="registration_ie">
            <% Html.RenderAction<AccountController>(c => c.LogOnState());%>
        </div>
    </div>
	<!--/noindex-->
    <div class="title" style="margin-left:0;">
        
      <div class="menu_top">
        <ul>
          <li> <%= Url.Group().Search(null, "Поиск расписания") %></li>
          <li><%= H.Anchor(SimplePages.FullUrls.Payment, "Записаться на курсы") %> </li>

          <% if (DateUtils.IsWorkTime()) { %>
          <li>
            <a id="header-order-call" href="#" title="Заказать обратный звонок">
            <%= Images.Main("blue/tel.png").Size(22,22) %>&nbsp;Обратный звонок
            </a>
          </li>
          <% } %>
        </ul>
      </div>
    </div>
</div>
<!--noindex-->
<div class="header_basket">
 <div class="basket_top">
  <div class="basket_top_l">
        </div>
        <div class="basket_top_r png">
        </div>
        <div class="basket_top_c png">
        </div>
		</div>
      
       <% Html.RenderAction<CartController>(c => c.StateNew()); %>
    <div class="basket_bottom">
        <div class="basket_bottom_l">
        </div>
        <div class="basket_bottom_r png">
        </div>
        <div class="basket_bottom_c png">
        </div>
    </div>
</div>
<!--/noindex-->
<div class="header_lang">
	<a class="active" title="Английская версия сайта" href="http://en.specialist.ru">
	     <%= Images.Main("en.png") %>
        <span style="color:#3c89c9">Рус</span>|<span style="color:#a1a1a3">Eng</span>
	</a>
</div>

<%= Htmls.HtmlBlock(HtmlBlocks.Header2) %>

<div class="header3" id="fixed-menu">
    <div class="search">
        <div class="search_corner">
		  <% using(Html.BeginForm<PageController>(c => c.Search(null, null))){ %>
		   <span class="image"><b>Поиск</b></span> 
               <input id="search" class="text" type="text" name="Text" value="" />
           <% } %>
           
        </div>
    </div>

    <div class="menu_main">
        <%= Htmls.HtmlBlock(HtmlBlocks.MainMenu) %>
    </div>
</div>
    <script type="text/javascript">
        watermark('search', 'Найти курс');
        $(function() {
            $("#header-order-call").click(function(e) {
                e.preventDefault();
                $("#consultant-slide-panel").stop().animate({ left: 0 });
            });

        });
    </script>
