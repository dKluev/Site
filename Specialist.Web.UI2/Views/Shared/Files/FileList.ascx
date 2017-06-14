<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Specialist.Entities.Passport" %>
<%@ Import Namespace="Specialist.Web.Helpers"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FileListVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<script type="text/javascript">
     confirmDelete();
</script>
<% if(Model.Files.Any()){ %>
    <table class="defaultTable">
        <tr>
            <th> �������� </th>
            <th> �������� </th>
            <% if(Html.InRole(Role.Trainer)) { %>
            <th></th>  
            <th></th>  
            <% } %>
        </tr>
        <% foreach (var file in Model.Files) { %>
        <tr>
            <td><%= Html.UserFileLink(file) %></td>
            <td><%= file.Description %></td>
            <% if(Html.InRole(Role.Trainer)) { %>
            <td><%= Html.ActionLinkImage<FileController>(c => c.Edit(file.UserFileID), 
                    Urls.Common("edit.gif")) %></td>
            <td>
            <%= Html.ActionLinkImage<FileController>(c => c.DeleteFileFrom(file.UserFileID,
                Model.CourseTC, Model.GroupID), Urls.Common("del.gif")) %>  
            </td>
            
            <% } %>
        </tr>
        <% } %>
    </table>
<% }else{ %>
    ���� ������ ���
<% } %>
    <% if(Html.InRole(Role.Trainer)) { %>
      <% if(Model.UserFiles.Any()){ %>
          <% using (Html.DefaultForm<FileController>(c => c.AddFileTo(null))) { %>
            <% if (Model.GroupID.HasValue) { %>
            <%= Html.HiddenFor(x => x.Group.Group_ID) %>
            <% } %>
            <% if (Model.CourseTC != null) { %>
            <%= Html.HiddenFor(x => x.Course.Course_TC) %>
            <% } %>
            <% Htmls.FormSection(() => {%>
          
            <%= Html.SelectFor(x => x.SelectedFileID, Model.UserFiles) %>
          <% }); %>
             <%= Htmls.Submit("ok") %>
          <% } %>
       <% }else{ %>
            <p>� ��� ��� ���������� ��� ����������.</p>
       <% } %>
       <%= Html.ActionLink<FileController>(c => c.List(1), "��� ���������")%>
    <% } %>



