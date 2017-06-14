<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.CartVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Const" %>
<table class="fullCourse">
    <% if(Model.InPlan.CourseOrderDetails.Any()){ %>
    <tr><th>Курс</th><th></th></tr>
    <% } %>
    <% foreach (var orderDetail in Model.InPlan.CourseOrderDetails) { %>
    <tr>
        <td><%= Html.CourseLink(orderDetail.Course) %></td>
        <td width="15px">
            <%= Html.ActionLinkImage<EditCartController>(
                c => c.ToggleCourse(orderDetail.OrderDetailID),
                Urls.Common("bin.gif"),
                "Вернуть в заказ")%>

        </td>
    </tr>
    <% } %>
    <% if (Model.InPlan.Tracks.Any()) { %>
    <tr>
        <th>
           <%= CommonTexts.TrackName %>
        </th>
        <th>
        </th>
    </tr>
    <% } %>
    <% foreach (var orderTrack in Model.InPlan.Tracks) { %>
    <tr>
        <td><%= Html.CourseLink(orderTrack.Track) %> </td>
        <td>
             <%= Html.ActionLinkImage<EditCartController>(
                c => c.ToggleTrack(orderTrack.Track.Course_TC),
                Urls.Common("bin.gif"),
                "Вернуть в заказ")%>
        </td>
    </tr>
    <% } %>
    <% if (Model.InPlan.Order.OrderExams.Any()) { %>
    <tr>
        <th>
            Экзамен
        </th>
        <th>
        </th>
    </tr>
    <% } %>
    <% foreach (var orderExam in Model.InPlan.Order.OrderExams) { %>
    <tr>
        <td><%= Html.ExamLink(orderExam.Exam) %> </td>
        <td>
             <%= Html.ActionLinkImage<EditCartController>(
                c => c.ToggleExam(orderExam.Exam_ID),
                Urls.Common("bin.gif"),
                "Вернуть в заказ")%>
        </td>
    </tr>
    <% } %>
    <% if (Model.InPlan.TestCerts.Any()) { %>
    <tr>
        <th>
            Сертификат тестирования
        </th>
        <th>
        </th>
    </tr>
    <% } %>

	 <% foreach (var test in Model.InPlan.TestCerts) { %>
    <tr>
        <td><%= Url.UserTestLink(test.UserTest) %> </td>
        <td>
             <%= Html.ActionLinkImage<EditCartController>(
                c => c.ToggleCourse(test.OrderDetailID),
                Urls.Common("bin.gif"),
                "Вернуть в заказ")%>
        </td>
    </tr>
    <% } %>
</table>
