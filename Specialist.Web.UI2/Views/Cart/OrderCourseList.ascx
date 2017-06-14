<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.CartVM>" %>
<table class="fullCourse">
    <tr class="sectionAct cart-table-head">
        <th class="white"> Курсы </th>
        <th> Дата </th>
        <% if(!Model.Order.IsOrganization) { %>
        <th> Место </th>
        <% } %>
        <th> Тип обучения<%= Htmls.CartInfo %></th>
        <% if(Model.Order.IsOrganization) { %>
        <th> Количество человек </th>
        <% } %>
        <th> Цена </th>
        <th> &nbsp; </th>
    </tr>
    <% foreach (var orderDetail in Model.CourseOrderDetails) { %>
    <tr>
        <td class="td_course3"> <%= Html.CourseLink(orderDetail.Course)%>
			<% Html.RenderPartial(PartialViewNames.OrderExtrases, Tuple.Create(orderDetail, Model)); %>
		</td>
        <td>
        <% if (!orderDetail.Course.IsVideo) { %>
        	<% if(Model.IsNearestGroupOrderDetails.Contains(orderDetail.OrderDetailID)) { %>
				Ближайшая группа<br/>
			<% } %>
            <%= orderDetail.Group.GetFullDateInfo(Html) %><br/>
			 <%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{EditDetailID = orderDetail.OrderDetailID}),
		 				H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Выбрать дату")) %>
        <% } %>
        </td>
        <% if(!Model.Order.IsOrganization) { %>
			<td>
            <% if (!orderDetail.Course.IsVideo) { %>
    			    <%= orderDetail.GetSeatNumber(Url) %>
            <% } %>
			</td>
        <% } %>
        <td>
        <% if (!orderDetail.Course.IsVideo) { %>
        	<%= orderDetail.GetStudyType() %><br/>
		 <%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{EditDetailID = orderDetail.OrderDetailID}),
		 				H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Изменить")) %>
        <% } %>
        </td>
        <% if(Model.Order.IsOrganization) { %>
        <td > <%= orderDetail.Count %> </td>
        <% } %>
        <td >
            <% if(orderDetail.HasDiscount){ %>
                <%= orderDetail.Price.MoneyString().Tag("strike") %>
                <span class="discount_color"><%= orderDetail.PriceWithDiscount.MoneyString() %></span>
            <% }else{ %>
                <%= orderDetail.Price.MoneyString()%>
            <% } %>
        <% if(orderDetail.OrderExtras.Any()) { %>
		    <% foreach (var orderExtras in orderDetail.OrderExtras) { %>
				<br />+<%= orderExtras.Price.MoneyString() %>
	        <% } %>
        <% } %>
        	<br/>
        <% if (!orderDetail.Course.IsVideo) { %>
		 <%= Url.Link<EditCartController>(oc => oc.Edit(
		         new EditCartVM {EditDetailID = orderDetail.OrderDetailID}),
		         H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Выбрать")) %>
        <% } %>
        </td>
        <td  class="td_control">
                                        
            <%= Html.ActionLinkImage<CartController>(oc => oc.DeleteCourse(
                orderDetail.OrderDetailID), Urls.Common("del.gif"), "Удалить")%>
        </td>
    </tr>
    <% } %> 
</table>
<br />

