<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<PaymentTypeChoiceVM>" %>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Order.Logic" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>
<%@ Import Namespace="Specialist.Entities.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(function() {
        var phoneText = "<br/>Возникли вопросы? Звоните: <%= CommonTexts.Phone %>";
        $("a.payment-type-link").click(function() {
            var paymentType = $(this).attr('rel');
            recordOutboundLink('Payment Types', paymentType);
        }).after(phoneText);
        $("input[type='image']").click(function() {
            var submitButton = $(this);
			submitButton.attr('disabled', 'disabled')
			submitButton.parents('form').append(controls.indicator);
            var paymentType = submitButton.attr('id');
            var data = { 
                paymentType: paymentType,
                orderID: <%= Model.Order.OrderID %>
             };
            $.post('<%= Url.Action<OrderController>(
                c => c.SetPaymentType(null, 0)) %>', data,
            function() {
                recordOutboundLink('Payment Types', paymentType);
                submitButton.parents('form').submit();
            });
            return false;
        }).after(phoneText);
    });
  
</script>

<% var orderSteps = Html.Partial(PartialViewNames.OrderSteps, Model); %>
<%= orderSteps %>

<p> <strong>Вы можете оплатить обучение следующими способами:</strong></p>
    <% if (Model.Order.OurOrg_TC != OurOrgs.Bt) { %>
    <% Html.RenderPartial(PartialViewNames.CashControl, Model.Order); %>
    <% } %>
    <% Html.RenderPartial(PartialViewNames.SberbankControl, Model.Order); %>
    
  
	<h2>Банковские карты</h2>
    <% Html.RenderPartial(Views.Order.PaymentTypes.SberMerchant, Model.Order); %>
    <% Html.RenderPartial(Views.Order.PaymentTypes.WebMoneyCardControl,
           PaymentDataCreator.WebMoney(Model.Order)); %>

   <% Html.RenderPartial(Views.Order.PaymentTypes.AlphaCreditControl, Model.Order); %>
    <% var webmoney = PaymentDataCreator.WebMoney(Model.Order);%>
    <% if (webmoney.Count > 0) { %>
	<h2>Электронные кошельки</h2>
    <% Html.RenderPartial(PartialViewNames.WebMoneyControl, webmoney); %>
    <% } %>
     
    <% Html.RenderPartial(PartialViewNames.WebMoneyCheckControl,
           PaymentDataCreator.WebMoney(Model.Order)); %>

 

<p>Внимание! Бронирование места в группе осуществляется только при предварительной оплате не менее 50% стоимости курса.</p>
<%= orderSteps %>
</asp:Content>