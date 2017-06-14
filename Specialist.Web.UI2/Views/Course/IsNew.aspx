<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.ViewModel.IsNewCoursesVM>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<%= Model.Text %>
 <% Html.RenderAction<CourseController>(c => c.IsNewBlock()); %>

</asp:Content>
