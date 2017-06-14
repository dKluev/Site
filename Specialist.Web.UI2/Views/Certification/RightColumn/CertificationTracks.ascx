<%@  Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<TrackDiscount>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>

<%= Htmls.BorderBegin("Комлексные программы") %>
<ul class="block_r_ul">
<% foreach (var trackDiscount in Model) { %>
    <li>
        <%=Html.CourseLink(trackDiscount.Track)%><br />
        <span class="old_price"><%= trackDiscount.Price.MoneyString() %></span>
        <span class="red"><%= trackDiscount.DiscountPrice.MoneyString() %></span>
    </li>
<% } %>

</ul>
<%= Htmls.BorderEnd %>



