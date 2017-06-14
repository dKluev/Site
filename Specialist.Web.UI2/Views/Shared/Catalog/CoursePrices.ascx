<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CourseBaseVM>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>



<% if(!(Model.HasFullTimePrice || Model.HasDistancePrice || Model.HasWebinar || Model.HasIntraExtra)) return; %>


<% if (Model.Course.IsVideo) { %>
<h3>
	Стоимость - <%= Model.GetPrice(PriceTypes.Main).MoneyString() %> рублей 
</h3>
    <%= Html.Site().AddCourseButton(Model) %>
<% return; %>
<% } %>

<table>
    <tr><td>
<h3 id="course-price-title">
	Стоимость обучения (рублей)*&nbsp;
</h3>
        <td>
<%= Url.Page().CourseTender("Курс может быть заказан согласно ФЗ-44, ФЗ-223 (закупка/аукцион/запрос котировок/конкурсные процедуры)") %>
            
        </td>

    </tr>
</table>


<%= Html.Site().FullTimePrice(Model)%>


<%= SiteHtmls.CreditBlock(Model.MinPriceForCredit) %>

<%--    <div id="trackBlock2"></div>--%>
	<% Html.RenderPartial(Views.Shared.Catalog.SecondCourseDiscount, Model); %>

<% var stars = "*"; %>
<% if(!Model.Course.IsTrackBool && Model.MorningDiscount.HasValue){%>
<%= Htmls.HtmlBlock(HtmlBlocks.CoursePrice) %>
<br/>
<% } %>

<% if (Model.HasIndividual) { %>
    <%= Htmls.HtmlBlock(HtmlBlocks.Individual) %> <br/>
<% } %>


