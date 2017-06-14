<%@ Page Title="" Language="C#" MasterPageFile="Field.Master" Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<string>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
    <%=Html.TextArea(Model.Name,Model.Value,
                new { @class = "textarea_history" })%>
</asp:Content>
