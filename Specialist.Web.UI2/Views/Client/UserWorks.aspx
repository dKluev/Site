<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<UserWorksVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Utils"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%= Html.Site().UserWorks(Model.UserWorks) %>
<%= Html.GetNumericPagerPretty(Model.UserWorks) %>

</asp:Content>
