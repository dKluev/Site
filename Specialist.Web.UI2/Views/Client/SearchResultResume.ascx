<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Resume>>" %>

<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<% foreach(var vacancy in Model){ %>
    <% Html.RenderPartial(Specialist.Web.Const.PartialViewNames.SearchResultResumeBlock, vacancy); %>
<% } %>
