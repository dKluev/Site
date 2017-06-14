
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LogOnStateVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<p>
<% if (Model.User != null) { %>
   <span class="enter">
<%--        <%= Images.Main("profile.gif").Style("vertical-align:middle; width:20px; margin-top:-3px; margin-right:4px;") %>--%>
        <%= Html.ActionLink<ProfileController>(
               c => c.Details(), Model.User.ShortFullName) %>

    </span>
    <span class="reg">
        <%= Html.ActionLink<AccountController>(c => c.LogOff(null), "Выйти") %>
    </span>
<% }else{ %>
   <span class="enter">
<%--        <%= Images.Main("profile.gif").Style("vertical-align:middle; width:20px; margin-top:-3px; margin-right:4px;") %>--%>
        <%= Html.ActionLink<AccountController>(c => c.LogOn((string)null), "Войти") %>
    </span>
    <span class="reg">
        <%= Html.ActionLink<ProfileController>(c => c.Register(null,null,null), "Регистрация") %>
    </span>
<% } %>
</p>