<%@ Page Title="" Language="C#" MasterPageFile="Field.Master" Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<bool>>" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
    <%=Html.CheckBox(Model.Name,Model.Value) %>
</asp:Content>
