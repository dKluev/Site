<%@ Page Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Passport.ViewModel.LogOnVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="Specialist.Web.Cms.Root.Socials" %>


<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
	<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/themes/redmond/jquery-ui.css"
		type="text/css" />
    
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js" type="text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/i18n/jquery.ui.datepicker-ru.js" type="text/javascript"></script>
    

    <%= H.b[H.Anchor(SimplePages.FullUrls.ProfileInstruction, "Инструкция по работе с Личным кабинетом сайта")]  %>
    
<% var registerUrl = Model.IsReturnAddSeminar 
     ? Url.SimpleReg().Registration(Model.ReturnUrl, "зарегистрируйтесь").ToString()
     : Html.ActionLink<ProfileController>(c => c.Register(null,Model.ReturnUrl,null), "зарегистрируйтесь").ToString(); %> 
    

<p>Если Вы первый раз на нашем сайте,  <%= registerUrl %>. <%= Htmls.RegBlock() %>
</p>
<p>Если Вы уже зарегистрированы на нашем сайте, введите ваш e-mail и пароль.</p>
<% Html.RenderPartial(PartialViewNames.LogOnControl, Model); %>
    
    
    <%= H.button["Использовать аккаунт Facebook"].Id("facebook-register") %>
<div class="attention"><strong>Внимание!</strong><p>Если Вы являетесь выпускником Центра, то для получения доступа к сервисам выпускника Вам также необходимо  <%= Html.ActionLink<ProfileController>(c => c.Register(null,null,null),
                                                                                                                                                                      "зарегистрироваться")%> на сайте и в вашем профиле указать логин и пароль из памятки выпускника.</p>  </div>
    
    <script>
        $(function() {
            $("#facebook-register").button();
            initFbConnect();
            function processResponse(response) {
                if (response.status === 'connected') {
                    document.location.href = updateURLParameter("<%= Url.Action<ProfileController>(x => x.FacebookLogin(null,Model.ReturnUrl)) %>", "token", response.authResponse.accessToken);
                }
            }

            $("#facebook-register").click(function(e) {
                e.preventDefault();
                FB.login(processResponse, { scope: '<%= FacebookService.ConnectPermission %>' });
            });
        });


    </script>


</asp:Content>
