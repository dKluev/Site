<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LogOnStateVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel" %>

<% if (Model.User != null) { %>
        <%= Model.User.FullName %> | 
        <%= Html.ActionLink<AccountController>(c => c.LogOff(null), "[Выйти]") %>
<% } %>
