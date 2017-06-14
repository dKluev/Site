<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Student>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<% if(Model == null || Model.Student_ID == 0) return;%>
<% var card = Model.Card; %>
<% var nextColor = ClabCardColors.Blue; %>
<% if (card != null) { %>
<%= H.Anchor(SimplePages.FullUrls.RealSpecialist, Images.StudentClabCard(card.ClabCardColor_TC).FloatLeft()) %> 
<h3>Настоящий <%= ClabCardColors.GetName(card.ClabCardColor_TC) %> Специалист</h3>
<b>
<% if (card.FullNumber.IsEmpty()) { %>
Получить Вашу карту Настоящего специалиста Вы можете в центральной приемной или любом из учебных комплексов.
<% }else{ %>
Номер карты - <%= card.FullNumber %>
<% } %>
</b>



<br/>
<% nextColor = ClabCardColors.NextColor(card.ClabCardColor_TC); %>
<% }else{ %>
<h3>Вы еще не являетесь обладателем статуса <%= SimpleLinks.RealSpecialist("Настоящий Специалист") %></h3>
<% } %>
<% if (nextColor != null) { %>
Чтобы получить <%= H.b[ClabCardColors.GetName(nextColor)] %> статус, Вам нужно закончить 
<% var count = ClabCardColors.ColorCount(nextColor); %>
<%= H.Anchor(MainMenu.Urls.MainCourses,
       count + " " + Linguistics.Plural("курс", count.Value )) %>.
<% } %>
<div class="clear"></div>