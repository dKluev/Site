<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GroupSeminar>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<% if(!Model.Any()) { %>
Пока ничего нет.
<% return;%>
<% } %>

    <script type="text/javascript">
        $(function() {
            $("a.with-confirm").unbind("click").click(function() {
            	
            		if (confirm("Хотите записаться?") == true)
            			return true;
            		else
            			return false;
            	
									
	
            });

        });
    </script>
<% var showImg = Model.Any(x => x.News != null); %>
<table class="defaultTable">
<tr>
    <% if(showImg){ %>
    <th></th>
	<% } %>
    <th>
        Тема</th>
    <th>Дата и время</th>
    <th>
        Место</th>
    <th>
        Проводит</th>
    <th> <%= Images.Main("ico_timetable2.gif")%></th>
 <th> <%= Images.Main("ico_signup2.gif")%></th>
</tr>
<% foreach (var groupSeminar in Model.OrderBy(gs => gs.Group.DateBeg)) { %>
    <% var group = groupSeminar.Group; %>
    <% var url = groupSeminar.Group.GetUrl(); %>
    <tr>
    <% if(showImg){ %>
    <td>
            <% if(groupSeminar.News != null){ %>
            <%= Images.NewsSmall(groupSeminar.News) %>
			<% } %>
        
    </td>
	<% } %>
        <td style="text-align: left;vertical-align: top; width: 50%;">
        	<b style="font-size: 14px;"><% if(url.IsEmpty()){ %>
	        	<%= group.Title %>
			<% }else { %>
				<%= H.Anchor(url,group.Title) %>
			<% } %></b>
            <% if(groupSeminar.News != null){ %>
            <br/>
            <%= groupSeminar.News.ShortDescription %>
			<% } %>
                        
		</td>
        <td><%= group.DateBeg.DefaultString() %><br />
          <span class="add-local-time"><%= group.TimeInterval %></span>
        </td>
        <td><%= Html.ComplexLink(groupSeminar.Group.Complex) %></td>
        <td><%= Html.EmployeeLink(groupSeminar.Group.Teacher) %></td>
        <td><%= Html.Calendar(groupSeminar.Group.Group_ID) %></td>        
        <td title="Записаться">
        	<% if(groupSeminar.IsMsSeminar){ %>
            <%= Url.Link<CenterController>(c => c.SeminarRegistration(group.Group_ID),
                Images.Common("bin.gif").ToString()) %> 
			<% }else { %>
            <%= Url.Link<CourseController>(c => c.AddSeminar(group.Group_ID, null),
                Images.Common("bin.gif").ToString()).Class("with-confirm") %> 
			<% } %>
            
        </td>
    </tr>
<% } %>
</table>
   