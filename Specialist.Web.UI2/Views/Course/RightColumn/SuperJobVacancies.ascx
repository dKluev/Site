<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Specialist.Services.Common.SuperJobVacancy>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<% if(!Model.Any()) return; %>
<%= Htmls2.Menu2("Вакансии Superjob") %>
<ul  class="square_blue">
<% foreach (var vacancy in Model) { %>
    <li>
        <span class="date"><%= vacancy.Company %></span> <br />
        <%= H.Anchor(vacancy.Url, vacancy.Name) %> <br />
		<%= vacancy.PaymentFrom.IfNotNull(x => 
			H.span.Class("zp")["з/п от ", H.strong[x], " руб."]) %> 
		<%= vacancy.Town.IfNotNull(x => H.span.Class("date")["г. " + x]) %>
    </li>
<% } %>

</ul>

