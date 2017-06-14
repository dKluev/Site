<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Web.Root.Tests.ViewModels.MainTestsVM>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Tests" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<h2 id="tests">Направления тестирования</h2>
<div class="block_action">

<div class="tab_content">

	<% foreach (var sections in Model.Sections
			   .OrderBy(e => e.WebSortOrder).ToList().Cut(2)) { %>
	<div class="tab_2column">
		<% foreach (var section in sections) { %>
		<% if (section == null) { %>
		<% break; %>
		<% } %>
		 <div class='link_block3'>
		 <%= Images.EntitySmall(section) %>
		    <h3> <%= Url.TestSectionLink(section) %></h3>
		</div>
		<% } %>
	</div>
	<% } %>
</div>

<%= Model.Description %>
</div>


<% Html.RenderAction<TestController>(c => c.Best(null)); %>