<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Tuple<string>>" %>
<%@ Import Namespace="SimpleUtils.Common" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Services.Common" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Common.Html" %>
<%@ Import Namespace="Specialist.Services.Interface.Passport" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Cms" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="SimpleUtils.FluentAttributes.Core"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="head" runat="server">
    <title>Генератор картинки</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Генератор картинки</h1>
    <form action="">
        <p>
            <%= H.textarea[Model.Item1].Name("text").Style("width:400px;height:200px;") %>
        </p>
        <%= H.Submit("Создать") %>
    </form>
    <% var url = Urls.CdnRoot + CdnFiles.ImageUrls.TempSocialImage + "info.png"; %>

    <p>
    <%= H.InputText("",url).Style("width:500px;") %>
    </p>
    <p>
        <%= H.Img(url + "?" + Guid.NewGuid()) %>
    </p>
    

</asp:Content>
