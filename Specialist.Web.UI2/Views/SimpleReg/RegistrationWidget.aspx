<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Passport.SimpleRegUser>" %>
<%--MasterPageFile="~/Views/Shared/Site.Master"--%>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="MvcContrib" %>
<%@ Import Namespace="Specialist.Web.Cms.Root.Socials" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/Views/simplereg.css" rel="stylesheet" />
</asp:Content>--%>

<link rel="stylesheet" type="text/css" href="/Content/Views/simplereg.css">

<%--<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">--%>
    <div id="wrapper">

        <!-- form -->
        <div class="form-wrapper">

            <%--<br><h1>RegistrationWidget</h1><br>--%>

            <h2><% if (Model.Source_TC != null && Model.Source_TC.ToUpper().Equals("YT")) Response.Write("Получить доступ к видео");
                    else if (Model.Source_TC != null && Model.Source_TC.ToUpper().Equals("SITE")) Response.Write("Записаться на семинар");
                    else Response.Write("Быстрая регистрация"); %> </h2>

            <!-- nav -->
            <div class="form-nav">
                <div class="form-nav__item">
                    <span><%=Html.ActionLink<AccountController>(x=>x.LogOn(Model.Url), "Войти")%>
                    </span>
                </div>
                <div class="form-nav__item">
                    <span>Зарегистрироваться</span>
                </div>
            </div>
            <!--/nav -->

            <!-- slider -->
            <div class="form-slider">
                <div class="form-slider__runner">

                    <!-- item -->
                    <div class="form-slider__item">
                        <form action="/simplereg/registrationpost" method="post" autocomplete="on">

                            <!-- inputs -->
                            <div class="form-inputs">
                                <div class="form-inputs__input">
                                    <input type="text" name="Name" placeholder="Имя">
                                </div>
                                <div class="form-inputs__input">
                                    <input type="text" name="LastName" placeholder="Фамилия">
                                </div>
                                <div class="form-inputs__input">
                                    <input type="text" name="Email" placeholder="Ваш e-mail">
                                </div>
                                <%=Html.HiddenFor(x=>x.Source_TC)%>
                                <%=Html.HiddenFor(x=>x.Url)%>
                            </div>
                            <!--/inputs -->

                            <!-- submit -->
                            <div class="form-submit">
                                <button type="submit">
                                    <span>Получить доступ</span>
                                </button>
                            </div>
                            <!--/submit -->

                        </form>
                    </div>
                    <!--/item -->



                </div>
            </div>
            <!-- slider -->

        </div>
        <!--/form -->

    </div>


<%--</asp:Content>--%>