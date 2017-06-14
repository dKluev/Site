<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Vacancy>>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Controllers.Common"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<% if(Model.Count() == 0) return; %>

<%= Htmls2.Menu2(Html.ActionLink<CenterController>(
       c => c.Vacancies(null), "Вакансии").ToString()) %>
 

<div class="block_chamfered_in v_group_discount">
	     <ul class="square">

    <% foreach (var vacancy in Model){ %>
        <li><%= Html.VacancyLink(vacancy) %><br />
            <span class="date"><%= vacancy.PublishDate.DefaultString() %></span></li>
<% } %>
        </ul>
</div>


