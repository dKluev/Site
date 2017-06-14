<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Order.ViewModel.OrderConfirmVM>" %>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="DynamicForm" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial(PartialViewNames.OrderDetails, Model.Cart); %>
    <%= Html.ValidationSummary() %>
      <% using (Html.DefaultForm<OrderController>(c => c.Confirm(null))) { %>
            
            <% Htmls.FormSection(" ", () => {%> 
            <%= Html.ControlFor(x => x.ConfirmInfo) %>
            <%= Html.HiddenFor(x => x.OrderID) %>
            <% }); %>
        	<%= Htmls.Submit("ok") %>
        <% } %>


</asp:Content>
