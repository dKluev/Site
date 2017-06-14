<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<RestorePasswordVM>" %>
<%@ Import Namespace="Specialist.Entities.Profile.MetaData"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<script src='/Scripts/jquery.validate.min.js' type='text/javascript' ></script>
<script src='/Scripts/jquery.validate.messages.ru.js' type='text/javascript' ></script>

<script type="text/javascript">
	$(function () {
		var validator = $("form.valid-form").validate({
			rules: {
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
			$('#form-result').html(controls.indicator);
			$.post(form.attr("action"), form.serializeJson(), function (data) {
			    $('#form-result').html(data.message);
			    if (data.found) {
			        var url = "<%= Url.Action<AccountController>(c => c.LogOn((string)null)) %>";
			        if (document.location.href.indexOf(url) < 0) {
    			        setTimeout(function() { document.location = url; }, 3000);
			        }
			    } 
			});
			return false;
		});
	});
</script>

    <% using (Html.BeginForm<ProfileController>(c => c.RestorePassword((string)null), 
	   FormMethod.Post,
		   new {@class="valid-form"})) { %>
        
        <div class="registr_form">
        <% Htmls.FormSection(" ", () => {%> 
            <%= Html.ControlFor(x => x.Email) %>
        <% }); %>
            <p id="form-result">
            </p>
        </div>

    	<%= Htmls.Submit("ok") %>
    <% } %>
