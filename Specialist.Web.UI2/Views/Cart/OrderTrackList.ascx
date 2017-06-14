<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.CartVM>" %>

<table class="fullCourse"> 
	<tr class="cart-table-head">
		<th><%= CommonTexts.TracksName %></th>
		<th>Дата</th>
        <% if(!Model.Order.IsOrganization) { %>
        <th> Место </th>
        <% } %>
		<th>Тип обучения</th>
		<% if(Model.Order.IsOrganization) { %>
        <th> Количество человек </th>
        <% } %>
		<th>Цена</th>
		<th>&nbsp;</th>
	</tr>
    <%foreach (var track in Model.Tracks){%>
	<tr class="complex">
		<td class="td_course3" colspan="<%= Model.Order.IsOrganization ? 2 : 3 %>">
          <strong><%=Html.CourseLink(track.Track) %></strong>  
		</td>
		<td>
			<%= track.OrderDetails.First().GetStudyType() %> <br/>
			 <%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{EditTrackTC = track.Track.Course_TC}),
		 				H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Изменить")) %>
		</td>
		<% if(Model.Order.IsOrganization) { %>
        <td > <%= track.OrderDetails.First().Count %> </td>
        <% } %>
		<td>
			<%= track.Price.MoneyString() %>
        	<br/>
			 <%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{EditTrackTC = track.Track.Course_TC}),
		 				H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Выбрать")) %>
		</td>
		<td class="td_control">
            <%= Html.ActionLinkImage<CartController>(oc => oc.DeleteTrack(
                                                track.Track.Course_TC), Urls.Common("del.gif"), "Удалить")%>
		</td>
	</tr>
    <% foreach (var orderDetail in track.OrderDetails) { %> 
    <tr>
        <td class="td_course3"> <%= Html.CourseLink(orderDetail.Course)%> </td>
        <td>
            <%= orderDetail.Group.GetFullDateInfo(Html) %> <br/>
            <%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{EditDetailID = orderDetail.OrderDetailID}), 
                H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Выбрать дату")) %>
 
        </td>
        <% if(!Model.Order.IsOrganization) { %>
			<td><%= orderDetail.GetSeatNumber(Url) %></td>
        <% } %>
        <td>
           
        </td>
       
        <td></td>
        <% if(Model.Order.IsOrganization) { %>
        <td></td> 
        <% } %>
        <td>
        </td>
    </tr>
    <% } %>
    <% } %>
</table>

<br />
