<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<City>>" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>������ ���������</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%= Htmls.Title("������ ��������� � ������� ���������� � ����������� �������� ������") %>
<% Html.RenderPartial(PartialViewNames.SiteManager, ViewData["Manager"]); %>
  �������� �������� �� ������:
  <ul>
  <li>
  � ����� ������������ ����� �� ������: �. ������, ������������ ��������, �. 4/6, 2 ����, �. ���������� <a href="//cdn.specialist.ru/Content/File/Complex/Map/stilobat.html">(�����
�������)</a>. ����� ������: �� ������ - � 9.30 �� 19.00, �� �������� - � 9.30 �� 17.00
  </li>
  <li>
 �� ���� <a href="/Locations">������� ����������</a> ������:<br><br> <strong><a href="/locations/complex/stilobat">��������</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/stilobat.html" >����� �������</a>),  <strong><a href="/locations/complex/baumanskiy">����������</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/baumanskiy.html" >����� �������</a>),   <strong><a href="/locations/complex/radio">�����</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/radio.html" >����� �������</a>),  <strong><a href="/locations/complex/belorussko-savelovskii">����������-�����������</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/belorussko-savelovskii.html" >����� �������</a>),  <strong><a href="/locations/complex/taganskii">���������</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/taganskii.html" >����� �������</a>),  <strong><a href="/locations/complex/park-pobedy">���� ������</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/park-pobedy.html" >����� �������</a>), <strong> <a href="/locations/complex/vernadskii">����������</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/vernadskii.html" >����� �������</a>),  <strong><a href="/locations/complex/polezhaevskii">������������</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/polezhaevskii.html" >����� �������</a>). 
  </li>
  </ul>
  <% ViewBag.HideWebinarPartner = true; %>
  <% Html.RenderPartial(PartialViewNames.Cities); %>

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

���� ��� �� ��������!


</asp:Content>
