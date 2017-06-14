<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.DiplomProgramListVM>" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<% Html.RenderPartial(Views.Page.CompactTableList, Model); %>
