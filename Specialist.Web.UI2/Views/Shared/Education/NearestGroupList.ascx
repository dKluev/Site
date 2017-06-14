<%@ Control Language="C#" 
Inherits="System.Web.Mvc.ViewUserControl<NearestGroupsVM>" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>

  <% if (!Model.Groups.Any()) { %>
         <p>На данный момент групп нет</p>   
     <% return; %>
    <% } %>




<% Model.ColNumber = 4; %>
<table class="table">
	<tr class="thead">
		<th class=" table_c_tl">
			&nbsp;
		</th>
		<th rowspan="2">
			<strong>Дата</strong>
		</th>
		<th rowspan="2">
			Режим обучения
		</th>
		<% if (!Model.HideComplex) { %>
		<th rowspan="2">
			Место&nbsp;обучения
		</th>
		<% Model.ColNumber++; %>
		<% } %>
		<% if (!Model.HideTrainer) { %>
		<th rowspan="2">
			Преподаватель
		</th>
		<% Model.ColNumber++; %>
		<% } %>
		<% if (Model.ShowDiscount) { %>
		<th rowspan="2">
			Скидка<div title="<%= CommonTexts.DiscountForDay %>" class='div-pointer'>*</div>
		</th>
		<% Model.ColNumber++; %>
		<% } %>
		<% if (Model.ShowPrice) { %>
		<th rowspan="2">
			Цена от
		</th>
		<% Model.ColNumber++; %>
		<% } %>
		<th rowspan="2">
			<%= Images.Main("add_ingrey.gif") %>
		</th>
	    <% if(!Model.HideCart){ %>
		<th rowspan="2" class="last_td">
			<%= Images.Main("ico_signup2.gif")%>
		</th>
		<% } %>
		<th class="table_c_tr" style="text-align: center;" >
		    <% if (Model.PdfUrl.IsEmpty()) { %>
                &nbsp;
            <% }else{ %>
        		    <%= H.Anchor(Model.PdfUrl, Images.Main("download-pdf.png")
              .Style("margin: 0 15px 0 0;").Title("Скачать расписание в формате PDF")).Style("width:100%;height:100%;display:block;").Class("ga-click").Rel("download-pdf").ToString() %>
            <% } %>
			
		</th>
	</tr>
	<tr>
		<th class="table_c_bl">
			&nbsp;
		</th>
		<th class="table_c_br">
			&nbsp;
		</th>
	</tr>
	<% Html.RenderPartial(Views.Shared.Education.NearestGroupRows); %>
</table>



<% if (Model.ShowDiscount) { %>
	<p style="font-size:10px;">*<%= CommonTexts.DiscountForDay %></p>
    <% if (Model.Groups.Any(x => x.IsOpenLearning)) { %>
	    <p style="font-size:10px;"><span class="discount_color">*</span><%= CommonTexts.OpenClasses +               H.Anchor(SimplePages.FullUrls.OpenClasses, "открытого обучения") %>. </p>
    <% } %>
<% } %>