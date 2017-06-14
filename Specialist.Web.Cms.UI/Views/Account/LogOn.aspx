<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LogOnVM>" %>
<%@ Import Namespace="Specialist.Entities.Passport.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Cms.Const" %>
<asp:Content ID="loginHead" ContentPlaceHolderID="head" runat="server">
    <title>¬ход</title>
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>¬ход</h2>
    <div class="ui-widget-content ui-corner-all" style="width:300px">
        <% Html.RenderPartial(PartialViewNames.LogOnControl, Model); %>
    </div>
</asp:Content>
