<%@ Page Title="" Language="C#" MasterPageFile="Field.Master" 
Inherits="ViewPage<DynamicForm.PropertyVM<IEnumerable<SelectListItem>>>" %>
<asp:Content ContentPlaceHolderID="Input" runat="server">
    <%=Html.DropDownList(Model.Name,Model.Value, new {@class = "select"})%>
</asp:Content>
