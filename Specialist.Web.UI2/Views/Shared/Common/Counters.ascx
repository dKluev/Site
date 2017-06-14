<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Web" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<% if(MvcApplication.IsDebug) return; %>

	<!--noindex-->

        <div class="social-buttons">			
            <%= Htmls.HtmlBlock(HtmlBlocks.Social) %>
		</div>


<div class="footer4">
	<div class="counter">
	<%= H.Anchor("http://www.webmoney.ru/rus/index.shtml", 
		Images.Common("logo_wm.gif").ToString()).Rel("nofollow") %>
	<%= H.Anchor("/center/about-center/payments/cyberplat",
		Images.Common("cyberplat1.gif").ToString()).Rel("nofollow") %> 
		<!-- RamblerTop100 -->
		<img src="//counter.rambler.ru/top100.cnt?363854" border="0" width="1" height="1" alt="" />

    	<%= H.Anchor("http://top100.rambler.ru", 
    		Images.Common("logo_ramblertop.gif").ToString()).Rel("nofollow") %>
		<!-- /RamblerTop100 -->
		<!--Openstat-->
		<span id="openstat13735"></span>
		<script type="text/javascript">
		    var openstat = { counter: 13735, image: 64, next: openstat, track_links: "all" }; document.write(unescape("%3Cscript%20src=%22http" +
		        (("https:" == document.location.protocol) ? "s" : "") +
		        "://openstat.net/cnt.js%22%20defer=%22defer%22%3E%3C/script%3E"));
		</script>
		<!--/Openstat-->
		<!--/COUNTER-->
			
<!-- Yandex.Metrika informer -->
<a href="https://metrika.yandex.ru/stat/?id=40005&amp;from=informer"
target="_blank" rel="nofollow"><img src="https://informer.yandex.ru/informer/40005/3_1_FFFFFFFF_EFEFEFFF_0_pageviews"
style="width:88px; height:31px; border:0;" alt="Яндекс.Метрика" title="Яндекс.Метрика: данные за сегодня (просмотры, визиты и уникальные посетители)" class="ym-advanced-informer" data-cid="40005" data-lang="ru" /></a>
<!-- /Yandex.Metrika informer -->

<!-- Yandex.Metrika counter -->
<script type="text/javascript">
    (function (d, w, c) {
        (w[c] = w[c] || []).push(function() {
            try {
                w.yaCounter40005 = new Ya.Metrika({
                    id:40005,
                    clickmap:true,
                    trackLinks:true,
                    accurateTrackBounce:true,
                    webvisor:true,
                    ecommerce:"dataLayer"
                });
            } catch(e) { }
        });

        var n = d.getElementsByTagName("script")[0],
            s = d.createElement("script"),
            f = function () { n.parentNode.insertBefore(s, n); };
        s.type = "text/javascript";
        s.async = true;
        s.src = "https://mc.yandex.ru/metrika/watch.js";

        if (w.opera == "[object Opera]") {
            d.addEventListener("DOMContentLoaded", f, false);
        } else { f(); }
    })(document, window, "yandex_metrika_callbacks");
</script>
<noscript><div><img src="https://mc.yandex.ru/watch/40005" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
<!-- /Yandex.Metrika counter -->

<!-- Rating@Mail.ru counter -->
<script type="text/javascript">
    var _tmr = window._tmr || (window._tmr = []);
    _tmr.push({id: "1578434", type: "pageView", start: (new Date()).getTime()});
    (function (d, w, id) {
        if (d.getElementById(id)) return;
        var ts = d.createElement("script"); ts.type = "text/javascript"; ts.async = true; ts.id = id;
        ts.src = (d.location.protocol == "https:" ? "https:" : "http:") + "//top-fwz1.mail.ru/js/code.js";
        var f = function () {var s = d.getElementsByTagName("script")[0]; s.parentNode.insertBefore(ts, s);};
        if (w.opera == "[object Opera]") { d.addEventListener("DOMContentLoaded", f, false); } else { f(); }
    })(document, window, "topmailru-code");
</script><noscript><div style="position:absolute;left:-10000px;">
<img src="//top-fwz1.mail.ru/counter?id=1578434;js=na" style="border:0;" height="1" width="1" alt="Рейтинг@Mail.ru" />
</div></noscript>
<!-- //Rating@Mail.ru counter -->
		<!--LiveInternet logo-->
		<a href="http://www.liveinternet.ru/click" target="_blank" rel="nofollow">
			<img src="//counter.yadro.ru/logo?26.11" title="LiveInternet: показано число посетителей за сегодня"
				alt="LiveInternet" border="0" width="88" height="15" /></a><!--/LiveInternet-->

				<!--LiveInternet counter--><script type="text/javascript"><!--
                           				    new Image().src = "//counter.yadro.ru/hit?r" +
                           				        escape(document.referrer) + ((typeof (screen) == "undefined") ? "" :
                           				            ";s" + screen.width + "*" + screen.height + "*" + (screen.colorDepth ?
                           				                screen.colorDepth : screen.pixelDepth)) + ";u" + escape(document.URL) +
                           				        ";" + Math.random();//--></script><!--/LiveInternet-->
	<!--/noindex-->
	<!--noindex-->
	</div>
