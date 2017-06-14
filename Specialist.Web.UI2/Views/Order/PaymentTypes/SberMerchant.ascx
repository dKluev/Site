<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.Order>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<% var hide = PaymentDataCreator.SberbankMerchant(Model).Count == 0; %>
<% if (hide) return; %>
<div class="block_action">
	<div class="action_img">
    <%= Images.Common("sbermerchant.jpg")%>
	   <p>
		 <%= Url.Link<OrderController>(
             x => x.SberMerchant(Model.CommonOrderId), H.Img(Urls.Button("pay"))).Class("payment-type-link") %>
	</div>
	<div class="action_text">
     <%= Htmls.HtmlBlock(HtmlBlocks.Payment.SberMerchant) %>
      <%= Htmls.AllPaymentTypes(ViewData) %>
    </div>
</div>
