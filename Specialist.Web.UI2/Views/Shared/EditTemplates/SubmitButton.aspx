<%@ Page Title="" Language="C#" MasterPageFile="Field.Master"  Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<object>>" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ContentPlaceHolderID="Label" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Input" runat="server">
    <%= HtmlControls.Submit("Сохранить") %>
</asp:Content>
