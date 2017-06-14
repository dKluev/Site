<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Cms.Core.ViewModel.JsTableVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Const" %>

<strong>Зарегистрировалось:</strong> <%= ViewData["RegCount"] %> <br /> <br />
<% Html.RenderPartial(PartialViewNames.JsTableControl); %>