<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.ViewModel.ExamVM>" %>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Examination.Const" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="Specialist.Entities.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<% if(!Model.Exam.Available){ %>
    <% if(Exams.Symantec.Contains(Model.Exam.Exam_TC.ToLower())){ %>
        <%= Htmls.HtmlBlock(HtmlBlocks.SymantecExam) %>
    <% }else{ %>
        <%= Htmls.HtmlBlock(HtmlBlocks.ExamUnavailable) %>
    <% } %>
<% } %>

    
    
<%= string.IsNullOrEmpty(Model.Exam.AdditionalInfo) ? null : H.div.Class("attention")[Model.Exam.AdditionalInfo] %>

<%--<% if (Model.Exam.Exam_TC == "002.10") { %>--%>
<%--<%= Htmls.HtmlBlock(HtmlBlocks.KasperExam) %>--%>
<%--<% } %>--%>
<%----%>
<%--<% if(Model.Exam.Exam_TC == "EX0-001"){ %>--%>
<%--<div class="attention">--%>
<%--<p style="font-size:120%;"><strong>��������!  </strong>��������� �������, ������ ������� ��� �������� <strong>EX0-017 ITIL Foundation  (syllabus 2011)</strong> �� ������� <strong>EX0-001 ITIL Foundation (syllabus 2011)</strong>. ���������� �� ���������� ������� �� ����������� ITIL Foundation �� ������ �� ������ <%= H.Anchor("http://www.specialist.ru/exam/itil-foundation") %> --%>
<%--    --%>
<%----%>
<%--</p>--%>
<%--</div>--%>
<%--    <% } %>--%>
    
<%= Htmls2.BorderBegin("�������� ��������") %>
<div class="tab_content2">
		<p>
        <strong>�����:</strong>
        <%= Model.Exam.Exam_TC %>
        </p>
        <% if(Model.Exam.ExamPrice.HasValue){ %>
        <p>
            <strong>����:</strong>
            <%= Model.Exam.ExamPrice.MoneyString() %>
            ���.</p>
        <% } %>

        <% if(!Model.Exam.Minutes.IsEmpty()){ %>
        <p>
            <strong>����������������� �����:</strong>
            <%= Model.Exam.Minutes %>
            ���.</p>
        <% } %>
        <% if(!Model.Exam.Languages.IsEmpty()){ %>
        <p>
            <strong>�����:</strong>
            <%= Model.Exam.Languages %>
        </p>
        <% } %>
        <% if(Model.Exam.DatePublished.HasValue){ %>
        <p>
            <strong>���� ����������:</strong>
            <%= Model.Exam.DatePublished.DefaultString() %>
            <% if(Model.Exam.DatePublished.Value > DateTime.Today){ %>
            (� ����������)
            <% } %>
        </p>
        <% } %>
        <% if(Model.Exam.DateClosed.HasValue){ %>
        <p>
            <strong>���� ��������:</strong>
            <%= Model.Exam.DateClosed.DefaultString() %>
            <% if(Model.Exam.DateClosed.Value < DateTime.Today){ %>
            (�������)
            <% } %>
        </p>
        <% } %>
        <% if(!Model.Exam.NumberOfQuestions.IsEmpty()){ %>
        <p>
            <strong>���-�� ��������:</strong>
            <%= Model.Exam.NumberOfQuestions %>
        </p>
        <% } %>
       
        <% if(Model.Exam.Certifications.Any()){ %>
        <p>
            <strong>������������:</strong>
        </p>
        <ul class="defaultUl">
            <% foreach (var certification in Model.Exam.Certifications.Distinct()) { %>
            <li>
                <%=Html.CertificationLink(certification)%></li>
            <% } %>
        </ul>
        <% } %>
        <% if(Model.Exam.ExamProviders.Any()){ %>
        <p>
            <strong>���������:</strong>
            <% var provider = Model.Exam.ExamProviders.Select(ep => ep.Provider).First(); %>
            <%= Html.ActionLink<ExamController>( 
        c => c.Provider(provider.Provider_ID), provider.Name) %>
        </p>
        <% } %>
        <% if(Model.Exam.Vendor != null){ %>
        <p>
            <strong>������:</strong>
            <%= Html.GetLinkWithoutCoursesPrefixFor(Model.Exam.Vendor) %>
        </p>
        <% } %>

        <%= Html.AddToCart(Model.Exam, true) %>
    </div>
  

  
<%= Htmls2.BorderEnd() %>


  
   <% Html.RenderAction<CourseController>(c => c.CourseListFor(Model.Exam)); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="RightColumn" runat="server">
<%= Htmls2.ChamBegin(true) %>
    <% Html.RenderPartial(PartialViewNames.NearestGroupsBlock, Model.Groups); %>
    <% Html.RenderPartial(PartialViewNames.MainNews, true);%>

<%= Htmls2.BlockEnd() %>
</asp:Content>
