<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Specialist.Web.Cms.ViewModel.TrainerInfoVM>>" %>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Cms.Util" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Преподаватели</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/datatable/jquery.dataTables.min.js" type="text/javascript"></script>
    <h2>Преподаватели</h2>

<p><%= Html.ActionLink<EmployeeEntityController>(c => c.ExportToExcel(), "Экспорт в Excel") %></p>

<script type="text/javascript">
    $(function () {
        $.fn.dataTableExt.oJUIClasses["sSortable"] = null;
        $.fn.dataTableExt.oJUIClasses["sSortAsc"] = null;
        $.fn.dataTableExt.oJUIClasses["sSortDesc"] = null;
        $("#trainers").dataTable({
            "bJQueryUI": true,
            "bPaginate": false,
            "bLengthChange": false,
            "bFilter": false,
            "bSort": true,
            "bInfo": false,
            "bAutoWidth": false,
            "aoColumns": [
			null,
			{ "sType": "html" },
            null,
            { "sType": "numeric" },
             null,
             { "sType": "numeric" },
                null
		]

        });
    });
</script>

<table class="simple-table" id="trainers">
<thead style="cursor:pointer">
 <tr>
        <th>Код</th>
        <th>ФИО</th>
        <th>Описание</th>
        <th>Отзывы</th>
        <th>Фото</th>
        <th>Группы</th>
        <th>Активный</th>
    </tr>
    </thead>
    <tbody>
<% foreach(var trainer in Model){ %>
   
    <tr>
    <td><%= trainer.Trainer.Employee_TC %> </td>
    <td><%= trainer.Link %></td>
    <td>
    <%= CmsHtmls.CheckIcon(trainer.HasDescription)%>  
    </td> 
    <td><%= trainer.ResponseCount %></td>
    <td><%= CmsHtmls.CheckIcon(trainer.HasPhoto) %></td>
    <td><%= trainer.Groups %></td>  
   <td><%= CmsHtmls.CheckIcon(trainer.Trainer.SiteVisible) %></td>
    </tr>
<% } %>
</tbody>
</table>

</asp:Content>

