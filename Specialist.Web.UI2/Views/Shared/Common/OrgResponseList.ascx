
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<OrgResponse>>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<% foreach(var OrgResponse in Model){ %>
    <% Html.RenderPartial(Specialist.Web.Const.PartialViewNames.OrgResponseBlock, OrgResponse); %>
<% } %>
