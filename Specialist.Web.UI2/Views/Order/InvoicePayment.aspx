<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Print.Master" Inherits="System.Web.Mvc.ViewPage<CartVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% var startDate = DateTime.Now.DefaultString(); %>
<% var endDate = DateTime.Now.AddDays(5).DefaultString(); %>
<% var companyName = Model.Order.User.Company.CompanyName; %>
   <p>&nbsp;

<% var org = Model.OurOrg; %>
<% var bank = Model.Bank; %>
<div align="center">
  <table border="0" width="645" cellspacing="0" cellpadding="3">
    <tr>
      <td width="100%" style="border: 1 solid #000000">
        <table border="0" width="100%" cellspacing="0" cellpadding="0">
          <tr>
            <td valign="top" width="80" class="newsb"><font color="#000000">���������:</font></td>
            <td class="head2"><font color="#000000"><%= org.FullName %></td>

          </tr>
        </table>
      </td>
    </tr>
    <tr>
      <td width="100%" style="border: 1 solid #000000">
        <table border="0" width="100%" cellspacing="0" cellpadding="0">
          <tr>
            <td width="80" class="newsb" valign="top"><font color="#000000">����. �����:
            </td>

            <td class="newsb">
			<%= org.LegalAddress %>
             </td>
          </tr>
        </table>
      </td>
    </tr>
    <tr>
      <td width="100%" class="head2" style="border: 1 solid #000000"><font color="#000000">���
        <%= org.INN %>&nbsp; / ��� <%= org.KPP %>&nbsp;�������� �� ��������������� ������������ �<%= org.LicenceNumber %><br/>

        ��� �����: <%= org.OKVED %> ��� ����: <%= org.OKPO %></font></td>
    </tr>
    <tr>
      <td width="100%" class="head2" style="border: 1 solid #000000"><font color="#000000">�/�
        <%= bank.SettlementAccount %>, �/� <%= bank.CorrespondentAccount %>, ��� <%= bank.BIK %><br/>����.�����:
        </font><font color="#000000"><%= bank.BankName %></font></td>
    </tr>
  </table>
</div>

<div align="center">
  <table border="0" width="650" cellpadding="4" cellspacing="0">
    <tr>
      <th width="650" colspan="3" class="heading6">
        <p><font color="#000000">���� �&nbsp;<%= Model.Order.InvoiceNumber %></font></th>
    </tr>
    <tr>
      <td class="form" nowrap><font color="#4D4D4D">����<br/>

        ��������. ��</font></td>
      <td width="100%" class="form" colspan="2"><%= startDate %><br/>
      <%= endDate %></td>
    </tr>
    <tr>
      <td class="form"><font color="#4D4D4D">����������</font></td>
      <td class="head2" colspan="2">
        <font color="#000000"><%= Model.Order.User.Company.CompanyName %></font>

      </td>
    </tr>
    <tr>
      <td class="form" nowrap><font color="#4D4D4D">������� �����</font></td>
      <td colspan="2" class="form"></td>
    </tr>
    <tr>
      <td valign="top" class="form" colspan="3">

        
              <table border="1" cellspacing="0" cellpadding="4" bgcolor="#E4E4E4" width="100%" bordercolor="#666666">
                <tr>
          
                  <td class="head2" bgcolor="#E4E4E4" width="340" > <font color="#000000">�������� �� �����</font> </td>
          
                  <th class="head2" bgcolor="#E4E4E4" > <font color="#000000">�.�.�.</font> </th>
                  <th class="head2" bgcolor="#E4E4E4" ><font color="#000000">����</font></th>

                  <th class="head2" bgcolor="#E4E4E4" nowrap ><font color="#000000">���-��</font></th>
                  <th class="head2" bgcolor="#E4E4E4" ><font color="#000000">�����</font></th>
                </tr>
                <% foreach (var orderDetail in Model.CourseAndTrackCourseOrderDetails) { %>


                 <tr>     
                  <td class="news" valign="top" bgcolor="#FFFFFF">
    				<b><%= orderDetail.Course.Name %> </b>
					</td>
                  <td class="news" valign="top" bgcolor="#FFFFFF">
                    <%= orderDetail.OrgStudents %>&nbsp;
                  </td>

                  <td class="news" valign="bottom" align="right" bgcolor="#FFFFFF" nowrap>
                  <b><%= orderDetail.PriceWithDiscount.MoneyString() %></b> ���.</td>
                  <th valign="bottom" class="form" bgcolor="#FFFFFF">
                    <%= orderDetail.Count %>&nbsp;
                  </th>
                  <td valign="bottom" class="news" align="right" bgcolor="#FFFFFF" nowrap>
                  <b><%= ((decimal)orderDetail.Count * 
                         orderDetail.PriceWithDiscount).MoneyString() %></b>
                   ���.
                  </td>

                </tr>
                <% } %>
              <tr>
                  <td class="news" valign="top" bgcolor="#FFFFFF" colspan="2">&nbsp;</td>
                  <th class="form" valign="bottom" bgcolor="#E4E4E4" colspan="2">
                  <font color="#000000">����� � ������:</font></th>
                  <td class="news" valign="bottom" class="form" align="right" bgcolor="#FFFFFF" nowrap>
                  <b><%= Model.Order.TotalPriceWithDescount.MoneyString() %></b> ���.


                  </td>

                </tr>
              </table>
      </td>
    </tr>

    <tr>
      <td class="form" colspan="3" align="right"><font color="#000000">��� �� ���������&nbsp;</font></td>
    </tr>

    <tr>
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr>
      <td class="head2"><font color="#4D4D4D">��������</font></td>
      <td width="100" style="border-bottom: 1 solid #C0C0C0">
        &nbsp;
      </td>

      <td class="head2" valign="bottom"><i><font color="#000000"><%= Model.BossFullName %></font></i></td>
    </tr>
    <tr>
      <td class="head2"><font color="#4D4D4D">�������<br/>
        ���������</font></td>
      <td style="border-bottom: 1 solid #C0C0C0">
        &nbsp;

      </td>
      <td class="head2" valign="bottom"><i><font color="#000000"><%= Model.AccounterFullName %></font></i></td>
    </tr>
    <tr>
      <td class="head2" colspan="3"><font color="#000000">������ ���������� ����� �������� ������ �������� � �������� ������� �������� � ������, 
      �������������� �� ������ <%= H.Anchor(SimplePages.FullUrls.Offert) %>  </font></td>
    </tr>
  </table>
