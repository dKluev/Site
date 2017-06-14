<%@ Page Title="" Language="C#" MasterPageFile="Field.Master"  Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<string>>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
<%--    <input type="file" class="file" size="50" name="<%= Model.Name %>" />  --%>
    <%= Html.File(Model.Name).Class("file").Size(50) %>  
</asp:Content>
