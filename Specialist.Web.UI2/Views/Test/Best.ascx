<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.TestBestVM>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Tests" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<div id="other-tests">
	<p>
		&nbsp;</p>
	<div class="courses_5">
		<table width="100%" cellpadding="5px" align="center">
			<tr>
				<% if (Model.Users.Any()) { %>
				<td width="50%" valign="top">
					<div class='link_block4'>
						<%= Images.Main("best.jpg").Class("ico") %>
						<%= Html.Site().BestTestUser(Model.Users) %>
					</div>
				</td>
				<% } %>
				<% if (Model.NewTests.Any()) { %>
				<td width="50%" valign="top">
					<div class='link_block4'>
						<%= Images.Main("new-icon.jpg").Class("ico") %>
						<h3> Новые тесты</h3> 
						<%= H.Ul(Model.NewTests.Select(x => Url.TestLink(x))) %>
					</div>
				</td>
				<% } %>
			</tr>
		</table>
	</div>
</div>
