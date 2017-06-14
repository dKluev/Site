<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Certification>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context.Extension" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>


<table class="fullCourse">
    <tr>
        <th> Преподаватели сертификаций </th>
    </tr>
  
    <% foreach (var certification in Model) { %>
    <tr>
        <td>
            <%= Html.ActionLink<CenterController>(c => c.CertificationTrainers(certification.UrlName), 
                certification.Name) %>
        </td>
    </tr>
    <% } %>
</table>


  



