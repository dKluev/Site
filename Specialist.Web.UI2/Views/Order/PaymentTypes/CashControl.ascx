<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.Order>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<div class="block_action">
	<div class="action_img">
 <%= Images.Image("SimplePage/money.jpg")%>
	   <p>
		 <%= Url.Link<OrderController>(
             x => x.CashInfo(Model.OrderID), H.Img(Urls.Button("pay"))).Class("payment-type-link").Rel(PaymentTypes.Cash) %>
	</div>
	<div class="action_text">
     <%= Htmls.HtmlBlock(HtmlBlocks.Payment.Cach) %>
      <%= Htmls.AllPaymentTypes(ViewData) %>
    </div>
</div>
