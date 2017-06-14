<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Order.ViewModel.ExpressOrderCompleteVM>" %>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Order.Logic" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context.Logic" %>
<%@ Import Namespace="Specialist.Entities.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <strong>Информация о заказе отправлена менеджеру. В ближайшее время с вами свяжутся.</strong>

</asp:Content>
