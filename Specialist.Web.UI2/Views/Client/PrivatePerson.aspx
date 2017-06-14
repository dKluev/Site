<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<PrivatePersonVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <% if(Model.IsSuccessStories){ %>
            <%= Html.Site().SuccessStories(Model.SuccessStories) %>
            <%= Html.GetNumericPagerPretty(Model.SuccessStories) %>
        <% } %>
        
        <% if(Model.IsResponses){ %>
            <h3 class="h3_tab"><%= Html.ActionLink<ClientController>(c => c.ChooseSectionResponses(), "Отзывы по направлениям") %></h3>
            <% Html.RenderPartial(PartialViewNames.ResponseList, Model.Responses);%>
            <%= Html.GetNumericPagerPretty(Model.Responses) %>
        <% } %>
        
        <% if(Model.IsUserWorks){ %>
            <% foreach(var rootSection in Model.UserWorks){ %>
            <h3><%= rootSection.Entity.Name %></h3>
            <% foreach(var section in rootSection.List){ %>
                <h4>
                    <%= Html.UserWorks(section.Entity) %></h4>
                <% foreach(var workSection in section.List.Where(ws => ws != null)){ %>
                    <%= Html.UserWorks(section.Entity, workSection) %><br/>
                <% } %>
                
            <% } %>
            <% } %>
        <% } %>

</asp:Content>


