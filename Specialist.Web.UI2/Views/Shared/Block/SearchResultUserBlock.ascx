
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<User>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Utils.Logic" %>

<p>

<strong>UserID:</strong> 
<%= Model.UserID%><br />
<strong>Имя:</strong> 
<%= Model.FirstName %><br />
<strong>Отчество:</strong> 
<%= Model.SecondName %><br />
<strong>Фамилия:</strong>
<%= Model.LastName %><br />
<strong>E-mail:</strong>
<%= Model.Email.Trim() %><br />
<strong>Пароль:</strong>
"<%= Model.Password %>"<br />
<strong>Статус:</strong>
<%= Model.Roles %><br />
<% if (true) { %>
    <strong>Промокод регистрации:</strong>
<%= CouponUtils.PromoCode(Model.UserID) %><br />

    <% } %>
</p>
<div class="response">
</div> 

