<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<ConflictVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Order.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% var opposite = OrderCustomerType.GetOpposite(Model.CustomerType); %>
<% var orderName =  OrderCustomerType.GetName(Model.CustomerType); %>
<% var oppositeName =  OrderCustomerType.GetName(opposite); %>
    Вы не можете произвести заказ, как <%= orderName%>, 
    так как в данный момент авторизованы
    как <%= oppositeName %>. Зарегистрируйтесь, как 
    <%= Html.ActionLink<AccountController>(x => x.LogOff(Model.CustomerType),
        orderName) %>
        
    или <%=Html.ActionLink<CartController>(oc => oc.Clear(), "очистите корзину")%>.
    

</asp:Content>
