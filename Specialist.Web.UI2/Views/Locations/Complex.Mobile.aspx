<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Center.ViewModel.ComplexVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
<%= MHtmls.Back(Url.Complexes()) %>
	<div id="content" class="longlist">
	<%= MHtmls.Title(Model.Complex.Name) %>
	  <p class="selected"><span class="selected">Адрес:</span><br />
<%= Model.Complex.Address %>
</p>
 <p class="mapcard"><a href="<%= Urls.GetComplexMap(Model.Complex.UrlName) %>" target="_blank">Посмотреть на яндекс.картах</a></p>
 <p>
	 
				<%= Htmls.ComplexDirection(Model.GeoLocation)%>
 </p>
<%--<div id="coord-indicator" style="display: none;">Получаем координаты <%= Images.Common("indicator.gif") %></div>--%>
 
  <p class="selected">Телефон: <a href="tel:+7(495)232-32-16" ><span id="cont_Phone">+7 (495) 232-32-16</span></a></p>
    <p><span class="selected">Запись на курсы:</span><br />
    <%= Model.Complex.WorkingHours %></p>
	<%= Model.Description.SecondPart %>
	
 <p class="moreplaces">Другие учебные комплексы:</p>
 <%= Html.Site().MobileComplexes(Model.OtherComplexes) %>


<%= Html.Site().MobileGroups(Model.NearestGroupSet.All.Take(5).ToList()) %>

</div>
<script type="text/javascript">
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(x) {
            var coord = x.coords.longitude + "," + x.coords.latitude;
            var link = document.getElementById("complex-direction-link");
            var href = link.getAttribute("href");
            var newHref = href.replace("rt=", "rt=" + coord);
            link.setAttribute("href", newHref);

        });
    }
</script>
</asp:Content>
