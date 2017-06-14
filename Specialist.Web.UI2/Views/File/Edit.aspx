<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<UserFileVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Core.Logic" %>

<table class="table_form">
    <%= BootHtmls.Info("���� ����� �������� ��� ���������� � ������� ���� ����� ��������") %>
    <%= Html.ValidationSummary() %>

    <% using (Html.DefaultForm<FileController>(c => c.Edit(null), 
           new { enctype = "multipart/form-data" }))
       { %>
        <%= Html.HiddenFor(x => x.IsNew) %>
       
        <%= Html.HiddenFor(x => x.UserFile.UserFileID) %>
      <label>��������</label> 
        <%= H.InputText(Model.For(x => x.UserFile.Name), Model.UserFile.Name).Class("form-control") %>
      <label>��������</label> 
        <%= H.textarea.Name(Model.For(x => x.UserFile.Description))[Model.UserFile.Description].Class("form-control") %>
      <label>����</label> 
    <%= Html.File(Model.For(x => x.File)) %>  
    <p class="help-block">����������: ���� � ������� <%= UserFiles.UserFileExt.JoinWith(" ") %> �������� �� ����� <%= UserFiles.MaxFileSizeMB %> mb</p>
        
    
    </table>
<% var tabs = _.List(Tuple.Create("�����",(object)
       H.div[Model.Courses.Select(x => H.Div("checkbox")[H.label[H.InputCheckbox(Model.For(z => z.CourseTCs), x.CourseTC)
           .SetChecked(Model.CourseTCs.Contains(x.CourseTC)), x.Name]])])); %>
<% if(Model.Groups.Any()) tabs.Add(Tuple.Create("������", (object)
       H.div[H.br, BootHtmls.Info("���� ���� ���� � ��� �� ��� ���� ����� �� ������ ������ �����, ��� ����� ����������� � ����� � �� � ������."), Model.Groups.Select(x => H.Div("checkbox")[H.label[H.InputCheckbox(Model.For(z => z.GroupIds),x.Group_ID.ToString())
    .SetChecked(Model.GroupIds.Contains(x.Group_ID)), x.DateInterval + " " + x.Course.Name]])]
       )); %>
<%= BootHtmls.Tabs(tabs) %>
 <%= H.Submit("���������").Class("btn btn-primary") %>
        
    <% } %>		
