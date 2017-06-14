<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<AddAnswerVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Entities.Message.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<% if (Model.CannotAddMessageToClub) { %>
    <%= Htmls.CannotAddMessageToClub %>
<% }else{ %>

<%= JavaScripts.TinyMce() %>
<%= JavaScripts.Local("Views/Message/messageUtils.js") %>
<script type="text/javascript">
    setupMce("Description");
</script>
    <%= Model.Message.Text %>
    <% using(Html.DefaultForm<MessageController>(x => x.AddAnswer(null), 
           Htmls.FormWithFile )){ %>
        <%= Html.ValidationSummary() %>
            <div><%= Html.HiddenFor(x => x.Message.UserMessageID) %></div>
            <div class="registr_form">
            <% Htmls.FormSection(" ", () => {%>
                <%= Html.ControlFor(x => x.Description) %>
                <% Html.RenderPartial(PartialViewNames.AddImagePart); %>
                <% }); %>
            </div>
            <%= Htmls.Submit("ok") %>
    <% } %>
    
<% } %>
</asp:Content>