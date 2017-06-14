<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<CorporateClientsVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<%= Htmls.BorderBegin() %>
		<ul class="bookmarks">
        <% foreach (var simplePage in Model.Pages) { %>
            <% if(simplePage.UrlName == Model.UrlName) { %>
            	<li class="active">
            <% }else{ %>
                <li>
            <% } %>
                <%= Html.ActionLink<ClientController>(c => c.CorporateClients(
                    simplePage.UrlName, null), simplePage.Name) %>
            </li>
        <% } %>
        </ul>
        <div class="tab_content">
        
        <% if(Model.IsResponses){ %>
		<% Html.RenderPartial(PartialViewNames.OrgResponseList, Model.Responses); %>
            <%= Html.GetNumericPagerPretty(Model.Responses) %>  
        <% } %>
        
        <% if(Model.IsProjects){ %>
            <% foreach(var project in Model.OrgProjects){ %>
                <h3><%= project.Name %></h3>
                <%= project.Description %> <br />
            
            <% } %>
        <% } %>
        </div>
<%= Htmls.BorderEnd %>

</asp:Content>
