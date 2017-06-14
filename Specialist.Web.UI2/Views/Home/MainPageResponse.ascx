<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Tuple<Response, OrgResponse>>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
	<div class="news_in_main"> 
<div class="h2_over_blue png">
		<h2> <%= SimpleLinks.Responses("Отзывы выпускников") %> </h2>
	</div>
	<div class="block anons_news" style="padding: 15px">
		<%= Images.Common("graduate-{0}.jpg".FormatWith(Model.Item1.IsMan ? "man" : "woman"))
			.Style("padding-top:10px").FloatLeft() %>
			<%= Html.Site().Response(Model.Item1) %>
		<br />
		<strong>
			<%= SimpleLinks.Responses("Все отзывы выпускников").Class("block1") %>
		</strong>
	</div>
</div>
<% if(Model.Item2 != null){ %>
<div class="news_in_main"> 
	<div class="h2_over_blue png">
		<h2> <%= Url.Client().CorporateClients(SimplePages.Urls.Responses, 
				 null, "Отзывы организаций") %> </h2>
	</div>
	<div class="block anons_news" style="padding: 15px">
	<%= Images.Organization(Model.Item2.Organization).Size(90, null)
			.Style("padding-top:10px").FloatLeft() %>
			<strong>Компания:</strong>
			<%= Url.Client().OrgResponse(Model.Item2.OrgResponseID, Model.Item2.Organization.Name)%>
			<div class="opinion_text">
			  <p><%= Model.Item2.ShortDescription %></p></div>

<p><strong style="color: rgb(204, 0, 0);">»</strong>&nbsp;
	<%= Url.Client().OrgResponse(Model.Item2.OrgResponseID, "Читать полностью")%>
 </p>

		<br />
		<strong>
			  <%=Url.Client().CorporateClients(SimplePages.Urls.Responses, 
				 null, "Все отзывы организаций").Class("block1") %>
		</strong>
	</div>
</div>
<% } %>