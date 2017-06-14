<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CourseVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Site" %>

<table width="100%">
<tr>
<% if(Model.Trainers.Any()){ %>
<td width="55%"> <h2 class="h2_block" style="margin-left:45px;"> Преподаватели курса </h2> </td>
<% } %>
<td>
<% if(Model.Responses.Any()){%>
<h2 class="h2_block" style="margin-left:20px;">
     Отзывы 
	<% if(Model.ResponseTotalCount > 0){%>
    о курсе 
    <% }else{ %>
    о Центре
    <% } %>
</h2>
<% } %>
</td>
</tr>
<tr>

<% if(Model.Trainers.Any()){ %>

	<td style="vertical-align:top;">
	<%= CommonSiteHtmls.Carousel(Model.Trainers.Select(x => 
		   Html.Site().LecturerTwo(x, Model.Course.UrlName))) %>
	</td>
<% } %>

	<% if(Model.Responses.Any() || Model.SuccessStories.Any()){%>

	<td style="padding-left:20px;vertical-align:top;">
	<% foreach (var response in Model.Responses) {
	    Html.RenderPartial(PartialViewNames.ResponseBlock, 
			   response.FluentUpdate(x => x.Course_TC = null)
			   .FluentUpdate(x => x.Employee_TC = null)
			   .FluentUpdate(x => x.Description 
				   = Htmls.GetShortWithShowOnClick(x.Description, x.ResponseID)));
	} %>
	<% if(Model.Responses.Count < Model.ResponseTotalCount){%>
		<br />
		<% var totalResponseText = "Все отзывы по курсу" + (Model.ResponseTotalCount > 10
		   	? " (" + Model.ResponseTotalCount + ")" : ""); %>
		<%= Html.ActionLink<CenterController>(c => c.CourseResponses(Model.Course.UrlName,1),
			totalResponseText)%>
    <% } %>
        
        <% if(Model.SuccessStories.Any()){ %>
        <h2 class="h2_block"> Истории успеха </h2>
        <%= Html.Site().SuccessStories(Model.SuccessStories) %>

    <% } %>
	</td>
	<% } %>
</tr>
</table>

	   



