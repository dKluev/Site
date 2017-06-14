<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Pages.AjaxGridVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers.Tests" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<table id="grid<%= Model.Postfix %>" class="resize-in-dialog">
</table>
<div id="grid-pager<%= Model.Postfix %>">
</div>
<script type="text/javascript">
	$(function(){
	    utils.loadScript("/Scripts/AjaxTable/jqgrid.all.min.js", function() {
	$.jgrid.no_legacy_api = true;
	$.jgrid.useJSON = true;

	        $("delete-button<%= Model.Postfix %>").button();
	        function reloadGrid() {
	            $("#grid<%= Model.Postfix %>").trigger("reloadGrid");
	        }
	        $("#grid<%= Model.Postfix %>").jqGrid({
	                url: '<%= Model.GetListUrl %>',
	                datatype: "json",
	                colNames: <%= JsonConvert.SerializeObject(Model.ColumnTitles) %> ,
	                colModel: <%= JsonConvert.SerializeObject(Model.Columns) %> ,
	                rowNum: 10,
	                rowList: [10, 20, 30,50],
	                pager: '#grid-pager<%= Model.Postfix %>',
	                viewrecords: true,
	                caption: "<%= Model.Caption %>",
	                autowidth: true,
	                height: "100%",
	                jsonReader: {
	                    root: "Rows",
	                    page: "Page",
	                    total: "Total",
	                    records: "Records",
	                    repeatitems: false,
	                    userdata: "UserData",
	                    id: "Id"
	                }
	            });

	        $("#delete-button<%= Model.Postfix %>").click(function() {

	        });
	        var currentId = 0;
	        function openDeleteDialog(id) {
	            currentId = id;
	            var dialog = $("#delete-dialog<%= Model.Postfix %>");
	            dialog.dialog({
	                    modal: true,
	                    width: 'auto',
	                    height: 'auto',
	                    hide: 'fade',
	                    show: 'fade',
	                    title: 'Удаление',
	                    buttons: {
	                        "Отмена": function() { $(this).dialog("close"); },
	                        "Да": function() {
	                            utils.postShowLoad('<%= Model.DeleteUrl %>/' + currentId, function() {
	                                $("#delete-dialog<%= Model.Postfix %>").dialog('close');
	                                reloadGrid();});
	                        }

	                    }

	                });
	        }

	        function openGridDialog(url) {
	            <% if (Model.OpenDialogsInPage) { %>
    	            document.location = url;
	            <% } else { %>
	               controls.openUIDialog(url).data('refreshCallback', reloadGrid);
                <% } %>
            }


	        $("#grid<%= Model.Postfix %>").jqGrid('navGrid', '#grid-pager<%= Model.Postfix %>', {
	            edit: true,
	            add: <%= JsonConvert.SerializeObject(!Model.DenyAdd) %> ,
	            search: false,
	            del: <%= JsonConvert.SerializeObject(!Model.DeleteUrl.IsEmpty()) %> ,
	            editfunc: function(id) {
	                openGridDialog('<%= Model.EditUrl %>/' + id);
	            },
	            delfunc: function(id) { openDeleteDialog(id); },
	            addfunc: function() {
	                openGridDialog('<%= Model.EditUrl %>');
	            }
	        })
	        <% if(!Model.ViewUrl.IsEmpty()) {%>
    	        .navButtonAdd('#grid-pager<%=Model.Postfix%>', {
    	            title: "Просмотр",
    	            caption: '',
    	            buttonicon: "ui-icon-search",
    	            onClickButton: function() {
    	                var id = $("#grid<%=Model.Postfix%>").jqGrid('getGridParam', 'selrow');
    	                if (id) {
	                        openGridDialog('<%=Model.ViewUrl%>/' + id);
    	                } else {
    	                    alert("Ничего не выбрано");
    	                }
    	            },
    	            position: "last"
    	        })
	        <% } %>

	            ;
	    });
	});
</script>

<div style="display:none;" id="delete-dialog<%= Model.Postfix %>">
<table><tr><td>
<div style="margin:20px;">
Уверены?
</div>
</td></tr></table>

</div>
