<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Helpers"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.CommonCourseListVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Filter" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%--
<% var priceText = 
		"Указаны цены на курсы для частных лиц с учетом специальных предложений."; %>
<table class="table">
	<tr class="thead">
		<th rowspan="2" class="table_c_tl">
			&nbsp;
		</th>
		<% if(Model.HasNew){ %>
		<th rowspan="3"></th>
		<% } %>
		<th rowspan="3" style="width: 48.4%;">
			Курс обучения
		</th>
		<th rowspan="3" class="subtitle">
			Ак.ч
		</th>
	
		<th colspan="2" rowspan="2" class="subtitle">
			Ближ.&nbsp;группа
			<br />
			<span style="font-size:10px">
			Кликните, <br />чтобы увидеть все
			</span>
		</th>
			<th colspan="3">
			Цена&nbsp;от<div 
			title="<%= priceText %>"
			class='div-pointer'>*</div>
		</th>
		
		
		<th rowspan="3" class="last_td">
		    <%= Images.Main("ico_signup2.gif")%>
		</th>
		<th rowspan="2" class="table_c_tr">
			&nbsp;
		</th>
	</tr>
	<tr>
		<th colspan="2">
			<%= H.Anchor(SimplePages.FullUrls.Fulltime, "Очное обучение")
				.Style("color:black") %>
		</th>
		<th rowspan="2">
			<%= H.Anchor(SimplePages.FullUrls.Webinar, "Вебинар").Style("color:black") %>
		</th>
	</tr>
	<tr>
		<th class="table_c_bl">
			&nbsp;
		</th>
			<th style="font-size:10px"><%= H.Anchor(SimplePages.FullUrls.Fulltime, "Очная")
				.Style("color:black") %>
		</th>
		<th style="font-size:10px">
			<%= SimpleLinks.Webinar("Вебинар").Style("color:black") %>
		</th>
		<th style="font-size:10px">Частные<br />лица
		</th>
		<th style="font-size:10px"> Органи-<br />зации
		</th>
	
		<th class="table_c_br">
			&nbsp;
		</th>
	</tr>

   <% foreach (var courseListItemVM in Model.GetFilteredByPriceCourses()) { %>
   <% var course = courseListItemVM.Course; %>
	<tr class="<%= course.IsNew ? "new-course" : (course.IsTrackBool && !course.IsSpecialTrack
	 ? "complex" : "") %>">
		
		<td class="table_c_tl">&nbsp;</td>
		<% if(Model.HasNew){ %>
		<td>  <%= course.IsNew ? Images.Main("red_new.png") : null %></td>
		<% } %>
		<td class="td_course" title="Хотите узнать больше? Звоните по телефону <%= CommonTexts.Phone %>!"> 
		<% if(course.IsTrackBool && !course.IsSpecialTrack){ %>
		<strong><%= Html.CourseLink(course) %> </strong>
		<% }else{ %>
		<%= Html.CourseLink(course) %> 

		<% } %>
		</td>

		<td><%= course.BaseHours.ToIntString() %></td>
	
     
		
		   <% if (courseListItemVM.HasNearestGroupOrWebinar) { %>
		<td>  
            <% if (courseListItemVM.NearestGroup != null) { %>
              
				  <%= Html.ActionLinkEx<GroupController>(
                 gc => gc.List(new GroupFilter { CourseTC = course.CourseTCOrFirst }, 1), 
                				 courseListItemVM.NearestDate.DefaultString()) %>
	    	    <% if(!course.IsTrackBool) { %>
					<%= Htmls2.Discount(courseListItemVM.NearestGroup) %>
				<% } %> 
				<% if(Model.CityTC == null) { %> 
					<br />
						<%= courseListItemVM.NearestGroupCity %> 
				<% } %> 
			<% } %> 
        </td>
		<td>
			<% if(courseListItemVM.NearestWebinar != null){ %>
				  <%= Html.ActionLinkEx<GroupController>(
                 gc => gc.List(new GroupFilter { CourseTC = course.CourseTCOrFirst }, 1), 
                				 courseListItemVM.NearestWebinar.DateBeg.DefaultString()) %>
				
			<% } %>
		</td>
            <% }else { %> 
			<td colspan="2">
				<% if(!courseListItemVM.Course.IsTrackBool){ %>
						<%= Htmls2.AskTimetable(course.Course_TC) %>
				<% } %> 
			</td>
			<% } %> 
			<td>
            <%= courseListItemVM.MinFulltimePrice.MoneyString() %>
		</td>
		<td>
            <%= courseListItemVM.MinOrgPrice.MoneyString() %>
		</td>
		<td>
            <%= courseListItemVM.WebinarPrice.MoneyString() %>
        </td>

		<td  class="last_td"> <%= Html.AddToCart(course) %> </td>
		<td class="table_c_tr">&nbsp;</td>

	</tr>
    <% } %>	
</table>

<p>*<%= priceText %></p>
--%>
