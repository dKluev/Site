<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Passport.ViewModel.LogOnVM>" %>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
	<div id="content" class="longlist">

	<h2>Авторизация</h2>

<p>Если Вы уже зарегистрированы на нашем сайте, введите ваш e-mail и пароль:</p>

<% using(Html.BeginForm<AccountController>(c => c.LogOn((LogOnVM)null))){ %>
<%= Html.ValidationSummary() %>
<p>E-mail:<br /><input type="email" name="Email" required="required" placeholder="E-mail" class="longinput" /></p>
<p>Пароль:<br />
<input type="password" placeholder="Пароль" name="Password" required="required" class="longinput" /></p>
<p><input type="checkbox" name="Remeber" value="true" checked="checked" />Запомнить </p>

<%= Html.HiddenFor(x => x.ReturnUrl) %>
        <p>
            
    <%= Url.Link<ProfileController>(c => c.RestorePassword((string)null), "Напомнить пароль") %>
        </p>
<p><input type="submit" value="Войти" /></p>
<% } %>

</div>

</asp:Content>
