<%@  Page Title="" Language="C#" 
    Inherits="ViewPage<IEnumerable<TrackDiscount>>" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Web.Common.Site" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<div class="block_yellow"><div class="block_c_tl"></div> <div class="block_c_tr"></div> <div class="block_c_br"></div> <div class="block_c_bl"></div>
<p>
<ul>
<% foreach (var trackDiscount in Model) { %>
    <li style="margin-bottom: 0.7em;">
        <% if ((bool) ViewData[DynamicValues.IsSecondTrackBlock]) { %>
         <strong>�� ���������� <%= Htmls2.DiscountText((trackDiscount.Saving).MoneyString()) %> ������</strong>, ������ �������� �� ������� ����� � ������� <%= CommonTexts.TracksName%> �<%= StringUtils.AddTargetBlank(Html.CourseLink(trackDiscount.Track))%>�.
        <% } else { %>
    	<strong>������ ���������� <%= (trackDiscount.Saving).MoneyString() %> ������?</strong> ��������� �������� �� ����� �� ���������� <%= CommonTexts.TrackName3 %> �<%= StringUtils.AddTargetBlank(Html.CourseLink(trackDiscount.Track))%>�, � ������� ������ ������ ����.
        <% } %>
    </li>
<% } %>

</ul>
<%= Url.Link<TrackController>(c => c.List(ViewData["CourseTC"].ToString()), "��� ��������� � ������ ������") %>
	
</p>
</div>