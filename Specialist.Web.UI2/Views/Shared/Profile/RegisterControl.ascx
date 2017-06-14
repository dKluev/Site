<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Passport.ViewModel.RegisterVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Utils.Logic" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="MvcContrib.UI.InputBuilder.Views" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>

<%= Htmls2.JQueryDatePicker %>

        <% if (CouponUtils.RegistrationIsActive) { %>
<div class="block_yellow" style="text-align:center;">
<p style="font-size:120%; margin-bottom:5px;"><strong>����������������� � �������� ����� <span style="color:#f00;">�� 500 ������</span>.  ����� ����� ������ ��� �� ����� ��� ���������� ���� ����� ������. </strong></p>
<p style="margin-top:0;"><a href="/center/marketingaction/action-500"><strong>������� ����������� �������</strong></a></p>
</div>
        <% } %>
    <%= HttpUtility.HtmlDecode(Html.ValidationSummary().NotNullString()) %>

    <% using (Html.DefaultForm<ProfileController>(c => c.RegisterPost(null), new {id = "register-form"})) { %>
        <%= Html.ControlFor(x => x.NextUrl) %>
        <%= Html.ControlFor(x => x.CustomerType) %>
        
        
        <% if(Model.IsCompany) { %>
            <% Htmls.FormSection("���������� � ��������", () => {%> 
                <%= Html.ControlFor(x => x.User.Company.CompanyName) %>
                <%= Html.ControlFor(x => x.User.Company.INN) %>
                <%= Html.ControlFor(x => x.User.Company.KPP) %>
                <%= Html.ControlFor(x => x.User.Company.LegalAddress) %>
            <% }); %>
           
			
        <% } %>
       
       
        <% Htmls.FormSection(Model.IsCompany ? "���������� ����":"������ ������ <b style='color:black;'>(���������)</b>", () => {%> 
            <%= Html.ControlFor(x => x.User.LastName) %>
            <%= Html.ControlFor(x => x.User.FirstName) %>
            <%= Html.ControlFor(x => x.User.SecondName) %>
            <%= Html.ControlFor(x => x.User.Sex) %>
            <%= Html.ControlFor(x => x.User.BirthDate) %>
            <%= Html.SelectFor(x => x.User.WorkBranch_ID, Model.WorkBranches, "�� �������") %>
            <%= Model.IsCompany ? null : Html.SelectFor(x => x.User.Metier_ID, new List<WorkBranch>()) %>
            
        <% }); %>
         <% Htmls.FormSection("���������� ����������", () => {%> 
            <%= Html.SelectFor(x => x.UserAddress.CountryID, Model.Countries) %>
            <%= Html.ControlFor(x => x.UserAddress.City) %>

            <% if(Model.IsCompany) { %>
                <%= Html.ControlFor(x => x.UserAddress.Address) %>
            <% } %>
       

            <%= Html.ControlFor(x => x.Phone) %>
        <% }); %>
      
        <% Htmls.FormSection("����������� ����������", () => {%> 
            <%= Html.ControlFor(x => x.User.Email) %>
            <% if (Model.User.IsFacebook) { %>
            <%= Html.HiddenFor(x => x.User.Password) %>
            <%= Html.HiddenFor(x => x.User.FbUserId) %>
            <%= Html.HiddenFor(x => x.User.FbToken) %>
            <% }else{ %>
            <%= Html.ControlFor(x => x.User.Password) %>
            <% } %>
            <%= Html.SelectFor(x => x.User.Source_ID, Model.Sources, " ") %>
        <% }); %>
        
        <% Htmls.FormSection("������������", () => {%> 
            <%= Html.ControlFor(x => x.CaptchaText) %>
        <% }); %>

      <%= Html.EditorFor(x => x.PersonalData) %> <%= H.Anchor(SimplePages.FullUrls.SoglasiePersDannye, "�������� �� ��������� ������������ ������").OpenInDialog().Data("width", "800") %> 
        
      <%= Htmls.Submit("ok") %>
        
    <% } %>