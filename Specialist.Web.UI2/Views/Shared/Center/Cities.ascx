<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.City>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Pages" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<% var moscow = Model.First(x => x.City_TC == Cities.Moscow); %>
<% var otherCities = Model.Where(x => x.City_TC != Cities.Moscow).ToList(); %>

<%= Htmls2.BorderBegin("Классы и филиалы", margin:true) %>
	<div class="tab_content">
	<div class="tab_2column">
		<div class="branches_block">
			<%= Images.EntitySmall(moscow) %>
			<h3 class="citysize" >
				<%= Html.CityLink(ViewBag.HideWebinarPartner == null 
	? moscow : new City{CityName = "Центральный офис", UrlName = moscow.UrlName}) %></h3>
			<%= SimplePageVMService.GetCityNote(moscow) %>
			<p>
				<%= Images.Common("metro.gif").Style("float:none;vertical-align:middle;") %>
				&nbsp; <strong>
					<%= moscow.MainComplex.Metro %></strong>
			</p>
		</div>
		<% foreach (var complex in moscow.BranchOffices.SelectMany(x => x.Complexes)
       .Where(c => c.IsPublished)){ %>
		<div class="branches_block">
			<%=Images.EntitySmall(complex)%>
			<h3>
				<%= Html.GetLinkFor(complex) %>
				</h3>
			<p>
				<%= complex.Address %></p>
			<p>
				<%= Images.Common("metro.gif").Style("float:none;vertical-align:middle;") %>
				&nbsp; <strong>
					<%= complex.Metro %></strong>
			</p>
		</div>
		<% } %>
	</div>
	<div class="tab_2column">
		<% foreach (var city in otherCities){ %>
		<div class="branches_block">
			<%= Images.EntitySmall(city) %>
			<h3 class="citysize" >
				<%= Html.CityLink(city) %></h3>
			<%= SimplePageVMService.GetCityNote(city) %>
		</div>
		<% } %>
		<% if(ViewBag.HideWebinarPartner == null){ %>
		<div class="branches_block">
			<%= Images.EntitySmall(new Complex{UrlName = "webinar-partner", Name = "Партнеры по вебинарам"}) %>
  
		   <h3 class="citysize"> <%= H.Anchor(SimplePages.FullUrls.PartnerWebinar, "Партнеры по вебинарам") %> </h3>
		</div>
		<% } %>
	</div>
</div>

<%= Htmls2.BorderEnd() %>


