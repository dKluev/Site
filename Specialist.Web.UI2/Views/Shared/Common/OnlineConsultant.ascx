<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%= Htmls2.Menu2("Online ������������") %>
	<p  style="font-size:12px;">
	<% Html.RenderPartial(Views.Shared.Common.ConsultantLinks); %>
<%= Images.Main("ico_signup.gif").Alt("���������� �� �����") %>
<%= H.Anchor(SimplePages.FullUrls.Payment, "���������� �� �����").Class("block1") %> <br />
<%= Images.Main("ico_timetable_big.gif").Alt("���������� ������") %>

		<%= Url.Link<GroupController>(c => c.Search(null), 
			"���������� ������").Class("block1") %> <br />
<%= Images.Main("ico_payment.gif").Alt("������� ������") %>

		<%= H.Anchor(SimplePages.FullUrls.Payment, "������� ������") %>
	</p>


