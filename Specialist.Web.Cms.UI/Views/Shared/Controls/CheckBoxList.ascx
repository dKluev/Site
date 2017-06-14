<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Cms.Core.ViewModel.ComboBoxVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Html"%>

<% foreach (var selectListItem in Model.Source) { %>
    <%= HtmlControls.CheckBox(Model.PropertyName, selectListItem.Selected, 
        selectListItem.Value) %> <%= selectListItem.Text %> <br />
<% } %>

