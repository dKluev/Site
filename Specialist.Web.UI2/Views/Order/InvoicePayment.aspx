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
            <td valign="top" width="80" class="newsb"><font color="#000000">Поставщик:</font></td>
            <td class="head2"><font color="#000000"><%= org.FullName %></td>

          </tr>
        </table>
      </td>
    </tr>
    <tr>
      <td width="100%" style="border: 1 solid #000000">
        <table border="0" width="100%" cellspacing="0" cellpadding="0">
          <tr>
            <td width="80" class="newsb" valign="top"><font color="#000000">Юрид. адрес:
            </td>

            <td class="newsb">
			<%= org.LegalAddress %>
             </td>
          </tr>
        </table>
      </td>
    </tr>
    <tr>
      <td width="100%" class="head2" style="border: 1 solid #000000"><font color="#000000">ИНН
        <%= org.INN %>&nbsp; / КПП <%= org.KPP %>&nbsp;Лицензия на образовательную деятельность №<%= org.LicenceNumber %><br/>

        Код ОКВЭД: <%= org.OKVED %> Код ОКПО: <%= org.OKPO %></font></td>
    </tr>
    <tr>
      <td width="100%" class="head2" style="border: 1 solid #000000"><font color="#000000">Р/С
        <%= bank.SettlementAccount %>, К/С <%= bank.CorrespondentAccount %>, БИК <%= bank.BIK %><br/>Наим.банка:
        </font><font color="#000000"><%= bank.BankName %></font></td>
    </tr>
  </table>
</div>

<div align="center">
  <table border="0" width="650" cellpadding="4" cellspacing="0">
    <tr>
      <th width="650" colspan="3" class="heading6">
        <p><font color="#000000">Счет №&nbsp;<%= Model.Order.InvoiceNumber %></font></th>
    </tr>
    <tr>
      <td class="form" nowrap><font color="#4D4D4D">Дата<br/>

        действит. до</font></td>
      <td width="100%" class="form" colspan="2"><%= startDate %><br/>
      <%= endDate %></td>
    </tr>
    <tr>
      <td class="form"><font color="#4D4D4D">Плательщик</font></td>
      <td class="head2" colspan="2">
        <font color="#000000"><%= Model.Order.User.Company.CompanyName %></font>

      </td>
    </tr>
    <tr>
      <td class="form" nowrap><font color="#4D4D4D">Предмет счета</font></td>
      <td colspan="2" class="form"></td>
    </tr>
    <tr>
      <td valign="top" class="form" colspan="3">

        
              <table border="1" cellspacing="0" cellpadding="4" bgcolor="#E4E4E4" width="100%" bordercolor="#666666">
                <tr>
          
                  <td class="head2" bgcolor="#E4E4E4" width="340" > <font color="#000000">Обучение по курсу</font> </td>
          
                  <th class="head2" bgcolor="#E4E4E4" > <font color="#000000">Ф.И.О.</font> </th>
                  <th class="head2" bgcolor="#E4E4E4" ><font color="#000000">Цена</font></th>

                  <th class="head2" bgcolor="#E4E4E4" nowrap ><font color="#000000">Кол-во</font></th>
                  <th class="head2" bgcolor="#E4E4E4" ><font color="#000000">Сумма</font></th>
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
                  <b><%= orderDetail.PriceWithDiscount.MoneyString() %></b> руб.</td>
                  <th valign="bottom" class="form" bgcolor="#FFFFFF">
                    <%= orderDetail.Count %>&nbsp;
                  </th>
                  <td valign="bottom" class="news" align="right" bgcolor="#FFFFFF" nowrap>
                  <b><%= ((decimal)orderDetail.Count * 
                         orderDetail.PriceWithDiscount).MoneyString() %></b>
                   руб.
                  </td>

                </tr>
                <% } %>
              <tr>
                  <td class="news" valign="top" bgcolor="#FFFFFF" colspan="2">&nbsp;</td>
                  <th class="form" valign="bottom" bgcolor="#E4E4E4" colspan="2">
                  <font color="#000000">Итого к оплате:</font></th>
                  <td class="news" valign="bottom" class="form" align="right" bgcolor="#FFFFFF" nowrap>
                  <b><%= Model.Order.TotalPriceWithDescount.MoneyString() %></b> руб.


                  </td>

                </tr>
              </table>
      </td>
    </tr>

    <tr>
      <td class="form" colspan="3" align="right"><font color="#000000">НДС не взимается&nbsp;</font></td>
    </tr>

    <tr>
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr>
      <td class="head2"><font color="#4D4D4D">Директор</font></td>
      <td width="100" style="border-bottom: 1 solid #C0C0C0">
        &nbsp;
      </td>

      <td class="head2" valign="bottom"><i><font color="#000000"><%= Model.BossFullName %></font></i></td>
    </tr>
    <tr>
      <td class="head2"><font color="#4D4D4D">Главный<br/>
        бухгалтер</font></td>
      <td style="border-bottom: 1 solid #C0C0C0">
        &nbsp;

      </td>
      <td class="head2" valign="bottom"><i><font color="#000000"><%= Model.AccounterFullName %></font></i></td>
    </tr>
    <tr>
      <td class="head2" colspan="3"><font color="#000000">Оплата настоящего счета означает полное согласие и принятие условий Договора – Оферты, 
      расположенного по адресу <%= H.Anchor(SimplePages.FullUrls.Offert) %>  </font></td>
    </tr>
  </table>
