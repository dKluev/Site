<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p><h2>Письмо консультанту</h2></p>
<div class="lecturers">
<div class="student_opinion">
Ваше письмо консультанту Службы трудоустройства успешно отправлено. Мы обязательно ответим на Ваши вопросы по e-mail, указанный при регистрации. 
Вы также можете позвонить в Службу трудоустройства и записаться на индивидуальную консультацию. Контакты Службы трудоустройства:<br /><br />
</div>
</div>
<div style="text-align: left;">
<p><img width="100" hspace="10" align="left" alt="Контакты" src="//cdn2.specialist.ru/Content/Image/SimplePage/contacts.jpg"></p>
Служба трудоустройства Центра «Специалист» доступна для Вас в учебном комплексе «<a target="_blank" href="/Locations/Complex/polezhaevskii">Полежаевский</a>».
<p><strong>Телефон:</strong> +7 (495) 926-25-59 (для соискателей и работодателей)</p>
<p><strong>E-mail:</strong> <a href="mailto:job@specialist.ru">job@specialist.ru</a></p>
<p><strong>Адрес:&nbsp;</strong> ст. м. «Полежаевская»,                                                                 ул. 4-я Магистральная, д. 11, 6-й этаж.</p>
<p style="text-align: left;"><a target="_blank" href="//cdn.specialist.ru/Content/File/Complex/Map/polezhaevskii.html">Подробная схема прохода к комплексу от метро и проезда на автомобиле</a></p>
<p><strong>Приемные дни: </strong>для соискателей &mdash; среда с <strong>10.00</strong> до <strong>17.30</strong>&nbsp;<br>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;для работодателей &mdash; с понедельника по пятницу с <strong>10.00</strong> до <strong>17.30</strong></p>
<strong style="color: red;">* </strong>Служба трудоустройства работает только для частных лиц, прошедших обучение за собственный счет.</div></asp:Content>
