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
	��������������� ����������</h2>
<div class="attention">

	<% foreach (var prerequisite in Model.Course.CoursePrerequisites
		.Where(x => x.Test_ID == null)
		.OrderBy(x => x.SortOrder)) { %>
<p>
<strong>
	<% if (prerequisite.IsRequired) { %>
	��������� ����������:
	<% } else { %>
	������������� ���������� (��������������):
	<% } %>
</strong>
	<% if (prerequisite.RequiredCourse_TC != null) { %>
	�������� ��������� �����
	<%= StringUtils.AddTargetBlank(Html.CourseLink(prerequisite.RequiredCourse)) %> ��� ������������� ����������.
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
		��� ����������� ������ ����� ��������������� ����������, ����������� ��� ������  
            <%= Url.Test().Prerequisite(Model.Course.Course_ID,"���������� ������������") %>.
	</p>
<% } %>
<p><b>�������� ������������ � ����������� ��������������� ���������� �� ����� �� ������ � ����� ����������: +7 (495) 232-32-16.</b>
</p>

<p style="font-size:10px">������� ��������������� ���������� �������� ������� ������ ��������� ��������. ��������������� ���������� ����������� � ���� �������� ������ ������ ������ (������������ ��������������� ����������).

��� ������� ��������� ��������� ���������� ����� � �������������� �������, ���� �� � ��� ������ � ����, ������������� ������ ���������.

���� �� ��������� �������� ����� 85-90% �������������� �����, �� �� ����������� ������ �������� ��������������� ����������.

������ ����� ����� �� ������� ����������� ��������� �� ��������� �����.
</p>
</div>

