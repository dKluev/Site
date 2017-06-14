<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Order>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>
<%@ Import Namespace="Specialist.Services.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<% var xml = AlphaBankGenerator.FromOrder(Model); %>
<% if (xml == null) return; %>
<h2>ќформить потребительский кредит онлайн</h2>
<div class="block_action">
	<div class="action_img">
    <%= Images.Common("alphacredit.gif")%>
        <p>
            <%= H.Form("https://anketa.alfabank.ru/alfaform-pos/endpoint")[
				H.textarea[xml].Name("InXML").Hide(),
				Images.Submit("pay").Id(PaymentTypes.AlphaCredit).ToString()
				].Enctype("application/x-www-form-urlencoded") %>
		</p>
	</div>
    <div class="action_text">
      <%= Htmls.HtmlBlock(HtmlBlocks.Payment.AlphaCredit) %>
 <%= Htmls.AllPaymentTypes(ViewData) %>
    </div>
</div>

   

