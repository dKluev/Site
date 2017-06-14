<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Profile.ProfileVM>" %>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
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
	<h2>Мой профиль</h2>

<p><b><%= Model.User.FullName %></b> </p>
        <% var student = Model.User.Student ?? new Student(); %>
<% Html.RenderPartial(Views.Profile.ClabCard, student); %>
<%= MHtmls.MainList( _.List(Url.Profile().Subscribes("Мои подписки"))) %>
<% if(Model.User.IsStudent){ %>
    <h2>Мое Обучение</h2>
<%= MHtmls.MainList( _.List(Url.Profile().Learning("Мои курсы"))) %>
<% } %>
<% if(student.Card != null){ %>
    <h2>Мой Специалист</h2>
<%= MHtmls.MainList( _.List(Url.RealSpecialist())) %>
<% } %>
<%= MHtmls.MainList( _.List(Url.Message().Forum("Форум"))) %>
    <h2>Мой менеджер</h2>
<%= MHtmls.MainList( _.List(Html.ManagerLink(Model.Manager))) %>
	

    <h2>Выход из системы</h2>
<%= MHtmls.MainList( _.List(Url.Link<AccountController>(c => c.LogOff(null), "Разлогиниться"))) %>

</div>

</asp:Content>
