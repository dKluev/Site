<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GroupFilter>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Util"%>
<%@ Import Namespace="Specialist.Entities.Filter"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.0/themes/redmond/jquery-ui.css" type="text/css" />
<title>Поиск расписания</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js" type="text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/i18n/jquery.ui.datepicker-ru.js" type="text/javascript"></script>

<script src="/Scripts/Views/Group/jquery.autocomplete.js" type="text/javascript"></script>
<script src="/Scripts/Views/Group/autocomplete.js" type="text/javascript"></script>

<script type="text/javascript">
    setAutocomplete('#CourseName',
        '<%=Url.Action<CourseController>(c => c.CourseNames(null)) %>');
</script>
	<%= H.JQuery(@"
	$.datepicker.setDefaults($.datepicker.regional['ru']);
	$('input.datetime').datepicker({ dateFormat: 'dd.mm.yy', showAnim: 'fadeIn' });") %>
<%= Htmls.Title("Поиск расписания") %>
    <% using (Html.BeginForm<GroupController>(c => c.SearchSubmit(null))) { %>
        <div class="registr_form">
            <% Htmls.FormSection("Параметры поиска", () => {%>
			 <tr>
                <td class="name"> Направление </td>
                <td class="field">
					<%= H.select.Name("SectionId")[_.List(H.option.Value("")["Любое"])
						.AddFluent(Model.Sections.SelectMany(
						s => _.List(s).AddFluent(s.SubSections.Where(y => y.IsActive)))
						.Select(x => H.option[x.Name].Value(x.Section_ID)
							.Style(x.IsMain ? "font-weight:bold" : "padding-left:10px")))] %>

                </td>
            </tr>
        <%= Html.ControlFor(x => x.CourseName) %>
            <tr>
                <td class="name">
                    Дата начала занятий
                </td>
                <td class="field">
                    c
                    <%= H.InputText(Model.For(x => x.BeginDate), Model.BeginDate.DefaultString())
                    	.Style("width:100px").Class("datetime") %>
                    по
                    <%= H.InputText(Model.For(x => x.EndDate), Model.EndDate.DefaultString())
                    	.Style("width:100px").Class("datetime") %>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Форма обучения 
                </td>
                <td class="field">
                    <%= Html.DropDownListFor(x => x.StudyTypeId, Model.GetStudyTypes()) %>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Время проведения занятий
                </td>
                <td class="field">
                    <%= Html.DropDownListFor(x => x.DayShiftTC, Model.GetDayShiftList()) %>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Выходной
                </td>
                <td class="field">
                    <%= Html.DropDownListFor(x => x.DaySequenceTC, Model.GetDaySequences()) %>
                </td>
            </tr>
            <tr>
                <td class="name"> Комплекс (Станция метро) </td>
                <td class="field">
                    <%= Html.DropDownListFor(x => x.ComplexTC, Model.GetComplexList()) %>
                </td>
            </tr>
              <tr>
                <td class="name"> Преподаватель </td>
                <td class="field">
                    <%= Html.DropDownListFor(x => x.EmployeeTC, 
                    SelectListUtil.GetSelectItemList(Model.Employees, 
                    x=> x.FullName, x => x.Employee_TC,true, "Любой")) %>
                </td>
            </tr>
            <% }); %>
        </div>
    	<%= Htmls.Submit("ok") %>
    <% } %>

 
</asp:Content>
