<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NewMessageVM>" %>
<%@ Import Namespace="Specialist.Entities.Message.ViewModel"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<% if (Model.Receiver == null) return; %>
    <% using (Html.DefaultForm<MessageController>(c => c.PrivateList(null))) { %>
        <%= Html.ValidationSummary() %>
        <div><%= Html.HiddenFor(x => x.Receiver.UserID) %></div>
        <% Htmls.FormSection(" ", () => {%>
            <%= Html.ControlFor(x => x.SendMessage) %>
        <% }); %>
    	<%= Htmls.Submit("ok") %>
    <% } %>