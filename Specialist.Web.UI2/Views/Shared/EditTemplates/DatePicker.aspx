<%@ Page Title="" Language="C#" MasterPageFile="Field.Master" 
Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM>" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="DynamicForm" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
 <% var tModel = Model as PropertyVM<object>; %>
 <% var value = tModel != null ? tModel.Value : Model.As<PropertyVM<DateTime?>>().Value.DefaultString(); %>
<%=Html.TextBox(Model.Name,value,new {@id=Model.Name}) %>
	<script language="javascript">
	    $(function () {
    		$("#<%=Model.Name%>").datepicker();
    	});
	</script>

</asp:Content>
