<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<MyResponses>" %>
<%@ Import Namespace="MvcContrib.UI.Pager"%>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="DynamicForm.Mvc.Extensions"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model.Responses.Any()) { %>
       <% Html.RenderPartial(Views.Shared.Common.ResponseList, Model.Responses); %>
       <%= Html.GetNumericPagerPretty(Model.Responses)  %>
    <% } else { %>
        Пока ничего нет
    <% } %>
</asp:Content>
