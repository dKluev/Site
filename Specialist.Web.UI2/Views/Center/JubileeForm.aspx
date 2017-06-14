<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Center.ViewModels.JubileeFormVM>" %>
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
				Message: "required"
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

<% using (Html.DefaultForm<CenterController>(c => c.JubileeFormPost(null),
		new {@class="valid-form"})) { %>
    <% Htmls.FormSection(" ", () => {%> 
	<%= Html.ControlFor(x => x.FullName) %>
	<%= Html.ControlFor(x => x.Message) %>
	<%= Html.ControlFor(x => x.CompanyName) %>
	<%= Html.ControlFor(x => x.VideoLink) %>
	  <tr>
               <td class="name">
                <label >
                    <strong>
                        Файл</strong>
                </label>
               </td>
                <td class="field">
                 <button id="upload-button">Прикрепить файл</button>
				 <img <%= Htmls.DisplayNone() %> id="indicator" src="//cdn.specialist.ru/Content/Image/Common/indicator.gif"/>
				 
                </td>
            </tr>
    <% }); %>
<p id="form-result" style="display:none;color:green;font-weight:bold;">Поздравление успешно отправлено</p>
    <%= Htmls.Submit("send") %>
<% } %>

<br />




<script src="/Scripts/ajaxupload.js" type="text/javascript"></script>
<script type="text/javascript">
	$(function () {
		new AjaxUpload('upload-button', {
			action: '<%= Url.Action<CenterController>(c => c.UploadJubileeFile(null)) %>',
			responseType: false,
			onSubmit: function (file, ext) {
				$("#indicator").show();
			},
			onComplete: function (file, response) {
				$("#indicator").hide();
				if (response == "Size") {
					alert('Слишком большой файл');
				} else if (response == "ok") {
					alert('Файл успешно прикреплен');
				}
			}
		});
	});

</script>
