<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SimpleUtils.Common.Tree<System.String>>>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>

<% if (Model.IsEmpty()) return;%>

<ul class="ui-widget-content ui-corner-all">
	<% foreach (var tree in Model) { %>
	    <li> 
	        <div> 
    	        <span style="float: left; text-decoration: underline">
    	            <%= tree.Value %></span> 
    	        <% if(!tree.IsLeaf) { %>
    	        <span class="ui-icon ui-icon-triangle-1-e" style="float:left"></span> 
    	        <% } %>
            </div>   
            
	        <% Html.RenderPartial(PartialViewNames.MainMenuPart, tree.Nodes); %>
   	    </li>
	<% } %>
</ul>
   	    