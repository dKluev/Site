<%@  Language="C#" Inherits="System.Web.Mvc.ViewPage<ReceiptVM>" %>
<%@ Import Namespace="Specialist.Entities.Order.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Квитанция</title>
    <link rel="stylesheet" type="text/css" href="/Content/receipt.css" />
</head>
<body>
    <table>
        <tr>
            <td class="left">
                <p>
                    <strong>Платеж</strong></p>
            </td>
            <% Html.RenderPartial(PartialViewNames.ReceiptPart, Model); %>
        </tr>
        <tr>
            <td class="left">
                <p>
                    <strong>Квитанция</strong></p>
            </td>
            <% Html.RenderPartial(PartialViewNames.ReceiptPart, Model); %>
        </tr>
    </table>

    <% Html.RenderPartial(PartialViewNames.GACommon); %>


	<!-- Yandex.Metrika counter -->

<script type="text/javascript">
                           	var ya_params = {};
                           	(function (w, c) {
                           		(w[c] = w[c] || []).push(function () {
                           			try {
                           				w.yaCounter40005 = new Ya.Metrika({ id: 40005,
                           					clickmap: true,
											accurateTrackBounce:true,
                           					trackLinks: true, 
											params: window.ya_params || {}
                           				});
                           			}
                           			catch (e) { }
                           		});
                           	})(window, "yandex_metrika_callbacks");
</script>
<script src="//mc.yandex.ru/metrika/watch_visor.js" type="text/javascript" defer="defer"></script>
<!-- /Yandex.Metrika counter -->


</body>
</html>
