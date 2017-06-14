<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Advice>>" %>

<% foreach(var advice in Model){ %>
    <h3> <%= advice.Name %></h3>
    <p>
        <span class="text">
            <%= Html.AdviceLink(advice) %></span>
    </p>
<% } %>
