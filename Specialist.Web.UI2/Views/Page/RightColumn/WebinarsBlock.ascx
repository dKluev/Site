<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.Group>>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<% if(Model.Count() == 0) return; %>
<%= Htmls2.Menu2("Вебинары")%>
<div class="block_chamfered_in v_group_discount">
	     <ul class="square">

    <% foreach (var group in Model){ %>
        <li><%= Html.CourseLink(group.Course) %><br />
            <span class="date"><%= group.DateBeg.DefaultString() %></span></li>
<% } %>
        </ul>
</div>
