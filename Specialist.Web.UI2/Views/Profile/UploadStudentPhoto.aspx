<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<UploadStudentPhotoVM>" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="DynamicForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model.PhotoExists) { %>
      <h3>Фотография внесена в базу</h3>
    <% } else { %>
        <%= Html.ValidationSummary() %>
        <% using (Html.DefaultForm<ProfileController>(c => c.UploadStudentPhoto(),
               Htmls.FormWithFile)) { %>
        <%= Html.ControlFor(x => x.Photo) %>
        <%= Htmls.Submit("ok") %>
        <% } %>
    <% } %>
</asp:Content>