</div>

<hr width="650" noshade size="1" color="#000000">
<div align="center">
  <center>
  <table border="0" width="648" class="news">
    <tr>
      <td colspan="4" class="news" width="640">
        <p align="center" class="newsb">Внимание!</p>
        <p align="center">В пл.
        поручении укажите Фамилию И.О. слушателя.
        Места в группе резервируются только
        после оплаты.<br/>
        Счет-фактуру и Свидетельство об окончании обучения можно получить по рабочим дням с 10
        до 19 часов по предъявлении
        доверенности организации-плательщика и личного паспорта.</p>

        <p align="center" class="newsb">Образец заполнения платежного поручения (фрагмент):
<hr width="650" noshade size="1" color="#000000">
      </td>
    </tr>
    <tr>
      <td colspan="3" width="510"></td>
      <th width="124">КРЕДИТ</th>
    </tr>
    <tr>

      <td valign="top" width="73"><b>Получатель</b></td>
      <td colspan="2" width="431"><font color="#000000">ИНН <%= org.INN %></font>
        <font color="#000000"><%= org.ShortName %></font></td>
      <td width="124"><font color="#000000"><%= bank.SettlementAccount %></font><br/>
        Сч.№</td>
    </tr>

    <tr>
      <td width="73"></td>
      <td width="280"><font color="#000000"><%= bank.BankName %></font></td>
      <td width="145"><font color="#000000"><%= bank.BIK %></font></td>
      <td width="124"><font color="#000000"><%= bank.CorrespondentAccount %></font><br/>
        Сч.№</td>
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
<p align="center" class="Heading6"><font color="#000000">Приложение к договору-оферте
 № <%= Model.Order.OrderID %></font>
