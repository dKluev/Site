<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Course>>" %>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Сортировка курсов</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script src="/Scripts/jquery.tablednd_0_5.js" type="text/javascript"></script>
<script type="text/javascript">
    var changedTCList = []
    $(function() {
        $("#table-sort").tableDnD({
            onDragClass: 'ui-state-hover',
            onDrop: function(table, row) {
                changedTCList.push(row.id);
            }

        });

        $("#save-button").click(function() {
            $("#indicator").show();
            trs = $("#table-sort").find("tr");
            list = [];
            trs.each(function() {
                list.push($(this).attr("id"));
            });
            var data = { changedTCList: changedTCList,
                allTCList: list
            };
            $.post('<%= Url.Action<CourseEntityController>(c => c.UpdateSorting(null, null)) %>',
            data,
            function(response) {
                if (response == "done") {
                    window.location.reload();
                } else {
                    $("#indicator").html(response);
                }
            }, "json");
        });
    });
</script>
    <h1>Глобальная сортировка курсов</h1>
    <br />
    <br />
    <% using(Html.BeginForm()){ %>
        <% Html.RenderPartial(PartialViewNames.SiteObjectSelector, 
           new ControlVM{PropertyName = "SiteObject2_ID"}); %>
    <% } %>
    <br />
    <% var siteObject = (SiteObject)ViewData["SiteObject"]; %>
    <% if(siteObject != null) { %>
        <%= Html.SiteObjectLink(siteObject) %>
    <% } %>
    <br />
    <br />
    <table id="table-sort">
    <% foreach (var course in Model) { %>
        <tr id="<%= course.Course_TC %>" class="ui-widget-content">
            <td> <%= course.Course_TC %> </td>
            <td> <%= course.Name %> </td>
        </tr>
    <% } %>
    </table>
    <br />
    <% if(Model.Any()) { %> 
        <%= HtmlControls.Submit("Сохранить", "save-button") %>
    <% } %>
    <div id="indicator" style="display:none"><%= HtmlControls.Image("/Content/Image/LoadIndicator.gif") %> </div>

</asp:Content>

