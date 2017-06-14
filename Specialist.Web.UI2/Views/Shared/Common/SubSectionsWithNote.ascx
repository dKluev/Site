<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SubSectionsWithNoteVM>" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Entities.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<% if(Model.List.Any()) { %>

<div class="tab_content">

<% foreach (var subSectionWithNotes in Model.List.Cut(2)) { %>
	<div class="tab_2column">
    <% foreach (var subSectionWithNote in subSectionWithNotes) { %>
        <% if(subSectionWithNote == null){ %>
            <% break; %> 
        <% } %>
		<div class="branches_block">
		    <% if(Model.SmallImage) {%>
                <%=Images.EntitySmall(subSectionWithNote.Entity)%>
            <% }else { %>
			    <% if(subSectionWithNote.Entity is Employee) {%>
	                <%=Images.Entity(subSectionWithNote.Entity).Width(115).Style("margin-right:5px;") %>
	            <% }else { %>
	                <%=Images.Entity(subSectionWithNote.Entity)%>
	            <% } %>

            <% } %>
            <h3> <%= Html.GetLinkFor(subSectionWithNote.Entity) %></h3>
            <%= subSectionWithNote.Note %>
        </div>
    <% } %>   
    </div>
<% } %>

</div>

<% } %>
  



