<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Catalog.GroupsForMainVM>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<% if(!Model.Groups.Any()) return; %>
	<div class="h2_over_red png">
		<h2><%= SimpleLinks.GroupDiscounts("Выберите курс со скидкой!") %> </h2>
	</div>
	<%= Htmls2.FreshBegin() %>

	    <ul class="square1">

    <% foreach (var group in Model.Groups){ %>
        <li>
		<span class="date"><%= group.DateBeg.ShortString() %></span>
		<%= Html.CourseLink(group.Course) %> 
		<%= Htmls2.Discount(group,true) %>
        </li>
<% } %>
                </ul>
				<br />
				<b><%= SimpleLinks.GroupDiscounts().Class("block1")	%></b>
                <%= H.Anchor(SimplePages.FullUrls.Discount, "Программа скидок: выбери свою скидку!").Class("block1") %>
					<br />
					<br />
		
	<%= Htmls2.BlockEnd() %>
	