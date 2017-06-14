<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Order.ViewModel.OrgOrderCompleteVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<p>
	Информация о заказе отправлена менеджеру. В ближайшее время с вами свяжутся.
</p>
<br/>
<%= Url.Link<OrderController>(c => c.InvoicePayment(Model.Order.OrderID), "Cчёт-фактура")
	.FluentUpdate(x => x.SetAttributeValue("target","_blank")).Id("invoice-button") %>


<script type="text/javascript">
	$(function () {
		$("#invoice-button").button();
	});
</script>