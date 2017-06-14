<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<PollListVM>" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% foreach(var poll in Model.Polls){ %>
    <% var pollVM = new PollVM { Poll = poll }; %>
    <%= Htmls.BorderBegin(pollVM.Poll.Name) %> 
    <ul class="block_r_ul">
        <% foreach (var option in poll.PollOptions) { %>
        <li> 
                    <%= option.Text %> -
					<% var percent = pollVM.GetPercent(option); %>
                    <strong><%= percent.ToString(percent >= 1 ? "0" : "0.#", CultureInfo.InvariantCulture) %>%</strong>
                </li> 
        <% } %>
    </ul>
    <p>
        Всего проголосовало <strong>
            <%= pollVM.GetTotal() %></strong>.</p>
    <%= Htmls.BorderEnd %> 
<% } %>

<%= Html.GetNumericPagerPretty(Model.Polls) %>

</asp:Content>


