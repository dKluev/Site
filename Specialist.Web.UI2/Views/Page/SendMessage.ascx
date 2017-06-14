<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SendMessageVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.ViewModel"%>
<%@ Import Namespace="DynamicForm"%>

<script src='/Scripts/jquery.validate.min.js' type='text/javascript' ></script>
<script src='/Scripts/jquery.validate.messages.ru.js' type='text/javascript' ></script>
<script type="text/javascript">
	$(function () {
		var validator = $("form.valid-form").validate({
			rules: {
				Title: "required",
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
		    $('.form-message').show();
		    return false;
		});
    });
</script>

    <% if (Model.Employee != null) { %>
       <h3> <%=   Html.EmployeeLink(Model.Employee)  %></h3> 
    <% } %>
<% using (Html.DefaultForm<PageController>(c => c.SendForWebMaster(string.Empty),
		new {@class="valid-form"})) { %>

    <%= Html.HiddenFor(x => x.Type) %>
    <%= Html.HiddenFor(x => x.CustomValue) %>
            
    <% Htmls.FormSection(" ", () => {%> 
	<tr class="form-message" <%= Htmls.DisplayNone() %> ><td></td><td>
	<strong style="color:green;">Ваше сообщение отправлено</strong>
	</td></tr>
	<%= Html.ControlFor(x => x.Title) %>
    <%= Html.HiddenFor(x => x.EmployeeTC) %>
    <% if(!Request.IsAuthenticated) { %>
        <%= Html.ControlFor(x => x.Email) %>
        <%= Html.ControlFor(x => x.SenderName) %>
    <% } %>
    <%= Html.ControlFor(x => x.Message) %>
    <% }); %>
    <%= Htmls.Submit("ok") %>
<% } %>
