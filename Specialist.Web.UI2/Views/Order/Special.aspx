<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Context.Order>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% if(Model == null) {%>

<%= Htmls.Title("Генерация ссылки оплаты спец заказа") %>

<script type="text/javascript">

	$(function () {
		$("#generate").click(function () {
			var url = '<%= Url.Action<OrderController>(c => c.SpecialLink(null)) %>';
			$("#generate-result").html(controls.indicator);
		    $.get(url, { id: $("#sigId").val() }, function(data) {
		        $("#generate-result").html(data);
		    });

		});
	});
  
</script>

<%= H.input.Id("sigId").Type("text") %> <%= H.button.Id("generate")["Сгенерировать ссылку"] %>
<div style="padding-top:10px;" id="generate-result"></div>

<% }else{ %>
    

<h1>Спец заказ <%= Model.OurOrgOrDefault %>: <%= Model.User.FullName %></h1>
<strong>Оплата <%= Model.TotalPriceWithDescount.MoneyString() %> руб.</strong>
<p> <strong>Вы можете оплатить обучение следующими способами:</strong></p>
    
    <% Html.RenderPartial(Views.Order.PaymentTypes.SberMerchant, Model); %>
    <% Html.RenderPartial(Views.Order.PaymentTypes.WebMoneyCardControl, PaymentDataCreator.WebMoney(Model)); %>
   
    <% Html.RenderPartial(Views.Order.PaymentTypes.WebMoneyControl,
           PaymentDataCreator.WebMoney(Model)); %>
   
<% } %>

</asp:Content>