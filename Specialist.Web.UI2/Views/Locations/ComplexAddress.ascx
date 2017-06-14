<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Center.ViewModel.ComplexVM>" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%--<%= Htmls2.BorderBegin(null, true) %>--%>
	<div class="overflow address_center">
		<div class="l">
			<div id="YMapsID" style="height: 300px; width: 415px;">
			</div>
            
            <%= Cities.Complexes.Blocks.ContainsKey(Model.Complex.Complex_TC) 
            ? Htmls.HtmlBlock(Cities.Complexes.Blocks[Model.Complex.Complex_TC]) : null %>
		</div>
		<div class="r">
			<h3>
				���������� ���������</h3>
			<% if (!Model.Complex.Metro.IsEmpty()) { %>
			<p>
				<%= Images.Common("metro.gif") %>
				&nbsp; <strong>
					<%= Model.Complex.Metro %></strong>
			</p>
			<% } %>
			<p>
				<strong>�����:</strong>
				<%= Model.Complex.Address %><br />
			</p>
			<% if (!Model.GeoLocation.IsEmpty()) { %>
			<p>
			<strong>
				<%= Htmls.ComplexDirection(Model.GeoLocation)%>
			</strong>
			</p>

			<% } %>
			<p>
				<strong><a href="<%= Urls.GetComplexMap(Model.Complex.UrlName) %>" target="_blank">��������� ����� �������</a></strong>
			</p>
			<p>
			    
				<strong>�������:</strong>
				<%= H.Tel(CommonTexts.Phone) %>
			</p>
			<p>
				<strong>������������:</strong>
				<%= Html.ManagerLink(Model.Complex.Admin) %>
			</p>
			<p>
				<strong>������ �� �����:</strong>
				<%= Model.Complex.WorkingHours %></p>
			<%= Model.Description.SecondPart %>
		</div>
	</div>
<%--	<%= Htmls2.BorderEnd() %>--%>
