<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CompetitionsVM>" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<% var user = (User)HttpContext.Current.Session["CurrentUserSessionKey"]; %>
<% if(Model.Competitions.IsEmpty()){ %>
<%    if (HttpContext.Current.User.Identity.IsAuthenticated && user!=null)  %> 
<% { %> 
    <p>Ничего нет</p>
      <%}
      else
      {%>
   <p>
     Сервис "Мои конкурсы" доступен только зарегистрированным пользователям.<br />
     <%= Html.ActionLink<AccountController>(c => c.LogOn(""), "Аутентификация на сайте") %>
     </p>
    <% } %> 

    <% return; %>
<% } %>
<%-- 
<table class="fullCourse">
    <tr>
        <th>
            Конкурс
        </th>
        <th>
            Условия
        </th>
    </tr>
<% foreach(var competition in Model.Competitions){ %>

<tr>
    <td> <%= competition.Name %></td>
    <td> <%= competition.Description %> 
        <p style="text-align: right;">
             <%= Html.ActionLink<ProfileController>(c => 
                c.Competition(competition.CompetitionID), 
                competition.WinnerID.HasValue ? "Результаты конкурса" 
                : (Model.MyCompetitions ? "Отправить еще одну заявку" 
                    : "Принять участие")) %>
        </p>
    </td>
</tr>
<% } %>
</table> --%>


 <% foreach (var competition in Model.Competitions) { %>
<div class="block_action">
    <div class="action_img">
        <%= Images.Competitions(competition)%>
    </div>
    <div class="action_text">
        <h3>
            <%= competition.Name %>
        </h3>
        <div class="over_p">
            <p>
                <%= competition.Description %>
            </p>
            <p class="all">
             <%= Html.ActionLink<CenterController>(c => 
                c.Competition(competition.CompetitionID),
                competition.WinnerID.HasValue || competition.WinnerName != null ? "Результаты конкурса" 
                : (Model.MyCompetitions ? "Отправить еще одну заявку" 
                    : "Принять участие")) %>
             </p>
        </div>
    </div>
</div>
<% } %>

