<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<UserWork>>" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="NLog.Config" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Site" %>
<% if (!Model.Any()) {
	   return;
   }%>
<%= Htmls2.Menu2("Работы выпускников") %>
<div class="block_chamfered_in v_works_graduate">
<%= CommonSiteHtmls.Carousel(Model.Select(x => H.Div("block_work_graduate")[
	Html.UserWorkLink(x), SiteHtmls.Graduate(x.FullName)]))%>

<br />
<%= Url.Link<ClientController>(c => c.PrivatePerson(SimplePages.Urls.Works, null), 
	"Все работы").Class("all") %>
</div>

