<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<ChangeStatusVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if(Model.IsStudent) { %>
        Ваш статус "Выпускник центра".
    <% }else{ %>
            
             <% using (Html.DefaultForm<ProfileController>(c => c.ChangeStatus())) { %>
                <% Htmls.FormSection("Введите информацию из памятки выпускника", () => {%>
                    <%= Html.ValidationSummary() %>
                    <%= Html.ControlFor(x => x.WebLogin) %>
                    <%= Html.ControlFor(x => x.WebKeyword) %>
                <% }); %>
                	<%= Htmls.Submit("ok") %>
            <% } %>
    <% } %>
</asp:Content>
