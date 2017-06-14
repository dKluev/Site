<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.DiplomProgramListVM>" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<table class="table">
<% if (Model.List.Any()) { %>
    <%= Html.Partial(Views.Shared.CourseList.AllCourseListPartHead, Model.List.First().Item2)  %>
<% } %>
<% foreach (var tuple in Model.List) { %>
    <% Html.RenderPartial(Views.Shared.CourseList.AllCourseListPart, tuple.Item2); %>
<% } %>
</table>

<p>*<%= Specialist.Web.Common.Html.Htmls.HtmlBlock(HtmlBlocks.PriceText) %></p>