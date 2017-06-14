<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<LetterVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p><h2>Письмо консультанту</h2></p>

   <% var user = (User)HttpContext.Current.Session["CurrentUserSessionKey"]; %>
   <% if (HttpContext.Current.User.Identity.IsAuthenticated && user!=null && user.IsStudent)  %> 
   <% { %> 

    <%= Htmls.BorderBegin(Model.Result)%>
        <%= Html.ValidationSummary()%>
    <% using (Html.BeginForm())
       { %>
     <h3>Основная информация</h3>
     <table class="table_form">
     <tr>
     <td class="name">* Имя </td>
     <td class="field">
     <%= Html.TextBox("YourName", user.FirstName)%></td>
    </tr>
    <tr>
     <td class="name">* Отчество </td>
     <td class="field">
     <%= Html.TextBox("YourPatronymic", user.SecondName)%></td>
    </tr>
    <tr>
     <td class="name">* Фамилия </td>
     <td class="field">
     <%= Html.TextBox("YourSurname", user.LastName)%></td>
    </tr>
     <tr>
     <td class="name">* E-mail </td>
     <td class="field">
     <%= Html.TextBox("YourEmail", user.Email, new { @class = "course-name", size = 55 })%></td>
    </tr>
    </table>
    <h3>Текст письма</h3>
     <%= Html.TextArea("YourText","", 25, 80, 50)%>
    <div>Разделы помеченные * обязательны для заполнения</div>
	<%= Htmls.Submit("send") %>
    <% } %>
    <%= Htmls.BorderEnd%>
      <% try
         {
             if (Model.isStartSearch)
             { %>
       <script language="javascript">
           window.location.href = "/client/lettersent";
       </script>
           <% }
         }
         catch { } %>
     <%}
      else
      {%>
   <p>
     Сервис "Послать письмо консультанту" доступен только зарегистрированным пользователям-выпускникам. <br />
       <%= Html.ActionLink<AccountController>(c => c.LogOn(""), "Аутентификация на сайте") %>
     </p>
    <% } %> 
</asp:Content>
