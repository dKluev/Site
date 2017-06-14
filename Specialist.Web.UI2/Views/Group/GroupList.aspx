<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<GroupListVM>" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop"%>
<%@ Import Namespace="Specialist.Web.Controllers.Shop"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Helpers"%>
<%@ Import Namespace="Specialist.Web.Util"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Util"%>
<%@ Import Namespace="MvcContrib.Unity" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Filter" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
   <% if(Model.Course != null) { %>
         <p><strong>����:</strong> <%= Html.CourseLink(Model.Course)%></p> 
    <% } %>
       

    
         <% if(Model.Filter.BeginDate.HasValue || Model.Filter.EndDate.HasValue) { %>
           <p>   <strong>���� ������ �������:</strong> 
        <% if(Model.Filter.BeginDate.HasValue) { %>
                c <%= Model.Filter.BeginDate.DefaultString() %>
                
        <% } if(Model.Filter.EndDate.HasValue) { %>
                �� <%= Model.Filter.EndDate.DefaultString() %>
        <% } %></p>
         <% } %> 
       
        
         <% if(!Model.Filter.DayShiftTC.IsEmpty()) { %>
		 <p>
               <strong>����� ���������� �������:</strong>
               <%= Model.Filter.GetCurrentDayShiftDescription()%>
		</p>
        <% } %>

         <% if(Model.Filter.StudyTypeId > 0) { %>
		 <p>
               <strong>����� ��������:</strong>
			<%= GroupFilter.StudyTypes[Model.Filter.StudyTypeId] %>
		</p>
         <% } %> 

         <% if(!Model.Filter.DaySequenceTC.IsEmpty()) { %>
		 <p>
               <strong>��������:</strong>
               <%= DaySequences.GetName(Model.Filter.DaySequenceTC) %>
		</p>
        <% } %>
       
         <% if(!Model.Filter.ComplexTC.IsEmpty()) { %>
           <p>      <strong>��������:</strong>
               <%= Html.ComplexLink(Model.Filter.GetCurrentComplex()) %> 
			   </p>
        <% } %>
		  <% if(Model.Filter.SectionId.HasValue) { %>
           <p>
				<strong>�����������:</strong>
               <%= Html.SectionLink(Model.Filter.GetCurrentSection())%> 
			   </p>
        <% } %>
    
     <% if(Model.Filter.EmployeeTC != null) { %>
           <p>
				<strong>�������������:</strong>
               <%= Html.TrainerLink(Model.Filter.GetTrainer())%> 
		</p>
        <% } %>
    
      <strong><%=  Html.ActionLinkEx<GroupController>( gc => gc.Search(Model.Filter),
            "�������� ������") %></strong> 

        
           
           <% if(Model.Groups.IsEmpty()){ %>
           <br/>
          <h2>�� ������� ������� ������ �� �������</h2> 
           
           <% }else{ %>
    <% var pdfUrl = StringUtils.GetHref(Html.ActionLinkEx<GroupController>(gc => gc
           .ListPdf(Model.Filter),
           "������� PDF")); %>
  
 <% Html.RenderPartial(PartialViewNames.NearestGroupList,
				 new NearestGroupsVM(Model.Groups, Model.Course != null){PdfUrl = pdfUrl}); %>

     <%= Html.GetNumericPager(Model.Groups) %>
     
     <% } %>
    
</asp:Content>
