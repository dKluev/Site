<%@ Page Title="" Language="C#" MasterPageFile="Field.Master"  Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM>" %>
<%@ Import Namespace="SimpleUtils.Common.Enum" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
<% var enumValue = (Enum)((dynamic)Model).Value; %>
<% var enumType = enumValue.GetType(); %>
      <% foreach (var value in Enum.GetValues(enumType).Cast<Enum>().Where(x => Convert.ToInt32(x) != 0)) { %>
	  
    <%= HtmlControls.CheckBox(Model.Name, enumValue.HasFlag(value) , 
        value) %> <%= EnumUtils.GetDisplayName(value) %> <br /> <br />
  
	<% }  %>
</asp:Content>