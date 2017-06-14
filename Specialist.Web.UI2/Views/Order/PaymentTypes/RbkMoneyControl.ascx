<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tuple<Order, string>>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>
<%@ Import Namespace="Specialist.Entities.Order.Logic" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<% 
   var type = Model.Item2;
   var data = PaymentDataCreator.RbkMoney(Model.Item1, type);
   if (data.Count == 0) return;
%>

<div class="block_action">
	<div class="action_img">
    <%= Images.Common("rbk/{0}.jpg".FormatWith(type))%>
        <p>
        <form action="https://rbkmoney.ru/acceptpurchase.aspx" method="post">
        <% foreach (var key in data.AllKeys) { %>
        <%=  HtmlControls.Hidden(key, data[key])%>
        <% } %>
            <%= Images.Submit("pay").Id(PaymentTypes.RbkMoney) %>
        </form>
 </p>  
	</div>
    <div class="action_text">
        <h3>
            <%= RbkTypes.Names[type] %></h3>
        <p>
        	<%= Htmls.HtmlBlock(RbkTypes.DescBlocks[type]) %>
 </p>

      <%= Htmls.AllPaymentTypes(ViewData) %>
      
  </div>
</div>


