<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Center.ViewModels.OrgCatalogFormVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.ViewModel"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<script src='/Scripts/jquery.validate.min.js' type='text/javascript' ></script>
<script src='/Scripts/jquery.validate.messages.ru.js' type='text/javascript' ></script>

<script type="text/javascript">
	$(function () {
		var validator = $("form.valid-form").validate({
			rules: {
				FullName: "required",
				Phone: "required",
				CompanyName: "required",
				"Position": "required",
				Index: "required",
				Address: "required",
				Count: "required",
				Email: "required"
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
			$.post(form.attr("action"), form.serializeJson());
			$("input[type!='submit']", form).val("");
			$("textarea", form).val("");
			$('#form-result').show();
			return false;
		});
        function toggleDiv() {
		    $("#org-catalog-form div.block_chamfered_content").eq(1).toggle(!$('#IamStudyManager').prop("checked"));
        }
		$('#IamStudyManager').click(function () {
		    toggleDiv()
		});
	    toggleDiv();
	});
</script>


<% using (Html.DefaultForm<CenterController>(c => 
		c.OrgCatalogPost(null),
		new {@class="valid-form", id="org-catalog-form"})) { %>
    <% Htmls.FormSection("Информация необходимая для получения каталога", () => {%> 
	<%= Html.ControlFor(x => x.CompanyName) %>
	<%= Html.ControlFor(x => x.FullName) %>
	<%= Html.ControlFor(x => x.Position) %>
	<%= Html.ControlFor(x => x.Index) %>
	<%= Html.ControlFor(x => x.Address) %>
	<%= Html.ControlFor(x => x.Email) %>
	<%= Html.ControlFor(x => x.Phone) %>
	<%= Html.ControlFor(x => x.Count) %>
	<%= Html.ControlFor(x => x.IamStudyManager) %>
    <% }); %>
    <% Htmls.FormSection("Контактные данные сотрудника, принимающего решение по обучению", () => {%> 
	<%= Html.ControlFor(x => x.SmFullName) %>
	<%= Html.ControlFor(x => x.SmPhone) %>
	<%= Html.ControlFor(x => x.SmEmail) %>
    <% }); %>
<p id="form-result" style="display:none;color:green;font-weight:bold;">Заявка успешно отправлена</p>
    <%= Htmls.Submit("send") %>
<% } %>

<br />



