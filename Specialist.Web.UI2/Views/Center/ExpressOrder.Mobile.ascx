<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Common.ViewModel.ExpressOrderVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<div id="content" class="longlist">
<h2>Экспресс-запрос менеджеру</h2>

<p>Оставьте свои данные и наши менеджеры свяжутся с Вами в течение получаса. По будням с 9:30 до 18:00</p>
<% using(Html.BeginForm<CenterController>(c => c.ExpressOrder(null))){ %>
<p>Ваше имя:<br /><input type="text" required="required" name="Name" placeholder="Ваше имя" class="longinput" /></p>
<p>Контактная информация:<br />
<textarea  id="info" name="Contact" required="required" placeholder="Контактная информация" rows="1" class="longinput"></textarea></p>
<p><input type="submit" value="Отправить" /></p>

<% } %>
	</div>
