<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Profile.EditProfileVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Cms.Root.Socials" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>


<%= Htmls2.JQueryDatePicker %>

<%= Html.ValidationSummary() %>

    <% using (Html.DefaultForm<ProfileController>(c => c.EditProfile(), 
           Htmls.FormWithFile)) { %>
        <%= Html.ControlFor(x => x.IsCompany) %>
        
        <% if(Model.IsCompany) { %>
            <% Htmls.FormSection("Информация о компании", () => {%> 
                <%= Html.ControlFor(x => x.User.Company.CompanyName) %>
                <%= Html.ControlFor(x => x.User.Company.INN) %>
                <%= Html.ControlFor(x => x.User.Company.KPP) %>
                <%= Html.ControlFor(x => x.User.Company.LegalAddress) %>
            <% }); %>
        <% } %>
            <% Htmls.FormSection("Личные данные", () => {%> 
<%--        <% if(!Model.User.IsStudent) { %>--%>
                <%= Html.ControlFor(x => x.User.LastName) %>
                <%= Html.ControlFor(x => x.User.FirstName) %>
                <%= Html.ControlFor(x => x.User.SecondName) %>
                <%= Html.ControlFor(x => x.User.BirthDate) %>
       <%-- <% }else{ %>
            <tr> <td class="name">ФИО</td>
                <td class="field">
                	<%= Model.User.FullName %> <br/>
			<%= Url.Link<ProfileController>(c => c.ChangeNameRequest(),"Запрос на смену ФИО") %>
                	 
                </td></tr>
        <% } %>--%>
                <%= Html.ControlFor(x => x.User.EngFullName) %>
            <% }); %>
        
        <% Htmls.FormSection("Почтовый адрес", () => {%> 
            <%= Html.SelectFor(x => x.UserAddress.CountryID, Model.Countries) %>
            <%= Html.ControlFor(x => x.UserAddress.City) %>
            <%= Html.ControlFor(x => x.UserAddress.Index) %>
            <%= Html.ControlFor(x => x.UserAddress.Address) %>
        <% }); %>
        
        <% Htmls.FormSection("Телефоны", () => {%> 
            <%= Html.ControlFor(x => x.Contacts.Phone) %>
            <%= Html.ControlFor(x => x.Contacts.Mobile) %>
            <%= Html.ControlFor(x => x.Contacts.WorkPhone) %>
        <% }); %>
            
        <% if(!Model.IsCompany) { %>
        <% Htmls.FormSection("Блоги и социальные сети", () => {%> 
            <% for(var i = 0; i < Model.Contacts.Socials.Count; i++) {%>
            <% var contact = Model.Contacts.Socials[i];%>
            <% var name = Model.For(x => x.Contacts.Socials) + "[" + i + "].Contact";%>
            <% var idName = Model.For(x => x.Contacts.Socials) + "[" + i +
                "].ContactTypeID";%>
            <tr>
               <td class="name">
                <label for="<%=name%>">
                    <%=Images.ContactType( contact.UserContactType.ContactTypeID )%>
                    <strong>
                        <%=contact.UserContactType.Name%></strong>
                </label>
               </td>
              
        
                <td class="field">
                    <%=Html.TextBox(name, contact.Contact, new {@class="text"})%>
                    <%=Html.Hidden(idName, contact.ContactTypeID)%>
                    <p class="explanation">
                        Пример: <%= Html.Encode(contact.UserContactType.Example)%></p> 
                </td>
            </tr>
            <% } %>

            <tr> <td class="name"></td>
                <td class="field">
                    <% if (Model.User.FbUserId.IsEmpty()) { %>
                        <%= H.button["Привязать профиль к аккаунту Facebook"].Id("facebook-register") %>
                                        
                        <script>
                            $(function() {
                                $("#facebook-register").button();
                                initFbConnect();
                                function processResponse(response) {
                                    if (response.status === 'connected') {
                                        document.location.href = updateURLParameter("<%= Url.Action<ProfileController>(x => x.LinkFacebook(null)) %>", "token", response.authResponse.accessToken);
                                    }
                                }

                                $("#facebook-register").click(function(e) {
                                    e.preventDefault();
                                    FB.login(processResponse, { scope: '<%= FacebookService.ConnectPermission %>' });
                                });
                            });


                        </script>
                    <% }else { %>
                       <b> Профиль привязан к аккаунту Facebook</b>
                    <% } %>
                    

                </td>
            </tr>

            <% }); %>
        <% Htmls.FormSection("Прочее", () => {%> 
			<% if(Model.User.IsStudent) %>
				<%= Html.ControlFor(x => x.User.HideCourses) %>
			<%= Html.ControlFor(x => x.User.HideContacts) %>
            <%= Html.ControlFor(x => x.Photo) %>
            <tr> <td class="name"></td>
                <td class="field">
                    <%= Images.UserPhoto(Model.User.UserID).Size(100, null) %>
                    <% if(Model.HasPhoto) { %>
                    <br />
                    <%= Html.ActionLink<ProfileController>(c => c.DeletePhoto(), 
                        "Установить фото по умолчанию") %>
                    <% } %>
                </td>
            </tr>


        <% }); %>
            
        <% } %>
       <%= Htmls.Submit("ok") %>

<% } %>