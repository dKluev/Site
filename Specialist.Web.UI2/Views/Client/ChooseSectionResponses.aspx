<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<List<Section>>>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Core"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= Htmls.TextBlock() %>
<% if(Model.Data.Any()) { %>

	    <div class="tab_content">

        <% foreach (var entityWithLists in Model.Data.Cut(2)) { %>
        <div class="tab_2column">
            <% foreach (var entityWithList in entityWithLists) { %>
                <% if(entityWithList == null){ %>
                    <% break; %> 
                <% } %>
                   <div class="link_block2">
                <%= Images.EntitySmall(entityWithList) %>
                <h3>
                    <%= entityWithList.Name %></h3>
                <ul>
                    <% foreach (var entity in entityWithList.SubSections) { %>
                    <li>
                        <%= Html.ActionLink<CenterController>(c => c.SectionResponses(entity.UrlName), entity.Name)%>
                    </li>
                     <% } %>
                </ul>
            </div>
            <% } %>   
        </div>
        <% } %>
        
	    </div>

<% } %>   
</asp:Content>
