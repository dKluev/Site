<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Order>" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
   <strong>Слушатель:</strong> <%= Model.User.FullName %> <br />
   <strong>Подразделение:</strong> <%= Model.OurOrgOrDefault %> <br />
<strong>Сумма заказа:</strong> <%= Model.TotalPriceWithDescount.MoneyString() %> <br />
<% var orderUrl = "http://www.specialist.ru/order/special?id=" + Model.OrderID; %>
<strong>Ссылка для оплаты заказа:</strong> <%= H.Anchor(orderUrl, orderUrl) %> <br />