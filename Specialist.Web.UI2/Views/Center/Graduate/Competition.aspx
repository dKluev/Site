<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<CompetitionVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <% var user = (User)HttpContext.Current.Session["CurrentUserSessionKey"]; %>
 
    <h3>
        Условия конкурса</h3>
<%= Model.Competition.Description %>

<% if(Model.Competition.CloseDate.HasValue){ %>
    Срок проведения конкурса 
    <%= Model.Competition.OpenDate.DefaultString()%> -
    <%= Model.Competition.CloseDate.DefaultString()%>
<% } %>
<% if (Model.Competition.WinnerID.HasValue || Model.Competition.WinnerName != null) { %>
    <h3>
        Победитель</h3>
    <p>
        <% if (Model.Competition.WinnerID != null)
           { %>
        <%= Model.Competition.Winner.FullName%>
        <% } %>
        <% else
            { %>
        <%= Model.Competition.WinnerName%>
        <% } %>
        </p>
    <%= Model.Competition.Congratulation %>
    
    <% if(Model.Competition.Result != null){ %>
    <h3>
        Итоги конкурса</h3>
        <%= Model.Competition.Result %>
    <% } %>
    
    <% if ((Model.Competition.Discount.HasValue || Model.Competition.WinnerName != null) && Model.Competition.Course_TC != null)
       { %>
    
    <h3> Выигрыш</h3>
    
    <p>
        Вы выиграли курс <%= Html.CourseLink(Model.Competition.Course) %>
    </p>
    <% if(Model.IsWinner){ %>
        <%= Html.AddToCart(x => x.AddCourse(Model.Competition.Course_TC, null), true)%>
     <% } %>
    
    <% } %>
    
    
    
    
    <% return; %>
<% } %>

<% if(Model.IsJoin){ %>
    <h3>
        Спасибо, Ваша заявка на участие в конкурсе принята. Желаем удачи!</h3>

<% } %>

<% if(Model.IsJoin){ %>
    <h3>Дослать информацию</h3>
<% }else{ %>
    <h3>Принять участие</h3>
<% } %>

<%    if (HttpContext.Current.User.Identity.IsAuthenticated && user!=null)  %> 
<% { %> 

<%= Html.ValidationSummary() %>
<% using (Html.DefaultForm<CenterController>(c => c.Competition(null), Htmls.FormWithFile
       )) { %>
    <div><%= Html.HiddenFor(x => x.Competition.CompetitionID) %></div>
    <% Htmls.FormSection(" ", () => {%>
        <%= Html.ControlFor(x => x.Request) %>
        <%= Html.ControlFor(x => x.File) %>
    <% }); %>
    <%= Htmls.Submit("ok") %>
<% } %>
      <%}
      else
      {%>
   <p>
     Сервис "Заполнить заявку на конкурс" доступен только зарегистрированным пользователям.<br />
     <%= Html.ActionLink<AccountController>(c => c.LogOn(""), "Аутентификация на сайте") %>
     </p>
    <% } %> 

<% Html.RenderAction<GroupController>(c => 
       c.ForCourseTCList(Model.Competition.CourseTCList,false,0)); %>
</asp:Content>
