<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<PrivateMessageListVM>" %>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Message.ViewModel"%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h3> <%=   Html.PublicProfileLink(Model.Receiver)  %></h3> 

<% Html.RenderPartial(PartialViewNames.SendPrivateMessage, Model); %>
    
<% Html.RenderPartial(PartialViewNames.MessagePagedList, Model.Messages); %>




</asp:Content>

