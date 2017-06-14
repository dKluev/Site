<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Context.CartVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Services"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $("img[src*='clear'], img[src*='del'], img[src*='case']")
                .parent().click(function() {
                    return confirmDialog();
            });

        });
    </script>
    

    <% if(Model.IsEmpty) { %>
        <% Html.RenderPartial(PartialViewNames.Empty); %>
    <% if(Model.LastCompleteOrder != null) { %>
    <h3>Выбор формы оплаты вашего предыдущего заказа</h3>
    <%= Url.Order().PaymentTypeChoice(Model.LastCompleteOrder.OrderID, 
        Model.LastCompleteOrder.Description) %>
    <% } %> 

        <% return; %>
    <% } %> 
	   
    <% if(Model.IsEmpty ) { %>
        <% return; %>
    <% } %> 
    <% Html.RenderPartial(PartialViewNames.OrderSteps, new StepsVM(OrderStep.Cart)); %>
    


    
    <%= Model.HasNsDiscount ? Htmls.HtmlBlock(HtmlBlocks.RsDiscountInfo) : null %>
    
	<br />
	<%= Htmls.BorderBegin() %>
    <div class="tabs-control">
		<ul class="bookmarks"  style="margin:0 0 0 10px;">
<%--		    <% if(!Model.Order.IsEmpty) { %>--%>
			<li class="active"><a href="#" rel="tab-main">Выбранные</a></li>
<%--			<% } %>--%>
		<%--	<li class='<%= Model.Order.IsEmpty ? "active" : "" %>'>
			    <a href="#" rel="tab-in-plan">Отложенные</a>
			</li>--%>
			<span style=" text-align:right;">  <%=Url.Link<CartController>(oc => oc.Clear(),
    		        H.strong["Очистить корзину"])%></span>

		</ul>
	    <div class="tab-main tab_content2" <%= Htmls.DisplayNone(Model.Order.IsEmpty) %>>
            <% if(Model.CourseOrderDetails.Any()) { %>
                <% Html.RenderPartial(PartialViewNames.OrderCourseList, 
                    Model); %>
            <% } %>

			 <% if(Model.TestCerts.Any()) { %>
                <% Html.RenderPartial(PartialViewNames.OrderTestCertList, 
                    Model); %>
            <% } %>
              
            <% if(Model.Tracks.Any()) { %>
                <% Html.RenderPartial(PartialViewNames.OrderTrackList, Model); %>
            <% } %>
            
            <% if(Model.Order.OrderExams.Any()) { %>
                <% Html.RenderPartial(PartialViewNames.OrderExamList, Model); %>
            <% } %>
            <%= Htmls.HtmlBlock(HtmlBlocks.CartInfo) %>
            <p class="" style="text-align: right;">
                <strong>Итого: 
                    <%= Model.Order.TotalPriceWithDescount.MoneyString() %></strong></p>
            <% if(Model.HasBusiness){ %>        
            <p class="total">
                <%= Images.Common("eat.gif")%>
                – В стоимость обучения включены обеды и кофе-брейки
            </p>
            <% } %>
            
            <% Html.RenderPartial(PartialViewNames.UpdatePromocode); %>
            

			<div class="buttons">

              
                <%=Html.ActionLinkImage<OrderController>(oc => oc.Register(),
                Urls.Button(Htmls.IsSecond ? "order-yellow" : "order"), "Оформить заказ")%>
            </div>
            <%= SiteHtmls.CreditBlock(Model.Order.TotalPriceWithDescount) %>

			<% Html.RenderPartial(Views.Cart.Recommendations); %>
			<% Html.RenderPartial(PartialViewNames.TrackDiscounts); %>
        </div>
	</div>
	
	<%=Htmls.BorderEnd %>

<!-- это код AW для отслеживания конверсии по показам на странице корзины - Елена - Google Code for &#1082;&#1086;&#1088;&#1079;&#1080;&#1085;&#1072; Conversion Page -->
<script type="text/javascript">
/* <![CDATA[ */
var google_conversion_id = 1059972133;
var google_conversion_language = "en";
var google_conversion_format = "1";
var google_conversion_color = "ffffff";
var google_conversion_label = "Jgs7COHpfBClyLf5Aw";
var google_conversion_value = 0;
/* ]]> */
</script>
<div style="display:none;">
<script type="text/javascript" src="https://www.googleadservices.com/pagead/conversion.js">
</script>
</div>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="https://www.googleadservices.com/pagead/conversion/1059972133/?value=0&amp;label=Jgs7COHpfBClyLf5Aw&amp;guid=ON&amp;script=0"/>
</div>
</noscript>

      
</asp:Content>
