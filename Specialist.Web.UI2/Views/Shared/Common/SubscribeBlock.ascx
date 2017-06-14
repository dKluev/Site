<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>



<%--<% Html.RenderPartial(PartialViewNames.ExpressOrderForm);%>--%>


    <% Html.RenderPartial(PartialViewNames.ExpressOrderForm, new ExpressOrderVM {Subscibe = true});%>

<%--<%= Htmls2.Menu2(H.l(Images.Main("blue/subscribe.png").Style("vertical-align:middle; padding:0px; margin:-1px 0 0 0;").Size(18,18), H.span["Подписаться на рассылку"].Style("font-size:14px;") )) %>--%>
<%----%>
<%--                  <div class="podpiska">--%>
<%----%>
<%--<form method="POST" action="https://sendsay.ru/form/specialist/3/">--%>
<%--    --%>
<%--<br>--%>
<%--<input type="checkbox" checked>Полезные статьи<br>--%>
<%--<input type="checkbox" checked>Обучающие материалы<br>--%>
<%--<input type="checkbox" checked>Афиши и билеты на конференции<br>--%>
<%--<input type="checkbox" checked>Подарки наших партнеров--%>
<%----%>
<%--   --%>
<%--<input type="text" name="_member_email" id="maillist-subscribe-email"/>--%>
<%--<input type="image" value="Подписаться" id="maillist-subscribe-button" src="//cdn1.specialist.ru/Content/Image/Main/blue/subscribe-mail-button.jpg" style="margin-top:-5px; vertical-align:middle;"/>--%>
<%--</form>--%>
<%----%>
<%--		</div>--%>
<%----%>
<%--<script type="text/javascript">--%>
<%--	$(function () {--%>
<%--	    watermark('maillist-subscribe-email', 'Ваш E-mail адрес');--%>
<%--		$("#maillist-subscribe-button").click(function () {--%>
<%--			recordOutboundLink("MailListSubscribe", "Subscribe");--%>
<%--		});--%>
<%--	})--%>
<%--</script>--%>
<%--	--%>