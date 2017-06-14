<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ComboBoxVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>

<%= Html.DropDownList( Model.PropertyName, Model.Source ) %>
                