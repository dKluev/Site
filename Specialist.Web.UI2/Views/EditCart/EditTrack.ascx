<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EditCourseVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>

<script type="text/javascript">
    $(function() {
        setupCitySections(false);
	    setupPriceTypeChoice();
    });
</script>

<h4><%= CommonTexts.TrackName %></h4>
<p> <%= Html.CourseLink(Model.OrderDetail.Track) %> </p>

<h4>
    Выберите тип обучения:</h4>
    <%using (Html.BeginForm<EditCartController>(c => c.EditCourse(null))) {%>
        <%= Html.HiddenFor(x => x.OrderDetail.OrderDetailID) %>
        
        <% var prices = Model.GetPrices(); %>
        <% if (prices.Count > 0){ %>
        <table class="defaultTable">
            <tr>
                <th>
                </th>
                <th>
                    Tип обучения
                </th>
                <th>
                    Цена
                </th>
            </tr>
            <% foreach (var price in prices) { %>
            <tr>
                <td>
                    <%= Html.RadioButtonFor(x => x.PriceTypeTC, price.PriceType_TC)%>
                </td>
                <td>
                    <%= PriceTypes.GetFullName(price.CommonPriceTypeTC) %>
                    <% if( PriceTypes.IsBusiness(price.CommonPriceTypeTC)) {%>
                    <%= Images.Common("eat.gif") %>
                    <% } %>
                </td>
                <td>
                    <%= price.Price.MoneyString()%>
                </td>
            </tr>
            <% } %>
        </table>
        
        <% } %>
      
        <% if(Model.OrderDetail.Order.IsOrganization) { %>
        <div>
        	<h4>Количество слушателей:</h4>
        	<p><%= Html.TextBoxFor(x => x.OrderDetail.Count) %></p>
                    <h4>ФИО слушателей:</h4>
    	<%= Html.TextAreaFor(x => x.OrderDetail.OrgStudents, 5, 100, null)%>
        </div>
        <% } %>
        <p><%= Images.Submit("ok") %></p> 
    <% } %>