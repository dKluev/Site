
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Center.ViewModels.MarketingActionsVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>

<% if(!Model.Actions.Any()){ %>
 ���� ������ ���
<% }else{ %>
<% Html.RenderPartial(Views.Shared.MarketingActions.ActionList, Model.Actions); %>
<% } %>


