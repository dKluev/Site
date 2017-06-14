<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<SectionListVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Message.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="MvcContrib.Unity"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
<div class="attention">
<h3>��������� ����������! </h3>
<p> ���� �� ��� ���������������� �� ����� �����, �� ��� ���� ����� �������� ��������� ��  ������, ��� ���������� ������ ����� ��� ����� ������� � �������, ��� ����� �������, ������ �� ������ � ������� ���� �����. ���� �� ��� �� ������������������, �� �� ������ ������ �����������, ������ �� ��������������� ������ � ������� ���� �����. �������! ���� ���� ������� � ���������!
   </p>
   </div>
   <br />
        <table class="defaultTable table_forum" >
            <tr>
                <th>
                    ����������
                </th>
                <th>
                    ����
                </th>
                <th>
                    ����<br/>
                    ����������<br/>
                    ����������
                </th>
            </tr>
    <% foreach (var messageSection in Model.MessageSections.OrderBy(x => x.Order)) { %>
            <tr>
                <td  class="td_discussion">
                    <h3>
                       <%= Html.MessageSectionLink(messageSection) %>   </h3>
                    <p>
                       <%= messageSection.Description %>
                    </p>
                </td>
                <td class="td_quantity">
                   <%= messageSection.MessageCount %>
                </td>
                <td class="td_date2">
                    <%= messageSection.LastMessageDate.DateTimeString() %>
                </td>
            </tr>
    <% } %>
        </table>
    
  <% if(Model.IsForum && Model.User != null) { %>
  <br />
   <table>
   <tr><td>
   <%= Html.ActionLinkImage<RssController>(c =>
        c.MessagesForUser(Model.User.UserID), Urls.Main("rss.gif"))%>
   </td>
   <td> - ������������ �����</td>
   </tr>

       <% if(Model.User.IsTrainerRole){ %>
   <tr>
       <td>
   <%= Html.ActionLinkImage<RssController>(c =>
        c.GroupMessages(Model.User.Employee_TC), Urls.Main("rss.gif"))%>
   </td>
   <td> - ����� ��������� �� ���� ������� ��������</td>
   </tr>
	<% } %>
   </table>

	<% } %>
</asp:Content>

