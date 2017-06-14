<%@  Page Title="" Language="C#" 
    Inherits="ViewPage<IEnumerable<Specialist.Entities.Catalog.Links.CourseLink>>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>

<% if (!Model.Any()) return; %>
<h2 class="h2_block">
	������������� ����� �� �������������</h2>
<p>����� ����� ��������������, �� ����������� ��� ������ � ���� ������ �������:</p>
<ul>
<% foreach (var course in Model) { %>
    <li>
        <%= course.IsNew ? H.span.Class("discount_color")["NEW!"] : null %> <%= StringUtils.AddTargetBlank(Html.CourseLink(course))%><br />
    </li>
<% } %>

</ul>


