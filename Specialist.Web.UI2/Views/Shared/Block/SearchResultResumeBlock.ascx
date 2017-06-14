
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Resume>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<p>
<strong>Ф.И.О.:</strong><%= Model.FirstName %><strong>  </strong><%= Model.SecondName %><strong>  </strong><%= Model.LastName %><br />
<strong>Возраст:</strong>
<%= Model.Age %><br />
<strong>Пол:</strong>
<%= Model.Sex %><br />
<strong>Образование:</strong>
<%= Model.Education %><br />
<strong>Олыт работы:</strong>
<%= Model.Experience %><br />
<strong>Желаемая должность:</strong>
<%= Model.Position %><br />
<strong>Направления:</strong>
<%= Model.Sections %><br />
<strong>Заработная плата:</strong><%= Model.Profit %><strong>  </strong><%= Model.Currency %><strong> в месяц</strong><br />
<strong>Город:</strong>
<%= Model.City %><br />
<strong>Ближайшая станция метроо:</strong>
<%= Model.Metro %><br />
<strong>Контактная информация</strong><br />
<strong>Телефон:</strong>
<%= Model.Phone.Replace(';', ' ') %><br />
<strong>E-mail:</strong>
<%= Model.Email %><br />
<strong>Дата:</strong>
<%= Model.UpdateDate %><br />
</p>
<div class="response">
</div> 
