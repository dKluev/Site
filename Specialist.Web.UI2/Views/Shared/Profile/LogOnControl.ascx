<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Passport.ViewModel.LogOnVM>" %>
<%@ Import Namespace="System.Data.Linq.Mapping" %>
<%@ Import Namespace="System.Runtime.InteropServices" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<% using (Html.DefaultForm<AccountController>(c => c.LogOn((string)null), new {id="form-logon"})) { %>
        <%= Html.ValidationSummary() %>
        <%= Html.ControlFor(x => x.ReturnUrl) %>
        <% Htmls2.FormSection(() => {%>
            <%=Html.ControlFor(x => x.Email)%>
            <%=Html.ControlFor(x => x.Password)%>
            <%=Html.ControlFor(x => x.Remeber)%>
        <% }); %>
    <%= Url.Link<ProfileController>(
                    c => c.RestorePassword((string)null), "Напомнить пароль")
					.Class("open-in-dialog") %>
	<%= Htmls.Submit("ok") %>
    <% } %>


<script>
    $(function () {
        var emailInput = $("#form-logon input[name='Email']");
        function checkEmail() {
            var email = emailInput.val();
            if (email && email.toLowerCase().indexOf("specialist.ru") >= 0) {
                $("input[name='Remeber']").prop('checked', false);
            }
        }
        emailInput.on("input", checkEmail);

    });
</script>

