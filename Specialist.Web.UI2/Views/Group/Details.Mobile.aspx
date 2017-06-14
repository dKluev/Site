<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<GroupVM>" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="Specialist.Entities.Education.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Center" runat="server">
    
	<%= MHtmls.LongList(MHtmls.Title(Model.Group.Course.WebName),
         MHtmls.MainList(Html.Site().MobileGroupLectures(Model.Lectures))) %>

</asp:Content>