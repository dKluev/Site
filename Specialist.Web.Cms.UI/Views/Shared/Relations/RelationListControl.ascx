<%@ Control Language="C#" 
    Inherits="System.Web.Mvc.ViewUserControl<RelationListVM>" %>
<%@ Import Namespace="Specialist.Services.Catalog" %>
<%@ Import Namespace="Specialist.Web.Cms.Controllers" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Helpers"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<table id="table-sort">
<% foreach (var siteObjectRelation in 
       Model.Relations.Where(x => x.RelationObject != null)) { %>
    
    <tr id="<%= siteObjectRelation.SiteObjectRelation_ID %>">
          <td width="20px">
          <%= Html.Image("/Content/Image/Delete.png",
                    new { @class = "delete", 
                        id = siteObjectRelation.SiteObjectRelation_ID })%>
          
                                
        </td>
        <td> <%= Html.SiteObjectLink(siteObjectRelation.RelationObject) %></td>
		<% if(Model.EntityType == typeof(Course)){ %>
        <td> <%= Url.Link<SiteObjectRelationEntityController>(c => 
			c.ReverseRelationSorting(typeof(Course).Name, siteObjectRelation.RelationObject_ID.ToString(), siteObjectRelation.RelationObjectType), "Сорт.") %></td>
		<% } %>
    </tr>
<% } %>
</table>

<% if (Model.Sortable) { %>

<script src="/Scripts/jquery.tablednd_0_5.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function() {
        $("#table-sort").tableDnD({
            onDragClass: 'ui-state-hover'
        });
        var saveButton = $("#save-sort-button").show();
        saveButton.click(function() {
            $("#indicator").show();
            trs = $("#table-sort").find("tr");
            var objId = <%= Model.SiteObject.ID %>;
            var objType = '<%= Model.SiteObject.Type %>';

            list = [];
            trs.each(function() {
                list.push($(this).attr("id"));
            });
            var data = { relationIds: list, objId:objId, objType: objType};
            $.post('<%= Url.Action<SiteObjectRelationEntityController>(c =>
                            c.UpdateRelationSorting(null)) %>',
            data,
            function(response) {
                
                if (response == "ok") {
                    $("#indicator").hide();    
                } 
            }, "json");
        });
    });
</script>

<% } %>