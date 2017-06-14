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
         <strong>Вы сэкономите <%= Htmls2.DiscountText((trackDiscount.Saving).MoneyString()) %> рублей</strong>, пройдя обучение по данному курсу в составе <%= CommonTexts.TracksName%> «<%= StringUtils.AddTargetBlank(Html.CourseLink(trackDiscount.Track))%>».
        <% } else { %>
    	<strong>Хотите сэкономить <%= (trackDiscount.Saving).MoneyString() %> рублей?</strong> Выбирайте обучение по одной из популярных <%= CommonTexts.TrackName3 %> «<%= StringUtils.AddTargetBlank(Html.CourseLink(trackDiscount.Track))%>», в которую входит данный курс.
        <% } %>
    </li>
<% } %>

</ul>
<%= Url.Link<TrackController>(c => c.List(ViewData["CourseTC"].ToString()), "Все программы с данным курсом") %>
	
</p>
</div>