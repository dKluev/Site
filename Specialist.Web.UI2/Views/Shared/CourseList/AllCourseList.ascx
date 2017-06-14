<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.ViewModel.AllCourseListVM.Entity>" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Const" %>



<%
    Html.RenderAction<CourseController>(c =>
        c.CourseList(Model.TypeName, Model.PK, Model.Name)); %>





