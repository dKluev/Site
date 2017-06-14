<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Order.ViewModel.OrgOrderCompleteVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<p>
	���������� � ������ ���������� ���������. � ��������� ����� � ���� ��������.
</p>
<br/>
<%= Url.Link<OrderController>(c => c.InvoicePayment(Model.Order.OrderID), "C���-�������")
	.FluentUpdate(x => x.SetAttributeValue("target","_blank")).Id("invoice-button") %>


<script type="text/javascript">
	$(function () {
		$("#invoice-button").button();
	});
</script>