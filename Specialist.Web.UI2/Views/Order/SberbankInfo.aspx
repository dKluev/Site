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
	<title>Информация для квитанции сбербанка</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%= Htmls.Title("Сбербанк России") %>
<% Html.RenderPartial(PartialViewNames.SiteManager, Model.Manager); %>
    <ol>
        <li>
            <p> 
                Распечатать квитанцию можно, нажав кнопку "ОК" после того как <a href="#sber-form">заполните форму</a>.</p>
        </li>
        <li>
            <p>
                Сохраните электронное письмо с инструкциями по подтверждению оплаты, которое выслано
                Вам от имени нашего сайта.</p>
        </li>
        <li>
            <p>
                Оплатить квитанции Вы можете:<br />
                - В любом отделении Сбербанка<br />
                - Через <a href="/Terminals">платежные терминалы</a>.</p>
            <p>
                На квитанции указан код для оплаты через терминалы.
Оплата без комиссии!</p>
            <p>
               Максимальный срок оплаты квитанции – 3 дня.</p>
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
        </li>
            <li>После оплаты обучения необходимо сразу же подтвердить факт платежа, позвонив менеджеру, либо пройдя по ссылке, высланной в письме (см. п.2),, и на первое занятие принести оплаченную квитанцию Сбербанка и паспорт.</li>
            <li>На основании полученной информации Вас зачислят в группу.
О зачислении в группу и точной дате начала занятий Вас оповестит наш менеджер-консультант. Если Вы по какой-либо причине не получили письмо-оповещение о зачислении на курсы, пожалуйста, свяжитесь с вашим менеджером. </li>
            <li>Оплата за экзамены и школьные курсы через терминалы не принимается. </li>
        </ol>
    Ждем Вас на обучение!
	<div id="sber-form"></div>
		<% using (Html.DefaultForm<OrderController>(c => c.SberbankInfo(0))) { %>
            <%= Html.ValidationSummary() %>
            <%= Html.ControlFor(x => x.UserAddress.ForSberbank) %>
            <%= Html.HiddenFor(x => x.OrderID) %>
            
            <% Htmls.FormSection("Заполните для формирования квитанции", () => {%> 
            <%= Html.ControlFor(x => x.UserAddress.City) %>
            <%= Html.ControlFor(x => x.UserAddress.Index) %>
            <%= Html.ControlFor(x => x.UserAddress.Address) %>
            <% }); %> 
            <% Htmls.FormSection("Телефон для связи менеджера с вами", () => {%> 
            <%= Html.ControlFor(x => x.Contacts.Phone) %>
            <%= Html.ControlFor(x => x.Contacts.Mobile) %>
            <%= Html.ControlFor(x => x.Contacts.WorkPhone) %>
            <% }); %>
            <%= Htmls.Submit("ok") %>
         <% } %>


</asp:Content>
