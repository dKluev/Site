<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.CartVM>" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
            <h3> ��� �����:</h3>

            <% if (Model.CourseOrderDetails.Any() || Model.Tracks.Any() || Model.Order.OrderExams.Any()) {%>             
        <table class="defaultTable">
            <tr>
                <th> ����� </th>
                <th> ���� </th>
                <th> ��� �������� </th>
                <% if(Model.HasDiscount) { %>
                <th> ������ </th>
                <th> ���� �� ������� </th>
                <% }else{ %>
                <th> ���� </th>
                <% } %>
            </tr>
            <% foreach (var orderDetail in Model.CourseOrderDetails) {%>             
            <tr>
				<td class="td_course3">
					<%=Html.CourseLink(orderDetail.Course)%>
					<% if(orderDetail.OrderExtras.Any()) { %>
						<br />
						<strong>�������������� ������</strong>
							<% foreach (var orderExtras in orderDetail.OrderExtras) { %>
								<br /><%= orderExtras.Extras.ExtrasName %>
								<%= orderExtras.Price.MoneyString() %> ���.
								
							<% } %>
					<% } %>
				</td>
                <td>
                    <%= orderDetail.Group.GetFullDateInfo(Html) %>
                </td>
                <td>
                    <%= orderDetail.GetStudyType() %>
                </td>
                <% if(Model.HasDiscount) { %>
                <td> 
                    <% if(orderDetail.PercentDiscount.HasValue) { %>
                      <span class="discount_color"><%= orderDetail.PercentDiscount %> %</span>  
                    <% } %>
                    <% if(orderDetail.MoneyDiscount.HasValue && orderDetail.MoneyDiscount > 0) { %>
                        <%= orderDetail.MoneyDiscount %>
                    <% } %>
                </td>
                <% } %>
                <td>
                    <%=orderDetail.PriceWithDiscount.MoneyString()%>
			        <% if(orderDetail.OrderExtras.Any()) { %>
					    <% foreach (var orderExtras in orderDetail.OrderExtras) { %>
					<br />+<%= orderExtras.Price.MoneyString() %>
				        <% } %>
			        <% } %>
                </td>
            </tr>
            <% }%> 
            
            <% foreach (var orderTrack in Model.Tracks) {%>             
            <tr>
                <td class="td_course3"> <%=Html.CourseLink(orderTrack.Track)%> </td>
                <td> </td>
                <td> </td>
                <% if(Model.HasDiscount) { %>
                <td> </td>
                <% } %>
                <td>
                    <%=orderTrack.Price.MoneyString()%>
                </td>
            </tr>
            <% }%> 
            <% foreach (var orderExam in Model.Order.OrderExams) {%>             
            <tr>
                <td class="td_course3"> <%= Html.ExamLink(orderExam.Exam) %> </td>
                <td> </td>
                <td> </td>
                <% if(Model.HasDiscount) { %>
                <td> </td>
                <% } %>
                <td> <%=orderExam.Price.MoneyString()%></td>
            </tr>
            <% }%> 


        </table>
                <% } %>
            <% if (Model.TestCerts.Any()) {%>             
<table class="defaultTable">
            <tr>
                <th> ���������� ������������ </th>
                <th> ���� </th>
            </tr>
            <% foreach (var orderDetail in Model.TestCerts) {%>             
            <tr>
                <td class="td_course3"> <%= Url.UserTestLink(orderDetail.UserTest) %> 
					<% if(orderDetail.OrderExtras.Any()) { %>
						<br />
						<strong>�������������� ������</strong>
							<% foreach (var orderExtras in orderDetail.OrderExtras) { %>
								<br /><%= orderExtras.Extras.ExtrasName %>
								<%= orderExtras.Price.MoneyString() %> ���.
								
							<% } %>
					<% } %>
				
				</td>
                <td> <%=orderDetail.Price.MoneyString()%> </td>
            </tr>
            <% }%> 
</table>
					<% } %>
		 <p class="" style="text-align: right;margin-right:50px;">
                <strong>�����: 
                    <%= Model.Order.TotalPriceWithDescount.MoneyString() %></strong></p>
        


       