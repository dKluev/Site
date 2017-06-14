<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Specialist.Entities.Announcement.Announce>>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Helpers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<% if (Model.Count() == 0) return; %>
    <%= Htmls2.Menu2(SimpleLinks.GroupDiscounts())%>


<div class="block_chamfered_in v_group_discount">
	     <ul class="square">

    <% foreach (var announce in Model){ %>
<% var group = announce.AnnounceGroups.First(); %>
        <li>
		<%= Html.CourseLink(announce.Course) %>
			<%= Htmls2.Discount(announce.AnnounceGroups.First(),true) %>
        <br />
            <span class="date"><%= group.DateBeg.DefaultString() %></span></li>
<% } %>
                </ul>
		<%= Htmls2.Rss(Url.Link<RssController>(c => c.HotGroups(), "RSS").Class("rss_a")) %>
</div>


   

