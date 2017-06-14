<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Vendor>>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<% if(!Model.Any()) return; %>
<h2>Вендоры</h2>
<div class="tab_content">
		<% foreach (var vendors in Model.Cut(2)) { %>
		<div class="tab_2column">
			<% foreach (var vendor in vendors) { %>
			<% if (vendor == null) { %>
			<% break; %>
			<% } %>
			<div class='link_block3'>
				<%= Images.EntitySmall(vendor) %>
				<h3>
					<%= Url.Link<VendorController>(c => c.Details(vendor.UrlName, VendorVM.Tab.Testing, 1), 
						vendor.Name) %>
				</h3>
			</div>
				<% } %>
		</div>
		<% } %>
	</div>