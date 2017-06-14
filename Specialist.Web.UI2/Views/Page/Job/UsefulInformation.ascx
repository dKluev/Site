<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<UsefulInformation>>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

 <% foreach(var usefulInformation in Model){ %>
<div class="block_action">
    <div class="action_img">
        <%= Images.UsefulInfomation(usefulInformation)%>
    </div>
    <div class="action_text">
        <h4>
            <%= Html.ActionLink<CenterController>(c =>
                        c.UsefulInformation(usefulInformation.UrlName), usefulInformation.Name)%></h4>
        <div class="over_p">
            <p>
                <%= StringUtils.GetFirstParagraph(usefulInformation.Description)%></p>
            <p class="all">
                <%= Html.ActionLink<CenterController>(c =>
                            c.UsefulInformation(usefulInformation.UrlName), "Подробнее")%>
        </p>
        </div>
    </div>
</div>
<% } %>