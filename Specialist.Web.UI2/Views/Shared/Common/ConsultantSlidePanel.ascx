<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>

<% if (!DateUtils.IsWorkTime())  return; %>
<div id="consultant-slide-panel" style="display:none;position: fixed; bottom: 200px; width: 300px; left: 0; z-index: 10;">
<a class="side-slide-panel-close" style="display: inline;float:right;font-size:16px;margin-right: 6px;cursor: pointer;">x</a>
<div class="h2_over_blue png" style="margin-top:13px;">
  <h2>Обратный звонок</h2>
</div>
<div style="font-size:12px; margin-left:10px;margin-top:40px" class="consultation-links">
        Оставьте свой номер телефона, наш специалист свяжется с Вами в течение 5 минут и проконсультирует по поводу обучения.
        <% Html.RenderPartial(PartialViewNames.ExpressOrderForm, new ExpressOrderVM());%>
</div>
</div>


	<script type="text/javascript">
		$(function () {
		    controls.initSideSlidePanel($("#consultant-slide-panel"), "visitCount");
		})
	</script>