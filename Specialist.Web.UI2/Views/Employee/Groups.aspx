<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<TrainerGroupsVM>" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<% Func<string, List<Specialist.Entities.Context.Group>, string> getIfAny = (x,groups) => groups.Any() ? x: null ; %>
 
<% Func<List<Specialist.Entities.Context.Group>, object> table = groups =>
       Html.Partial(Views.Employee.EmployeeGroupList, groups); %>
<%= Htmls2.Tabs(_.List(
    getIfAny("�������", Model.StartedGroups),
    getIfAny("�����������", Model.Groups),
    getIfAny("���������", Model.EndedGroups),
    getIfAny("��������", Model.SeminarGroups)),
    new []{ 
        table(Model.StartedGroups),
        table(Model.Groups),
        table(Model.EndedGroups),
        table(Model.SeminarGroups),
    }) %>

   <table>
   <tr><td>
   <%= Html.ActionLinkImage<RssController>(c =>
        c.GroupMessages(Model.User.Employee_TC), Urls.Main("rss.gif"))%>
   </td>
   <td> - ����� ��������� �� ���� �������</td>
   </tr>
   </table>
</asp:Content>
