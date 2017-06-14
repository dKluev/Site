<%--
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="MvcContrib.FluentHtml.ModelViewPage<SmartGroupChoiceVM>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="MvcContrib.FluentHtml"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Планирование обучения</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using(Html.BeginForm()){ %>
<div>
    <%= Html.RadioButtonFor(x => x.Type, SmartChoiceType.Fast) %> Быстро <br />
    <%= Html.RadioButtonFor(x => x.Type, SmartChoiceType.Economic) %> Экономично <br />
    <%= Html.RadioButtonFor(x => x.Type, SmartChoiceType.Custom) %> Параметры <br />
    <%= Html.CheckBoxList(Model.For(x => x.DayOfWeeks), Model.GetWeekDays()) %>
    <%= Html.DropDownListFor(x => x.ComplexTC, Model.GetComplexList()) %><br />
    <%= Html.DropDownListFor(x => x.DayShiftTC, Model.GetDayShifts()) %><br />
    <%= HtmlControls.Submit("Подобрать") %>
</div>
<% } %>

<% if(Model.IsResult) { %>
    <% foreach (var orderDetail in Model.OrderTrack.OrderDetails
           .Where(od => od.Group_ID.HasValue)) { %>
        <% Model.SetFullFill(orderDetail.Group); %> 
        <% if(!Model.FullFillComplex) { %>
            Комплекс исключен из параметров <br />
        <% } %>
        <% if(!Model.FullFillDayShift) { %>
            Режим обучения исключен из параметров <br />
        <% } %>
        <% if(!Model.FullFillWeekDays) { %>
            Дни недели исключены из параметров <br />
        <% } %>
        <%= orderDetail.Course.Name %> <br />    
        <%= orderDetail.Group.DateInterval %> <br />    
        <%= orderDetail.Group.Complex.Name %> <br />    
        <%= orderDetail.Group.DayShift.Name %> <br />    
        <%= orderDetail.Group.Group_ID %> <br />    
      <%--  <% Html.RenderPartial(PartialViewNames.CalendarControl, 
           orderDetail.Group.Lectures.Select(l => l.LectureDateBeg)); %>♦1♦
    <% } %>
    
    <% using(Html.BeginForm<EditCartController>(c => c.SelectGroups(null))){ %>
    <% var orderDetails = Model.OrderTrack.OrderDetails; %>
    <% for (var i = 0; i < orderDetails.Count; i++) { %>
        <%= this.Hidden(x => orderDetails[i].OrderDetailID) %>
        <%= this.Hidden(x => orderDetails[i].Group_ID) %>
    <% } %>    
    <%= this.SubmitButton("Сохранить") %>
<% } %>

<% } %>


</asp:Content>
--%>
