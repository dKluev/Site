<%@  Page Title="" Language="C#"
    Inherits="ViewPage<IEnumerable<Specialist.Entities.Core.TagWithEntity>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
	<div class="block_chamfered_in v_know">

<% if(Model.Any()) { %>
		<h3 class="h3_blue">Продукты и технологии курса</h3>
				
        <ul class="square_blue">
            <% foreach (var tag in Model) { %>
                <li><%= Html.TagLink(tag) %></li>
            <% } %>
        </ul>
<% } %>
</div>

