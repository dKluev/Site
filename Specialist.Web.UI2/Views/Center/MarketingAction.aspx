<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<MarketingActionVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% var discount = Model.MarketingAction.FirstDiscount; %>

<%= Images.MarketingAction(Model.MarketingAction).FloatLeft() %>

  
    <p>
    <% if(discount != null){ %>
    <strong>Скидка:</strong>
    <span class="discount2">
        <%= discount.DiscountText %></span>
    <br />
    <% if(discount.MaxPercentValue.HasValue){ %>
        <span class="explanation">(максимальная скидка не должна превышать 
            <%= discount.MaxPercentValue.Value %>%)</span>
    
    
    <% } %>
    <% } %>
    </p>
    <p>
        <% if(!Model.MarketingAction.DateInterval.IsEmpty()){ %>
        <strong>Срок акции:</strong>
        <%= Model.MarketingAction.DateInterval %>
        <% } %>
    </p>
    <%= Model.MarketingAction.Description %>  
	
	<%= Html.AddThis() %>

    <% Html.RenderAction<PageController>(c => c.Banner()); %>
    
 <% Html.RenderAction<CenterController>(c => c.ActionsBlock()); %>

<% Html.RenderAction<GroupController>(c => c.ForCourseTCList(Model.MarketingAction.CourseTCList,true,0)); %>
</asp:Content>