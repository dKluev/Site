<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<VacancyVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Root.Catalog" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%= Model.Vacancy.Description %>
    

<% Html.RenderAction<GroupController>(c => c.ForCourseTCList(Model.Vacancy.CourseTCList,false, GroupTitleType.Vacancy)); %>


</asp:Content>