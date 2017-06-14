<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Catalog.ViewModel.MainCoursesVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
	<%= MHtmls.LongList(MHtmls.Title("Курсы:"),
         Html.Site().MobileSections(
		 Model.RootSections.Select(x => x.Entity.As<Section>()).ToList())) %>
<% Html.RenderPartial(Views.Shared.Education.NearestGroupMobile); %>
</asp:Content>
