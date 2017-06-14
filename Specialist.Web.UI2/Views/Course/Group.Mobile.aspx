<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.ViewModel.MobileCourseVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
	 <% if(Model == null) { %>
	 Группа заполнена
	 <% return; %>
	<% } %>
<% var course = Model.Course; %>
<% var group = Model.Group; %>
<%= MHtmls.Back(Html.CourseLinkAnchor(course.UrlName, "На страницу курса")) %>
<div id="content" class="longlist">

  <h2 class="groupinfo">Информация о группе </h2>

  <p class="group_name"><b>Курс:</b> <%= course.GetName() %></p>

  <p  class="group_long"><b>Продолжительность:</b> <%= course.BaseHours.ToIntString() %> ак. ч.</p>
  <% if(Model.Prices.Any()){ %>
  <p class="group_price"><b>Цена:</b> от <%= Model.Prices.SelectMin(x => x.Price).Price.MoneyString() %>* руб</p>
  <%= Model.CanOrder ? Html.AddToCartMobile(c => c.AddGroup(Model.Group.Group_ID)) : null %>
  <% } %>

  <p class="group_dates"><b>Даты:</b> <%= group.DateInterval %></p>

  <p class="group_time"><b>Время:</b> <%= group.TimeInterval %></p>

  <p class="group_days"><b>График:</b> <%= group.DaySequence %>  </p>

  <p class="group_days"><b>Место:</b> <%= Url.ComplexLinkAnchor(group.Complex).Class("link") %></p>

  <% if(Model.Group.IsOpenLearning){ %>
  <p class="group_days">Открытое обучение</p>

  <% } %>
  <% if(Model.Group.Teacher != null && Model.Group.Teacher.FinalSiteVisible){ %>
  <p class="group_trainer"><b>Преподаватель:</b> <%= Html.TrainerLink(group.Teacher).Class("link") %></p>
  <% } %>

  
  <p>*Цена зависит от формата обучения: очное, вебинар, открытое и от времени занятий группы: утро, день, вечер, выходной</p>

</div>

<%= Html.Site().MobileCourseGroups(Model.Groups) %>
</asp:Content>
