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

<div class="header1">
	<!--noindex-->
    <div class="registration">
        <div class="registration_ie">
            <% Html.RenderAction<AccountController>(c => c.LogOnState());%>
        </div>
    </div>
	<!--/noindex-->
    <div class="title" style="margin-left:0;">
        <div class="title2">
            <p> <strong>
                <%= Htmls.HtmlBlock(HtmlBlocks.Header) %>
            </strong></p>
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
    <div class="basket_center">
      
       <% Html.RenderAction<CartController>(c => c.State()); %>
       <%=  
		Images.Main("ico_timetable.gif").Alt("Поиск расписания")
			.Title("Поиск расписания").Class("timetable") %>
            <%= Url.Link<GroupController>(c => c.Search(null), "Поиск расписания").Style("margin-left:18px") %>
    </div>
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
	<a class="active" title="Английская версия сайта" href="http://en.specialist.ru"> <%= Images.Main("en.png") %> </a>
</div>
<div class="header2">
    <a class="logo" href="/" title="На главную">
        <%= Images.Main("logo.gif").Alt("Центр компьютерного обучения «Специалист»")%>
    </a>
    <div class="info">
        <div class="info_ie"  style="padding-top: 15px;">
            <p class="phone" id='ya-phone-1' >
            	<%= Model is VacancyVM ? CommonTexts.VacancyPhone : CommonTexts.Phone %>
			</p>
         

			            <p class="e-mail" style="margin-top:-4px;">
						<%= HtmlControls.MailTo(CommonConst.EmailForSite) %>&nbsp;&nbsp;
<%= Url.Link<CenterController>(c => c.Skype(),
                                         Images.Main("ico_skype.png").Alt("skype")).Title("Skype-консультация").Style("text-decoration:none;") %> 
       <%= Url.Link<CenterController>(c => c.Skype(),
                                          Images.Main("ico_icq.png").Alt("icq")).Title("Icq-консультация").Style("text-decoration:none;") %>

                <br/>


            </p>

        </div>
    </div>
</div>

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
            var $menu = $('#fixed-menu');
            var $header2 = $("div.header2");
            $(window).bind('scroll', function () {
                var top = $header2.offset().top + $header2.height();
                if ($(window).scrollTop() > top) {
                    $menu.addClass('top-fixed');
                } else {
                    $menu.removeClass('top-fixed');
                }
            });

        });
    </script>

