<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<div class="attention">
    Вы пока не выбрали ни одного курса. Выбрать программу обучения можно 
    <%= HtmlControls.Anchor(MainMenu.Urls.MainCourses, "здесь") %>.
</div>
  <%--      <p>
            Пока Вы не запланировали обучение в нашем Центре. Вы можете:</p>
        <ul>
            <li>Выбрать курс из <a href="/">общего каталога</a> </li>
            <li>Выбрать курс со скидкой из наших <a href="#">специальных предложений</a></li>
            <li>Выбрать <a href="#">сертификационный экзамен</a></li>
            <li>Выбрать <a href="#">бесплатный семинар или консультацию</a></li>
            <li>Связаться с <a href="#">нашим менеджером</a> для получения консультации</li>
        </ul>
--%>