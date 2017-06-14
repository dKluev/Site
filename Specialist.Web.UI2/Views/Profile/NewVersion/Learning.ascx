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
    var tabs = new List<string> { "Текущие", "Предстоящие", "Законченные", "Экзамены", };
    if (Model.NextCourses.Any()) {
        tabs.Add("Рекомендуемые");
    }
%>

<%= Htmls2.Tabs(tabs.ToArray(), new object[] {
    Html.Partial(Views.Profile.NewVersion.GroupList, Model.GetGroupList(Model.Student.CurrentGroups, "Сейчас у Вас нет занятий.", true)),
     Html.Partial(Views.Profile.NewVersion.GroupList, Model.GetGroupList(Model.Student.ComingGroups, 
     "У Вас не выбран ни один курс.", false)),
Html.Partial(Views.Profile.NewVersion.GroupList, Model.GetGroupList(Model.Student.EndedGroups, "Вы еще не обучались в нашем Центре.", true)),
    Html.Partial(Views.Profile.NewVersion.ExamList, Model),
      H.h2["Ваш персональный менеджер рекомендует повысить Вашу квалификацию на следующих курсах"] + 
	Htmls.DefaultList(Model.NextCourses.Select(x => Html.CourseLink(x).ToString()))
    }, tabContentType: 2) %>

