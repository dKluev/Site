<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Specialist.Entities.Catalog.Links.CourseLink>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<% var count = 2; %>
<% if(!Model.Any()) return; %>
<%= Htmls2.ChamBegin() %>
<%= Htmls2.Menu2("Вы просмотрели курсы") %>
<%= H.Ul(Model.Take(count).Select(x => Html.CourseLink(x))).Class("square_blue") %>
<% if (Model.Count > 3) { %>
    <%= Htmls.ShowOnClick("more-visited-courses","Остальные", 
    H.Ul(Model.Skip(count).Select(x => Html.CourseLinkAnchor(x.UrlName, x.WebName)
        .FluentUpdate(a => a.Href(a.Attribute("href").Value +"?src=yousaw" )))).Class("square_blue").ToString()
    
) %>
<% } %>

<%= Htmls2.BlockEnd() %>

