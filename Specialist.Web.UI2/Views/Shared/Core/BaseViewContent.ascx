<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Pages.BaseVM>" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Extensions" %>
<% if(HttpContext.Current.Request.IsAjaxRequest()) { %>
<% var title = new TitleCreator().Get(Model.Parts.FirstOrDefault(x => x != null && x.Model != null).GetOrDefault(x => x.Model)); %> 
	<% if(!title.IsEmpty()) %> <%= Htmls.Title(title) %>
<% } %>
<% var id = "view-base-content-" + Guid.NewGuid().ToString("N"); %>
<div id="<%=id %>">
<% foreach (var pagePart in Model.Parts.Where(x => x != null)) { %>
<% if (pagePart.Content != null) { %>
<%= pagePart.Content %>
<% } else if(pagePart.BaseView != null) { %>
<%= pagePart.BaseView.Get() %>
<% } else { %>
<% Html.RenderPartial(pagePart.View, pagePart.Model); %>
<br />
<% } %>
<% } %>

</div>
<% if(!Model.IsBootStrap){ %>
<script src="/Scripts/initcontrols.js?v=5" type="text/javascript"></script>
<script type="text/javascript">	$(function () { initControls('<%= id %>'); });</script>
<% } %>