<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AllCourseListVM>" %>
<%@ Import Namespace="SimpleUtils.FluentHtml.Tags" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>

<% var priceText = Specialist.Web.Common.Html.Htmls.HtmlBlock(HtmlBlocks.PriceText); %>
<% var isIntraExtra = Model.IsIntraExtra; %>
<% var showUnlimited = Model.ShowUnlimited; %>
	<tr class="thead">
		<th rowspan="2" class="table_c_tl">
			&nbsp;
		</th>
		<% if(Model.Common.HasIcons){ %>
		<th rowspan="3"></th>
		<% } %>
		<th rowspan="3" style="width: 48.4%;text-align: left;font-weight: bold;">
			<%= Model.IsDiplomPage ? "Дипломные программы" : "Курсы обучения" %>
		</th>
		<th rowspan="3" class="subtitle">
			Ак.ч
		</th>
	    <% var groupColSpan = Model.HideIntraGroup ? 1 : 2; %>
		<th colspan="<%= groupColSpan %>" rowspan="2" class="subtitle">
			Ближайшая группа
			<br />
			<span style="font-size:10px">
			Кликните, <br />чтобы увидеть все
			</span>
		</th>
			<th colspan="4">
			Цена&nbsp;от<div 
			title="<%= priceText %>"
			class='div-pointer'>*</div>
		</th>

        <% if (showUnlimited) { %>
    		<th rowspan="3">
                <%= SimpleLinks.Unlimited("Специальная цена для слушателей БО").Style("color:black") %>
    		</th>
        <% } %>
		
		
		<th rowspan="3" class="last_td">
		    <%= Images.Main("ico_signup2.gif")%>
		</th>
		<th rowspan="2" class="table_c_tr">
			&nbsp;
		</th>
	</tr>
	<tr>
	    <% var firstTitle = H.th[H.Anchor(SimplePages.FullUrls.Fulltime, "Очное обучение")].Colspan(2); %>
        <% var secondTitle = H.th[isIntraExtra ?
               SimpleLinks.IntraExtra().Style("color:black") : SimpleLinks.Webinar("Вебинар")].Colspan(2); %>
        <% if(Model.IsDiplomPage){ %>
        <%= secondTitle %>
		<%= firstTitle %>
        <% }else{ %>
		<%= firstTitle %>
        <%= secondTitle %>
        <% } %>
	</tr>
	<tr>
		<th class="table_c_bl">
			&nbsp;
		</th>
        <% var firstHead = Model.HideIntraGroup ? null : H.th[H.Anchor(SimplePages.FullUrls.Fulltime, "Очная")
               .Style("color:black")].Style("font-size:10px;"); %>
        <% TagA secondHeadLink = null;

           if (isIntraExtra || Model.HideIntraGroup) {
               if (Model.IsOpenClasses) {
                   secondHeadLink = SimpleLinks.OpenClasses();
               } else {
                   secondHeadLink = SimpleLinks.IntraExtra("Очно — заочное");
               }
           } else {
               secondHeadLink = SimpleLinks.Webinar("Вебинар");
           }
           var secondHead = H.th[secondHeadLink].Style("font-size:10px;");
           
        %>
        <%= firstHead %>
        <%= secondHead %>

		<th style="font-size:10px">Частные<br />лица
		</th>
		<th style="font-size:10px"> Органи-<br />зации
		</th>
	
		<th style="font-size:10px">Частные<br />лица
		</th>
		<th style="font-size:10px"> Органи-<br />зации
		</th>
		<th class="table_c_br">
			&nbsp;
		</th>
	</tr>