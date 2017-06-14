<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Center.ViewModels.MtsEmployeeFormVM>" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.ViewModel"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Util" %>

<script src='/Scripts/jquery.validate.min.js' type='text/javascript' ></script>
<script src='/Scripts/jquery.validate.messages.ru.js' type='text/javascript' ></script>

<script type="text/javascript">
    $(function () {
        var validator = $("form.valid-form").validate({
            rules: {
                FullName: "required",
                "Number": "required"
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
		c.MtsEmployeePost(null),
		new {@class="valid-form", id="org-catalog-form"})) { %>
    <% Htmls.FormSection(" ", () => {%> 
	<%= Html.ControlFor(x => x.FullName) %>
	<%= Html.ControlFor(x => x.Organization) %>
	<%= Html.ControlFor(x => x.Email) %>
	<%= Html.ControlFor(x => x.Phone) %>
	<%= Html.ControlFor(x => x.Number) %>

            <tr>
               <td class="name">
                <label> <strong> Курс</strong> </label>
               </td>
                <td class="field">
                    <%=Html.DropDownList("Course", SelectListUtil.GetSelectItemList(Model.Courses, 
                    x=> x.WebName, x => x.CourseTC)) %>
                </td>
            </tr>
            <tr>
               <td class="name">
                <label> <strong> Срок обучения</strong> </label>
               </td>
                <td class="field">
                    <%=Html.DropDownList("Date", new List<SelectListItem>()) %>
                </td>
            </tr>
    <% }); %>
<p id="form-result" style="display:none;color:green;font-weight:bold;">Заявка успешно отправлена</p>
    <%= Htmls.Submit("send") %>
<% } %>

<br />




<script type="text/javascript">

    controls.initSelectCascade("#Course", "#Date",
        <%= JsonConvert.SerializeObject(Model.Dates) %>, "Нет даты");

    window.onload = function() {
        document.getElementById("search").style.display = "none";
        var anchors = document.getElementsByTagName("a");
        for (var i = 0; i < anchors.length; i++) {
            anchors[i].href = "#";
        }
    };
</script>