</p>
<div align="center">
  <center>
  <table border="0" width="650" class="text">
    <tr>
      <td colspan="2">г.Москва</td>

    <td colspan="2">
      <p align="right"><%= DateTime.Now.DefaultString() %></td>
  </tr>
  <tr>
    <td colspan="4" class="text">
      <p align="justify">Заказчик&nbsp; <b><%= companyName %></b>

      <p align="justify">в лице
      ____________________________________________________________________________<p align="justify">&nbsp;действующего на основании
      __________________________________________________________
      <p align="justify">Исполнитель - <font color="#000000">ОЧУ «Специалист»</font>
      (Государственная лицензия №<font color="#000000">024563</font>) в лице  директора <%= Model.BossFullName %>,
      действующего на основании Устава , и <nobr>Слушател(и)ь:</nobr><br/>
         <b> </b> <br/>
				<br/>заключили настоящий договор о нижеследующем:

			
      <p align="justify">1. Заказчик поручает Исполнителю обучение Слушателя по специализации:<br/>

		<% foreach (var orderDetail in Model.CourseOrderDetails) { %>
			<b><%= orderDetail.Course.Name %></b> <br/>
		
		<% } %>


              
	и обязуется оплатить это обучение
      до&nbsp;<b><%= endDate %></b>.<br/>
      Итого к оплате:&nbsp;<b><%= Model.Order.TotalPriceWithDescount.MoneyString() %></b> руб.
      <font size="1">(НДС не облагается)</font>

      
      <p align="justify">2. Исполнитель обязуется по поступлении оплаты на его р/с обучить Слушателя по вышеназванной специализации и при
      успешном завершении обучения выдать ему
      свидетельство установленного образца.</p>
      
      <p align="justify">3. Слушатель обязуется посещать занятия и добросовестно пройти полный курс обучения по вышеназванной
      специализации.</p>

      <p align="justify">4. За неисполнение или ненадлежащее исполнение обязательств по настоящему договору стороны несут ответственность
      в соответствии с действующим законодательством.</p>
      
      <p align="justify">5. Банковские реквизиты и адреса сторон<p>
      <b>
      Заказчик: &nbsp;<%= companyName %></b><br/>&nbsp;

      <br/>ИНН _________________ , КПП _________________ , р/с ___________________________________</p>
      <p>в ___________________________________________________________________________
      ,</p>
      <p>БИК _________________ , к/с___________________________________ ,</p>
      <p>ОКВЭД ______________ ,
      ОКПО _________________ ,</p>
      <p>Юридический адрес _________________________________________________________________ ,</p>
      <p>Фактический адрес __________________________________________________________________ ,</p>

      <p>Тел. ______________</p>
      <p>
      <b>
      Исполнитель:<br/>
      
      </b>
      <font color="#000000"><%= org.FullName %></font>,<br/>
      ИНН <font color="#000000"><%= org.INN %> / КПП <%= org.KPP %></font>, р/с <font color="#000000"><%= bank.SettlementAccount %></font> в <font color="#000000"><%= bank.BankName %></font>

      к/с
      <font color="#000000"><%= bank.CorrespondentAccount %></font>,<br/>
      БИК
      <font color="#000000"><%= bank.BIK %></font> ОКВЭД <%= org.OKVED %>, ОКПО <font color="#000000"><%= org.OKPO %></font>, тел.780-48-48 / факс 780-48-49
      
      </p>
      
    </td>
  </tr>

  <tr>
    <td class="form" colspan="4">&nbsp;&nbsp;
      <p>&nbsp;</p>
    </td>
  </tr>
  <tr>
    <td class="form" width="33%"><b>Исполнитель
      </b>

      <p>&nbsp;</p>
    </td>
    <td colspan="2" class="form" width="33%"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><b>Заказчик</b>
      <p>&nbsp;</p>
    </td>
    <td class="form" width="33%"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b><b>Слушатель
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
    <td class="news">МП</td>
    <td colspan="2" class="news"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>МП</td>
    <td> <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b> </td>
  </tr>
  <tr>
    <td colspan="4">
        
<% if(Model.NearestGroups.Any()){ %>
        <br/>
        <hr/>
<h3>Ближайшие группы</h3>
    
    <% foreach (var group in Model.NearestGroups) { %>
    <p>
        
		<%= group.DateBeg.ShortString() %>
		<%= group.Course.WebName %> 
    <% if(group.Discount.HasValue){ %>
		<%= group.Discount %>% скидка
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
    <title>Счет-фактура</title>
</asp:Content>
