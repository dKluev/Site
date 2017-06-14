<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<RequestForWebinarVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

    <p><h3><%= Model.Result %></h3></p>
    <%= Html.ValidationSummary()%>
    <% using (Html.BeginForm())
       { %>
    <table>
    <tr>
    <td>ФИО:<br />
    <%= Html.TextBox("YourName", "",  new { @class = "course-name", size = 40 })%></td>
    </tr><tr>
     <td>Телефон (или e-mail):<br />
     <%= Html.TextBox("YourEmail", "", new { @class = "course-name", size = 40 })%></td>
     </tr><tr>
     <td>Курс, пожелания к заказу:<br />
     <%= Html.TextArea("YourText","", 5, 40, 50)%></td>
     </tr><tr>
    <td>Все поля обязательны для заполнения</td>
    </tr>
    </table>
    <p><input type="submit" value="Отправить" /></p>
    <% } %>
