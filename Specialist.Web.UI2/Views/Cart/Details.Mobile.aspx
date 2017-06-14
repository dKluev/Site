<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<CartVM>" %>

<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<asp:Content ContentPlaceHolderID="Center" runat="server">
	<div id="content" class="longlist">
		<h2>
			Корзина</h2>
		<% if (Model.IsEmpty) { %>
		<p>
			Корзина пока пуста.
		</p>
		<p>
			Вы можете выбрать нужный курс в разделе:
		</p>
		<p>
			<%= Url.Link<CourseController>(c => c.MainCourses(), "Направления обучения")
	 .Class("link") %></p>
		<% } else { %>
		<p>
			В корзине находятся курсы, которые Вы планируете пройти в нашем Центре:</p>
			<% foreach (var orderDetail in Model.CourseOrderDetails) { %>
		<table border="0" cellpadding="0" cellspacing="0" class="buskettable" width="100%">
			<tr>
				<td class="buskettable_course">
					<%= Url.CourseLink(orderDetail.Course).Class("link") %>
				</td>
				<td class="buskettable_x">
					<%= Url.Link<CartController>(oc => oc.DeleteCourse(
                orderDetail.OrderDetailID), "X").Confirm("Удалить из корзины?") %>
				</td>
			</tr>
			<tr>
				<td colspan="2" class="buskettable_desc">
					<% var group = orderDetail.Group; %>
						<p>
							Режим обучения:
							<%= orderDetail.IsWebinar ? "Вебинар" : "Очное" %><br/>
					<% if(orderDetail.IsWebinar || 
			((orderDetail.Group == null || orderDetail.Group.WebinarExists) && Model.WithWebinar.Contains(orderDetail.Course_TC))){ %>
					<%= Url.Link<EditCartController>(oc => oc.ToggleWebinar(
                orderDetail.OrderDetailID), "Поменять на {0}".FormatWith( orderDetail.IsWebinar ?  "очное" : "вебинар")).Confirm("Поменять?") %>

					<% } %>
						</p>
					<% if (group != null) { %>
					<p class="busketdays">
						Даты:
						<%= group.DateInterval %></p>
					<p class="buskettimes">
						Время:
						<%= group.TimeInterval %>;
						<%= group.DaySequence %></p>
					<p class="busketplace">
						Место: УК «<%= group.Complex.Name %>»</p>
					<p>
						<%= Url.CourseGroupLink(group, "подробнее о группе") %></p>
					<% } else { %>
					<p>
						Вы можете
						<%= Url.CourseLink(orderDetail.Course, "подобрать нужную группу").Class("link")  %> </p>

						
						<% } %>
						<p class="pricebusket <%= orderDetail.HasDiscount ? "discount" : "" %>">
							Цена:
							<%= orderDetail.PriceWithDiscount.MoneyString() %>
							руб
							<% if (orderDetail.HasDiscount) { %>
							<span>(cкидка
								<%= orderDetail.PercentDiscount %>%)</span>
							<% } %>
						</p>
				</td>
			</tr>
		</table>
			<% } %>
		<p class="busketclear">
			<%= Url.Link<CartController>(c => c.Clear(), "Очистить корзину").Class("link")
   .Confirm("Очистить корзину?") %></p>
  <p class="busketall">Итого: <br /><%= Model.Order.TotalPriceWithDescount.MoneyString() %> руб</p>
  <%= MHtmls.ButtonLink(Url.Action<OrderController>(c => c.Contract()), "Начать оформление заказа") %>
  <% if(!Request.IsAuthenticated){ %>
  <p class="warning">Оформить заказ могут только зарегистрированные пользователи!</p>
	<% } %>

		<% } %>
		
	</div>

</asp:Content>