</div>

<hr width="650" noshade size="1" color="#000000">
<div align="center">
  <center>
  <table border="0" width="648" class="news">
    <tr>
      <td colspan="4" class="news" width="640">
        <p align="center" class="newsb">��������!</p>
        <p align="center">� ��.
        ��������� ������� ������� �.�. ���������.
        ����� � ������ ������������� ������
        ����� ������.<br/>
        ����-������� � ������������� �� ��������� �������� ����� �������� �� ������� ���� � 10
        �� 19 ����� �� ������������
        ������������ �����������-����������� � ������� ��������.</p>

        <p align="center" class="newsb">������� ���������� ���������� ��������� (��������):
<hr width="650" noshade size="1" color="#000000">
      </td>
    </tr>
    <tr>
      <td colspan="3" width="510"></td>
      <th width="124">������</th>
    </tr>
    <tr>

      <td valign="top" width="73"><b>����������</b></td>
      <td colspan="2" width="431"><font color="#000000">��� <%= org.INN %></font>
        <font color="#000000"><%= org.ShortName %></font></td>
      <td width="124"><font color="#000000"><%= bank.SettlementAccount %></font><br/>
        ��.�</td>
    </tr>

    <tr>
      <td width="73"></td>
      <td width="280"><font color="#000000"><%= bank.BankName %></font></td>
      <td width="145"><font color="#000000"><%= bank.BIK %></font></td>
      <td width="124"><font color="#000000"><%= bank.CorrespondentAccount %></font><br/>
        ��.�</td>
    </tr>

  </table>
  </center>
</div>
<hr width="650" noshade size="1" color="#000000">
<p align="center">&nbsp;
</p>
<p align="center">&nbsp;
</p>
<p align="center">&nbsp;
</p>
<p align="center">&nbsp;
</p>
<p align="center">&nbsp;
</p>
<p align="center">&nbsp;

</p>
<p align="center">&nbsp;
</p>
<p align="center" class="Heading6"><font color="#000000">���������� � ��������-������
 � <%= Model.Order.OrderID %></font>
