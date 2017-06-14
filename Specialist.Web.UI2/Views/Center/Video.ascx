<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Video>" %>
<%@ Import Namespace="Specialist.Entities.Catalog" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<div style="text-align: center;">
    <iframe width="640" height="360" src="https://www.youtube.com/embed/<%= Model.YouTubeID %>" frameborder="0" allowfullscreen></iframe>

</div>
<%= Model.Description %>
<table class="block-table" style="padding-top: 10px;width:100%;">
    <tr>
        <td> <%= Htmls.SocialLikes() %> 
<h3>Поделиться</h3>
<%= H.InputText("", CommonConst.SiteRoot + Url.Center().Urls.Video(Model.VideoID,"")).Style("width:400px;margin-bottom:10px;") %>
<%= Htmls.SocialButtons() %>

        </td>
        <td style="text-align: right;">
            
<iframe id="fr" src="http://www.youtube.com/subscribe_widget?p=SpecialistTV" style="overflow: hidden; height: 105px; width: 300px; border: 0;"></iframe>

 
               </td>

    </tr>
</table>
<% Htmls.SocialScripts(); %>
<br/>
<% Html.RenderAction<GroupController>(c => c.ForCourseTCList(Model.CourseTCList,false,0)); %>