<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<SectionMessageListVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Message.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p>
<strong>
    <% if(Request.IsAuthenticated){ %>
    <%= Html.ActionLink<MessageController>(
        c => c.AddMessage(Model.Section.MessageSectionID), "Создать новое обсуждение") %>
    <% } 
      else
      {%>
 <%--     <%= Html.ActionLink<ProfileController>(
                c => c.Register((string)null), "Создать новое обсуждение")%> --%>
    <%= Html.ActionLink<AccountController>(c => c.LogOn(""), "Создать новое обсуждение")%>
    <% } %>
</strong>
 </p>
 
    <table class="defaultTable table_forum" >
        
        <tr>
            <th>
                Обсуждение
            </th>
            <th>
                Сообщения
            </th>
			<th>Отвечено</th>
        </tr>
    <% foreach (var message in Model.Messages) { %>
        <tr>
            <td  class="td_discussion">
                <%= Html.MessageLink(message) %>
            </td>
            <td class="td_quantity">
                <%=Model.MessageCounts[message.UserMessageID] %>
            </td>
			<td> <%= Images.Forum(message.IsAnsweredSysName) %> </td>
        </tr>
    <% } %>
    </table>

   <%= Html.GetNumericPager(Model.Messages, "{0}") %>

   <table>
   <tr><td>
   
 <%= Html.ActionLinkImage<RssController>(c =>
        c.Messages(Model.Section.MessageSectionID, null), Urls.Main("rss.gif"))%>
   </td>
   <td> - новые обсуждения</td>
   </tr>
   <tr><td>
 <%= Html.ActionLinkImage<RssController>(c =>
        c.AllMessages(Model.Section.MessageSectionID), Urls.Main("rss.gif"))%>
   </td>
   <td> - новые обсуждения и сообщения</td>
   </tr>
   </table>


   

</asp:Content>

