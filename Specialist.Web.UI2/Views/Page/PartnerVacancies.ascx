<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Vacancy>>" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>


<% foreach(var vacancy in Model){ %>
    <p class="vacancy">
     <%= vacancy.Logo.IsEmpty() ? null : Cdns.ImageVacancy(vacancy.Logo).Img() %>   <%= Html.VacancyLink(vacancy) %>
    </p>
<% } %>