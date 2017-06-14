<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<SeminarListVM>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<% if(!Model.GroupSeminars.Any()) { %>

Пока ничего нет.
<% return;%>
<% } %>

<% if(Model.Consultation){ %>
<p>
    В рамках программы поддержки выпускников выпускникам Центра предоставляется возможность
    в течение 6 месяцев после окончания курса бесплатно посещать семинары по теме законченного
    курса.
</p>
<% }else{ %>


    <div style="float:left; margin-right:1em; margin-bottom:1em;"><img width="250px" alt="Семинары" src="//cdn.specialist.ru/content/image/course/stoi.jpg" /></div>
<p style="color: #1a405e;">Бесплатные мастер-классы центра "Специалист" - это работающие методики и эффективные лайфхаки, актуальные знания и маленькие секреты от ведущих преподавателей. <strong>Подпишитесь на нашу рассылку и получайте уведомления о будущих мастер-классах.</strong></p>
<!--
<div><img width="100%" alt="Семинары" src="//cdn.specialist.ru/content/image/SimplePage/en-lit.jpg" mce_src="//cdn.specialist.ru/content/image/SimplePage/en-lit.jpg" /></div>
-->
<%--<div style="text-align: center; padding: 20px 20px 0px 20px;"><a href="http://www.specialist.ru/profile/customertypechoice"><img src="//cdn1.specialist.ru/Content/Image/Main/Button/subscr-email-button.png" /></a></div>--%>

<% Html.RenderAction<SimpleRegController>(c => c.RegistrationWidget(Request.Url.AbsoluteUri, "СЕМИНАР")); %>
<p style="color: #07538f; text-align:center;"><strong><a href="/videos">Видеозаписи прошедших семинаров (657)</a></strong></p>
<br />
    <br />
<div style="clear:left; margin-bottom:-2em;">&nbsp;</div>
<h2>Расписание бесплатных мастер-классов:</h2>
<p>для того, чтобы записаться на семинар, нажмите на кнопку <img height="20" width="20" alt="bin.gif" src="//cdn1.specialist.ru/Content/Image/Common/bin.gif" style="vertical-align:middle;" /> справа от названия интересующего Вас мероприятия.</p>

<%--<%= Htmls.TextBlock() %>--%>

<br>
<%--<% Html.RenderAction<AccountController>(c => c.LogOnControl(null));%>--%>


<% /*Html.RenderPartial(PartialViewNames.LogOnControl, Model);*/ %>
<br>


<% } %>

<% var seminars = Model.GroupSeminars.Where(x => x.Group.IsStandart); %>

<% Html.RenderPartial(Views.Shared.Education.SeminarList,seminars); %>

<% var careerDays = Model.GroupSeminars.Where(x => x.Group.IsCareerDay); %>
<% if(careerDays.Any()){ %>

<h2>День карьеры</h2>

<% Html.RenderPartial(Views.Shared.Education.SeminarList,careerDays); %>
<% } %>
<% var imageSeminar = Model.GroupSeminars.Where(x => x.Group.IsImage); %>
<% if(imageSeminar.Any()){ %>
<h2>Формирование позитивного имиджа работодателя </h2>
<% Html.RenderPartial(Views.Shared.Education.SeminarList,imageSeminar); %>
<% } %>
<% if(Model.ProbWebinars.Any()){ %>
<h2><%= H.Anchor(SimplePages.FullUrls.WebinarSiteTerm,"Пробные вебинары") %></h2>
<% Html.RenderPartial(Views.Shared.Education.SeminarList,Model.ProbWebinars); %>
<% } %>
</asp:Content>
