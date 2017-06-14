<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Order>" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
   <strong>���������:</strong> <%= Model.User.FullName %> <br />
   <strong>�������������:</strong> <%= Model.OurOrgOrDefault %> <br />
<strong>����� ������:</strong> <%= Model.TotalPriceWithDescount.MoneyString() %> <br />
<% var orderUrl = "http://www.specialist.ru/order/special?id=" + Model.OrderID; %>
<strong>������ ��� ������ ������:</strong> <%= H.Anchor(orderUrl, orderUrl) %> <br />