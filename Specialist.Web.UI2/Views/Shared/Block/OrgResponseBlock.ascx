<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.OrgResponse>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<div class="block_action">
	<div class="action_img">
		<%= Images.Organization(Model.Organization).Size(140, null) %>
	</div>
	<div class="action_text">
		<p class="name">
			<strong>Компания:</strong>
			<%= Html.ActionLink<ClientController>(c => c.OrgResponse(
                            Model.OrgResponseID), Model.Organization.Name)%></p>
		<div class="response2">
			<%= StringUtils.GetShortText(Model.Description) %>
		</div>
		<p class="details">
		<%= Url.Client().OrgResponse(Model.OrgResponseID, "читать дальше »")%>
		</p>
	</div>
</div>
