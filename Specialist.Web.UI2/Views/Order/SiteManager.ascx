<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Employee>" %>
    <div class="attention">
        <strong>Ваш персональный менеджер</strong> - <%= Html.ManagerLink(Model.Employee_TC.ToLower(),
													 Model.FullName) %>.
        <p>
            Все вопросы по организации обучения, подтверждению оплаты, зачислении в группу Вы
            можете задать по <strong>тел. +7(495) 780-48-48,+7(495) 232-32-16</strong>, или по электронной почте -
            <%= HtmlControls.MailTo(Model.EMail) %></p>
    </div>