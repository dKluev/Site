<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DiscountListVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.ViewModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>������</h2>
     <%= Html.ValidationSummary() %>
     <% using (Html.BeginForm()) { %>
               <p>
                    <label for="<%= Model.For(x => x.GroupID) %>">������:</label>
                    <%= Html.TextBoxFor(x => x.GroupID) %>
                </p>
              <p>
                    <label for="<%= Model.For(x => x.StudentID) %>">���������:</label>
                    <%= Html.TextBoxFor(x => x.StudentID) %>
                 
                </p>
                <%-- <p>
                    <label for="<%= Model.For(x => x.PriceType_TC) %>">
                        ��� ����:
                    </label>
                    <%= Html.TextBoxFor(x => x.PriceType_TC) %>
                 
                </p>--%>
                <p>
                    <%= HtmlControls.Submit("�������� ������") %>
                </p>

       
    <% } %>
    <% if(Model.Student != null) { %>
        ���������: 
         <%= string.Format("{0} {1} {2}", Model.Student.LastName, Model.Student.FirstName, 
           Model.Student.MiddleName) %>
        <br />
    <% } %>
    
    <% if(Model.Group != null) { %>
        ������: 
        <%= Model.Group.DateBeg.NotNullToString("dd.MM.yyyy") %>
        <br />
    <% } %>
    <% if(Model.Discounts.Count == 0) { %>
        ������ ���
    <% } %>
    
    <% foreach (var discount in Model.Discounts) { %>
          <% if(discount.MarketingAction != null) { %>
              �����: 
            <%= discount.MarketingAction.Name %> 
          <% } %>
          <br />
          <% if(discount.PercentValue.HasValue) { %>
            ������: <%= discount.PercentValue %>%<br />
          <% } %>
          <% if(discount.MoneyValue.HasValue) { %>
              ������: <%= discount.MoneyValue %> ���.<br /> 
          <% } %>
          <% if(discount.Present != null) { %>
            �������: <%= discount.Present.Name %> <br />
          <% } %>
          
          
    <% } %>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>������</title>
</asp:Content>
