<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<SubscribesVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Utils.Logic" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<div id="content" class="longlist">
    <% using(Html.BeginForm()){ %>
        <%= HttpUtility.HtmlDecode(Html.ValidationSummary().NotNullString()) %>
        <div class="registr_form">
		<h3>Онлайн подписка</h3>
        <% if(!CommonConst.IsMobile){ %>
            <% if(CouponUtils.MailListIsActive){ %>
            <%= Htmls.HtmlBlock(HtmlBlocks.MailListCoupon) %>
            <% } %>
            
        <div class="attention_block">
	        
	        <p>Получать рассылку на ваш email: <strong id="user_mail"><%= Model.User.Email %></strong> 
                <%= Url.Profile().ChangePassword("Изменить").Class("discount_color subscribe") %>
           </p>
        </div>
        <% } %>
        <% Htmls.FormSection(() => {%>
            <%= Html.ControlFor(x => x.User.MailListSubscribed) %> 
            <%= Html.ControlFor(x => x.MailListTypes) %> 
        <% }); %>
        <% if(!CommonConst.IsMobile){ %>
            <p class='submit_p'> <input type="submit" value="Подписаться"/> </p>
		<h3>Оффлайн подписка</h3>
        <div class="attention_block">
	        <p>Получать рассылку на ваш почтовый адрес: <strong id="user_address"><%= Model.User.AddressDescription %></strong> 
                <%= Url.Profile().EditProfile("Изменить").Class("discount_color subscribe") %>
	        </p>
        </div>

        <% Htmls.FormSection(() => {%>
            <%= Html.ControlFor(x => x.Subscribes) %> 
        <% }); %>
        <% } %>
        </div>
        <p class='submit_p'> <input type="submit" value="Заказать бесплатный каталог"/> </p>
    <% } %>
    
   


    
</div>