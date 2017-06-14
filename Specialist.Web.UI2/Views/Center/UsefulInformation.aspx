<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<UsefulInformationVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%-- <%= Images.UsefulInfomation(Model.UsefulInfo).FloatLeft()%> --%>
<%= Model.UsefulInfo.Description %>  

</asp:Content>