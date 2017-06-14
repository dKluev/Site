<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"  
    Inherits="System.Web.Mvc.ViewPage<SearchUserVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<p><h2>Поиск зарегистрированных пользователей</h2></p>

  
    <%= Htmls.BorderBegin(Model.Result)%>
    <% using (Html.BeginForm())
       { %>
     <table class="table_form">
     <tr>
     <td class="name"> UserID </td>
     <td class="field">
     <%= Html.TextBox("YourUserID")%></td>
    </tr>
     <tr>
     <td class="name"> Имя </td>
     <td class="field">
     <%= Html.TextBox("YourName")%></td>
    </tr>
    <tr>
     <td class="name"> Отчество </td>
     <td class="field">
     <%= Html.TextBox("YourPatronymic")%></td>
    </tr>
    <tr>
     <td class="name"> Фамилия </td>
     <td class="field">
     <%= Html.TextBox("YourSurname")%></td>
    </tr>
     <tr>
     <td class="name"> E-mail </td>
     <td class="field">
     <%= Html.TextBox("YourEmail", "", new { @class = "course-name", size = 55 })%></td>
    </tr>
    </table>
	<%= Htmls.Submit("search") %>
    <% } %>
    <%= Htmls.BorderEnd%>

    <% if (Model.PassportUser != null) { %>
            <p>
        <% Html.RenderPartial(PartialViewNames.SearchResultUser, Model.PassportUser);%>
        </p>
    <% } %>
   
</asp:Content>
