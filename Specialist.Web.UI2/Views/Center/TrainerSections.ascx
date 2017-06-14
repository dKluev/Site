<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<List<Section>>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Core"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SimpleUtils.Extension" %>
<% if(Model.Any()) { %>

	    <div class="tab_content">

        <% foreach (var entityWithLists in 
               Model.Select(x => Grouping.New(x,x.SubSections))
               .Cast<IGrouping<Section,Section>>().ToList().GetColumns(2,3)) { %>
        <div class="tab_2column">
            <% foreach (var sectionKey in entityWithLists) { %>
                <% var section = sectionKey.Key; %>
                <% if(section == null){ %>
                    <% break; %> 
                <% } %>
                <div class="link_block2">
                    <%= Images.EntitySmall(section) %>
                    <h3> <%= Url.Center().SectionTrainers(section.UrlName, section.Name) %> </h3>
                    <%= H.Ul(section.SubSections.Select(x => Url.Center().SectionTrainers(x.UrlName, x.Name))) %>
                </div>
            <% } %>   
        </div>
        <% } %>
        
	    </div>

<% } %>
