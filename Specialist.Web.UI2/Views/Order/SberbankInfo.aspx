<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SberbankInfoVM>" %>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>���������� ��� ��������� ���������</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%= Htmls.Title("�������� ������") %>
<% Html.RenderPartial(PartialViewNames.SiteManager, Model.Manager); %>
    <ol>
        <li>
            <p> 
                ����������� ��������� �����, ����� ������ "��" ����� ���� ��� <a href="#sber-form">��������� �����</a>.</p>
        </li>
        <li>
            <p>
                ��������� ����������� ������ � ������������ �� ������������� ������, ������� �������
                ��� �� ����� ������ �����.</p>
        </li>
        <li>
            <p>
                �������� ��������� �� ������:<br />
                - � ����� ��������� ���������<br />
                - ����� <a href="/Terminals">��������� ���������</a>.</p>
            <p>
                �� ��������� ������ ��� ��� ������ ����� ���������.
������ ��� ��������!</p>
            <p>
               ������������ ���� ������ ��������� � 3 ���.</p>
            <div class="attention2">
                <p>
                    <table>
                        <tr>
                            <td>
                                <span style="font-size: 75px; color: #095E86; padding-right: 20px;" class="attentionsign">
                                    !</span>
                            </td>
                            <td>
                                ���� ��� ��������� ������ �� ��������� <a href="/special-offers/reserve">������</a>, �� ��������� ����� �������� � ��� ��, ��� �� ��������� ����. � ��������� ������ ���� �������� ��������� ���������� � ��� ����� ����� ������� ����� �����. ������ ������ ��� ���� ����� ��������� ������. 
                            </td>
                        </tr>
                    </table>
                </p>
            </div>
        </li>
            <li>����� ������ �������� ���������� ����� �� ����������� ���� �������, �������� ���������, ���� ������ �� ������, ��������� � ������ (��. �.2),, � �� ������ ������� �������� ���������� ��������� ��������� � �������.</li>
            <li>�� ��������� ���������� ���������� ��� �������� � ������.
� ���������� � ������ � ������ ���� ������ ������� ��� ��������� ��� ��������-�����������. ���� �� �� �����-���� ������� �� �������� ������-���������� � ���������� �� �����, ����������, ��������� � ����� ����������. </li>
            <li>������ �� �������� � �������� ����� ����� ��������� �� �����������. </li>
        </ol>
    ���� ��� �� ��������!
	<div id="sber-form"></div>
		<% using (Html.DefaultForm<OrderController>(c => c.SberbankInfo(0))) { %>
            <%= Html.ValidationSummary() %>
            <%= Html.ControlFor(x => x.UserAddress.ForSberbank) %>
            <%= Html.HiddenFor(x => x.OrderID) %>
            
            <% Htmls.FormSection("��������� ��� ������������ ���������", () => {%> 
            <%= Html.ControlFor(x => x.UserAddress.City) %>
            <%= Html.ControlFor(x => x.UserAddress.Index) %>
            <%= Html.ControlFor(x => x.UserAddress.Address) %>
            <% }); %> 
            <% Htmls.FormSection("������� ��� ����� ��������� � ����", () => {%> 
            <%= Html.ControlFor(x => x.Contacts.Phone) %>
            <%= Html.ControlFor(x => x.Contacts.Mobile) %>
            <%= Html.ControlFor(x => x.Contacts.WorkPhone) %>
            <% }); %>
            <%= Htmls.Submit("ok") %>
         <% } %>


</asp:Content>
