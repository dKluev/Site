<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Pages.BaseVM>" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
<% foreach (var pagePart in Model.Parts.Where(x => x != null)) { %>
	<% if (pagePart.Content != null) { %>
	<%= pagePart.Content %>
	<% } else if (pagePart.BaseView != null) { %>
	<%= pagePart.BaseView.Get() %>
	<% } else { %>
	<% Html.RenderPartial(pagePart.View, pagePart.Model); %>
	<% } %>
<% } %>
</asp:Content>
