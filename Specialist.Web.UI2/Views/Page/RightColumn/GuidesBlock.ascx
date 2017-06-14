<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.Guide>>" %>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Controllers.Common"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Context" %>
<% if(!Model.Any()) return; %>

<script src='/Scripts/ddpowerzoomer.min.js' type='text/javascript' ></script>
<%= Htmls2.Menu2("Интерактивные путеводители") %>
<div class="block_chamfered_in v_guide">
    <% foreach(var guide in Model){ %>   
        <h3><%= guide.Name %></h3>
		<p class="guide_img">
 <%= StringUtils.AddTargetBlank(Url.Course().Guide(guide.GuideID,
            Images.Root("Guide/Small/" + guide.SmallImage).ToString()).Title(guide.Name).ToString()) %>

		</p>


	<% } %>
</div>



<script type="text/javascript">
	$(function () {
		$("a.fancy-box[rel='guide-image']").click(function () {
			recordOutboundLink("GuideImage", "Click");
		});
	});
</script>


