<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<UserMessage>>" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Controllers.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<% if(Model.Count() == 0) return; %>
<%= Htmls2.Menu2("Форум", "blue") %>
<% var message = Model.First(); %>
<div class="block_chamfered_in v_forum">
	<h3>
		<%= Html.MessageLink(message, message.Title ) %></h3>
	<div>
		<p>
			<%= StringUtils.GetShortText(message.Text) %></p>
	</div>
	<p class="date">
		<%= message.CreateDate.DefaultString() %></p>
</div>

