
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OrgVacancy>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<p>
<% var lang = Model.ForeignLanguages.Substring(0, Model.ForeignLanguages.IndexOf(";"));
   var level = Model.ForeignLanguages.Substring(Model.ForeignLanguages.IndexOf(";") + 1);  %>
<strong>Должность:</strong> 
<%= Model.Position %><br />
<strong>Возраст с:</strong><%= Model.AgeSince %><strong> по:</strong><%= Model.AgeTill %><strong> лет</strong><br />
<strong>Образование:</strong> 
<%= Model.Education %><br />
<strong>Опыт:</strong> 
<%= Model.Experience %><br />
<strong>Пол:</strong> 
<%= Model.Sex %><br />
<strong>Занятость:</strong> 
<%= Model.Busy %><br />
<strong>График работы:</strong>
<%= Model.Schedule %><br />
<strong>Иностранный язык:</strong><%= lang %><strong> уровень:</strong><%= level %><br />
<strong>Заработная плата от:</strong><%= Model.Profit %><%= Model.Currency %><strong> в месяц</strong><br />
<strong>Город:</strong>
<%= Model.City %><br />
<strong>Ближайшая станция метро:</strong>
<%= Model.Metro %><br />
<strong>Описание вакансии:</strong>
<%= Model.Description %><br />
<strong>Направления:</strong>
<%= Model.Sections %><br />
<strong>Контактная информация</strong><br />
<strong>Компания:</strong>
<%= Model.Company %><br />
<strong>Должность:</strong>
<%= Model.OrgPosition %><br />
<strong>Ф.И.О.:</strong>
<%= Model.Name %><br />
<strong>Телефон:</strong>
<%= Model.Phone %><br />
<strong>E-mail:</strong>
<%= Model.Email %><br />
<strong>Дата:</strong>
<%= Model.UpdateDate %><br />
</p>
<div class="response">
</div> 
