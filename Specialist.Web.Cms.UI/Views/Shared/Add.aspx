<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EditVM>" %>
<%@ Import Namespace="Specialist.Web.Cms.Core.FluentMetaData.Attributes"%>
<%@ Import Namespace="Specialist.Web.Cms.Util"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Reflection"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Cms.Core"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title><%= Model.MetaData.DisplayName() %>[создание]</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% var listBackUrl = HtmlControls.Anchor(Url.Action("List", 
       Model.EntityTypeName + Common.ControlPosfix, 
       new {pageIndex = 1}), "Вернуться к списку"); %>
  <table><tr> <td >
            <%= listBackUrl %>
    </td> </tr> </table>
    <div class="ui-widget-content ui-corner-all">
        <%= CmsHtmls.ContentWithIcon(CmsHtmls.Icons.Add.ToString(),
            (Model.MetaData.DisplayName()).Tag("h3") ) %>
     </div>
     <br />


    <fieldset class="ui-corner-all ui-widget-content">
        <legend>Свойства</legend>
    <% using (Html.BeginForm()) {%>
        <%= Html.Hidden("QueryString") %>
        <% Html.RenderPartial("PropertyGrid", Model); %>
        <p>
            <input type="submit" value="<%= OperationType.Save %>" name="operationType" />
            <input type="submit" value="<%= OperationType.Apply %>" name="operationType" />
        </p>


    <% } %>
    </fieldset>

    <div>
        <%= listBackUrl %>
    </div>

</asp:Content>

