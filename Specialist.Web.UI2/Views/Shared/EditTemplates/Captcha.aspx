<%@ Page Title="" Language="C#" MasterPageFile="Field.Master"  Inherits="System.Web.Mvc.ViewPage<DynamicForm.PropertyVM<string>>" %>
<%@ Import Namespace="System.Web.Mvc.Html"%>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Input" runat="server">
    <%= HtmlControls.Image(Url.Action<ChartController>(c => c.Captcha()) + "?r=" + DateTime.Now.ToString("ss-fff")) %> <br />
    <br />
    <%=Html.TextBox(Model.Name, Model.Value, new {size = 37}) %>
</asp:Content>