<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Specialist.Entities.Passport.UserContact>>" %>
<table class="table_contact">
	<% foreach (var contact in Model) { %>
	<tr>
		<td class="name">
			<%= Images.ContactType( contact.UserContactType.ContactTypeID )%>
			<strong>
				<%=contact.UserContactType.Name%></strong>
		</td>
		<td class="field">
			<%: contact.Contact %>
		</td>
	</tr>
	<% } %>
</table>