</div>

<script type="text/javascript"> var islteIE7 = false; </script>
<!--[if lte IE 7]> <script type="text/javascript"> var islteIE7 = true; </script> <![endif]-->
<!-- Яндекс.Метрика: Целевой звонок -->
<script type="text/javascript">
                           				    var yaphonescript = document.createElement('script');
                           				    yaphonescript.src = '//mc.yandex.ru/metrika/phone.js';
                           				    yaphonescript.type = 'text/javascript';
                           				    yaphonescript.setAttribute('class', "counter40005");
                           				    yaphonescript.defer = "defer";
                           				    if (window.islteIE7) {
                           				        document.getElementsByTagName('head')[0].appendChild(yaphonescript);	
                           				    } else {
                           				        document.getElementsByTagName('body')[0].appendChild(yaphonescript);	
                           				    }
                           				</script>
<!-- /Яндекс.Метрика: Целевой звонок -->

	<%= Session[Htmls.BottomScriptKey] %>
	<% Session[Htmls.BottomScriptKey] = null;%>


<!-- Google Code for &#1074;&#1089;&#1077; &#1087;&#1086;&#1089;&#1077;&#1090;&#1080;&#1090;&#1077;&#1083;&#1080; &#1089;&#1072;&#1081;&#1090;&#1072; Remarketing List -->
<script type="text/javascript">
/* <![CDATA[ */
    var google_conversion_id = 1059972133;
    var google_conversion_language = "en";
    var google_conversion_format = "3";
    var google_conversion_color = "666666";
    var google_conversion_label = "ceozCNOykwIQpci3-QM";
    var google_conversion_value = 0;
/* ]]> */
</script>
<script type="text/javascript" src="https://www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="https://www.googleadservices.com/pagead/conversion/1059972133/?value=0&amp;label=ceozCNOykwIQpci3-QM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>


<!-- Google Code for &#1079;&#1074;&#1086;&#1085;&#1086;&#1082; Conversion Page
In your html page, add the snippet and call
goog_report_conversion when someone clicks on the
phone number link or button. -->
<script type="text/javascript">
    /* <![CDATA[ */
    goog_snippet_vars = function() {
        var w = window;
        w.google_conversion_id = 1059972133;
        w.google_conversion_label = "IuS_CIPChAgQpci3-QM";
        w.google_conversion_value = 1.00;
        w.google_conversion_currency = "RUB";
        w.google_remarketing_only = false;
    }
    // DO NOT CHANGE THE CODE BELOW.
    goog_report_conversion = function(url) {
        goog_snippet_vars();
        window.google_conversion_format = "3";
        var opt = new Object();
        opt.onload_callback = function() {
            if (typeof(url) != 'undefined') {
                window.location = url;
            }
        }
        var conv_handler = window['google_trackConversion'];
        if (typeof(conv_handler) == 'function') {
            conv_handler(opt);
        }
    }
/* ]]> */
</script>
<script type="text/javascript"
  src="//www.googleadservices.com/pagead/conversion_async.js">
</script>

<script type="text/javascript">
    $(function() {
        $('a[href^="tel:"]').click(function() {
            goog_report_conversion();
        });
    })
</script>








<%  if (!Request.Url.AbsolutePath.StartsWith("/testrun/")
        && !Request.Url.AbsolutePath.StartsWith("/group/videos/")) { %>

<!-- BEGIN JIVOSITE CODE {literal} -->
<script type='text/javascript'>
    (function(){ var widget_id = 'Cd2oVmvYAr';
        var s = document.createElement('script'); s.type = 'text/javascript'; s.async = true; s.src = '//code.jivosite.com/script/widget/' + widget_id; var ss = document.getElementsByTagName('script')[0]; ss.parentNode.insertBefore(s, ss);
    })();
    function jivo_onLoadCallback() {
//        jivo_api.showProactiveInvitation('Здравствуйте, чем я могу Вам помочь?'); 
    }

</script>
<!-- {/literal} END JIVOSITE CODE -->


<% } %>





	<!--/noindex-->
	