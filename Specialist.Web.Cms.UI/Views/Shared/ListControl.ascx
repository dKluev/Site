<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ListVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Cms.MetaData.Utils"%>
<%@ Import Namespace="Specialist.Web.Cms.Util"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<% var helps = Helps.GetHelp(Model.MetaData.EntityType); %>
 
    <table class="simple-table">
        <tr>
        <% if(!Model.MetaData.IsReadOnly()) { %>
            <th style="width:20px"></th>
        <% } %>
        <% if(Model.MetaData.CanDelete()) { %>
            <th style="width:20px"></th>
        <% } %>
            <% foreach (var property in Model.MetaData.GetProperties()) { %>
            <th class=""> 
            <% if(property.Control() != SimpleUtils.FluentAttributes.Const.Controls.Html) { %>
                <%= Html.ActionLink(property.DisplayName(), "List", new RouteValueDictionary
                    {{"orderColumn.ColumnName", property.Name}, 
                    {"orderColumn.IsDesc", !(Model.OrderColumn.ColumnName == property.Name && 
                        Model.OrderColumn.IsDesc) },
                    {"pageIndex", Model.Entities.PageIndex + 1}}, 
                    new Dictionary<string, object>{{"title",  helps.GetValueOrDefault(property.Name)}}) %> 
                <% if(Model.OrderColumn.ColumnName == property.Name) { %>
                    <% if(Model.OrderColumn.IsDesc) { %>
                        <span style="float:right" class="ui-icon ui-icon-carat-1-s"/>
                    <% }else{ %>
                        <span style="float:right" class="ui-icon ui-icon-carat-1-n"/>
                    <% } %>
                   
                <% } %>
            <% }else{ %>
                <%= property.DisplayName() %>
            <% } %>
            </th>
            <% } %>
             <% foreach (var column in Model.AdditionalColumns) { %>
                <th style="width:50px"> <%= column %> </th>
            <% } %>
            <% foreach (var extraControl in Model.ExtraControls) { %>
                <th style="width:50px"> <%= extraControl.DisplayName %> </th>
            <% } %>
        </tr>

    <% foreach (var row in Model.TableData) { %>
    
        <tr>
        <% if(!Model.MetaData.IsReadOnly()) { %>
            <td>
            <%= HtmlControls.Anchor(Url.Action("Edit", Model.MetaData.EntityType.Name
                + Common.ControlPosfix, new { Id = Server.UrlEncode(row.Id.ToString()) }),
                CmsHtmls.Icons.Edit.ToString()).Attr(new {title ="Изменить"}) %>
            </td>
        <% } %>
        <% if(Model.MetaData.CanDelete()) { %>
            <td>
            <%= CmsHtmls.DeleteButton(Url.Action("Delete",
            Model.MetaData.EntityType.Name, 
                        new { Id = Server.UrlEncode(row.Id.ToString()) }))%>
            </td>
        <% } %>
  

            <% foreach (var value in row.Values) { %>
                    
            <td> 
                <% if(value is bool) {%>
                    <%if ((bool)value) {%>
                    <%= CmsHtmls.CheckIcon((bool)value) %>
                    <% } %>
                <% }else{ %>
                    <%= value %>
                <% } %>
 
             </td>
            
            <% } %>
            
            <% foreach (var extraControl in Model.ExtraControls) { %>
                <td> <% Html.RenderPartial("Controls/" + extraControl.Name, 
                            new ExtraControlVM(Model.MetaData, row.Entity, 
                                extraControl)); %></td>
            <% } %>
        </tr>
    
    <% } %>

    </table>

  

    <%= Html.GetNumericPager(null, Model.Entities.ItemCount, Model.Entities.PageSize, Model.Entities.PageIndex) %>
  