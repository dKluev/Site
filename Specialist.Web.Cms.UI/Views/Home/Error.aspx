<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TrackVM>" %>
<%@ Import Namespace="SimpleUtils.Common" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Services.Common" %>
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
    <title>Ошибка</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Ошибка</h1>
    Что то пошло не так <br/>
    <%= Server.GetLastError().GetOrDefault(x => x.GetBaseException().Message) %>
</asp:Content>
