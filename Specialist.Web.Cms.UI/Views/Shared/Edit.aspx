<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EditVM>" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.Reflection.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Cms.MetaData.Utils"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Cms.Util"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core.Controls"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Reflection"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title><%= Model.MetaData.DisplayName() %> <%= SelectListUtil.GetDisplay(Model.Entity, 
                                                             Model.MetaData) %> [редактирование]</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(function() {
        $("#tabs").tabs({
            fx: { opacity: 'toggle' },
            selected: <%= Model.TabFocus %>
            
        });
        $("#remove-tabs-link").click(function() {
            $("#tabs").tabs('destroy');
            $("iframe").height("400px");
			$("#tabs>ul").hide();
            return false;
        });
    });
</script>
     <table><tr> <td >
       <%= HtmlControls.Anchor(Url.Action("List", 
       Model.EntityTypeName + Common.ControlPosfix, 
       new {pageIndex = 1}), "Вернуться к списку") %>
    </td> </tr> </table>
    <div style="padding:10px" class="ui-widget-content ui-corner-all">
            <%= CmsHtmls.Icons.Edit.Style("float: left") %>
            <% if(!Model.SiteUrl.IsEmpty()) { %>
                <%= HtmlControls.Anchor(Common.SiteDomain + Model.SiteUrl,
                    CmsHtmls.Icon().Class("ui-icon-link").Style("float:left").ToString())%>
            <% } %>
          <%=  (Model.MetaData.DisplayName() + (Model.MetaData.DisplayProperty() != null 
                    ? ": " + Model.MetaData.DisplayProperty().GetValue(Model.Entity)
                    : "")).Tag("strong") %>
                    
<%--        <%= CmsHtmls.ContentWithIcon(CmsHtmls.Icons.Edit.ToString(),
            (Model.MetaData.DisplayName() + (Model.MetaData.DisplayProperty() != null 
                    ? ": " + Model.MetaData.DisplayProperty().GetValue(Model.Entity)
                    : "")).Tag("h3") ) %>
       
            <%= CmsHtmls.Icons.Edit.Style("float: right") %>--%>
     </div>
     <br />

<%--             <% foreach (var extraControl in 
                    Model.MetaData.ExtraControls.OfType<ActionLinkControl>()) { %>
            <td width="20px">               
             <% Html.RenderPartial("Controls/" + extraControl.Name, 
                            new ExtraControlVM(Model.MetaData, Model.Entity, 
                                extraControl)); %>
            </td>
            <% } %>--%>
           
     
  <% if(Model.SiteObject != null) { %>
    <script type="text/javascript">
        $(function() {
            $("#relationDialog").dialog({
                title: 'Теги',
                autoOpen: false,
                height: 400,
                width: 800,
                modal: true
            });

        });
       
        
    </script>
    <div id="relationDialog" style="display:none">
         <% Html.RenderPartial(PartialViewNames.EditRelationListControl, 
             new EditRelationListVM{ForLinkCreator = true, 
                SiteObject = Model.SiteObject}); %>
    </div>

    <% } %>

<div id="tabs">
	<ul>
		<li><a href="#main-tab">Общее</a></li>
		 <% foreach (var extraControl in Model.ExtraControls) { %>
            <li><a href="#<%= extraControl.DisplayName.GetHashCode() %>">
                <%= extraControl.DisplayName %>
            </a></li>  
        <% } %>
		
	</ul>
	<div id="main-tab">
        <% using (Html.BeginForm())
  {%>

    <fieldset class="ui-corner-all ui-widget-content">
        <legend>Свойства</legend>
            <%
      Html.RenderPartial("PropertyGrid", Model);%>
            <%
      if (!Model.MetaData.IsReadOnly()) { %>
            <p>
                 <input type="submit" value="<%=OperationType.Save%>" name="operationType" />
            <input type="submit" value="<%=OperationType.Apply%>" name="operationType" />
            </p>
            <% } %>
        </fieldset>
        <% } %>
    </div>
     <% foreach (var extraControl in 
            Model.ExtraControls.OfType<ActionLinkControl>()) { %>
        <div id="<%= extraControl.DisplayName.GetHashCode() %>">
		    <iframe width="100%" scrolling="auto" height="800px" frameborder="no" 
		        src="<%= Url.Action(extraControl.Action,
    extraControl.Controller, extraControl.GetRouteValues(Model.Entity))%>">
            </iframe>
	    </div>
             
     <% } %>
    
   

</div>
<a id="remove-tabs-link" style="float:right;" href="#">Убрать табы</a>
   

</asp:Content>

