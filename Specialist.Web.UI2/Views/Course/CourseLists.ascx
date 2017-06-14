<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Core.EntityWithList<Specialist.Entities.Catalog.Interface.IEntityCommonInfo, Specialist.Entities.Catalog.Interface.IEntityCommonInfo>> >" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<% foreach (var entity in Model){ %>
    <% if (Htmls.NamesForHide.Contains(entity.Entity.Name)) { %>
        <div class="ab-test-2-hide ab-test-3-hide">
            <% Html.RenderAction<CourseController>(c => c.CourseListFor(entity.Entity)); %>    
        </div>
        
    <% } else { %>
        <% Html.RenderAction<CourseController>(c => c.CourseListFor(entity.Entity)); %>
    <% } %>
	
<% } %>
<%= H.JQuery("utils.initABTest();") %>
