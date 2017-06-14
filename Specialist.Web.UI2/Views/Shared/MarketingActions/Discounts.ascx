<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MarketingAction>>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<table class="discount_table">
<% foreach(var marketingAction in Model.OrderByDescending(ma => ma.MaxPercent)){ %>
<tr>
    <td style="width:50px;">
        <span class="discount_color"><%= marketingAction.DiscountInterval %></span></td>
    <td>
<%--        <% if(marketingAction.SysName == MarketingActions.Reserve){ %>--%>
         <%--   <%= HtmlControls.Anchor(SimplePages.FullUrls.Reserve, "Программа резерв") %>
        <% }else{ %>--%>
            <%= marketingAction.Description %>
            <% foreach(var discount in marketingAction.Discounts
                   .Where(d => d.IsActive && d.Description != null)
                   .OrderBy(x => x.PercentValue)){ %>
                <%= discount.DiscountText %> - <%= discount.Description %> <br /> 
            <% } %>
    </td>
</tr>
<% } %>
</table>