<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%= Htmls2.Menu2("Online консультация") %>
	<p  style="font-size:12px;">
	<% Html.RenderPartial(Views.Shared.Common.ConsultantLinks); %>
<%= Images.Main("ico_signup.gif").Alt("Записаться на курсы") %>
<%= H.Anchor(SimplePages.FullUrls.Payment, "Записаться на курсы").Class("block1") %> <br />
<%= Images.Main("ico_timetable_big.gif").Alt("Расписание курсов") %>

		<%= Url.Link<GroupController>(c => c.Search(null), 
			"Расписание курсов").Class("block1") %> <br />
<%= Images.Main("ico_payment.gif").Alt("Способы оплаты") %>

		<%= H.Anchor(SimplePages.FullUrls.Payment, "Способы оплаты") %>
	</p>


