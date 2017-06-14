<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Catalog.MainPageVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
	<div id="firm">
	</div>
	<div id="content" class="main">
		<%= H.ul.Class("mainlist")[
            MHtmls.IndexMenu("08", Url.Link<ProfileController>(c => c.Details(),
				"Мой Специалист")).Id("cont_Discount"),
			MHtmls.IndexMenu("08", Url.NewsAndActionsLink()).Id("cont_Discount"),
			MHtmls.IndexMenu("03", Url.AboutCenter()),
			MHtmls.IndexMenu("06", Url.CoursesLink()),
			MHtmls.IndexMenu("10", Url.Link<LocationsController>(c => c.Contacts(),
				"Контакты")),
			MHtmls.IndexMenu("12", Url.Link<CenterController>(c => c.ExpressOrder(null),
				"Экспресс-запрос")),
			MHtmls.IndexMenu("16", Htmls.Tel(CommonTexts.Phone).Id("contact-phone-link"), true).Id("cont_Phone"),
			MHtmls.IndexMenu("03", H.Anchor("https://play.google.com/store/apps/details?id=ru.specialist", "Android"))
            ] %>

  <div id="social-buttons"></div>
	
	<%= H.JavaScript().Src("/scripts/socials/otherbuttons.js?v=4") %>
	</div>

		
<% Html.RenderPartial(Views.Shared.Education.NearestGroupMobile); %>
    
    
    
    
    
    
    <script type="text/javascript">
        /* <![CDATA[ */
        goog_snippet_vars = function () {
            var w = window;
            w.google_conversion_id = 1059972133;
            w.google_conversion_label = "IuS_CIPChAgQpci3-QM";
            w.google_conversion_value = 0;
            w.google_remarketing_only = false;
        }
        // DO NOT CHANGE THE CODE BELOW.
        goog_report_conversion = function (url) {
            goog_snippet_vars();
            window.google_conversion_format = "3";
            window.google_is_call = true;
            var opt = new Object();
            opt.onload_callback = function () {
                if (typeof (url) != 'undefined') {
                    window.location = url;
                }
            }
            var conv_handler = window['google_trackConversion'];
            if (typeof (conv_handler) == 'function') {
                conv_handler(opt);
            }
        }
        /* ]]> */
    </script>
<script type="text/javascript"
   src="//www.googleadservices.com/pagead/conversion_async.js">
</script>
    

    
    
    

</asp:Content>
