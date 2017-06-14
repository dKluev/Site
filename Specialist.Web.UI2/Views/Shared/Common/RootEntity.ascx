<%@ Control Language="C#" Inherits="ViewUserControl<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="SimpleUtils.Reflection"%>
<%@ Import Namespace="Specialist.Entities.Core"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Reflection.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<% var isSimplePage = Model.Entity is SimplePage; %>

<div class='link_block<%= isSimplePage ? "2" : "3" %>'>
    <% var anchor = Html.GetLinkFor(Model.Entity); %>
    <%= H.Anchor(StringUtils.GetHref(anchor), Images.EntitySmall(Model.Entity).ToString())  %>
    
    <h3> <%= anchor %></h3>
	<% if (Model.List.Any()) { %>
    <ul>
       <% foreach (var entity in Model.List) { %>
        <li>

			<% if (entity is City) { %>
		      <strong><%= Html.GetLinkFor(entity)%></strong>
		    <% }else{ %>
		      <%= Html.GetLinkFor(entity)%>
			  <% var isNew = entity.As<Product>().GetOrDefault(x => x.IsNew) 
                            || entity.As<Course>().GetOrDefault(x => x.IsNew); %>
			  <% var isHit = entity.As<Product>().GetOrDefault(x => x.IsHit) 
                            || entity.As<Course>().GetOrDefault(x => x.IsHit); %>
			  <%= isNew ? Images.Main("new.png").Style("float:none;"):null %>
            <%= isHit ? Images.Main("hit-small.png").Style("float:none;"):null %>
		    <% } %>
        </li>
    <% } %>
    <% if (!isSimplePage) { %>
    <% var link = Html.GetLinkFor(Model.Entity);
       link = Regex.Replace(link, ">.*<", ">Все курсы<"); %>
    <li><strong><%= link %> </strong></li>
    <% } %>

    </ul>
	<% } %>
</div>


