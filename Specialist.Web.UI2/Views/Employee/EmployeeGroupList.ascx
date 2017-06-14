<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Specialist.Entities.Context.Group>>" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<% var hasTest = Model.Any(x => x.Course.TestId.HasValue); %>
<% var guid = Guid.NewGuid().ToString("N"); %>
<table class="defaultTable <%= guid %>">
    <tr>
	    <th>Дата</th>
		<th>Курс</th>
        <% if(hasTest){ %>
		<th>Тестирование</th>
        <% } %>
    </tr>
<% foreach(var group in Model){ %>
    <tr>
        <td style="vertical-align: top;" class="table-collapse-<%= group.MegaGroup_ID %>"> <%= group.DateInterval %>
        </td>
        <td style="text-align: left;"><%= Html.GroupLink(group.Group_ID, group.Course.Name) %></td>
        <% if(hasTest){ %>
        <td>
	        <% if(group.Course.TestId.HasValue) { %>
			<%= Url.GroupTest().PlanTestUserStats(group.Group_ID, "Тестирование") %>
			<% } %>
        </td>
        <% } %>
    </tr>
<% } %>
    
</table>


    <script type="text/javascript">
        
        $(function() {
        var megaIds = <%= JsonConvert.SerializeObject(Model.Select(x => x.MegaGroup_ID)
    .Where(x => x.HasValue).Distinct()) %>;
                log(megaIds);
            $.each(megaIds, function(i, x) {
                var selector = "table.<%=guid%> td.table-collapse-" + x;
                var rows = $(selector + ":not(:first)").hide();
                $(selector + ":first").attr("rowspan", rows.length + 1);
            });
        })
    </script>