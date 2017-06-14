<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EditVM>" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Tests.Consts" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>

<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Reflection"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>
<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="SimpleUtils.Extension" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title><%= Model.MetaData.DisplayName() %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% var order = Model.Entity.As<Order>(); %>
<% var cart = new CartVM(order); %>
  <div>
        <a href="javascript: history.go(-1)">���������</a>
    </div>
<h2>����� � <%= order.OrderID %> <%= order.OurOrg_TC == null ? null : "[{0}]".FormatWith(order.OurOrgOrDefault)  %></h2>
<% if(order.IsOrganization){ %>
<strong>���� � <%= order.InvoiceNumber %></strong>

<% } %> 


   <h3>���������� � �������</h3>
   <%= order.User.MailDescription%>
   <% if (cart.TestCerts.Any()) {%>
   <b><%= Url.Link<OrderEntityController>(c => c.Envelope(order.UserID.Value), "�������") %></b><br/>
   <% } %> 
   <br />
<% if(!order.IsOrganization){ %>
   <strong>������������� � �� "����������":</strong>
   <% if(order.Exported) { %> �� <% }else{ %> 
   ��� <br />
   <% using(Html.BeginForm<OrderEntityController>(c => c.Export(0))){ %>
       <%= HtmlControls.Hidden("orderID", order.OrderID) %>
       <%= HtmlControls.Submit("��������������") %>
   <% } %> 
   <% } %> 
   
   <% } %>
   <% if(!order.PromoCode.IsEmpty()){ %> 
   <br />
   <strong>��������: </strong> <%= order.PromoCode %>
   <% } %>
    
    <% var socialUrl = order.SocialUrl; %>
   <% if(!socialUrl.IsEmpty()){ %> 
   <br />
   <strong>������ �� ���. ���� ��� ������: </strong> <%= socialUrl %>
   <% } %>
   
   <br />
   <br />
    
    <% if (cart.TestCerts.Any()) {%>
	<br />
     <table class="simple-table">
            <tr>
                <th> ���� </th>
				<th> � "����������"</th>
                <th> ���� </th>
                <th> ���� </th>
            </tr>

					  <% foreach (var orderDetail in cart.TestCerts){%>             
            <tr>
                <td> 
				<%= Html.ActionLink<OrderEntityController>(c => c.TestCert(orderDetail.OrderDetailID,null), orderDetail.UserTest.Test.Name) %>
    <% if (orderDetail.Params.Lang == TestCertLang.RusEng) {%>
				<%= Html.ActionLink<OrderEntityController>(c => c.TestCert(orderDetail.OrderDetailID, true), "[���������]") %>

	<% } %>
				
				 </td>
                <td>
                 <%= orderDetail.StudentInGroup_ID %> 
                </td>
				<td> <%= orderDetail.CreateDate %></td>
                <td>
                    <%=orderDetail.PriceWithDiscount.MoneyString()%> ���.
                </td>
               
            </tr>
            <% }%> 
</table>
    <br/>
    <% }%> 
    

    <% if (cart.CourseOrderDetails.Any() || cart.Order.OrderExams.Any() || cart.Tracks.Any()) {%>
   <% var calSpan = 4 + cart.HasDiscount.ToInt() + cart.Order.IsOrganization.ToInt(); %>
     <table class="simple-table">
            <tr>
                <th> ����� </th>
                <th> ���� </th>
                  <% if (order.IsOrganization){ %>
                    <th> ��������� </th>                    
                <% } %>     
                <th> ��� �������� </th>
                <th> ��� ���� </th>
                         
                <% if (cart.HasDiscount){ %>
                <th> ������ </th>
                <th> ���� �� ������� </th>
                <% } else { %>
                <th> ���� </th>
                <% } %>


            </tr>
            <% foreach (var orderDetail in cart.CourseOrderDetails)
               {%>             
            <tr>
                <td> <%= orderDetail.Course.Name %> </td>
                <td>
                    <%= StringUtils.RemoveTags(orderDetail.Group
                    	.GetFullDateInfo(Html).Replace("<br />", " ")) %>
                    <%= orderDetail.City.GetOrDefault(x => x.Name) %>
                </td>
                  <% if (cart.Order.IsOrganization){ %>
                    <td> <%= orderDetail.OrgStudents %> </td>                    
                <% } %>     

                <td>
                    <%= orderDetail.GetStudyType()%>
                </td>
                   <td>
                    <%=orderDetail.PriceType_TC %>
                </td>
                <% if (cart.HasDiscount)
                   { %>
                <td> 
                    <% if (orderDetail.PercentDiscount.HasValue)
                       { %>
                        <%= orderDetail.PercentDiscount%>%
                    <% } %>
                    <% if (orderDetail.MoneyDiscount.HasValue && orderDetail.MoneyDiscount > 0)
                       { %>
                        <%= orderDetail.MoneyDiscount.MoneyString() %> ���
                    <% } %>
                </td>
                <% } %>
              
                <td>
                    <%=orderDetail.PriceWithDiscount.MoneyString()%> ���.
                </td>
               
            </tr>
            <% }%> 

            
            <% foreach (var orderTrack in cart.Tracks)
               {%>  
               <% var orderDetail = orderTrack.OrderDetails.First(); %>           
            <tr>
                <td> <%=orderTrack.Track.Name%> </td>
                <td> </td>
                   <% if (cart.Order.IsOrganization){ %>
                    <td> <%= orderDetail.OrgStudents %> </td>                    
                <% } %>     
                <td> </td>
                 <td>
                    <%= orderDetail.PriceType_TC %>
                </td>
                <% if (cart.HasDiscount)
                   { %>
                <td> </td>
                <% } %>
                
                <td>
                    <%=orderTrack.Price.MoneyString()%> ���.
                </td>
               
            </tr>
            <% }%> 
            <% foreach (var orderExam in cart.Order.OrderExams)
               {%>             
            <tr>
                <td> <%= orderExam.Exam.ExamName %> </td>
                <td> </td>
                    <% if (cart.Order.IsOrganization){ %>
                    <td>  </td>                    
                <% } %>     
                <td> </td>
                 <td></td>

                <% if (cart.HasDiscount)
                   { %>
                <td> </td>
                <% } %>
                <td> <%=orderExam.Price.MoneyString()%></td>
               
            </tr>
            <% }%> 
          


        </table>
            <% }%> 

    <h3 style="float:right">
        
	����� - <%= order.TotalPriceWithDescount.MoneyString() %> ���. 
    </h3>

</asp:Content>

