<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<User>>" %>

<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<% foreach(var passportUser in Model){ %>
    <% Html.RenderPartial(Specialist.Web.Const.PartialViewNames.SearchResultUserBlock, passportUser); %>
<% } %>

