<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.CityFilterVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>


<div id="city-coupon-block" style="width:400px;">
    <%= Htmls.HtmlBlock(HtmlBlocks.CityCouponInfo) %>
</div>

<script>
    $(function () {
        controls.showDialog("#city-coupon-block", false);
    });
</script>