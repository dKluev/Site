<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<CertificationVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Images.Entity(Model.Certification).Class("float_left") %>
    <%= Model.Certification.Description %>
    
    <% if(!Model.Certification.Exams.Any()){ %>
        <% if(Model.Children.Any()){ %>
        <h3>Сертификации</h3>
            <%= Htmls.DefaultList(Model.Children.Select(x => Html.CertificationLink(x))) %>
        <% } %>
    <% } %>
    
    <% Action<IEnumerable<Exam>> renderExamList = x => 
           Html.RenderPartial( Model.IsMicrosoft ? Views.Certification.MsExamList 
           : Views.Certification.ExamList, x); %>
    
    
    <% var groups = Model.ExamGroups(); %>
    <% foreach(var group in groups){ %>
    <% if(groups.Count() > 1) { %>
    <h3>Путь <%= group.Key %></h3>
    <% } %>
    <% var requiredExams = group.Where(ce => ce.IsRequired).Select(ce => ce.Exam); %>
    <% if(requiredExams.Any()){ %>
   <h4>Обязательные экзамены</h4>
   <% renderExamList(requiredExams); %>
   <% } %>
    <% var exams = group.Where(ce => !ce.IsRequired).Select(ce => ce.Exam); %>
    <% if(exams.Any()){ %>
    <h4>
        Экзамены по выбору</h4>
    <% renderExamList(exams); %>
    <% } %>
    <% } %>
    
    
    <% foreach(var table in Model.Certification.CertificationTables
           .Where(t => t.CertificationExams
               .All(ce => ce.CertificationVariant_ID == null))){ %>
    <h4> <%= table.Description %></h4>
    <% renderExamList(table.CertificationExams.Select(ce => ce.Exam)); %>
    <% } %>
    <% var variants = Model.Certification.CertificationVariants; %>

    <% if(variants.Count > 1){ %>
    <h4> Варианты прохождения сертификации</h4>
    <ul class="defaultUl">
        <% foreach(var variant in variants){ %>
        <li><a href="#variant<%= variant.CertificationVariant_ID %>">
            <%= variant.Description %></a></li>
        <% } %>
    </ul>
    <% } %>
    <% foreach(var variant in Model.Certification.CertificationVariants){ %>
        <h3>
            <a name="variant<%= variant.CertificationVariant_ID %>"></a> 
            <%= variant.Description %></h3>
        <% foreach(var table in variant.CertificationExams.Select(ce => ce.CertificationTable)
               .Where(ct => ct != null).Distinct()){ %>
            <h4>
                <%= table.Description %></h4>
            <% renderExamList(table.CertificationExams.Select(ce => ce.Exam)); %>
        <% } %>  
            <% renderExamList(variant.CertificationExams.Where(ce => ce.CertificationTable == null)
               .Select(ce => ce.Exam)); %>  
                  
    <% } %>
    
   <% Html.RenderPartial(Views.Shared.Block.ActionsBlock, Model.Actions); %>
	
	<% if(Model.Trainers.Any()){ %>
	<% if(Model.Trainers.Any(x => x.IsTrainer)){ %>
	<h3>Преподаватели</h3>
    <% }else{ %>
	<h3>Сотрудники</h3>
    <% } %>
	<%= Htmls.DefaultList(Model.Trainers.Select(x => Html.EmployeeLink(x))) %>
    <% } %>
    
    <%= Html.Site().NearestGroups(Model.Groups) %>
    
    <script type="text/javascript">
        $(function() {
            $('div.dropdown-content').hide();
            $('ul.sertifications > li a[href="#"]').click(
                function () {
                    $(this).parent().next().slideToggle();
                    $(this).children().toggleClass('changed-list-icon');
                    return false;
                }
            );
        });
        
    </script>
</asp:Content>


<asp:Content ContentPlaceHolderID="RightColumn" runat="server">


<%= Htmls2.ChamBegin(true) %>
    <% if(Model.Tracks.Any()){ %>
        <% Html.RenderPartial(PartialViewNames.CertificationTracks, Model.Tracks); %>
    <% } %>
    <% Html.RenderPartial(PartialViewNames.MainNews, true);%>
<%= Htmls2.BlockEnd() %>
</asp:Content>
