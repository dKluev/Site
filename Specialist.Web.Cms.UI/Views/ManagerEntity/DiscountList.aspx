<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DiscountListVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.ViewModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Скидки</h2>
     <%= Html.ValidationSummary() %>
     <% using (Html.BeginForm()) { %>
               <p>
                    <label for="<%= Model.For(x => x.GroupID) %>">Группа:</label>
                    <%= Html.TextBoxFor(x => x.GroupID) %>
                </p>
              <p>
                    <label for="<%= Model.For(x => x.StudentID) %>">Выпускник:</label>
                    <%= Html.TextBoxFor(x => x.StudentID) %>
                 
                </p>
                <%-- <p>
                    <label for="<%= Model.For(x => x.PriceType_TC) %>">
                        Тип цены:
                    </label>
                    <%= Html.TextBoxFor(x => x.PriceType_TC) %>
                 
                </p>--%>
                <p>
                    <%= HtmlControls.Submit("Показать скидки") %>
                </p>

       
    <% } %>
    <% if(Model.Student != null) { %>
        Выпускник: 
         <%= string.Format("{0} {1} {2}", Model.Student.LastName, Model.Student.FirstName, 
           Model.Student.MiddleName) %>
        <br />
    <% } %>
    
    <% if(Model.Group != null) { %>
        Группа: 
        <%= Model.Group.DateBeg.NotNullToString("dd.MM.yyyy") %>
        <br />
    <% } %>
    <% if(Model.Discounts.Count == 0) { %>
        Скидок нет
    <% } %>
    
    <% foreach (var discount in Model.Discounts) { %>
          <% if(discount.MarketingAction != null) { %>
              Акция: 
            <%= discount.MarketingAction.Name %> 
          <% } %>
          <br />
          <% if(discount.PercentValue.HasValue) { %>
            Скидка: <%= discount.PercentValue %>%<br />
          <% } %>
          <% if(discount.MoneyValue.HasValue) { %>
              Деньги: <%= discount.MoneyValue %> руб.<br /> 
          <% } %>
          <% if(discount.Present != null) { %>
            Подарок: <%= discount.Present.Name %> <br />
          <% } %>
          
          
    <% } %>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Скидки</title>
</asp:Content>
