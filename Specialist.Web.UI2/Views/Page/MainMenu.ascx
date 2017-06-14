<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Tuple<List<MainMenu>,int>>" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<div class="menu_main">
    <ul>
        <% foreach (var mainMenu in Model.Item1) { %>
        <li>
            <%= mainMenu.Url != null ? H.Anchor(mainMenu.Url, mainMenu.Name)
                /*.Class(mainMenu.IsActions ? "sale" : "")*/.ToString() 
                    : H.span[mainMenu.Name].ToString() %>
            <%= mainMenu.SubMenu != null ? H.Ul(mainMenu.SubMenu.Select(x => H.Anchor(x.Url, x.Name)))
                    .Class("menu_small") : null %>
<%--            <% if (mainMenu.IsActions) { %>--%>
<%--            <p class="count-action"><%= Model.Item2 %></p>--%>
<%--            <% } %>--%>
        </li>
        <% } %>
    </ul>
</div>
