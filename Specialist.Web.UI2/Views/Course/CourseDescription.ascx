<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CourseVM>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>

<h3 style="color:#AC3D4B; font-weight:bold;">
<%= Model.Course.WebNameComment %>
</h3>

<div class="advert" style="width: 100%; background-color: rgb(255, 255, 255);">
<table>
<tbody><tr>
<td align="center" valign="top">
<p>
<%= Images.Entity(Model.Course).Style("margin-left:10px;margin-right:10px").Alt(Model.Course.WebName) %>
</p>

<% if(Model.CompleteCourseCount >= 100 && !CourseTC.HideStudentCount.Contains(Model.Course.Course_TC)) { %>
<p style="text-align:center;font-size:10px;">
Этот курс в нашем Центре <br />
успешно закончили <br />
<span class="discount_color"><%= Model.CompleteCourseCount %></span> человек!
</p>
<% } %>
<%= Html.Site().AddCourseButton(Model) %>

<% var maxDiscount = Model.MaxDiscount; %>
<% if(Model.WebinarDiscounts.ContainsKey(Model.Course.Course_TC)){ %>
    <%= Images.Main("Discount/webinar40.jpg") %>
<% }else{ %>
    <% if(maxDiscount.HasValue && !CourseTC.HalfTracks.ContainsKey(Model.Course.Course_TC)) { %>
    	<%= Url.Link<GroupController>(c => c.WithDiscount(Model.Course.Course_TC), 
     Images.Main("Discount/{0}.png".FormatWith(maxDiscount.Value)).Style("padding-top:5px;")) %>
    <% } %>
<% } %>
</td>
<td valign="top" rowspan="2">
<%= Model.Course.NameOfficialEn.If(x => !x.IsEmpty(), x => H.h3[x]) %>
<% if (Model.Course.Course_TC == "ЭКСЕЛЬ6-А") { %>
    <% if(Htmls.IsSecond) { %>
        <%= Htmls.HtmlBlock(HtmlBlocks.Excel6Desc1) %>
    <% } else if(Htmls.IsThird) { %>
        <%= Htmls.HtmlBlock(HtmlBlocks.Excel6Desc2) %>
    <% } else { %>
        <%= Model.Course.Description %>
    <% } %>
<% } else { %>
<%= Model.Course.Description %>
<% } %>
<br />
<%= Model.ShowCiscoBlock ? Htmls.HtmlBlock(HtmlBlocks.CiscoBlock) : null %>
<%= Model.Course.IsMs ? Htmls.HtmlBlock(HtmlBlocks.MeasureUp) : null %>
<%= Model.Course.IsMs ? Htmls.HtmlBlock(HtmlBlocks.MsEnglish) : null %>
<%= Model.HasPaperBook ? Htmls.HtmlBlock(HtmlBlocks.MsBook) : null %>
<%= Model.Is3dPrint ? Htmls.HtmlBlock(HtmlBlocks.InnovationClass) : null %>

<%= Html.AddThis() %>


</td>
</tr>

</tbody></table>
    <% var menu = _.List(
           Tuple.Create("trainers", "Преподаватели"),
           Tuple.Create("trainers", "Отзывы"),
           Tuple.Create("prerequisites", "Подготовка"),
           Model.HasCertExams ? Tuple.Create("exams", "Экзамены") : null,
           Tuple.Create("contents", "Программа"),
           Tuple.Create("groups", "Расписание"),
           Tuple.Create("prices", "Стоимость"),
           Tuple.Create("documets", "Документы")).Where(x => x != null); %>

<%= H.Div("course-menu")[menu.Select(x => H.span[H.Anchor("#" + x.Item1, x.Item2)]
    .Class("cmenu-item"))] %>
</div>
<br />
<br />

<% if (Model.Course.Course_TC == CourseTC.BuhSem) { %>
  <h3>
  <%= Url.Cart().AddCourseWithSocialLink("Хотите принять участие в семинаре? Узнайте, как записаться со скидкой 50%!") %>
  </h3>  
<br/>
<% } %>
	<div id="trackBlock"> </div>


<% Html.RenderPartial(Views.Shared.Block.ActionsBlock, Model.Actions); %>



<% if(!Model.Course.Introduction.IsEmpty()) { %>
<strong>Цель курса:</strong>
<%= Model.Course.Introduction %>
<% } %>

<div style="float:right; text-align:center; width: 200px; margin: 20px 0 20px 20px;"><a href="/guarantee-quality"><img width="150" height="100" border="0" alt="Гарантия качества" src="//cdn1.specialist.ru/Content/Image/SimplePage/guarantee-quality-1.jpg"></a>
<p class="mark_arrow"><a href="/guarantee-quality"><strong>Мы гарантируем 100% качество обучения!</strong></a></p></div>

  
<% if(!Model.Course.OnComplete.IsEmpty()) { %>
<br />
<strong>По окончании курса Вы будете уметь:</strong>
<%= Model.Course.OnComplete %>

 <% if(!Model.Course.IsSchool) { %>
Специалисты, обладающие этими знаниями и навыками, в настоящее время крайне востребованы. Большинство выпускников наших курсов делают успешную карьеру и пользуются уважением работодателей.

    <% } %>

<br/><br/>
<% } %>

<strong>Продолжительность курса - <%= Model.Course.BaseHours.ToIntString() %> ак. ч.</strong>

