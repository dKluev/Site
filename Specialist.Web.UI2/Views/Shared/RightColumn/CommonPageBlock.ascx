    <%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Controllers.Message" %>

<script type="text/javascript">
    $(function() {
        lazyContent("#advices",
            '<%=Url.Action<CenterController>(c => c.AdviceBlock()) %>');
   <%--     lazyContent("#forum",
            '<%=Url.Action<MessageController>(c => c.MessageBlock()) %>');--%>
    });
</script>

<% Html.RenderPartial(PartialViewNames.MainNews, true);%>
<div id="advices"> </div>