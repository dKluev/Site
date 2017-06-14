<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Context.ViewModel.PaymentTypeChoiceVM>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>
<%@ Import Namespace="Specialist.Entities.Order.Logic" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Const" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">

<script type="text/javascript">
    $(function() {
        $("input[type='submit']").click(function() {
            var submitButton = $(this);
            submitButton.attr('disabled', 'disabled');
			submitButton.parents('form').append('<img class="ajax-indicator" src="//cdn.specialist.ru/Content/Image/Common/indicator.gif"/>');
            var paymentType = submitButton.attr('id');
            var data = { 
                paymentType: paymentType,
                orderID: <%= Model.Order.OrderID %>
             };
            $.post('<%= Url.Action<OrderController>(
                c => c.SetPaymentType(null, 0)) %>', data,
            function() {
	            submitButton.removeAttr('disabled');
                submitButton.parents('form').submit();
            });
            return false;
        });
    });
  
</script>

<div id="content" class="longlist">
<h2>Выбор формы оплаты</h2>

<p>Вы можете оплатить обучение следующими способами:</p>
<ul class="mainlist">
  <li><%= Url.Link<OrderController>(
             x => x.CashInfo(Model.Order.OrderID),"Оплата наличными") %></li>
  <li><%= Url.Link<OrderController>(
             x => x.SberbankInfo(Model.Order.OrderID),"Сбербанк России") %></li>

  <li><%= Url.Link<OrderController>( x => x.SberMerchant(Model.Order.CommonOrderId), "Банковские карты") %></li>
  <li> <%= MHtmls.PayForm(PaymentDataCreator.WebMoneyUrl, 
	   PaymentDataCreator.WebMoney(Model.Order), "WebMoney", PaymentTypes.WebMoney) %>
  </li>
  
</ul>
    
<%= Htmls.AllPaymentTypes(ViewData) %>
<p class="warning">
Внимание! Бронирование места в группе осуществляется только при предварительной оплате не менее 50% стоимости курса.</p>

</div>




</asp:Content>
