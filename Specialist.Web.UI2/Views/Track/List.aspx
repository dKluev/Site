<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Catalog.ViewModel.TrackListVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--    <% if (Model.Courses != null && Model.Courses.Any()) { %>--%>
        <% Html.RenderAction<CourseController>(c => c.CourseListForModel(Model)); %>
<%--    <% }else { %>--%>
<%--        Ничего нет--%>
<%--    <% } %>--%>
</asp:Content>


