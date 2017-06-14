<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Context.Advice>>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<% if(Model.Count() == 0) return; %>
<%= Htmls2.Menu2("Советы") %>

<div class="block_chamfered_in v_forum">

<% foreach (var advice in Model) { %>

	<h3>
			<%= Html.AdviceLink2(advice) %>
</h3>
	<div>
		<%= StringUtils.GetShortText(advice.Description) %>

	</div>
				<p> <%= Html.Advices("Все советы").Class("all") %> </p>
	

<% } %>
<%= Htmls2.Rss(Url.Link<RssController>(c => c.Advices(), "RSS").Class("rss_a"))%>

</div>


