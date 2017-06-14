<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<GroupForumVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="DynamicForm"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Education.ViewModel"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% Html.RenderPartial(PartialViewNames.MessagePagedList, Model.UserMessages); %>

</asp:Content>