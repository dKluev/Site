<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<EditExamQuestionnaireVM>" %>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Const"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <% using (Html.BeginForm()) {%>
                
                   <% Html.RenderPartial(PartialViewNames.EditExamQuestionnaireControl, 
                          Model.ExamQuestionnaire);%>
               <%= Htmls.Submit("ok") %>
            <% } %>
</asp:Content>


