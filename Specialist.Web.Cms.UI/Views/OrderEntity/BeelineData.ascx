<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Cms.Core.ViewModel.JsTableVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Const" %>

<strong>������������������:</strong> <%= ViewData["RegCount"] %> <br /> <br />
<% Html.RenderPartial(PartialViewNames.JsTableControl); %>