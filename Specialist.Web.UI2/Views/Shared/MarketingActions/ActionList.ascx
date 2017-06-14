<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MarketingAction>>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<% foreach(var marketingAction in Model){ %>
<% var discount = marketingAction.FirstDiscount; %>
<div class="block_action">
    <div class="action_img">
        <%= Images.MarketingActionSmall(marketingAction) %>
    </div>
    <div class="action_text">
        <h3>
            <%= Html.MarketingActionLink(marketingAction) %></h3>
            <p>
                <%= marketingAction.GetShortDescription() %>
			</p>
    <% if(discount != null){ %>
        <p><strong>Скидка:</strong> <span class="discount2">
            <%= discount.DiscountText %></span></p>
    <% } %>
    <% if(!marketingAction.DateInterval.IsEmpty()){ %>
        <p><strong>Срок акции:</strong> <%= marketingAction.DateInterval %>
        </p>
    <% } %>
    <% if(ViewBag.ShowCoupon == true && Urls.GetCoupons().Contains(marketingAction.UrlName)){ %>
	<strong>Купон:</strong>
	<p>
		<%= Images.Coupon(marketingAction.UrlName).Width("300").Style("margin-bottom:5px;") %> <br />
		<%= Images.Main("/Icon/print.gif").Style("vertical-align:bottom;") %> 
		<%= H.Anchor(Images.Coupon(marketingAction.UrlName).Attribute("src").Value, "Для печати") %>
	</p>
    <% } %>
        <p class="details">
            <%= Html.ActionLink<CenterController>(c => 
    c.MarketingAction(marketingAction.UrlName), "Подробнее") %>
    </p>
    </div>
</div>
<% } %>