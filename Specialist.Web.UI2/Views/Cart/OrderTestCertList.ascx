<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.CartVM>" %>
<%@ Import Namespace="Specialist.Entities.Tests.Consts" %>
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
<table class="fullCourse">
    <tr class="sectionAct cart-table-head">
        <th class="white"> Сертификат тестирования </th>
        <th> Тип</th>
        <th> Язык</th>
        <th> Цена </th>
        <th> &nbsp; </th>
    </tr>
    <% foreach (var orderDetail in Model.TestCerts) { %>
    <tr>
        <td class="td_course3"> <%= Url.UserTestLink(orderDetail.UserTest)%>
			<% Html.RenderPartial(PartialViewNames.OrderExtrases, Tuple.Create(orderDetail, Model)); %>
		</td>
        <td>
        	<%= TestCertType.GetName(orderDetail.Params.Type) %> <br/>
            <%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{UserTestId = orderDetail.UserTestId}),
				H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Изменить")
                    ) %>
		
		 </td>
        <td>
        	<%= TestCertLang.GetName(orderDetail.Params.Lang) %> <br/>
			<%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{UserTestId = orderDetail.UserTestId}),
				H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Изменить")
                    ) %>
		
		</td>
        <td >
                <%= orderDetail.Price.MoneyString()%>
        <% if(orderDetail.OrderExtras.Any()) { %>
		    <% foreach (var orderExtras in orderDetail.OrderExtras) { %>
				<br />+<%= orderExtras.Price.MoneyString() %>
	        <% } %>
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

