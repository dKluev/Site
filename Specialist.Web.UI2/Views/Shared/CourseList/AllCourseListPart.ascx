<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AllCourseListVM>" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Filter" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.FluentHtml.Tags" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Root.Catalog.Views" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<% if(!Model.Common.HasCourses) return; %>
<% var isPhotoshop = Request.Url.AbsolutePath.ToLower() == "/product/photoshop-courses"; %>
<% var isTrackPage = Model.IsTrack; %>
<% var isIntraExtra = Model.IsIntraExtra; %>
<% var showUnlimited = Model.ShowUnlimited; %>
<% var compact = Model.IsDiplomPage || Model.IsTrainingProgramsPage; %>
<% var showTrackCourseDiscount = false; %>
		
    <% var tdtl = H.td.Class("table_c_tl")[H.Raw("&nbsp;")]; %>
    <% var tdtr = H.td.Class("table_c_tr")[H.Raw("&nbsp;")]; %>
<% if (!compact) { %>
<% if(!isTrackPage){ %>
    <div id="section-<%= Linguistics.UrlTranslite(Model.EntityName) %>"></div>
	<h2 class="h2_block" style="margin-bottom: 5px;margin-top: 40px;font-size:18px;">
		 <%= Model.EntityType == typeof (Product) ? "Расписание по курсам" : "" %> 
		 <%= Model.EntityName %>
	</h2>
<% }else{ %>
<br />
		<br />
		<br />
		<br />
<% } %>

<% if (Model.Common.OnlyVideo) { %>
    <% Html.RenderPartial(Views.Shared.CourseList.VideoCourseList, Model); %>
    <% return; %>
<% } %>

<p class="signs1">
	 <%= Images.Main("ico_complex.gif")%>
    - <%= H.Anchor(SimplePages.FullUrls.Tracks,CommonTexts.TrackName) %> 
    <%= Images.Main("ico_signup.gif")%>
    - Записаться
</p>
<table class="table">
    <%= Html.Partial(Views.Shared.CourseList.AllCourseListPartHead, Model) %>
<% }else{ %>	
    <%= H.Row(tdtl, H.td[Model.EntityName].Style("text-align: left; margin: 1px; font-weight: bold; font-size: 16px; color: #000066;").Colspan(10), tdtr) %>
<% } %>	
    <% var i = 0; %>
    <% var courses = Model.Common.GetFilteredByPriceCourses(); %>
   <% foreach (var courseListItemVM in courses) { %>
   <% var course = courseListItemVM.Course; %>
    <% var showLastCourseDiscount = 
            course.Course_TC != CourseTC.DpCons 
            && !Model.IsTrackDiplom && i == courses.Count - 1 && isTrackPage; %>
    <% var isIntraExtraRow = courseListItemVM.Course.IsIntraExtraTrack ; %>
    <% var lastCourseDiscount = Model.GetTrackLastCourseSave(lastCourse: isTrackPage && showLastCourseDiscount); %>
    <% var present = lastCourseDiscount >= 100; %>
       <% i++; %>

	<tr class="<%= course.IsTrackBool ? "complex" : "" %>">
		
        <%= tdtl %>
		<% if(Model.Common.HasIcons){ %>
		<td>
			  <%= course.Icon.GetOrDefault(x => Images.Main(x.Item1 + ".gif").Title(x.Item2) )%>
		</td>
		<% } %>
		<td class="td_course" title="Хотите узнать больше? Звоните по телефону <%= CommonTexts.Phone %>!"> 
		<% if(course.IsTrackBool && !course.IsSpecialTrack){ %>
			
    		<% var count = Model.TrackCounts.GetValueOrDefault(course.Course_TC); %>
    		<% if(isTrackPage){ %>
        		<strong>
        			<%= course.GetName() %> <br/>
        			Включает в себя <%= count %> <%= Linguistics.Plural("курс", count) %>:
        		</strong>

    		<% }else{ %>
        		<strong> <%= Html.CourseLink(course) %> </strong> 
                (<%= course.IsSchool ? "" : ((course.IsDiplom ? "Дипломная программа" : CommonTexts.TrackName) + " - ") %><%= count %> <%= Linguistics.Plural("курс", count) %>)
        		<% if(!isIntraExtraRow && !course.HideTrackDiscount){ %>
                    <% var trackDiscount = EntityUtils.RoundDiscount(
                           Model.GetLastCourseDiscount(course.Course_TC)); %>
                    <% var text = trackDiscount >= 100 ? Htmls2.DiscountText(CommonTexts.OneFreeCourse) :
                           (trackDiscount > 0 ? CommonTexts.TrackDiscount.FormatWith(
                           Htmls2.DiscountText(trackDiscount + "%", true), Linguistics.GetOrdinalName(count)): ""); %>
                    <b><%= text %></b>
        		<% } %>
    		<% } %>
		<% }else{ %>
		<%= Html.CourseLink(course) %> 
            <% if(lastCourseDiscount > 0 && !present){ %>
            <%= Htmls2.DiscountText(" - скидка {0}%".FormatWith(EntityUtils.RoundDiscount(lastCourseDiscount)),true) %><%= H.Div("div-pointer")["**"].Title(CommonTexts.TrackCourseDiscountText) %>
            <% showTrackCourseDiscount = true; %>
    		<% } %>
		<% } %>
		</td>

		<td><%= ((isIntraExtra && isIntraExtraRow) || !isIntraExtra ? course.BaseHourCalc : course.FullHours).ToIntString() %></td>
     
        <% var hideGroups = isTrackPage && !course.IsTrackBool; %>
	       <% if (hideGroups) { %>	
			<td> </td> <td></td>
		   <% } else if (courseListItemVM.HasNearestGroupOrWebinar) { %>
		   <% if (!Model.HideIntraGroup) { %>
		<td>  
            <% if (courseListItemVM.NearestGroup != null) { %>
              
				  <%= Html.ActionLinkEx<GroupController>(
                 gc => gc.List(new GroupFilter { CourseTC = course.CourseTCOrFirst }, 1), 
                				 courseListItemVM.NearestDate.DefaultString()) %>
	    	    <% if(!course.IsTrackBool && !isTrackPage) { %>
					<%= Htmls2.Discount(courseListItemVM.NearestGroup) %>
				<% } %> 
			<% } %> 
        </td>
			<% } %> 
		<td>
			<% if(courseListItemVM.NearestWebinar != null){ %>
				  <%= Html.ActionLinkEx<GroupController>(
                 gc => gc.List(new GroupFilter { CourseTC = course.CourseTCOrFirst }, 1), 
                				 courseListItemVM.NearestWebinar.DateBeg.DefaultString()) %>
				
			<% } %>
		</td>
            <% }else { %> 
			<td colspan="2">
				<%= Htmls2.AskTimetable(course.Course_TC) %>
			</td>
			<% } %> 
			<% if(courseListItemVM.Course.IsTrackBool){ %>
		<% var price1 = H.td[courseListItemVM.TrackPrice(PriceTypes.PrivatePersonWeekend, courseListItemVM.MinFulltimePrice.Item1)];
		   var price2 = H.td[courseListItemVM.TrackPrice(PriceTypes.Corporate, courseListItemVM.MinOrgPrice)];
		   var price3 = H.td[courseListItemVM.TrackPrice(courseListItemVM.GetDistancePriceType(), courseListItemVM.DistancePrice.Item1)];
		   var price4 = H.td[courseListItemVM.TrackPrice(courseListItemVM.GetDistanceOrgPriceType(), courseListItemVM.DistanceOrgPrice)]; %>
            <%= Model.IsDiplomPage ? H.l(price3,price4,price1, price2) : H.l(price1,price2,price3,price4) %>
            <% }else { %> 
                <% if(present){ %>
                <td colspan="4"><span class="discount_color"><%= CommonTexts.FreeCourse %></span></td>

                <% }else { %> 
        		<td> <%= CourseListView.ShowCoursePrice(courseListItemVM.MinFulltimePrice, 
                   Model.GetTrackLastCourseSave(lastCourse: isTrackPage && showLastCourseDiscount)) %> </td>
        		<td> <%= CourseListView.ShowCoursePrice(PriceUtils.OnePrice(courseListItemVM.MinOrgPrice), 
                       Model.GetTrackLastCourseSave(PriceTypes.Corporate,isTrackPage && showLastCourseDiscount)) %> </td>
        		<td> <%= CourseListView.ShowCoursePrice(courseListItemVM.DistancePrice, 
               Model.GetTrackLastCourseSave(PriceTypes.Webinar,isTrackPage && showLastCourseDiscount)) %> </td>
        		<td> <%= CourseListView.ShowCoursePrice(PriceUtils.OnePrice(courseListItemVM.DistanceOrgPrice), 
               Model.GetTrackLastCourseSave(PriceTypes.WebinarOrg,isTrackPage && showLastCourseDiscount)) %> </td>

                <% } %> 
			<% } %> 
        <% if (showUnlimited) { %>
        <td>
            
        <% if (!courseListItemVM.Course.IsTrackBool) { %>
            <% var unlimitPrice = courseListItemVM.GetPrice(PriceTypes.Unlimited); %>
           <%= unlimitPrice.HasValue ? SimpleLinks.Unlimited(unlimitPrice.MoneyString()) : 
           (Model.CourseWithUnlimit.Contains(courseListItemVM.Course.Course_TC) 
           ? SimpleLinks.Unlimited("Бесплатно") : null) %> 
        <% } %>
        </td>
        <% } %>

		<td  class="last_td">
		    <% if(!isIntraExtra || isIntraExtraRow){ %>
                <% if (courseListItemVM.NearestGroup == null 
    				   || courseListItemVM.Course.IsTrackBool || !isPhotoshop) { %>
    				 <%= Html.AddToCart(course) %> 
                <% }else { %> 
    				<%= Html.AddToCart(x => x.AddGroup(courseListItemVM.NearestGroup.Group_ID)) %>
    			<% } %> 
			<% } %> 
		</td>
        <%= tdtr %>

	</tr>
    <% } %>	
<% if (!compact) { %>
</table>
<% } %>	

<%= Model.EntityUrl != null ? H.Row(tdtl, H.td[H.Anchor(Model.EntityUrl, "Все курсы по этому направлению")].Style("text-align: left; margin: 1px; font-weight: bold; font-size: 12px; color: #000066;").Colspan(10), tdtr) : null %>
<% if (!compact) { %>
    <p>*<%= Specialist.Web.Common.Html.Htmls.HtmlBlock(HtmlBlocks.PriceText) %></p>
<% if(showTrackCourseDiscount) { %>
    <p>**<%= CommonTexts.TrackCourseDiscountText %></p>
<% } %>	
<% } %>	
