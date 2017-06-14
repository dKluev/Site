<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Student>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<% if(Model == null || Model.Student_ID == 0) return;%>

<% var card = Model.Card; %>
<% if (card != null) { %>

<ul class="card">
    <li class="verch">
    <%= H.Anchor(SimplePages.FullUrls.RealSpecialist, Images.StudentClabCardNew(card.ClabCardColor_TC).Width(144)) %> 
    <% if (!card.FullNumber.IsEmpty()) { %>
        Карта <span style="color: #000069;">№&nbsp;<%= card.FullNumber %></span>
    <% } %>
    </li>
<% var nextColor = ClabCardColors.NextColor(card.ClabCardColor_TC); %>
<% if (nextColor != null) { %>
<% var count = ClabCardColors.ColorCount(nextColor); %>
    <li>
        <p>Отличный старт! Ваша карта &ndash; <b><%= ClabCardColors.GetName2(card.ClabCardColor_TC) %></b>.<br>
            Чтобы добыть «<%= ClabCardColors.GetName3(nextColor) %>», Вам надо пройти 
            
    <%= H.Anchor(MainMenu.Urls.MainCourses, count + " " + Linguistics.Plural("курс", count.Value )) %>.</p>
        <p style="font-size: 10px;">Знания станут еще доступней вместе с программой «Настоящий специалист»!<br>
            Копите курсы и получайте приятные бонусы и скидки на обучение!<br>
            Узнайте, как сделать учебу
            <%= H.Anchor(SimplePages.FullUrls.RealSpecialist, "выгодней =>") %>
         </p>
    </li>
<% } %>
</ul>

<% }else{ %>
<h3>Вы еще не являетесь обладателем статуса <%= SimpleLinks.RealSpecialist("Настоящий Специалист") %></h3>
<% } %>

