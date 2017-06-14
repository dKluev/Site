<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Center.ViewModels.SeminarRegisterFormVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.ViewModel"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<% var group = Model.Group; %>
<p>
	Дата: <%= group.DateBeg.DefaultString() %></p>
<p>
	Время:
	<%= group.TimeInterval %></p>
<p>
	Место: <%= Html.ComplexLink(group.Complex) %></p>

<script src='/Scripts/jquery.validate.min.js' type='text/javascript' ></script>
<script src='/Scripts/jquery.validate.messages.ru.js' type='text/javascript' ></script>

<script type="text/javascript">
	$(function () {
		var validator = $("form.valid-form").validate({
			rules: {
				FullName: "required",
				Phone: "required",
				CompanyName: "required",
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

<% using (Html.DefaultForm<CenterController>(c => 
		c.SeminarRegistrationPost(group.Group_ID,null),
		new {@class="valid-form"})) { %>
    <% Htmls.FormSection(" ", () => {%> 
	<%= Html.ControlFor(x => x.FullName) %>
	<%= Html.ControlFor(x => x.CompanyName) %>
	<%= Html.ControlFor(x => x.Email) %>
	<%= Html.ControlFor(x => x.Phone) %>
	<%= Html.ControlFor(x => x.Position) %>
	<%= Html.ControlFor(x => x.Region) %>
	<%= Html.ControlFor(x => x.StudyManger) %>
	<%= Html.ControlFor(x => x.Section) %>
	<%= Html.ControlFor(x => x.Courses) %>
	<%= Html.ControlFor(x => x.HowMany) %>
	<%= Html.ControlFor(x => x.WhereAbout) %>
    <% }); %>
<p id="form-result" style="display:none;color:green;font-weight:bold;">Заявка успешно отправлена</p>
    <%= Htmls.Submit("send") %>
<p style="font-size: 10px;">Количество мест ограничено. Центр в праве отказать в бесплатной регистрации на мероприятие без объяснения причин</p>
<% } %>

<br />



