<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<JsTableVM>" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Cms.Util"%>
<%@ Import Namespace="SimpleUtils.Extension"%>
<%@ Import Namespace="SimpleUtils.Extension"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<script src="/Scripts/datatable/jquery.dataTables.min.js" type="text/javascript"></script>

<script type="text/javascript">
	$(function () {

	function parseDate(x){
		var parts = x.split(' ');
		var time = parts[1];
		var date = parts[0].split('.');
		var result = new Date([date[1],date[0],'20' + date[2]].join('/') + ' ' + time)
		return result;
	}

	function dateCompare(a,b){
		return parseDate(a).getTime() - parseDate(b).getTime();
	}
	


jQuery.fn.dataTableExt.oSort['date-rus-asc'] = function(a, b) {
	return dateCompare(a,b);
};

jQuery.fn.dataTableExt.oSort['date-rus-desc'] = function(a, b) {
	return -dateCompare(a,b);
}; 




		$.fn.dataTableExt.oJUIClasses["sSortable"] = null;
		$.fn.dataTableExt.oJUIClasses["sSortAsc"] = null;
		$.fn.dataTableExt.oJUIClasses["sSortDesc"] = null;
		$("#<%= Model.Id %>").dataTable({
			"bJQueryUI": true,
			"bPaginate": false,
			"bLengthChange": false,
			"bFilter": false,
			"bSort": true,
			"bInfo": false,
			"bAutoWidth": false,
			"bStateSave": true,
			"aoColumns": [
				<%= Model.Columns.Select(x => x.Value == null ? "null" : "{{ 'sType': '{0}' }}".FormatWith(x.Value))
				.JoinWith(",") %>
			]
		});
	});
</script>


<%= H.table.Class("simple-table").Id(Model.Id)[
	H.thead[H.tr[Model.Columns.Select(c => H.th[c.Key])]].Style("cursor:pointer") ,
	H.tbody[Model.Rows.Select(x => H.tr[x.Select(y => H.td[y])])] ] %>
