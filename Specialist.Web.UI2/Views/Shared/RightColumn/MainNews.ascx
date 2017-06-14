<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<bool?>" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>

<div id="news"> </div>

<script type="text/javascript">
	$(function () {
		loadUrlTo("#news",
            '<%=Url.Action<PageController>(c => c.NewsForMain()) %>');
	});
</script>

   