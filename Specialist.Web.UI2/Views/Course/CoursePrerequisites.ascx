<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CourseVM>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Tests" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<% if (!Model.HasPrerequisites) return; %>

<h2 class="h2_block">
	Предварительная подготовка</h2>
<div class="attention">

	<% foreach (var prerequisite in Model.Course.CoursePrerequisites
		.Where(x => x.Test_ID == null)
		.OrderBy(x => x.SortOrder)) { %>
<p>
<strong>
	<% if (prerequisite.IsRequired) { %>
	Требуемая подготовка:
	<% } else { %>
	Рекомендуемая подготовка (необязательная):
	<% } %>
</strong>
	<% if (prerequisite.RequiredCourse_TC != null) { %>
	Успешное окончание курса
	<%= StringUtils.AddTargetBlank(Html.CourseLink(prerequisite.RequiredCourse)) %> или эквивалентная подготовка.
	<% } %>
	<%= prerequisite.Text %>
</p>
<% } %>
    <% var test = Model.Course.CoursePrerequisites.FirstOrDefault(x => x.Test_ID != null && x.SortOrder == 11111); %>
<% if (test != null) { %> 
    <% if (!test.Text.IsEmpty()) { %> 
       <%= test.Text %> 
    <% } %>
	<p>
		Для определения уровня Вашей предварительной подготовки, рекомендуем Вам пройти  
            <%= Url.Test().Prerequisite(Model.Course.Course_ID,"бесплатное тестирование") %>.
	</p>
<% } %>
<p><b>Получить консультацию о необходимой предварительной подготовке по курсу Вы можете у наших менеджеров: +7 (495) 232-32-16.</b>
</p>

<p style="font-size:10px">Наличие предварительной подготовки является залогом Вашего успешного обучения. Предварительная подготовка указывается в виде названия других курсов Центра (Обязательная предварительная подготовка).

Вам следует прочитать программу указанного курса и самостоятельно оценить, есть ли у Вас знания и опыт, эквивалентные данной программе.

Если Вы обладаете знаниями менее 85-90% рекомендуемого курса, то Вы обязательно должны получить предварительную подготовку.

Только после этого Вы сможете качественно обучиться на выбранном курсе.
</p>
</div>

