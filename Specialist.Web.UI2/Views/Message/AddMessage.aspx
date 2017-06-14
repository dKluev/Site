<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<EditMessageVM>" %>
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

    <% using(Html.BeginForm(Model.MessageId.HasValue ? "EditPost" : "AddMessage",
		   "Message", FormMethod.Post,Htmls.FormWithFile )){ %>
        <%= Html.ValidationSummary() %>
		<% if(Model.MessageId.HasValue){ %>
            <%= Html.HiddenFor(x => x.MessageId) %>
		<% }else{ %>
            <%= Html.HiddenFor(x => x.MessageSection.MessageSectionID) %>
		<% } %>
            <div class="registr_form">
            <% Htmls.FormSection(" ", () => {%>
                <% if (!Model.MessageId.HasValue) { %>
                <%= Html.ControlFor(x => x.MessageTitle) %> 
                <% }else{ %>
                <%= Html.HiddenFor(x => x.MessageTitle) %>
        		<% } %>

                <%= Html.ControlFor(x => x.Description) %>
                <% if (!Model.MessageId.HasValue) { %>
                    <% Html.RenderPartial(PartialViewNames.AddImagePart); %>
                <% } %>
                <% }); %>
            </div>
            <%= Htmls.Submit("ok") %>
    <% } %>
    
    <% } %>
</asp:Content>