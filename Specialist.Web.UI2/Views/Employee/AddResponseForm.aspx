<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Center.ViewModels.AddResponseVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.ViewModel"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Util" %>

<script src='/Scripts/jquery.validate.min.js' type='text/javascript' ></script>
<script src='/Scripts/jquery.validate.messages.ru.js' type='text/javascript' ></script>
<script src='/Scripts/select2.min.js' type='text/javascript' ></script>

<script type="text/javascript">
	$(function () {
		$('select[name="CourseTC"]').select2({});

		var validator = $("form.valid-form").validate({
			rules: {
				Text: "required"
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

<% using (Html.DefaultForm<EmployeeController>(c => 
		c.AddResponsePost(null),
		new {@class="valid-form"})) { %>
    <% Htmls.FormSection(" ", () => {%> 
	<%= Html.SelectForWithList(x => x.CourseTC,
		SelectListUtil.GetSelectItemList(Model.Courses, x => x.WebName, x => x.CourseTC)) %>
	<%= Html.ControlFor(x => x.Text) %>
	<%= Html.HiddenFor(x => x.EmployeeTC) %>
    <% }); %>
<p id="form-result" style="display:none;color:green;font-weight:bold;">Отзыв успешно отправлен</p>
    <%= Htmls.Submit("send") %>
<% } %>

<br />



