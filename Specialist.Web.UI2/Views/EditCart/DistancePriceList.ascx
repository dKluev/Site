<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<EditCourseVM>" %>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>

<table class="defaultTable">
    <tr>
        <th></th>
        <th>Tип обучения</th>
        <th>Цена</th>
        <th>Описание</th>
	</tr>
    <% foreach (var price in Model.DistancePrices) {%>
    <tr>
        <td><%=Html.RadioButtonFor(x => x.PriceTypeTC, price.PriceType_TC)%></td>
        <td><%=PriceTypes.GetFullName(price.CommonPriceTypeTC)%> </td>
        <td><%=price.Price.MoneyString()%></td>
        <td><%=price.PriceType.Description %></td>
    </tr>
    <% } %>
</table>
