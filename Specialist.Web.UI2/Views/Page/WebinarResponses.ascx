
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Response>>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<h3 class="h3_tab">Отзывы слушателей</h3>

<% foreach(var response in Model){ %>
    <% Html.RenderPartial(Specialist.Web.Const.PartialViewNames.ResponseBlock, response); %>
<% } %>
