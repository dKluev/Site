<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Center.ViewModels.PaperCatalogFormVM>" %>
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
	});
</script>

<% using (Html.DefaultForm<CenterController>(c => c.OrderPaperCatalogPost(null),
		new {@class="valid-form"})) { %>
    <% Htmls.FormSection(" ", () => {%> 
	<%= Html.ControlFor(x => x.FullName) %>
	<%= Html.ControlFor(x => x.Email) %>
    <% }); %>
<p id="form-result" style="display:none;color:green;font-weight:bold;">Заявка успешно отправлена</p>
    <%= Htmls.Submit("send") %>
<% } %>

<br />



