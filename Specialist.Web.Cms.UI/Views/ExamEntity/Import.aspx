<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.Core.ViewModel.JsTableVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Const" %>
<%@ Import Namespace="Specialist.Web.Cms.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Импорт экзаменов</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Импорт экзаменов</h2>
<% if(Model == null){ %>
<h3 id="status"></h3>
<br />
<div id="progressbar" style="width:200px;height:10px"></div>
<br />
<%= H.Submit("Загрузить").Id("import-button").Class("confirm-dialog") %>
<% }else{ %>

<% Html.RenderPartial(PartialViewNames.JsTableControl); %>
<h3 id="status"></h3>
<br />
<div id="progressbar" style="width:200px;height:10px"></div>
<br />
<%= H.Submit("Обновить цены в базе").Id("update-button").Class("confirm-dialog") %>
<% } %>
<script>
	$(function () {
		var progBar = $("#progressbar");
		function updateStatus() {
			$.post('<%= Url.Action<ExamEntityController>(c => c.Status()) %>', function (data) {
				if (data == "done")
					location.reload();
				else {
					$("#status").html(data.text);
					progBar.progressbar({ value: data.percent });
				}
			});
		}
		updateStatus();
		$("#import-button").click(function () {
			progBar.progressbar({ value: 10 });
			$.post('<%= Url.Action<ExamEntityController>(c => c.StartImport()) %>', 
				function () { updateStatus();});
			
		});

		$("#update-button").click(function () {
			progBar.progressbar({ value: 10 });
			$.post('<%= Url.Action<ExamEntityController>(c => c.UpdatePrices()) %>',
				function () { updateStatus();});
		});

		setInterval(updateStatus, 3000);
	});
</script>
</asp:Content>