</p>
<div align="center">
  <center>
  <table border="0" width="650" class="text">
    <tr>
      <td colspan="2">�.������</td>

    <td colspan="2">
      <p align="right"><%= DateTime.Now.DefaultString() %></td>
  </tr>
  <tr>
    <td colspan="4" class="text">
      <p align="justify">��������&nbsp; <b><%= companyName %></b>

      <p align="justify">� ����
      ____________________________________________________________________________<p align="justify">&nbsp;������������ �� ���������
      __________________________________________________________
      <p align="justify">����������� - <font color="#000000">��� �����������</font>
      (��������������� �������� �<font color="#000000">024563</font>) � ����  ��������� <%= Model.BossFullName %>,
      ������������ �� ��������� ������ , � <nobr>��������(�)�:</nobr><br/>
         <b> </b> <br/>
				<br/>��������� ��������� ������� � �������������:

			
      <p align="justify">1. �������� �������� ����������� �������� ��������� �� �������������:<br/>

		<% foreach (var orderDetail in Model.CourseOrderDetails) { %>
			<b><%= orderDetail.Course.Name %></b> <br/>
		
		<% } %>


              
	� ��������� �������� ��� ��������
      ��&nbsp;<b><%= endDate %></b>.<br/>
      ����� � ������:&nbsp;<b><%= Model.Order.TotalPriceWithDescount.MoneyString() %></b> ���.
      <font size="1">(��� �� ����������)</font>

      
      <p align="justify">2. ����������� ��������� �� ����������� ������ �� ��� �/� ������� ��������� �� ������������� ������������� � ���
      �������� ���������� �������� ������ ���
      ������������� �������������� �������.</p>
      
      <p align="justify">3. ��������� ��������� �������� ������� � ������������� ������ ������ ���� �������� �� �������������
      �������������.</p>

      <p align="justify">4. �� ������������ ��� ������������ ���������� ������������ �� ���������� �������� ������� ����� ���������������
      � ������������ � ����������� �����������������.</p>
      
      <p align="justify">5. ���������� ��������� � ������ ������<p>
      <b>
      ��������: &nbsp;<%= companyName %></b><br/>&nbsp;

      <br/>��� _________________ , ��� _________________ , �/� ___________________________________</p>
      <p>� ___________________________________________________________________________
      ,</p>
      <p>��� _________________ , �/�___________________________________ ,</p>
      <p>����� ______________ ,
      ���� _________________ ,</p>
      <p>����������� ����� _________________________________________________________________ ,</p>
      <p>����������� ����� __________________________________________________________________ ,</p>

      <p>���. ______________</p>
      <p>
      <b>
      �����������:<br/>
      
      </b>
      <font color="#000000"><%= org.FullName %></font>,<br/>
      ��� <font color="#000000"><%= org.INN %> / ��� <%= org.KPP %></font>, �/� <font color="#000000"><%= bank.SettlementAccount %></font> � <font color="#000000"><%= bank.BankName %></font>

      �/�
      <font color="#000000"><%= bank.CorrespondentAccount %></font>,<br/>
      ���
      <font color="#000000"><%= bank.BIK %></font> ����� <%= org.OKVED %>, ���� <font color="#000000"><%= org.OKPO %></font>, ���.780-48-48 / ���� 780-48-49
      
      </p>
      
    </td>
  </tr>

  <tr>
    <td class="form" colspan="4">&nbsp;&nbsp;
      <p>&nbsp;</p>
    </td>
  </tr>
  <tr>
    <td class="form" width="33%"><b>�����������
      </b>

      <p>&nbsp;</p>
    </td>
    <td colspan="2" class="form" width="33%"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><b>��������</b>
      <p>&nbsp;</p>
    </td>
    <td class="form" width="33%"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><b>���������
      </b>
      <p>&nbsp;</p>
    </td>

  </tr>
  <center>
  <tr>
    <td>________________ <br/><%= Model.BossFullName %></td>
    <td colspan="2"><b>&nbsp;&nbsp;&nbsp;&nbsp;</b><b>&nbsp;&nbsp;&nbsp;&nbsp;</b><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>________________/</td>
    <td><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>________________/ </td>
  </tr>

  <tr>
    <td class="news">��</td>
    <td colspan="2" class="news"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>��</td>
    <td> <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b> </td>
  </tr>
  <tr>
    <td colspan="4">
        
<% if(Model.NearestGroups.Any()){ %>
        <br/>
        <hr/>
<h3>��������� ������</h3>
    
    <% foreach (var group in Model.NearestGroups) { %>
    <p>
        
		<%= group.DateBeg.ShortString() %>
		<%= group.Course.WebName %> 
    <% if(group.Discount.HasValue){ %>
		<%= group.Discount %>% ������
    <% } %>
    </p>
    <% } %>
<% } %>
    </td>

  </tr>
  </table>
  </center>
</div>
    


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>����-�������</title>
</asp:Content>
