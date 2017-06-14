<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Profile.ViewModel.RealSpecialistVM>" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Center" runat="server">


<div id="content" class="longlist">
<% Html.RenderPartial(Views.Profile.ClabCard, Model.Student ?? new Student()); %>

<br/>
<% var card = Model.Student.GetOrDefault(x => x.Card); %>
<% if (card != null) { %>
<% var color = card.ClabCardColor_TC; %>
    <p>
    <strong>Моя постоянная скидка - <%= ClabCardColors.ColorDiscounts[color] %>%</strong>
    </p>
    <p><strong>Скидка для друзей - 7%</strong> </p>
<% } %>
</div>

</asp:Content>