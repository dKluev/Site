<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<NewsVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Root.Catalog" %>
<%@ Import Namespace="Specialist.Entities.Const" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

		<%= Model.News.Description %>
   <% var sem = Model.Seminar; %> 
    <%= sem != null && sem.Group_ID != 241944
            ? H.Div("attention2")[H.p["{4}<b>«{0}»</b> пройдет {1} ({2}) в учебном комплексе {3}".FormatWith(sem.Title, sem.DateBeg.DefaultString(), sem.TimeInterval, Html.ComplexLink(sem.Complex),
    sem.HasIntraExtra ? "<b>Данное занятие Вы можете пройти как в очном формате, так и дистанционно в режиме вебинара.</b><br/>" : null),H.br, H.br,Url.Course().AddSeminar(sem.Group_ID,null, Images.Button("register").ToString())]] 
    : null %>
        <%= Images.Gallary(Model.News) %>
        <br />
		
        <%= Html.AddThis() %>
		<div class="data"><%= Model.News.PublishDate.DefaultString() %></div>
		<br/>
		<div id="vk_comments"></div>
    
       <%= Model.News.CourseTCList.IsEmpty() ? null : Htmls.GroupSort(GroupTitleType.Get(Model.News.IsFreeWebinar ? GroupTitleType.Webinar : 0),
       Url.Group().Urls.ForNews(Model.News.NewsID, 0) ) %>

<%--<% Html.RenderAction<GroupController>(c => c.ForCourseTCList(Model.News.CourseTCList,false, --%>
<%--       Model.News.IsFreeWebinar ? GroupTitleType.Webinar : 0)); %>--%>



</asp:Content>

<asp:Content ContentPlaceHolderID="bottom" runat="server">
	<!--[if IE]>
	<script type="text/javascript">
	    $(function () {
	        var i = 0;
	        function tryRemoveDescription() {
	            if (i++ > 10)
	                return;
	            setTimeout(function () {
	                var frame = $("#vk_comments iframe:first");
	                if (frame.length) {
	                    var src = frame.attr('src');
	                    src = src.replace(/description=(.*?)&/, "");
	                    frame.attr('src', src);
	                } else {
	                    tryRemoveDescription();
	                }

	            }, 100);
	        }
	        tryRemoveDescription();
	    });
	</script>
	<![endif]-->

</asp:Content>
