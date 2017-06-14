<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.CartVM>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>

<% if(Model.WebinarRecords == null) Model.WebinarRecords = new List<WebinarRecord>(); %>
<% if (!Model.WebinarRecords.Any()) return; %>
	   
<table class="defaultTable">
	<tr>
		<th><img hspace="10" align="left" width="50" src="//cdn.specialist.ru/content/image/simplepage/basket/max.png" /></th>
		<th style="text-align: left">
			 <p><span style="color: #006; font-weight:bold;">Максимальная выгода!</span><br />
                          <span style="font-size:80%;">Добавьте к Вашему заказу сейчас!</span></p>
		</th>
		<th>
			 <span style="color: #006; font-weight:bold;">Цена</span>
		</th>
		<th> <span style="color: #006; font-weight:bold;">Ваша экономия</span></th>
		<th>
			<%= Images.Main("ico_signup2.gif") %>
		</th>
	</tr>
</table>

<p><br/></p>