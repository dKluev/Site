<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%--
<!-- Начало Видео-приветствие -->
<div id="welcome-block" style="display: none; right: -300px;">
<a class="welcome-close" href="#" alt="" title="Закрыть">&#215;</a>
<a class="fancy-video welcome-video" onclick="welcomebox('welcome-block');" href="https://www.youtube.com/watch?v=8XLRObfWwqQ&autoplay=1" alt="" target="_blank"><img class="8XLRObfWwqQ" alt="" src="//cdn1.specialist.ru/Content/Image/Main/wel-fon.png"/></a>
</div>
<!-- Конец Видео-приветствие -->

	<script type="text/javascript">
	    $(function () {
	        $("a.welcome-close").click(function (e) {
	            $("#welcome-block").fadeOut();
	            e.preventDefault();
	        });
	        $("a.welcome-video").click(function (e) {
	            $("#welcome-block").fadeOut();
	        });
	        var visited = $.cookie("visited");
	        if (!visited) {
	            $.cookie("visited", 1, { path: '/', expires: 1000 });
        		$("#welcome-block").fadeIn().animate({ right: 0}, 2000);
            }
	    })
	</script>--%>