<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.CartVM>" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<table class="fullCourse">
    <tr class="cart-table-head">
    	<th>Экзамены</th>
    	<th>Дата</th>
    	<th>Цена</th>
    	<th width="15px"></th>
    </tr>
    <%foreach (var orderExam in Model.Order.OrderExams){%>
    <tr>
    	<td class="td_course3"><%= Html.ExamLinkName(orderExam.Exam) %> </td>
    	<td>
            <%= orderExam.Group.GetFullDateInfo(Html) %> <br/>
						 <%= Url.Link<EditCartController>(oc => oc.Edit(
                new EditCartVM{EditExamID = orderExam.Exam_ID}),
		 				H.l(H.Img("//cdn.specialist.ru/content/image/simplepage/basket/cogwheel.png"), "Выбрать дату")) %>

        </td>
    	<td ><%= orderExam.Price.MoneyString() %></td>
    	<td class="td_control"> 
            <%= Html.ActionLinkImage<CartController>(oc => oc.DeleteExam(
                                orderExam.Exam_ID), Urls.Common("del.gif"), "Удалить")%>
        </td>
    </tr>
    <% } %>
</table>

<br /><br />