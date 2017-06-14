<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="SimpleUtils.Collections" %>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Order>" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Services" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.ViewModel.Orders" %>
<%
    var order = Model; %>

   <% var products = order.OrderDetails.Select(x => new YandexOrderJson.Good {
                   id = x.Course_TC,
                   name = x.Course.Name,
                   price = (int) x.PriceWithDiscount,
               }).ToList();
               var yaData = new {
                   ecommerce = new {
                       purchase = new {
                           actionField = new {
                               id = order.OrderID.ToString(),
                               goal_id = 24716545,
                               coupon = order.PromoCode
                           },
                           products
                       }
           }
       };
       %>
		<script type="text/javascript">
		    $(function() {
		        $.post('<%= Url.Action<OrderController>(
                c => c.IsGAExport(order.OrderID)) %>',
		            function(data) {
		                if (!data) {
		                    exportToGA();
		                    exportToMetric();
		                }
		            });

		    });

		    function exportToMetric() {
    		    window.dataLayer = window.dataLayer || [];
    		    dataLayer.push(<%= JsonConvert.SerializeObject(yaData) %>);
		    }



            function exportToGANew() {
                ga('require', 'ec');
                ga('set', 'currencyCode', 'RUB');

		        <% foreach (var orderDetail in order.OrderDetails){ %>

		        ga('ec:addProduct', {
		            'id': '<%= orderDetail.Course_TC %>',             
		            'name': '<%= orderDetail.Course.Name + (orderDetail.Group_ID.HasValue ? "|" + orderDetail.Group_ID : "") %>',  
		            'category': '<%= orderDetail.Course.CourseDirectionA_TC %>',
		            'price': '<%= (int)orderDetail.PriceWithDiscount %>.00',
		            'quantity': '<%= orderDetail.Count %>'
		        });

                <% } %>


                ga('ec:setAction', 'purchase', {
                  'id': '<%= order.OrderID %>'
                });
		  
		        <% if(order.OrderDetails.Any()){ %>
		        ga('set', 'dimension1', '<%= order.OrderDetails.First().PriceType_TC %>');
		        var utmContent = getUtm().utmcct;
		        if (utmContent) {
		            ga('set', 'dimension2', utmContent);
		        }
		        <% } %>
		        ga('ecommerce:send');
                <% if (order.PromoCode != null) { %>
    		        recordOutboundLink("order", "promocode", "<%= order.PromoCode %>");
                <% } %>
		        ga('send', 'pageview');


            }

		    function exportToGA() {
		        ga('require', 'ecommerce', 'ecommerce.js');

		        ga('ecommerce:addTransaction', {
		            'id': '<%= order.OrderID %>',       
		            'affiliation': 'www.specialist.ru',  
		            'revenue': '<%= (int)order.TotalPriceWithDescount %>.00'
		        });
		        
		        <% foreach (var orderDetail in order.OrderDetails){ %>

		        ga('ecommerce:addItem', {
		            'id': '<%= order.OrderID %>',             
		            'name': '<%= orderDetail.Course.Name + (orderDetail.Group_ID.HasValue ? "|" + orderDetail.Group_ID : "") %>',  
		            'sku': '<%= orderDetail.Course_TC %>',              
		            'category': '<%= orderDetail.Course.CourseDirectionA_TC %>',
		            'price': '<%= (int)orderDetail.PriceWithDiscount %>.00',
		            'quantity': '<%= orderDetail.Count %>'
		        });

		        <% } %>
		  
		        <% if(order.OrderDetails.Any()){ %>
		        ga('set', 'dimension1', '<%= order.OrderDetails.First().PriceType_TC %>');
		        var utmContent = getUtm().utmcct;
		        if (utmContent) {
		            ga('set', 'dimension2', utmContent);
		        }
		        <% } %>
		        ga('ecommerce:send');
                <% if (order.PromoCode != null) { %>
    		        recordOutboundLink("order", "promocode", "<%= order.PromoCode %>");
                <% } %>
		        ga('send', 'pageview');
		    }
		</script>	

<% var admitad = Model.GetAdmitad(); %>
<% if(admitad.Any()) { %>
<script type="text/javascript">
    (function (d, w) {
        w._admitadPixel = {
            response_type: 'img',     
            action_code: '1',
            campaign_code: '4415a1bb1f'
        };
        w._admitadPositions = w._admitadPositions || [];
        <% foreach (var x in admitad) { %>
            w._admitadPositions.push(
                <%= DictionaryUtils.ToJson(x) %>
            );
        <% } %>
        var id = '_admitad-pixel';
        if (d.getElementById(id)) { return; }
        var s = d.createElement('script');
        s.id = id;
        var r = (new Date).getTime();
        var protocol = (d.location.protocol === 'https:' ? 'https:' : 'http:');
        s.src = protocol + '//cdn.asbmit.com/static/js/npixel.js?r=' + r;
        var head = d.getElementsByTagName('head')[0];
        head.appendChild(s);
    })(document, window)
</script>
<noscript>
    <% var urlParams = admitad.Select(DictionaryUtils.ToUrlParams).JoinWith("&"); %>
    <img src="//ad.admitad.com/r?campaign_code=4415a1bb1f&action_code=1&payment_type=sale&response_type=img&<%= urlParams %>" width="1" height="1" alt="">
</noscript>

<% } %>


