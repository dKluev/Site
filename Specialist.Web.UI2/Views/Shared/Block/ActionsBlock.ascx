<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.MarketingAction>>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Site" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<% if (!Model.Any())
	   return;%>
	   <div style="display: block; margin: 20px 0px 10px 0;" class="groups_on_rebate">
<div class="h2_over_red png">
<h2><a href="/special-offers/activities" style="color:#FFFFFF;">Акции Центра</a></h2>
</div>
<div style="border: 1px solid #CCCCCC; " class="block fresh">
<div class="fresh_l">
<div class="fresh_r">
<div class="fresh_b">
	<%= CommonSiteHtmls.Carousel(Model.Select(x => 
	H.div.Style("margin:10px 25px 0 35px;")[H.Div("action_img")[Images.MarketingActionSmall(x)
	.ToString()],
		H.Div("action_text")[H.h3[Html.MarketingActionLink(x)],H.Raw(x.GetShortDescription()),
	H.p.Class("details")[H.Anchor(SimplePages.FullUrls.Activities, "Все акции Центра")]	]])
		.ToList(),autoPlay:false,hideAll: true) %>
	

</div>
</div>
</div>
</div>
</div>
