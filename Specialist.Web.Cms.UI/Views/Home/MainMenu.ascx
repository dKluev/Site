<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SimpleUtils.Common.Tree<System.String>>" %>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>

<script type="text/javascript">
    $(function() {
        $('ul.jd_menu').jdMenu();
    });
</script>

	<ul class="jd_menu ui-state-default ui-corner-all">
	<% foreach (var node in Model.Nodes) { %>
	    <li> 
	        <div> 
    	        <span style="float:left"><%= node.Value %></span> 
    	        <span class="ui-icon ui-icon-triangle-1-s" style="float:left"></span> 
            </div>   
	        <% Html.RenderPartial(PartialViewNames.MainMenuPart, node.Nodes); %>
   	    </li>
	<% } %>
   	</ul>