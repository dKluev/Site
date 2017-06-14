<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Employee>" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<% var employee = Model; %>
<% if (employee.IsTrainer) { %>
<table class="table_contact">
	<% foreach (var contact in Model.PublicContacts) { %>
	<tr>
		<td class="name">
			<% var contactType =
									   ContactTypes.ConvertSpecialist(
									   contact); %>
			<% if (contactType.HasValue) { %>
			<%= Images.ContactType(contactType.Value) %>
			<% }else if(contact.ContactType_ID == ContactTypes.Specialist.Site){ %>
			<%= Images.ContactType("link") %>
			<% } %>
			<% if (contact.ContactType_ID == ContactTypes.Specialist.Site
									  && contactType.HasValue) { %>
			<%= ContactTypes.GetName(contactType.Value) %>
			<% } else { %>
			<%= contact.ContactType.Name %>
			<% } %>
		</td>
		<td class="field">
			<% if (contact.ContactType_ID == ContactTypes.Specialist.Site) { %>
			<%= HtmlControls.Anchor(contact.ContactValue)%>
			<% } else { %>
			<%= contact.ContactValue %>
			<% } %>
		</td>
	</tr>
	<% } %>
</table>
<% return;%>
<% } %>
<% if (Request.IsAuthenticated) { %>
<table class="table_contact">
	<% if (employee.FirstEmail != null) { %>
	<tr>
		<td class="name">
			<%= Images.ContactType(ContactTypes.Email) %>
			Email
		</td>
		<td class="field">
			<%= HtmlControls.MailTo(employee.FirstEmail) %>
		</td>
	</tr>
	<% } %>
	<% foreach (var contact in employee.EmployeeContacts) { %>
	<tr>
		<td class="name">
			<% var contactType =
									   ContactTypes.ConvertSpecialist(
									   contact); %>
			<% if (contactType.HasValue) { %>
			<%= Images.ContactType(contactType.Value) %>
			<% } %>
			<% if (contact.ContactType_ID == ContactTypes.Specialist.Site
									  && contactType.HasValue) { %>
			<%= ContactTypes.GetName(contactType.Value) %>
			<% } else { %>
			<%= contact.ContactType.Name %>
			<% } %>
		</td>
		<td class="field">
			<% if (contact.ContactType_ID == ContactTypes.Specialist.Site) { %>
			<%= HtmlControls.Anchor(contact.ContactValue)%>
			<% } else { %>
			<%= contact.ContactValue %>
			<% } %>
		</td>
	</tr>
	<% } %>
	<% if (employee.Department.Department_TC.In(
			Departments.Ofl, Departments.Corp)) {%>
	<tr>
		<td class="name">
			<%= Images.ContactType("2") %>
			Телефон
		</td>
		<td class="field">
			<%= employee.WorkIn(Departments.Ofl) 
	? "(495) 232-32-16" : "(495) 780-48-44" %>
		</td>
	</tr>
	<tr>
		<td class="name">
			<%= Images.ContactType("fax") %>
			Факс
		</td>
		<td class="field">
			<%= employee.WorkIn(Departments.Ofl) 
	? "(495) 780-48-49" : "(495) 780-48-44" %>
		</td>
	</tr>
	<% } %>
</table>
<% } else { %>
<table class="table_contact">
	<% foreach (var contact in Model.PublicContacts) { %>
	<tr>
		<td class="name">
			<% var contactType =
									   ContactTypes.ConvertSpecialist(
									   contact); %>
			<% if (contactType.HasValue) { %>
			<%= Images.ContactType(contactType.Value) %>
			<% } %>
			<% if (contact.ContactType_ID == ContactTypes.Specialist.Site
									  && contactType.HasValue) { %>
			<%= ContactTypes.GetName(contactType.Value) %>
			<% } else { %>
			<%= contact.ContactType.Name %>
			<% } %>
		</td>
		<td class="field">
			<% if (contact.ContactType_ID == ContactTypes.Specialist.Site) { %>
			<%= HtmlControls.Anchor(contact.ContactValue)%>
			<% } else { %>
			<%= contact.ContactValue %>
			<% } %>
		</td>
	</tr>
	<% } %>
	<% if (employee.Department.Department_TC.In(
			Departments.Ofl, Departments.Corp)) {%>
	<tr>
		<td class="name">
			<%= Images.ContactType("2") %>
			Телефон
		</td>
		<td class="field">
			<%= employee.WorkIn(Departments.Ofl) 
	? "(495) 232-32-16" : "(495) 780-48-44" %>
		</td>
	</tr>
	<tr>
		<td class="name">
			<%= Images.ContactType("fax") %>
			Факс
		</td>
		<td class="field">
			<%= employee.WorkIn(Departments.Ofl) 
	? "(495) 780-48-49" : "(495) 780-48-44" %>
		</td>
	</tr>
	<% } %>
</table>
<% } %>
