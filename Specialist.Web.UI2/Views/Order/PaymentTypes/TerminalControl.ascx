<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.Order>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<div class="block_action">
	<div class="action_img">
    <%= Images.Common("express_oplata.gif")%>
        <p>
		 <%= Url.Link<OrderController>(
             x => x.SberbankInfo(Model.OrderID), H.Img(Urls.Button("pay"))).Class("payment-type-link").Rel(PaymentTypes.SberBank) %>
	</div>
    <div class="action_text">
      <%= Htmls.HtmlBlock(HtmlBlocks.Payment.Terminal) %>
    
      <%= Htmls.AllPaymentTypes(ViewData) %>
    </div>

</div>
