<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LibraryVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Reflection.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>

<% if (Model.Files.Any()) { %>
<table class="defaultTable">
    <tr><th>Курс</th><th>Файлы</th></tr>
<% foreach(var courseFiles in Model.Files){ %>
    <tr>
        <td><%= courseFiles.Item1.GetName() %></td>
        <td style="text-align: left;">
            <%= H.div[courseFiles.Item2.GroupBy(x => x.UserName)
            .Select(x => H.div[H.b[x.Key ?? "Общее"], H.Ul(x.Select(z => H.Anchor(z.Url, z.Name)))] )] %>
        </td>
    
    </tr>
<% } %>
</table>


<% } else { %>

Пока ничего нет

<% } %>
