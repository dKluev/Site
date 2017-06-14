<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Order.ViewModel.RegisterVM>" %>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>

<% Html.RenderPartial(PartialViewNames.OrderSteps, Model); %>

<%= Htmls.BorderBegin() %>
<% if (Htmls.IsSecond) { %>


<div style="margin-left: 0em; width: 45%; clear: none; float: left;" class="block_chamfered_content">
<div class="block_chamfered_t ">
<div class="block_chamfered_b">
<div class="block_chamfered_r">
<div class="block_chamfered_l">
<ul style="background: #FBFBFB url(//cdn.specialist.ru/Content/Image/Main/bookmark_bg33.png) bottom right repeat-x; height: 33px; margin: 0 -11px;" class="bookmarks">
<li style="height: 33px;"><a style="border: none; font-size:120%; margin-top:10px; font-weight:bold;">Новый слушатель</a></li>
</ul>
<div class="tab_content2">
    
 <% if(!Model.Order.IsOrganization) { %>
	<p>Закажите без регистрации:<br/> 

	<%= Url.Link<OrderController>(c => c.ExpressRegister(),
			Images.Button("fastreg").Style("vertical-align:middle; margin-top:1em;").ToString()).Id("one-click-button") %>

 </p>
 <% } %>
	<p>Зарегистрируйтесь и начните накапливать скидки и бонусы.<br/> 
	<%= Url.Link<ProfileController>(c => c.Register(Model.Order.CustomerType,Request.Url.PathAndQuery,null), 
 Images.Button("registeruser").Style("margin-top:1em;").ToString()) %>

    <%= Htmls.RegBlock() %>
	</p>

</div>
</div>
</div>
</div>
</div>
</div>

<div style="margin-left: 4em; width: 45%; float: left; clear: none;" class="block_chamfered_content">
<div class="block_chamfered_t ">
<div class="block_chamfered_b">
<div class="block_chamfered_r">
<div class="block_chamfered_l">
<ul style="background: #FBFBFB url(//cdn.specialist.ru/Content/Image/Main/bookmark_bg33.png) bottom right repeat-x; height: 33px; margin: 0 -11px;" class="bookmarks">
<li style="height: 33px;"><a style="border: none; font-size:120%; margin-top:10px; font-weight:bold;">Зарегистрированный слушатель</a></li>
</ul>
<div class="tab_content2">
    
<p>Авторизуйтесь для быстрого оформления заказа. Накапливайте скидки и бонусы!</p>

	<% Html.RenderAction<AccountController>(c => c.LogOnControl(null));%>

</div>
</div>
</div>
</div>
</div>
</div>

<% }else { %>



<div style="margin-left: 0em; width: 45%; clear: none; float: left;" class="block_chamfered_content">
<div class="block_chamfered_t ">
<div class="block_chamfered_b">
<div class="block_chamfered_r">
<div class="block_chamfered_l">
<ul style="background: #FBFBFB url(//cdn.specialist.ru/Content/Image/Main/bookmark_bg33.png) bottom right repeat-x; height: 33px; margin: 0 -11px;" class="bookmarks">
<li style="height: 33px;"><a style="border: none; font-size:120%; margin-top:10px; font-weight:bold;">Зарегистрированный слушатель</a></li>
</ul>
<div class="tab_content2">
<p>Авторизуйтесь для быстрого оформления заказа. Накапливайте скидки и бонусы!</p>

	<% Html.RenderAction<AccountController>(c => c.LogOnControl(null));%>

</div>
</div>
</div>
</div>
</div>
</div>

<div style="margin-left: 4em; width: 45%; float: left; clear: none;" class="block_chamfered_content">
<div class="block_chamfered_t ">
<div class="block_chamfered_b">
<div class="block_chamfered_r">
<div class="block_chamfered_l">
<ul style="background: #FBFBFB url(//cdn.specialist.ru/Content/Image/Main/bookmark_bg33.png) bottom right repeat-x; height: 33px; margin: 0 -11px;" class="bookmarks">
<li style="height: 33px;"><a style="border: none; font-size:120%; margin-top:10px; font-weight:bold;">Новый слушатель</a></li>
</ul>
<div class="tab_content2">
    
	<p>Зарегистрируйтесь и начните накапливать скидки и бонусы.<br/> 
	<%= Url.Link<ProfileController>(c => c.Register(Model.Order.CustomerType,Request.Url.PathAndQuery,null), 
 Images.Button("registeruser").Style("margin-top:1em;").ToString()) %>

    <%= Htmls.RegBlock() %>
	</p>

 <% if(!Model.Order.IsOrganization) { %>
	<p>Закажите без регистрации:<br/> 

	<%= Url.Link<OrderController>(c => c.ExpressRegister(),
			Images.Button("fastreg").Style("vertical-align:middle; margin-top:1em;").ToString()).Id("one-click-button") %>

 </p>
 <% } %>
</div>
</div>
</div>
</div>
</div>
</div>


<% } %>

<div class="clear"></div>

<h2>Нужна помощь в оформлении заказа?</h2>
<%= Htmls.SkypeTextBlock() %>



<script type="text/javascript">
	$(function () {
		$("#one-click-button").click(function () {
			var url = $(this).attr("href");
			controls.openUIDialog(url);
		    return false;
		});
	})

</script>




