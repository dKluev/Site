<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Center.ViewModel.CityVM>" %>

<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<% var city = Model.City; %>
	<div class="city_block">
		<div class="city" <%= Model.Complexes.Any() ? "" : "style='width:100%'" %>>
			<div class="city_in">
				<% if(Model.Complexes.Any()){ %>
					<%= Images.Entity(city) %>
				<% } else { %>
					<%= Images.Entity(city).FloatLeft() %>
				<% } %>
				<%= city.Description %>
				<% if(city.Description.IsEmpty()){ %>
					<% if(!city.MainComplex.Address.IsEmpty()){ %>
					<p> Адрес: <%= city.MainComplex.Address %></p>
					<% } %>
					<% if(!city.Email.IsEmpty()){ %>
					<p>
						E-mail:
						<%= HtmlControls.MailTo(city.Email) %></p>
					<% } %>
				<p>
					Телефон:
					<%= city.PhoneList.Take(2).JoinWith(", ") %>
				</p>
				<% } %>
			</div>
		</div>
		<div class="city_branches">
			<% foreach (var complex in Model.Complexes) { %>
			<div class="branches_block">
				<%=Images.EntitySmall(complex)%>
				<h3>
					<%= Html.GetLinkFor(complex) %></h3>
				<p>
					<%= complex.Address %></p>
			</div>
			<% } %>
		</div>
	</div>

	<% if(Model.City.City_TC.In(Cities.Kazan, Cities.Piter)) {%>
		<h3>Обучитесь на курсах в «Специалисте» в режиме вебинара!</h3>
		<% Html.RenderPartial(PartialViewNames.NearestGroupList, new NearestGroupsVM(Model.Groups)); %>
	<% } %>

	<% Html.RenderAction<SectionController>(c => c.SectionsResponses()); %>
</asp:Content>
