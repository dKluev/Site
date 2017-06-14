<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tuple<OrderDetail, CartVM>>" %>
<%@ Import Namespace="Specialist.Entities.Order.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<% var cart = Model.Item2; %>
<% var order = Model.Item1; %>
    <% if(order.OrderExtras.Any()) { %>
		<br />
		<strong>Дополнительные услуги</strong> 
			
		    <% foreach (var orderExtras in Model.Item1.OrderExtras) { %>
				<br /><%= orderExtras.Extras.ExtrasName %> 
				<%= orderExtras.Price.MoneyString() %> руб. 
	        <% } %>
            <% if(Model.Item1.OrderExtras.All(x => !Extrases.IsTravel(x.Extras_ID))){ %>
			<br />
	        <%= Url.Link<EditCartController>(oc => oc.EditExtras(
	            Model.Item1.OrderDetailID), "Изменить дополнительные услуги").Class("open-in-dialog not-link")%>
            <% } %>
        <% }else{ %>
			<% if(!Model.Item2.Order.IsOrganization && Model.Item2.CourseTCHasExtrases.Contains(Model.Item1.Course_TC)){ %>
			<br />
			Также Вы можете 
	        <%= Url.Link<EditCartController>(oc => oc.EditExtras(
	            Model.Item1.OrderDetailID), "Выбрать дополнительные услуги").Class("open-in-dialog not-link discount_color")%>
			<% } %>
		<% } %>

<% foreach (var text in cart.ExtrasTexts) { %>
<% var show = text.Courses.Contains(order.Course_TC) 
       && Model.Item1.OrderExtras.All(x => !text.Extrases.Contains(x.Extras_ID)); %>
<%= show ? H.p[text.Text].Style("color:" + (text.Alert ? "red;" : "green;")) : null %>
  
<% } %>
