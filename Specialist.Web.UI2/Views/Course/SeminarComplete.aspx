<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Catalog.ViewModel.SeminarCompleteVM>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    ���� ������ �� ������� � ��������/������������ <%= Model.GroupSeminar.Group.Title %>
     �������. � ��������� ����� �� ��� ����������� ����� ����� ���������� ������ � �������������� ����������� �� �������. �� ����������� �������� ����������� - 
    <%= HtmlControls.MailTo("info@specialist.ru")%> <br />
    ����� ���� ������ ��� �� ����� �����������!


   
</asp:Content>
