<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<MainPageVM>" %>

<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Controllers.Message" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Logic" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
 <title> 
 <%= HtmlTitleCreator.Get(Model) ?? "Компьютерные курсы «СПЕЦИАЛИСТ» при МГТУ им.Баумана - лучший компьютерный учебный центр России. Обучение в лучшем учебном центре." %>
 </title>
	<link href="/Content/slider.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="Center" runat="server">


<div class="banners" style="margin: 0;">
    <div>
        <%= Htmls.HtmlBlock(HtmlBlocks.NewSlider) %>
    </div>
        	<script src="/Scripts/responsiveslides.min.js" type="text/javascript"> </script>
        <script type="text/javascript">
            $(function () {
                $("#slider").responsiveSlides({
                    pager: true,
                    nav: true,
                    speed: 500,
                    namespace: "centered-btns"
                });
            });
        </script>
    </div>
    
    <div class="left1">

        <%= Specialist.Web.Common.Utils.CacheUtils.Get("RootSectionsForMain1",
			   () => Html.Partial(PartialViewNames.RootSections)) %>

		
    </div>

    <div class="right1">

		<% Html.RenderPartial(PartialViewNames.About); %>

    </div>

         <script type="text/javascript">
         	$(function () {
         		lazyContent("#hot-groups", '<%=Url.Action<PageController>(c => c.HotGroupsForMain(null)) %>');
         	    
         		$.get('<%=Htmls.IsNewVersion ? Url.Action<HomeController>(c => c.MainPageResponseNew()) : Url.Action<HomeController>(c => c.MainPageResponse()) %>', function (data) {
         		    $("#main-response").replaceWith(data);
         		});

         	});
            </script>
</asp:Content>
