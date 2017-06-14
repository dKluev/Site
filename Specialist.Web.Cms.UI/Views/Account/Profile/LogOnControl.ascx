<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Passport.ViewModel.LogOnVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Html"%>

 <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm("LogOn", "Account")) { %>
              
                <%= Html.HiddenFor(x => x.ReturnUrl) %>
               <p>
                    <label for="<%= Model.For(x => x.Email) %>">e-mail:</label>
                    <%= Html.TextBoxFor(x => x.Email) %>
                 
                </p>
                 <p>
                    <label for="<%= Model.For(x => x.Password) %>">Пароль:</label>
                    <%= Html.PasswordFor(x => x.Password) %>
                </p>
                <p>
                    <%= Html.CheckBoxFor(x => x.Remeber) %> 
                    <label class="inline" for="<%= Model.For(x => x.Remeber) %>">
                    Запомнить?</label>
                </p>
                <p>
                    <input type="submit" value="Войти" />
                </p>

       
    <% } %>