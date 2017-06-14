<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<City>>" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Оплата наличными</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%= Htmls.Title("Оплата наличными в учебных комплексах и центральной приемной Центра") %>
<% Html.RenderPartial(PartialViewNames.SiteManager, ViewData["Manager"]); %>
  Оплатить обучение Вы можете:
  <ul>
  <li>
  В кассе центрального офиса по адресу: г. Москва, Госпитальный переулок, д. 4/6, 2 этаж, м. Бауманская <a href="//cdn.specialist.ru/Content/File/Complex/Map/stilobat.html">(схема
проезда)</a>. Режим работы: по будням - с 9.30 до 19.00, по субботам - с 9.30 до 17.00
  </li>
  <li>
 Во всех <a href="/Locations">учебных комплексах</a> Центра:<br><br> <strong><a href="/locations/complex/stilobat">Стилобат</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/stilobat.html" >схема проезда</a>),  <strong><a href="/locations/complex/baumanskiy">Бауманский</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/baumanskiy.html" >схема проезда</a>),   <strong><a href="/locations/complex/radio">Радио</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/radio.html" >схема проезда</a>),  <strong><a href="/locations/complex/belorussko-savelovskii">Белорусско-Савеловский</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/belorussko-savelovskii.html" >схема проезда</a>),  <strong><a href="/locations/complex/taganskii">Таганский</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/taganskii.html" >схема проезда</a>),  <strong><a href="/locations/complex/park-pobedy">Парк Победы</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/park-pobedy.html" >схема проезда</a>), <strong> <a href="/locations/complex/vernadskii">Вернадский</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/vernadskii.html" >схема проезда</a>),  <strong><a href="/locations/complex/polezhaevskii">Полежаевский</a></strong> (<a href="//cdn.specialist.ru/Content/File/Complex/Map/polezhaevskii.html" >схема проезда</a>). 
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
                                Если Вам назначена скидка по программе <a href="/special-offers/reserve">Резерв</a>, то квитанцию нужно оплатить в тот же, или на следующий день. В противном случае срок действия квитанции закончится и Вам нужно будет сделать новый заказ. Размер скидки при этом будет рассчитан заново. 
                            </td>
                        </tr>
                    </table>
                </p>
            </div>

Ждем Вас на обучение!


</asp:Content>
