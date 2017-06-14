<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.CommonVM<List<Specialist.Entities.Utils.Grouping<Section,Course>>>>" %>
<%@ Import Namespace="Specialist.Web.Const" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
        <%= Specialist.Web.Common.Utils.CacheUtils.Get("WithoutWebinar",
			   () => Html.Partial(PartialViewNames.CoursesBySections, Model.Data) ) %>
</asp:Content>
