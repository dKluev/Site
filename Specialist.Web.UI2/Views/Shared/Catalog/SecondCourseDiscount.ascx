<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.CourseBaseVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>

<% if (Model.SecondCourse == null) return;  %>
<% var discount = Model.SecondCourse; %>
<div class="vygoda">
<p><strong>Ваша выгода может быть <span class="vygoda_red"><%= discount.Discount.MoneyString() %> рублей</span></strong></p>
<table>
  <tbody>
    <tr>
      <td> <%= Images.Course(Model.Course.UrlName) %> <%= Html.CourseLink(Model.Course) %> </td>
      <td><span class="vygoda_blue">+</span></td>
      <td><%= Images.Course(discount.SecondCourse.UrlName) %><%= Html.CourseLink(discount.SecondCourse) %></td>
      <td><span class="vygoda_blue">=&nbsp;<%= discount.SumWithDiscount.MoneyString() %>&nbsp;руб.</span><br />
     <span style="text-decoration:line-through;"> <%= discount.Sum.MoneyString() %>&nbsp;руб.</span></td>
      <td>
          <div style="margin-top: 5px;">
          <%= Html.AddToCart(x => x.AddWithSecondCourse(Model.Course.Course_TC, discount.SecondCourse.CourseTC)) %>
          </div>
<%--          <img src="http://cdn1.specialist.ru/Content/Image/Simplepage/korzina_vygoda.png" style="margin-top:-5px;"/>--%>
      </td>
    </tr>
  </tbody>
</table>

    <% if (Model.HasTracks) { %>
<p> <%= Url.Track().List(Model.Course.Course_TC, "Все варианты комплексного обучения со скидками")
            .Class("all_vygoda") %> </p>
    <% } %>
</div>

