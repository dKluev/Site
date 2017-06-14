<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ListVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Cms.MetaData.Utils"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Cms.Util"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>[<%= Model.MetaData.DisplayName() %>]</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="ui-widget-content ui-corner-all">
    <table>
        <tr>
<%--            <td width="20px"> <%= CmsHtmls.Icons.Table %> </td>--%>
             <% if(!Model.MetaData.IsReadOnly() && !Model.MetaData.IsNotAdd()) { %>
             <td width="20px"> 
                <div class="button">
                <% var titleForAdd = string.Format("Добавить"); %>
                <%= HtmlControls.Anchor(Url.Action("Add", Model.MetaData.EntityType.Name +
                    Common.ControlPosfix, 
                    Model.GetFilterRouteValues()), CmsHtmls.Icons.Add.ToString())
                        .Attr(new {title = titleForAdd}) %>
                </div>
            </td>
            <% } %>
             <td> <b> [<%= Model.MetaData.DisplayName() %>] </b> </td>
            
        </tr>
    </table>
</div>
<br />
    
    
    <% Html.RenderPartial("FilterControl", Model); %>
    <%// Html.RenderPartial("JqGridControl", Model); %>
    <% Html.RenderPartial("ListControl", Model); %>

</asp:Content>

