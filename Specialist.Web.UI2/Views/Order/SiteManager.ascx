<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Employee>" %>
    <div class="attention">
        <strong>��� ������������ ��������</strong> - <%= Html.ManagerLink(Model.Employee_TC.ToLower(),
													 Model.FullName) %>.
        <p>
            ��� ������� �� ����������� ��������, ������������� ������, ���������� � ������ ��
            ������ ������ �� <strong>���. +7(495) 780-48-48,+7(495) 232-32-16</strong>, ��� �� ����������� ����� -
            <%= HtmlControls.MailTo(Model.EMail) %></p>
    </div>