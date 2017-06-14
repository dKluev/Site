<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Profile.ViewModel.ClassmatesVM>" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<% var somebody = false; %>
<% foreach (var course in Model.Courses) { %>
<% var users = course.Item2.Select(x => new {
	   User = x,
	   Contacts = x.UserContacts
		   .Where(y => ContactTypes.GetAllSocialServices().Contains(y.ContactTypeID))
		   .ToList()
   }).Where(x => x.Contacts.Any()); %>
	<% if(users.Any()){ %>
		<% somebody = true; %>

<% var link = Html.CourseLinkAnchor(course.Item1.UrlName, course.Item1.WebName); %>
		<h2><%= link %></h2>
		
		<iframe src="//www.facebook.com/plugins/like.php?href=<%= Server.UrlEncode(link.AbsoluteHref().Attribute("href").Value) %>&amp;send=false&amp;layout=standard&amp;width=450&amp;show_faces=true&amp;action=like&amp;colorscheme=light&amp;font&amp;height=30&amp;appId=180742018637213" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:450px; height:30px;" allowTransparency="true"></iframe>
		<% foreach (var user in users) { %>
			<% if(user.Contacts.Any()){ %>
				<h3><%= user.User.FullName %></h3>
				<% Html.RenderPartial(Views.Profile.ContactList, user.Contacts); %>
			<% } %>
		<% } %>
	<% } %>
<% } %>

<% if(!somebody){ %>
Пока ничего нет
<% } %>