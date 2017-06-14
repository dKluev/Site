<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<SimpleUtils.Common.Tuple<System.Guid, bool,string>>>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
	var bestName = Model.Data.V3; %>
 Хотите удивить Ваших друзей? Поделиться своим успехом и продемонстрировать силу знаний?


<p>Сделайте свой уникальный аватар для социальных сетей. Такой фотографии не будет ни у кого! Расскажите своим друзьям и знакомым о Ваших достижениях!
Закачайте свою фотографию (не более <%= UserImages.MaxBestSize.Value %> Kb,
 в формате jpg), сохраните на компьютер и используйте в качестве аватара в своем профиле в социальных сетях!</p>

<div style="cursor:pointer;" id="upload-button">
<%= Images.Button("upload_photo") %></div> 
<img <%= Htmls.DisplayNone() %> id="indicator" src="//cdn.specialist.ru/Content/Image/Common/indicator.gif"/>
<br />

<div <%= Htmls.DisplayNone(Model.Data.V2) %> id="default-photo">
<%= Images.Root("User/" + bestName + "/default.jpg") %>
</div>

<div <%= Htmls.DisplayNone(!Model.Data.V2) %> id="result-photo">
<%= UserImages.Best(bestName, Model.Data.V1).Id("user-photo") %>
</div>

<br />
<div <%= Htmls.DisplayNone(!Model.Data.V2) %> id="download">
<%= Html.ActionLinkImage<GraduateController>(c => c.DownloadBest(bestName), 
    Urls.Button("download")) %>
</div>
<script src="/Scripts/ajaxupload.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        new AjaxUpload('upload-button', {
            action: '<%= Url.Action<GraduateController>(c => c.UploadBest(bestName, null)) %>',
            responseType: false,
            onSubmit: function (file, ext) {
                if (!(ext && /^(jpg)$/i.test(ext))) {
                    alert('Неверное расширение');
                    return false;
                }
                $("#indicator").show();
            },
            onComplete: function (file, response) {
                utils.randomPostfix("#user-photo");
                if (response == "error") {
                    alert('Слишком большой файл');
                } else {
                    $("#download").fadeIn();
                    $("#result-photo").fadeIn();
                    $("#default-photo").fadeOut();
                }
                $("#indicator").hide();
            }
        });
    });

</script>
</asp:Content>
