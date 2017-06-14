<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NameValueCollection>" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<% if (Model.Count == 0) return; %>
<div class="block_action">
	<div class="action_img">
    <%= Images.Common("webmoneycard.jpg") %>
        <p>
        <form action="<%= PaymentDataCreator.WebMoneyUrl %>" method="post" accept-charset='windows-1251'>
        <% foreach (var key in Model.AllKeys) { %>
        <%=  HtmlControls.Hidden(key, Model[key])%>
        <% } %>
            <%= Images.Submit("pay").Id(PaymentTypes.WebMoney) %>
        </form>
 </p>  
	</div>
    <div class="action_text">
      <%= Htmls.HtmlBlock(HtmlBlocks.Payment.WebmoneyCard) %>
     
      <%= Htmls.AllPaymentTypes(ViewData) %>
  </div>
</div>


