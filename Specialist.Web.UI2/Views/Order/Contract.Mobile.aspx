<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Context.ViewModel.ContractVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
<div id="content" class="longlist">
<h2>Оформление заказа</h2>

<p><strong>В корзине находятся курсы, которые Вы планируете пройти в нашем Центре</strong></p>
<p>Чтобы перейти к выбору формы оплаты, Вам следует подтвердить своё согласие с договором-офертой</p>
  
<p><a href="/oferta-univer-spec" class="link">Текст договора</a></p>
<%= MHtmls.ButtonLink(Url.Action<OrderController>(c => c.PaymentTypeChoice( Model.Cart.Order.OrderID)), "Согласен")[H.Hidden("orderId", Model.Cart.Order.OrderID)] %>
</div>

</asp:Content>
