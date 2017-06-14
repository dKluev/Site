<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<MessageVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common"%>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Entities.Message"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.GetNumericPager(Model.Answers, "{0}") %>
    <% Html.RenderPartial(PartialViewNames.MessageList, Model.AllMessages); %>
    <%= Html.GetNumericPager(Model.Answers, "{0}") %>
    
    <% if(Request.IsAuthenticated){ %>
    <h3><%= Html.ActionLink<MessageController>(x => x.AddAnswer(
        Model.Message.UserMessageID), "Написать сообщение") %></h3>
    <% }
      else
      {%>
   <h3>
     <%= Html.ActionLink<AccountController>(c => c.LogOn(""), "Написать сообщение")%>
     </h3>
    <% } %>

 <div>
</div>

   <table>
   <tr><td>
   <%= Html.ActionLinkImage<RssController>(c =>
        c.Messages(null, Model.Message.UserMessageID), Urls.Main("rss.gif"))%>
   </td>
   <td> - новые сообщения</td>
   </tr>
   </table>
</asp:Content>

