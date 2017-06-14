<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="MvcContrib.FluentHtml.ModelViewPage<EditCartVM>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const"%>
<%@ Import Namespace="MvcContrib.FluentHtml"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Context.ViewModel"%>


<asp:Content ID="Head" ContentPlaceHolderID="Head" runat="server">
<title>Редактирование заказа</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="/Scripts/Views/Cart/EditCourse.js?v=2" type="text/javascript"></script>

<%= Htmls.Title("Редактирование заказа")%>

<%if (Model.EditDetailID.HasValue){ %>
  <% Html.RenderAction<EditCartController>(
        cc => cc.EditCourse(Model.EditDetailID.Value)); %>
<%} else if(Model.EditTrackTC != null){ %>
  <% Html.RenderAction<EditCartController>(
        cc => cc.EditTrack(Model.EditTrackTC)); %>
<%} else if(Model.UserTestId.HasValue){ %>
  <% Html.RenderAction<EditCartController>(
        cc => cc.EditTestCert(Model.UserTestId.Value)); %>
<%} else if(Model.EditExamID.HasValue){ %>
  <% Html.RenderAction<EditCartController>(
        cc => cc.EditExam(Model.EditExamID.Value)); %>
<% } %>

</asp:Content>
