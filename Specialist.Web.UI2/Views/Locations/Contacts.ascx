<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<City>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<div id="content" class="longlist">
	<%= MHtmls.Title("Контакты") %>
	<% var city = Model; %>
	<%= city.Description %>
	<% if (city.Description.IsEmpty()) { %>
	<% if (!city.MainComplex.Address.IsEmpty()) { %>
	<p>
		Адрес:
		<%= city.MainComplex.Address %></p>
	<% } %>
	<% if (!city.Email.IsEmpty()) { %>
	<p>
		E-mail:
		<%= HtmlControls.MailTo(city.Email) %></p>
	<% } %>
	<p>
		Телефон:
		<%= city.PhoneList.Take(2).JoinWith(", ") %>
	</p>
	<% } %>
	<p><%= Url.Complexes() %></p>
</div>
