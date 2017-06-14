<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Passport" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<div class="footers">
    <div class="footer1">
        <div class="certificate_block">
            <p>
				<%= Htmls.HtmlBlock(HtmlBlocks.FooterVendors) %>
            </p>
        </div>
    </div>
    <div class="footer2">
        <div class="footer2_bg_t">
        </div>
        <div class="award">
            <%= Url.Link<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
                c.Details(845, null), Images.Main("prize1.png").Alt("Лауреат премии Рунета").Size(115, 131))
                .Title("Лауреат премии Рунета")%>
        </div>
		<%= Htmls.HtmlBlock(HtmlBlocks.FooterLinks) %>
    </div>
    <div class="footer3">
		<%= Htmls.HtmlBlock(HtmlBlocks.Footer) %>
    </div>
 
       
    <% Html.RenderPartial(PartialViewNames.Counters); %>
</div>
