<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/EditTemplates/Field.Master" Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<bool>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
    <%=Html.RadioButton(Model.Name,true, Model.Value) %> �������
    <%=Html.RadioButton(Model.Name,false, !Model.Value) %> �������
</asp:Content>
