<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Context.Order>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="block">
    <div class="text">
        <h1>Квитанция Сбербанка</h1>
        <%= Html.ActionLink<OrderController>(c => c.Receipt(Model.OrderID), "Квитанция") %>
    </div>
</div> 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
