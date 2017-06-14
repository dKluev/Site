<%@ Page Title="" Language="C#" MasterPageFile="HiddenField.Master"  Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<object>>" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<asp:Content ContentPlaceHolderID="Label" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="Input" runat="server">
    <div>
        <%=Html.Hidden(Model.Name,Model.Value) %>    
    </div> 
</asp:Content>
