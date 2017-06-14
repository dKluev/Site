<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<UserMessage>>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<%@ Import Namespace="Microsoft.Security.Application"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web" %>
<%@ Import Namespace="Specialist.Services.Passport" %>
<%@ Import Namespace="Specialist.Entities.Passport" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<% var user = MvcApplication.Container.Resolve<AuthService>().CurrentUser ?? new User(); %>
<% foreach (var userMessage in Model) { %>
<div class="forum3_block">
        <p class="date_time">
            <span class="date"> <%=  userMessage.CreateDate.DefaultString() %></span> 
            <span class="time"> <%=  userMessage.CreateDate.ToLongTimeString() %> </span>
        </p>
	<div style="width: 100px;">
   	<%= !userMessage.CreatorUser.IsEmployee
   		? Images.UserPhoto(userMessage.CreatorUserID).Class("img").ToString()
   		: Images.Employee(userMessage.CreatorUser.Employee_TC).Class("img").Width(80).ToString() %>

    <% if(userMessage.BestTypes != null && userMessage.BestTypes.Any()){ %>

    <% foreach(var type in userMessage.BestTypes){ %>
	<% var img = Images.Main("StudentBestTypes/{0}.png".FormatWith(type)).Style("float:left;"); %>
	<% if(type == UserMessage.BestGraduate){ %>
		<%= SimpleLinks.BestGraduate(img) %>
    <% }else if(type.StartsWith(UserMessage.RealSpecialist)){ %>
		<%= SimpleLinks.RealSpecialist(img) %>
	<% }else if(type == UserMessage.ExcelMaster){ %>
		<%= SimpleLinks.ExcelMaster(img) %>
    <% } %>
    <% } %>
    <% } %>
	</div>
    <div class="text">
			 <% if((user.InRole(Role.Employee) && user.UserID == userMessage.CreatorUserID) || user.InRole(Role.Admin)){ %>
        <p class="date_time">
                    <%= Url.Link<MessageController>(m => m.Edit(
                        userMessage.UserMessageID), Images.Common("edit.gif").ToString())%>
        </p>
                <% } %>
        <p class="author">
			<%= H.strong[Html.PublicProfileLink(userMessage.CreatorUser)] %>
            </p>
            <%= Sanitizer.GetSafeHtmlFragment(userMessage.Text) %>
			
	   <% if((user.InRole(Role.Trainer) && user.UserID == userMessage.CreatorUserID) || Html.InRole(Role.ForumAdmin )){ %>
					<div style="float:right;">
						
					
                    <% if(Model.Count() == 1 || userMessage.ParentMessageID.HasValue){ %>
					&nbsp;
                    <%= Htmls.DeleteButton(Url.Message().Urls.Delete(
                        userMessage.UserMessageID))%>
                    <% } %>
                    <% if(!userMessage.ParentMessageID.HasValue){ %>

						<%= Url.Message().AnsweredToggle(
                        userMessage.UserMessageID, 
						Images.Forum(userMessage.IsAnsweredSysName)) %> 
                    <% } %>
					</div>
                    <% } %>
    </div>
</div>
<% } %>
