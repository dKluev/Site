<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Specialist.Entities.Catalog.Interface.IEntityCommonInfo>>" %>
<% /*if(!Model.Any())*/ return; %>

	<div id="also-slide-panel" style="z-index:10;position:fixed; bottom:30px;width: 400px;display:none;">
		<div style="background:none repeat scroll 0 0 #FFFFFF;">
<div class="side-slide-panel-n side-slide-panel-bg"></div>
<div class="side-slide-panel-ne side-slide-panel-bg"></div>
<div class="side-slide-panel-e side-slide-panel-bg"></div>
<div class="side-slide-panel-se side-slide-panel-bg"></div>
<div class="side-slide-panel-s side-slide-panel-bg"></div>
<div class="side-slide-panel-sw side-slide-panel-bg"></div>
<div class="side-slide-panel-w side-slide-panel-bg"></div>
<div class="side-slide-panel-nw side-slide-panel-bg"></div>

		<div style="padding:10px">
		<strong>Смотрите также:</strong>
		<%= Htmls.DefaultList(Model.Select(x => Html.GetLinkFor(x))) %>
		</div>
		<a class="side-slide-panel-close" style="display: inline;"></a>
		</div>
</div>


	<script type="text/javascript">
		$(function () {
		    controls.initSideSlidePanel($("#also-slide-panel"),"scroll");
		})
	</script>