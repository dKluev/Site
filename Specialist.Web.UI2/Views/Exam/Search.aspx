<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Root.Exams.ViewModels.ExamSearchVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="DynamicForm" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% using (Html.DefaultForm<ExamController>(c => c.Search(Model.Vendor.Vendor_ID, null))) { %>
    <% Htmls.FormSection(" ", () => {%> 
	<%= Html.ControlFor(x => x.Number) %>
    <% }); %>
    <%= Htmls.Submit("ok") %>
<% } %>

<% if(!(Model.Number.IsEmpty() || Model.Exams.Any())){ %>
	Ничего не найдено
<% } %>

<% Html.RenderPartial(PartialViewNames.CommonExamList, Model.Exams); %>
  
</asp:Content>
