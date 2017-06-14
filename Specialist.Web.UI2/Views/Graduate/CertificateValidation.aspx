<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Learning.ViewModels.CertificateValidationVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.ViewModel"%>
<%@ Import Namespace="DynamicForm"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src='/Scripts/jquery.validate.min.js' type='text/javascript' ></script>
<script src='/Scripts/jquery.validate.messages.ru.js' type='text/javascript' ></script>

<script type="text/javascript">
	$(function () {
		var validator = $("form.valid-form").validate({
			rules: {
				FullName: "required",
				Number: "required"
			},
			errorClass: 'input-error-val',
			validClass: 'input-valid'
		});


		$('form.valid-form').submit(function () {
			var form = $(this);
			if (!form.valid())
				return false;
			$(".input-validation-error").removeClass("input-validation-error");
			$(".input-valid").removeClass("input-valid");
			$.post(
				form.attr("action"),
				form.serializeJson(), function (data) {
					$('#form-result').html(data);
				});
			$('#form-result').html(controls.indicator);
			return false;
		});
	});
</script>

<% using (Html.DefaultForm<GraduateController>(c => c.CertificateValidation(),
		new {@class="valid-form"})) { %>

    <% Htmls.FormSection(" ", () => {%> 
    <%= Html.ControlFor(x => x.Number) %>
	<%= Html.ControlFor(x => x.FullName) %>
    <% }); %>
    <%= Htmls.Submit("ok") %>
<% } %>

<p id="form-result"></p>

</asp:Content>
