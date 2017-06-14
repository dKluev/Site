<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

	<%@ Import Namespace="Specialist.Web.Controllers.Common" %>    
	<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%= Htmls2.Menu2("Группы со скидками", "lightblue")%>

<div class="block_chamfered_in v_group_discount">
	<div id="hot-groups">
		
	</div>
		<%= Htmls2.Rss(Url.Link<RssController>(c => c.HotGroups(), "RSS").Class("rss_a")) %>
</div>



   