<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<CompanyFileListVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const" %>

<script type="text/javascript">
     confirmDelete();
</script>
   <p><%= Html.ActionLink<CompanyFileController>(c => c.Add(), "Добавить файл") %></p>
    
    <table class="table">
        <tr>
            <th></th>
            <th></th>
            <th> Название </th>
            <th> Дата создания </th>
        </tr>
        <% foreach (var file in Model.Files) { %>
        <% var owner = file.UserID == Model.User.UserID; %>
        <tr>
            <td><%= owner ? Url.CompanyFile().Edit(file.Id, "Редактировать"): null %></td>
            <td><%= owner ? Url.CompanyFile().Delete(file.Id, "Удалить").Class("js-confirm-click"): null %></td>
            <td><%= file.Name %></td>
            <td><%= file.CreateDate.DefaultString() %></td>
        </tr>
        <% } %>
    </table>
