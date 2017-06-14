<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Print.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.ViewModel.GroupListVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
<style type="text/css">
a, a:link, a:hover, a:visited {
	color:black;
	text-decoration:none;
}
</style>
	<title>Расписание курсов Центра компьютерного обучения "Специалист" при МГТУ им.Н.Э.Баумана</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% Action<IEnumerable<Specialist.Entities.Context.Group>> groupTable = groups => {%>
  <table style="border-collapse: collapse;">
    <% foreach (var gr in groups) {%>
	<tr>
		<td style="border:1px #CCCCCC solid;width:500px;"><%= Html.CourseLinkAnchor(gr.Course.UrlName, gr.Course.GetName()).AbsoluteHref() %></td>
		<td style="border:1px #CCCCCC solid;width:90px;">
		<%= gr.DaySequence %> <%=gr.DateInterval%>
		</td>
		<td style="border:1px #CCCCCC solid;width:50px;"><%=gr.TimeInterval%></td>
		<td style="border:1px #CCCCCC solid;width:50px;">
			<%=gr.Complex.GetOrDefault(x => x.Name) %>
		</td>
	</tr>
<% } %>
  </table>
<% }; %>

<h1>Расписание курсов Центра компьютерного обучения "Специалист" при МГТУ им.Н.Э.Баумана</h1>
   <% if(!Model.Filter.CourseTC.IsEmpty()) { %>
         <p><strong>Курс:</strong> <%= Html.CourseLink(Model.Course)%></p> 
    <% } %>
       

    
         <% if(Model.Filter.BeginDate.HasValue || Model.Filter.EndDate.HasValue) { %>
           <p>   <strong>Дата начала занятий:</strong> 
        <% if(Model.Filter.BeginDate.HasValue) { %>
                c <%= Model.Filter.BeginDate.DefaultString() %>
<%--				<%= MonthUtil.GetName(Model.Filter.LeftMonth.Value) %>--%>
                
        <% } if(Model.Filter.EndDate.HasValue) { %>
                по <%= Model.Filter.EndDate.DefaultString() %>
<%--				<%= MonthUtil.GetName(Model.Filter.RightMonth.Value) %>--%>
        <% } %></p>
         <% } %> 
       
        
         <% if(!Model.Filter.DayShiftTC.IsEmpty()) { %>
		 <p>
               <strong>Время проведения занятий:</strong>
               <%= Model.Filter.GetCurrentDayShiftDescription()%>
		</p>
        <% } %>
       
         <% if(!Model.Filter.ComplexTC.IsEmpty()) { %>
           <p>      <strong>Комплекс:</strong>
               <%= Model.Filter.GetCurrentComplex().Name %> 
			   </p>
        <% } %>
    
      <% if(Model.Filter.EmployeeTC != null) { %>
           <p>
				<strong>Преподаватель:</strong>
               <%= Model.Filter.GetTrainer().Name %> 
		</p>
        <% } %>
			  <% if(Model.Filter.SectionId.HasValue) { %>
           <p>
				<strong>Направление:</strong>
               <%= Model.Filter.GetCurrentSection().Name %> 
			   </p>

			   <% groupTable(Model.Groups.Source); %>
        <% }else{ %>

			<% foreach (var section in Model.SectionGroups){ %>
				<h3><%= section.Key.Name %></h3>
			   <% groupTable(section.OrderBy(g => g.Course.Name)); %>

	        <% } %>

        <% } %>



    
</asp:Content>
