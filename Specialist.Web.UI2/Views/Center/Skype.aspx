<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<List<Employee>>>" %>
<%@ Import Namespace="Specialist.Services.Common.Utils" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
<% var isOnline = new TimeSpan(9, 00, 00) <= DateTime.Now.TimeOfDay
	   && DateTime.Now.TimeOfDay <= new TimeSpan(18, 00, 00); %>
<% var icqIcon = Images.Main(isOnline ? "ico_icq.png" : "icq_offline.gif"); %>
<% var skypeIcon = Images.Main(isOnline ? "skype_online.png" : "skype_offline.png"); %>
<%= TemplateEngine.GetText(Htmls.TextBlock(), new {icqIcon, skypeIcon}) %>
    
<script type="text/javascript">
    $(function() {
        $("#skype-contacts img").each(function() {
            var $img = $(this);
            var name = /smallicon\/(.*?)$/.exec($img.attr("src"))[1];
            var skype = "http://mystatus.skype.com/" + name + ".txt"
            var url = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D%27" 
                + skype + "%27%0A&format=json";
                        $.get(url, function (x) {
                            if (x.query.results.body.p == "Offline") {
                                $img.parent().hide();
                            }
                        });
        });
    });

</script>

</asp:Content>


