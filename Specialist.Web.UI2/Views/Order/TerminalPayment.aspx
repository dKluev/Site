<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Context.Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="block">
        <div class="all">
            Коды для оплаты через терминал
            <% foreach (var orderDetail in Model.OrderDetails) { %>
                <%= orderDetail.StudentInGroup_ID %>
            <% } %>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
