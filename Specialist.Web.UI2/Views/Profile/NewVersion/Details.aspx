<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"
    Inherits="System.Web.Mvc.ViewPage<ProfileVM>" %>

<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Passport" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Common.Utils.Logic" %>
<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Entities.Profile" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Root.Profile.Logic" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
	<link href="<%= JavaScripts.PersonalStyle %>" rel="stylesheet" type="text/css" />
    <%= Htmls.MetaTitle("Профиль") %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if(!Model.User.IsCompany && !Model.User.IsStudent){ %>
    <div class="attention">
        <p>
            <%= Html.ActionLink<ProfileController>(c => c.ChangeStatus(),
                                "Получить доступ к сервисам слушателя (активировать данные из памятки выпускника)")%>.</p>
    </div>
    <% } %>

    <div id="header-line">
            <div class="plate online">
                               <div class="pull-left online">
                                               <div class="hallo">Здравствуйте, <%= Model.User.FirstSecondName %>!</div> 
                               </div>
                               <div class="pull-right online">
                               <%if(Model.ExistGroup) { %>
                                  <div class="manager">Ваш личный менеджер: <b><%= Model.Manager.LastFirstName %> (<a href="mailto:<%= Model.Manager.FirstSpecEmail %>">написать сообщение</a>)</b> </div>
                              <% } else { %>     
                                   <div class="manager">Записаться на курс или получить консультацию менеджера: 7 (495) 232-32-16</div>
                                <% } %>
                                 
                               </div>
                </div>
        

    </div>
    <div id="shapka">
<div id="podshapka">


    <div id="leftcolumn">
        <%= Images.UserPhoto(Model.User.UserID) %>
        <p style="font-size: 70%"><b><%= Url.Profile().EditProfile("изменить фотографию").Style("color: #e7e7e7; text-decoration: none;") %> </b></p>
        <p><b><%= Model.User.FullName %></b></p>
        <div class="chart"></div>
    </div>

<div id="rightcolumn">
<p class="phead">Ваши награды</p>
<div id="profile-badges"></div>

<div class="clear"></div>

<div style="width: 600px;"><!-- див карточек-->
<p class="phead">Учись выгодно! Программа <%= H.Anchor(SimplePages.FullUrls.RealSpecialist, "«Настоящий специалист»") %>!</p>

<% Html.RenderPartial(Views.Profile.NewVersion.ClabCard, Model.User.Student ?? new Student()); %>

</div><!-- див карточек-->

</div><!--правая колонка-->

</div>

</div><!--//end-second-div-->
    <% var menu = _.List(
           Tuple.Create("Мои курсы", Url.Profile().Urls.Learning(), true),
           Tuple.Create("Мой профиль", Url.Profile().Urls.ProfileAjax(), false),
           Tuple.Create("Мои тесты", Url.Profile().Urls.Tests(0), false),
           Tuple.Create("Мои сертификаты", Url.Graduate().Urls.GroupCerts(), true),
           Tuple.Create("Спецпредложения", Url.Center().Urls.MarketingActions(), true),
           Tuple.Create("Обратная связь", Url.Page().Urls.SendForManager(Model.Manager.Employee_TC), false),
           Tuple.Create("Трудоустройство", Url.Profile().Urls.JobAjax(), true),
           Tuple.Create("Библиотека<br/> учебных материалов", Url.Profile().Urls.Library(), true),
           Tuple.Create("Форум", Url.Message().Urls.Forum(), true)
           ); %>
    <% menu = Model.User.IsStudent ? menu : menu.Where(x => !x.Item3).ToList(); %>
    <hr size="1"/>

    <div id="wrapper">
        <div id="content">
            <div class="content active" id="profile-nav-content">
            </div>
        </div>
            <%= H.div.Id("nav")[menu.Select((x, i) => {
    var isForum = x.Item1 == "Форум";

    return isForum 
    ? H.Div("item")[H.Div("icon forum"), H.Div("name")[H.Anchor(x.Item2, x.Item1)]]
    : H.Div("item").Data("url", x.Item2)[H.Div("icon n0" + i), H.Div("name")[x.Item1]];
})] %>
        </div>
    
<div id="other_blocks">
<div class="video-club">
<div class="name">Полезное видео</div>
</div>
</div>
    
    <table cellpadding="20" style="width: 100%;">
<tbody>
    <%= H.tr[Model.Videos.Select(x => H.td[Htmls2.YouTube(x.YouTubeID, x.Name) ])]  %>
</tbody>
</table>


    <script type="text/javascript">
        recordOutboundLink("Profile", "Email", "<%= Model.User.Email %>");
        $(function () {

            function loadTo(selector, url) {
                var $target = $(selector);
                $target.html(controls.indicator);
                $.get(url, function (txt) {
                    $target.hide();
                    $target.html(txt);
                    controls.initTables();
                    controls.initAllTabs();
                    $target.fadeIn("fast");
                });
            }


            var navItems = $("#nav div.item");
            $("div.item[data-url]").click(function () {
                var url = $(this).data("url");
                navItems.each(function() {
                    $(this).removeClass("active");
                });
                $(this).addClass("active");
                loadTo('#profile-nav-content', url);
            });
            lazyContent('#profile-badges', '/profile/bangesajaxnew', '#profile-badges');
            navItems[0].click();
        });
    </script>

</asp:Content>
