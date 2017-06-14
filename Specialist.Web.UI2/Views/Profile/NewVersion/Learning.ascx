<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LearningVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<% if(Model.Student.IsNull()){ return; }%>
<% 
    var tabs = new List<string> { "�������", "�����������", "�����������", "��������", };
    if (Model.NextCourses.Any()) {
        tabs.Add("�������������");
    }
%>

<%= Htmls2.Tabs(tabs.ToArray(), new object[] {
    Html.Partial(Views.Profile.NewVersion.GroupList, Model.GetGroupList(Model.Student.CurrentGroups, "������ � ��� ��� �������.", true)),
     Html.Partial(Views.Profile.NewVersion.GroupList, Model.GetGroupList(Model.Student.ComingGroups, 
     "� ��� �� ������ �� ���� ����.", false)),
Html.Partial(Views.Profile.NewVersion.GroupList, Model.GetGroupList(Model.Student.EndedGroups, "�� ��� �� ��������� � ����� ������.", true)),
    Html.Partial(Views.Profile.NewVersion.ExamList, Model),
      H.h2["��� ������������ �������� ����������� �������� ���� ������������ �� ��������� ������"] + 
	Htmls.DefaultList(Model.NextCourses.Select(x => Html.CourseLink(x).ToString()))
    }, tabContentType: 2) %>

