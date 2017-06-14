<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.ViewModel.ReverseRelationSortingVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Сортировка [<%= SiteObjectType.AllBySysName[Model.ObjectType].Name %>]</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


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
            
            list = [];
            trs.each(function() {
                list.push($(this).attr("id"));
            });
            var data = { relationIds: list};
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
    <h1>Сортировка [<%= SiteObjectType.AllBySysName[Model.ObjectType].Name %>]</h1>
    <br />
    <br />
    <% using(Html.BeginForm()){ %>
        <% Html.RenderPartial(PartialViewNames.SiteObjectSelector, 
           new ControlVM{PropertyName = "SiteObject2_ID"}); %>
    <%= H.Hidden("className", Model.ClassName) %>
    <% } %>
    <% if(Model.Relations.Any()){ %>
            <%= Html.SiteObjectLink(Model.SiteObject) %> 
    <br />
    <br />
    
    <table id="table-sort">
    <% foreach (var relation in Model.Relations) { %>
        <tr id="<%= relation.SiteObjectRelation_ID %>"  class="ui-widget-content">
            <td>
        <%= relation.Object.GetOrDefault(x => x.Name) %>
            </td>
        </tr>
    <% } %>
    </table>
    <br />
    
        <%= HtmlControls.Submit("Сохранить", "save-sort-button") %>
    <div id="indicator" style="display:none"><%= HtmlControls.Image("/Content/Image/LoadIndicator.gif") %> </div>
    <% } %>
</asp:Content>

