<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<UserFileListVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const" %>

<script type="text/javascript">
     confirmDelete();
</script>
   <p><%= Html.ActionLink<FileController>(c => c.Add(), "Добавить файл") %></p>
    
    <table class="table">
        <tr>
            <th></th>
            <th></th>
            <th> Название </th>
            <th> Описание </th>
            <th> Дата создания </th>
            <th> Группы </th>
            <th> Курсы </th>
        </tr>
        <% foreach (var file in Model.Files) { %>
        <tr>
            <td><%= Url.File().Edit(file.UserFileID, BootHtmls.Icon("edit")) %></td>
            <td>
                <% var cannotDelete = file.GroupFiles.Any() || file.CourseFiles.Any(); %>
                <% if(!cannotDelete){ %>
                    <%= Url.File().Delete(file.UserFileID, BootHtmls.Icon("trash")).Class("js-confirm-click") %>
                <% } %>
            </td>
            <td><%= file.Name %></td>
            <td><%= file.Description %></td>
            <td><%= file.CreateDate %></td>
            <td>
            <% foreach (var groupFile in file.GroupFiles) { %>
                <%= Html.GroupLink(groupFile.Group_ID, groupFile.Group_ID.ToString()) %>
            <% } %>
            </td>
            <td>
            <% foreach (var courseFile in file.CourseFiles) { %>
                <%= Html.CourseFiles(courseFile.Course_TC, courseFile.Course_TC) %>
            <% } %>
            </td>
        </tr>
        <% } %>
    </table>
    <br />
    <%= Html.GetNumericPager(Model.Files, "?pageIndex={0}") %> 
    Привязать материалы к группам и курсам можно через страницы: <br />
    <%= Html.TrainerGroups() %> 
    <%= Html.TrainerCourses() %> 
