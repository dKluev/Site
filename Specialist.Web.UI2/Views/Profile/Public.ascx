
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Profile.PublicProfileVM>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<div class="page_profile_l">
		<%= Images.UserPhoto(Model.User.UserID) %>
    		<% if (Model.IsOwner) { %>
            <%= H.p[ Url.Profile().EditProfile("Редактировать профиль")] %>

    		<% } %>
	</div>
	<div class="page_profile_r">
		<div class="page_profile_r_ie">
		    <div>
			<% if (Model.User.UserAddresses.Any()) { %>
			<% HUtil.NotNull(Model.User.UserAddresses.First().Country
						.GetOrDefault(x => x.CountryName), x => { %>
			<p>
				<strong>Страна:</strong>
				<%= x %></p>
			<% }); %>
			<% HUtil.NotNull(Model.User.UserAddresses.First().City, x => { %>
			<p>
				<strong>Город:</strong>
				<%: x %>
			</p>
			<% }); %>
			<% } %>
		        
		    </div>

			<% if (Model.Competitions.Any()) { %>
			<div class="attention2">
				<p>
					<strong>Победитель конкурсов</strong>
					<% foreach (var competition in Model.Competitions) { %>
					<%= Html.ActionLink<CenterController>(c => 
						c.Competition(competition.CompetitionID), competition.Name) %>
					<% } %>
				</p>
			</div>
			<% } %>
		
		<% if (Model.Socials.Any()) { %>
		<%= Htmls2.BorderBegin("Средства внешних интернет-коммуникаций", notContent:true)%>
		<div class="tab_content2">
			<% Html.RenderPartial(Views.Profile.ContactList, Model.Socials); %>
		</div>
		<%= Htmls2.BorderEnd() %>
		<% } %>
		<% if (Model.SuccessStory != null) { %>
		<div class="registr_form">
			<%= Htmls2.BorderBegin("Моя история", notContent:true)%>
			<div class="my_history tab_content">
				<p class="history">
					<%: StringUtils.GetShortText(Model.SuccessStory.Description) %>
				</p>
				<p class="read_full">
					<%= Html.ActionLink<ClientController>(
                    c => c.SuccessStory(Model.SuccessStory.SuccessStoryID), 
                            "читать далее") %></p>
			</div>
			<%= Htmls2.BorderEnd() %>
		</div>
		<% } %>
		<% if (Model.IsBest) { %>
			<%= SimpleLinks.BestGraduate(Images.Main("bestgraduate.png")) %>
		<% } %>
		<% if (Model.IsExcelMaster) { %>
			<%= SimpleLinks.ExcelMaster(Images.Main("excelmaster.png")) %>
		<% } %>
		<% var card = Model.User.Student.GetOrDefault(x => x.Card); %>

		<% if (card != null) { %>
			<%= SimpleLinks.RealSpecialist(Images.StudentClabCard(card.ClabCardColor_TC)) %> 
		<% } %>

		<% if (!Model.User.HideCourses) { %>
			<% if (Model.User.Student.GetOrDefault(x => x.EndedGroups.Any() )) { %>
		  <h3>Законченные курсы</h3>

        <table class="defaultTable">
            <tr>
                <th>
                    Курс
                </th>
            </tr>

        <% Model.User.Student.EndedGroups.ForEach((group, i) => { %>
            <tr>
			<td class="td_course"> <%= i+1 %>. 
                <%=Html.CourseLinkByGroup(group)%>
			</td>
               
            </tr>
		<% }); %>
        </table>
		<% } %>

		<% if (Model.Tests.Any()) { %>
		  <h3>Сданные тесты</h3>

	        <table class="defaultTable">
	            <tr> <th> Тест </th> </tr>
		        <% Model.Tests.ForEach((test, i) => { %>
		            <tr>
					<td class="td_course"><%= i+1 %>. <%=Url.TestActiveOnlyLink(test)%></td>
		            </tr>
		        <% }); %>
	        </table>
		<% } %>


		<% } %>

		</div>
		</div>
