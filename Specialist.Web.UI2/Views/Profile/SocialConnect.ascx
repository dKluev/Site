<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Passport.User>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<h2>Facebook</h2>
<div class="fb-login-button" data-scope="publish_stream,create_event" data-show-faces="true" data-width="400" data-max-rows="1"></div>

<h2>ВКонтакте</h2>
<% if (!Model.VkToken.IsEmpty()) { %>
    <strong>Вы авторизованы</strong> <br/>
<% } %>
<%= H.Anchor("http://oauth.vk.com/authorize?client_id=3388678&scope=offline,wall&redirect_uri=blank.html&response_type=token"
,"Пройдите по ссылке") %>, после того как со всем согласитесь, вы попадете на белый экран с текстом, скопируйте урл этой страницы и вставте в поле ввода
<br/>
<%= Images.Common("vkontakte_access.jpg") %>
<%= H.Form(Url.Action<ProfileController>(c => c.VKontakteAccessToken(null)))[H.strong["Введите урл"],
H.InputText("tokenUrl","").Style("width:400px;"),H.button["ОК"]] %>


<script>
    initFbConnect(function(token) {
		var data = { token: token };
		$.post("<%= Url.Action<ProfileController>(c => c.FaceBookAccessToken(null)) %>", data);
    })
</script>



<script>
	function fbLogout() {
		FB.logout(function (response) {
			//Do what ever you want here when logged out like reloading the page
			window.location.reload();
		});
	}
</script>

<%--<span id="fbLogout" onclick="fbLogout()"><a class="fb_button fb_button_medium"><span class="fb_button_text">Logout</span></a></span>--%>