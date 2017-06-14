<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CourseVM>" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>

<% if (Model.Course.IsVideo) return; %>

<% if (Model.NearestGroups.WebinarOnly) { %>
<h3>����������</h3>

<p>

<strong>
<%= Htmls2.AskTimetable(Model.Course.Course_TC, "�������� ���������� ������ �������� � ���������") %>
</strong></p>
<%	if(!Model.NearestGroups.IsEmpty){%>
<div class="attention2">
<p>
��� ������ ���� ������������ � ������ <%= H.Anchor(SimplePages.FullUrls.Webinar, "��������") %> . 
����� ���������� �� �������, � ������� �������� ��� �������� �� ������� � �������� ������� ��� ��� ������.
</p>
</div>
<h3>���������� ���������</h3>
<% } %>

<% } else { %>
<h2 class="h2_block_select">
	��������� ������&nbsp;
    <%= Model.ShowRss ? Url.Rss().CourseGroups(Model.Course.Course_TC, Images.Common("rss.gif").ToString()) : null %>

</h2>
<div class="clear"></div>
<% if (Model.HasWebinar) { %>
<div class="attention2">
<p>
			

<b>������ ���� �� ������ ������ ��� � ����� �������, ��� � ������������ � ������ <%= H.Anchor(SimplePages.FullUrls.Webinar, "��������") %> .</b> 
����� ���������� �� �������, � ������� �������� ��� �������� �� ������� � �������� ������� ��� ��� ������.<br/>
    <b><a href="/chem-otlichaetsya-obuchenie">��� ���������� �������� � ������ �������� �� ������ ����� ��������?</a></b>
</p>
</div>
<% } %>
<% } %>
<% Html.RenderPartial(PartialViewNames.NearestGroupSet, Model.NearestGroups); %>
<%	if(Model.NearestGroups.HasMoreGroups){%>
<h3><%= Html.GroupsLinkForCourseText(Model.Course.Course_TC) %></h3>
<% } %>


