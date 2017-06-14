<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CartVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<% if (Model.TrackDiscounts == null || !Model.TrackDiscounts.Any()) return; %>
<% var trackCount = 3; %>

<table class="defaultTable">
	<tr>
		<th  style="text-align:left;">
			<p><img hspace="10" align="left" width="50" src="//cdn.specialist.ru/content/image/simplepage/basket/pig.png"> <span style="color: #006; font-weight:bold;">Больше курсов — больше преимуществ!</span><br />
                          <span style="font-size:80%;">Выбранные Вами курсы входят в следующие <%= CommonTexts.TracksName %>. Скидка при одновременной оплате до 18%!</span></p>
		</th>
		<th>
			 <span style="color: #006; font-weight:bold;">Цена</span> 
		</th>
		<th>
			 <span style="color: #006; font-weight:bold;">Ваша<br />
                        экономия</span> 
		</th>
		<th>
			<%= Images.Main("ico_signup2.gif") %>
		</th>
	</tr>
	<%
   Model.TrackDiscounts.ForEach((trackDiscount, i) => {%>
	<%
   	var moneyDiscount = (trackDiscount.Price - trackDiscount.DiscountPrice).MoneyString();%>
	<tr <%= Htmls.DisplayNone(i > 2) %> class="show-on-click-track-discount">
		<td class="td_course3">
			Я хочу записаться на <%= trackDiscount.Track.IsDiplom ? "Дипломную программу" : CommonTexts.TrackName2 %>
			<br />
			<strong>
				<%=Html.CourseLink(trackDiscount.Track)%></strong> 
		</td>
		<td>
			<span style="text-decoration:line-through;">
				<%= trackDiscount.Price.MoneyString()  %>
			</span><br/>
			<strong><%=trackDiscount.DiscountPrice.MoneyString()%></strong>
		</td>
		<td>
			<span class="discount_color">
				<%=moneyDiscount%></span>
		</td>
		<td>
			<%=Html.AddToCart(trackDiscount.Track, true)%>
		</td>
	</tr>
	<%
   }); %>

</table>
<% if(Model.TrackDiscounts.Count > trackCount){ %>
<br />
<div style='text-align:center'>
<strong style="font-size:14px"><a href='#' class='show-on-click not-link' rel='track-discount'>Показать все <%= CommonTexts.TracksName %></a></strong>
</div>
<% } %>

			<p><br/></p>