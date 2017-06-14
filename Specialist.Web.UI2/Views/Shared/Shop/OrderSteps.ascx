<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StepsVM>" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<p class='steps'>
	<%= Url.CoursesLink("Вернуться к покупкам") %> >>
<% var steps = this.GetAll(); %>
<% steps.ForEach((step, index) => {%>
    <% var last = index == steps.Count - 1;  %>
    <% if (step.IsCurrent) {%> 
        <span> <%=step.Link%> </span> 
    <% } else if (step.IsPass) {%>
        <%=step.Link%>          
    <% } else {%>
        <span class='step_f'> <%=step.Link%></span>
    <% }%>
    <% if(!last){ %>
        >>
    <% } %>

<% }); %>
</p>
