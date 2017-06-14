<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<UserExamQuestionnaire>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>

<%= Html.ValidationSummary() %>
<% Htmls.FormSection(" ", () => {%> 
    <%= Html.ControlFor(x => x.FirstName) %>
    <%= Html.ControlFor(x => x.LastName) %>
    <%= Html.ControlFor(x => x.MiddleInitial) %>
    <%= Html.ControlFor(x => x.Country) %>
    <%= Html.ControlFor(x => x.City) %>
    <%= Html.ControlFor(x => x.PostalCode) %>
    <%= Html.ControlFor(x => x.Street) %>
    <%= Html.ControlFor(x => x.House) %>
    <%= Html.ControlFor(x => x.Flat) %>
    <%= Html.ControlFor(x => x.PrometricCode) %>
    <%= Html.ControlFor(x => x.McpCode) %>
<% }); %> 
    <%-- <fieldset>
            <legend>Анкета</legend>
       
    <%= Html.ValidationSummary("Ошибка") %>
    <%= Html.HiddenFor(x => x.UserID) %>
            <p>
                <label for="SylvanID">SylvanID:</label>
                <%= Html.TextBox("SylvanID", Model.SylvanID) %>
                <%= Html.ValidationMessage("SylvanID", "*") %>
            </p>
            <p>
                <label for="FirstName">FirstName:</label>
                <%= Html.TextBox("FirstName", Model.FirstName) %>
                <%= Html.ValidationMessage("FirstName", "*") %>
            </p>
            <p>
                <label for="LastName">LastName:</label>
                <%= Html.TextBox("LastName", Model.LastName) %>
                <%= Html.ValidationMessage("LastName", "*") %>
            </p>
            <p>
                <label for="MiddleInitial">MiddleInitial:</label>
                <%= Html.TextBox("MiddleInitial", Model.MiddleInitial) %>
                <%= Html.ValidationMessage("MiddleInitial", "*") %>
            </p>
            <p>
                <label for="Flat">Flat:</label>
                <%= Html.TextBox("Flat", Model.Flat) %>
                <%= Html.ValidationMessage("Flat", "*") %>
            </p>
            <p>
                <label for="City">City:</label>
                <%= Html.TextBox("City", Model.City) %>
                <%= Html.ValidationMessage("City", "*") %>
            </p>
            <p>
                <label for="Country">Country:</label>
                <%= Html.TextBox("Country", Model.Country) %>
                <%= Html.ValidationMessage("Country", "*") %>
            </p>
            <p>
                <label for="PostalCode">PostalCode:</label>
                <%= Html.TextBox("PostalCode", Model.PostalCode) %>
                <%= Html.ValidationMessage("PostalCode", "*") %>
            </p>
            <p>
                <label for="PhoneNumber">PhoneNumber:</label>
                <%= Html.TextBox("PhoneNumber", Model.PhoneNumber) %>
                <%= Html.ValidationMessage("PhoneNumber", "*") %>
            </p>
            <p>
                <label for="House">House:</label>
                <%= Html.TextBox("House", Model.House) %>
                <%= Html.ValidationMessage("House", "*") %>
            </p>
            <p>
                <label for="Street">Street:</label>
                <%= Html.TextBox("Street", Model.Street) %>
                <%= Html.ValidationMessage("Street", "*") %>
            </p>
            <p>
                <%= HtmlControls.Submit("Сохранить") %>
            </p>
        </fieldset>--%>