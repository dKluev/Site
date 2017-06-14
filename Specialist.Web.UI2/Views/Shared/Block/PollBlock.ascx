<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PollVM>" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Services" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<% var nextPollId = Model.NextPollId; %>
<% if (Model.Poll != null) { %>
<script type="text/javascript">
    $(function() {
        $("#pollVote").click(function() {

            var messages = <%= JsonConvert.SerializeObject(Model.Messages) %>;
            
            var pollOption = $('input[name=PollOptionID]:checked');
            var optionID = pollOption.val();
            var pollAnswer = $('#pollAnswer').val();
            if(optionID == null)
                return false;
            var data = { 
                pollOptionID: optionID,
                pollAnswer: pollAnswer,
                pollID: <%= Model.Poll.PollID %>
            };
            $.post('<%= Url.Action<PageController>(
                c => c.PollVote(0, null, 0)) %>', data,
            function() {

                <% if (nextPollId > 0) { %>
                    $("#poll").load('<%= Url.Page().Urls.GetPoll(nextPollId) %>');
                <% }else { %>
                    var message = messages[optionID];
                    if (message) {
                        $("#poll-main-div").hide();
                        $("#poll-answers-div").html(message).show();
                        initOpenInDialog();
                    } else {
                        controls.loadPoll(function() {
                            $("#poll-answers-div").show();
                        });
                   }
                <% } %>
            });
            return false;
        });
        var voted = parseInt($.cookie("<%= UserSettingsService.PollCookieName %>")) == <%= Model.Poll.PollID %>;
        var id = voted ? "poll-answers-div" : "poll-main-div";
        $("#" + id).show();
    });
</script>

    

<%= Htmls2.Menu2("Опрос") %>
<p> <%= Model.Poll.Name %> </p>
    

    <div id="poll-answers-div" style="display: none;">
        <ul class="mark_arrow">
            <% foreach (var option in Model.Poll.PollOptions.OrderBy(x => x.WebSortOrder)) { %>
                <li> 
                    <%= SimpleLinks.PollOption(option) %> -
					<% var percent = Model.GetPercent(option); %>
                    <strong><%= percent.ToString(percent >= 1 ? "0" : "0.#", CultureInfo.InvariantCulture) %>%</strong>
                </li> 
            <% } %>
        </ul>
        <p> Всего проголосовало <strong><%= Model.GetTotal() %></strong>.</p>
    </div>

    <div id="poll-main-div" style="display: none;">
        <% using (Html.BeginForm<PageController>(c => c.PollVote(0, null, 0))) { %>
            <div> <%= Html.Hidden("pollID", Model.Poll.PollID) %></div>
            <p>
            <% foreach (var option in Model.Poll.PollOptions.OrderBy(x => x.WebSortOrder)) { %>
                <%= Html.RadioButton(option.For(x => x.PollOptionID),
                        option.PollOptionID) %>
                <label> <%= SimpleLinks.PollOption(option) %></label>
				<% if (option.Text.Equals("другое", StringComparison.OrdinalIgnoreCase)) { %>
					<%= H.InputText("pollAnswer", "").Id("pollAnswer").Style("width:140px;") %>
	            <% } %> 
				<br />
            <% } %>
            </p>
            <p class='submit_p'> <%= Images.Submit("ok").Id("pollVote") %> </p>
        <% } %>

    </div>
    <% } %>

