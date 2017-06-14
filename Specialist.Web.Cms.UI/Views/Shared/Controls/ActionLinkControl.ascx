<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ExtraControlVM>" %>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core.Controls"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<% var extraControl = Model.ExtraControl.As<ActionLinkControl>(); %>
<%= HtmlControls.ImgAnchor(Url.Action(extraControl.Action, 
    extraControl.Controller, extraControl.GetRouteValues(Model.Entity)), 
    "/Content/Image/" + extraControl.ImageName + ".png", extraControl.DisplayName ) %>