<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<AdviceListVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% Html.RenderPartial(PartialViewNames.AdviceList, Model.Advices); %>

<%= Html.GetNumericPagerPretty(Model.Advices) %>

</asp:Content>


