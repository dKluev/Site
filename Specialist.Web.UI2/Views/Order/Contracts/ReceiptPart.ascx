<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ReceiptVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Order.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<% var order = Model.Order; %>
<% var org = Model.OurOrg; %>
<% var orgbank = Model.OurOrgBank; %>

<td>
    <p>
        <strong>Получатель: </strong><span class="font13_"><%= org.ShortName %> </span>
    </p>
    <p>
        <strong>ИНН:</strong>
        <span class="font14_"><%= org.INN%></span></p>
    <p>
       <strong>P/сч.:</strong>
        <span class="font14_"><%= orgbank.SettlementAccount%></span></p>
    <p>
        <strong>в:</strong> <span class="font13_"><%= orgbank.BankName%></span></p>
    <p>
        <strong>БИК:</strong> <span class="font14_"><%= orgbank.BIK%></span>&nbsp;&nbsp;&nbsp;&nbsp;<strong>К/сч.:</strong>
        <span class="font14_"><%= orgbank.CorrespondentAccount%></span></p>
    <p>
        Для оплаты через платежные терминалы.
        <% foreach(var orderDetail in order.OrderDetails){ %>
             Код <%= orderDetail.StudentInGroup_ID %>, сумма
            <%= orderDetail.PriceWithDiscount.MoneyString() %> руб. <br />
        <% } %>
    </p>
    <p>
        <strong>Платеж:</strong> <span class="font13_">
            <%= order.Description %></span></p>
    <p>
        <strong>Плательщик:</strong> <span class="font13_">
            <%= order.User.FullName %></span></p>
    <p>
        <strong>Адрес плательщика:</strong> <span class="font13_">
            <%= order.User.AddressDescription %></span></p>
    <p>
        <strong>Сумма:</strong> <span class="font14"><span class="font_">
            <%= order.TotalPriceWithDescountForSber.MoneyString() %></span> руб. <span
            class="font_">00</span> коп.</span> 
        <br />
        <br />
        <span class="font14">Подпись: ________________________&nbsp;&nbsp;&nbsp;&nbsp;Дата:
            " __ "&nbsp;_______&nbsp;&nbsp;<%= DateTime.Now.Year %> г. </span>
        <br />
        <br />
    </p>
</td>
