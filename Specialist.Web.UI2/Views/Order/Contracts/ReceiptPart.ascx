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
        <strong>����������: </strong><span class="font13_"><%= org.ShortName %> </span>
    </p>
    <p>
        <strong>���:</strong>
        <span class="font14_"><%= org.INN%></span></p>
    <p>
       <strong>P/��.:</strong>
        <span class="font14_"><%= orgbank.SettlementAccount%></span></p>
    <p>
        <strong>�:</strong> <span class="font13_"><%= orgbank.BankName%></span></p>
    <p>
        <strong>���:</strong> <span class="font14_"><%= orgbank.BIK%></span>&nbsp;&nbsp;&nbsp;&nbsp;<strong>�/��.:</strong>
        <span class="font14_"><%= orgbank.CorrespondentAccount%></span></p>
    <p>
        ��� ������ ����� ��������� ���������.
        <% foreach(var orderDetail in order.OrderDetails){ %>
             ��� <%= orderDetail.StudentInGroup_ID %>, �����
            <%= orderDetail.PriceWithDiscount.MoneyString() %> ���. <br />
        <% } %>
    </p>
    <p>
        <strong>������:</strong> <span class="font13_">
            <%= order.Description %></span></p>
    <p>
        <strong>����������:</strong> <span class="font13_">
            <%= order.User.FullName %></span></p>
    <p>
        <strong>����� �����������:</strong> <span class="font13_">
            <%= order.User.AddressDescription %></span></p>
    <p>
        <strong>�����:</strong> <span class="font14"><span class="font_">
            <%= order.TotalPriceWithDescountForSber.MoneyString() %></span> ���. <span
            class="font_">00</span> ���.</span> 
        <br />
        <br />
        <span class="font14">�������: ________________________&nbsp;&nbsp;&nbsp;&nbsp;����:
            " __ "&nbsp;_______&nbsp;&nbsp;<%= DateTime.Now.Year %> �. </span>
        <br />
        <br />
    </p>
</td>
