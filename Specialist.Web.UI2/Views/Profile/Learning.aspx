<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<LearningVM>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Profile.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Entities.Profile"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="attention2">
       <p>
<strong>�� �������� ������ ��� �������� ��������� �����������:</strong> <br/>
�� ������ ������� ����������� ������ ������������� ������ ����������� <br/>
�� ������ ������� ����������� ����������, ��������������, ��� �� ���������� ���������� �������� ������<br/>
��� ������ �������� ���������� ������� ����� ������<br/>
�� ������ ���������� � �������������� � ���������������<br/>
<%= Url.Profile().Public(Model.User.UserID, "������ �� ��� �������") %>  � �� ������ �������� �� � ���� ������, ������������ � ���������� �����, � ����� ��� � ����� ������ �����
           
       </p>
       
   </div> 
<% if(Model.Student.IsNull()){ return; }%>
<% var hotGroupList = SimpleLinks.GroupDiscounts("������� �����").ToString(); %>

    <a name='<%= LearningListType.Current %>'></a> 
    <h3>����������� �����</h3>
    <% if(Model.Student.ComingGroups.Any()) { %>
        <table class="defaultTable">
             <tr>
                <th>
                    ���� ������
                </th>
                <th>
                    ���� ���������
                </th>
                <th>
                    ����
                </th>
                <th>
                    <%= Images.Main("add_ingrey.gif") %>
                </th>
                 <th> ��� �������� </th>
                <th>
                    �������� ������
                </th>
            </tr>


        <% foreach (var group in Model.Student.ComingGroups) { %>
            <tr>
                <td><%= group.DateBeg.DefaultString() %></td>
                <td><%= group.DateEnd.DefaultString() %></td>
                <td><%= Html.CourseLinkByGroup(group) %></td>
                <td>
                     <%= group.DateBeg.HasValue && group.DateEnd.HasValue ? Html.Calendar(group.Group_ID) : null %>  
                </td>
                <td> <%= CourseStudyTypes.GetLinks(group, Model.IsWebinar(group.Group_ID)) %> </td>
                <td><%= group.IsLightBlue ? null : Html.GroupLink(group) %></td>
            </tr>
        <% } %>
        </table>
    <% }else{ %>
       � ��� �� ������ �� ���� ����. <%= hotGroupList %>
    <% } %>    
    




    <a name='<%= LearningListType.Current %>'></a> 
    <h3>������� �����</h3>
	<% var groups = Model.Student.CurrentGroups; %>

    <% if(groups.Any()) { %>
	<% var hasTest = groups.Any(x => x.Course.TestId.HasValue); %>
        <table class="defaultTable">
             <tr>
                <th> ���� ������ </th>
                <th> ���� ��������� </th>
                <th> ���� </th>
                <th> ��� �������� </th>
                <th> � ������ </th>
                <th> �������� ������ </th>
			    <% if(hasTest) { %>
                <th> ������������ </th>
			    <% } %>    
            </tr>


        <% foreach (var group in groups) { %>
        <% if(group.IsNotVisible)continue; %>
            <% var debt = Model.Debt(group.Group_ID); %>
            <tr>
                <td><%= group.DateBeg.DefaultString() %></td>
                <td><%= group.DateEnd.DefaultString() %></td>
                <td><%= Html.CourseLinkByGroup(group) %></td>
                <td> <%= CourseStudyTypes.GetLinks(group, Model.IsWebinar(group.Group_ID)) %> </td>
                <td> <%= debt > 0 ? H.b[debt.MoneyString()].Style("color:red;").ToString() : "��������" %> </td>
                <td><%= Html.GroupLink(group) %></td>
			    <% if(hasTest) { %>
                <td>
				    <% if(group.Course.TestId.HasValue) { %>
	                 <%= Url.Test().CoursePlanned(group.Course_TC, "������������")  %> 
				    <% } %>    
                </td>
			    <% } %>    
            </tr>
            <% } %>
        </table>
    <% }else{ %>
       ������ � ��� ��� �������. <%= hotGroupList %>
    <% } %>    
    
    <a name='<%= LearningListType.Ended %>'></a> 
     
    <h3>����������� �����</h3>

	<% var endgroups = Model.Student.EndedGroups; %>

    <% if(endgroups.Any()) { %>
    <% if(Model.Student.Card != null && ClabCardColors.WebinarRecord.Contains(Model.Student.Card.ClabCardColor_TC)){ %>
        <p>���������� � ������� � ������ �������� �� �����, �� ������� ����� �� �������� ������</p>
    <% } %>    

	<% var hasTest = endgroups.Any(x => x.Course.TestId.HasValue); %>
        <table class="defaultTable">
            <tr>
                <th>
                    ���� ������
                </th>
                <th>
                    ���� ���������
                </th>
                <th>
                    ����
                </th>
                <th> ��� �������� </th>
                <th>
                    �������� ������
                </th>
			    <% if(hasTest) { %>
                <th> ������������ </th>
			    <% } %>    
            </tr>

        <% foreach (var group in endgroups) { %>
            <tr>
                <td><%= group.DateBeg.DefaultString() %></td>
                <td><%= group.DateEnd.DefaultString() %></td>
                <td><%= Html.CourseLinkByGroup(group) %></td>
                <td> <%= CourseStudyTypes.GetLinks(group, Model.IsWebinar(group.Group_ID)) %> </td>
                <td><%= Html.GroupLink(group) %></td>
			    <% if(hasTest) { %>
                <td>
				    <% if(group.Course.TestId.HasValue) { %>
	                 <%= Url.Test().CoursePlanned(group.Course_TC, "������������")  %> 
				    <% } %>    
                </td>
			    <% } %>    
            </tr>
            <% } %>
        </table>
    <% }else{ %>
       �� ��� �� ��������� � ����� ������. <%= hotGroupList %>
    <% } %>    
    <a name='<%= LearningListType.Exam %>'></a> 
    <h3>��������</h3>
    <% if(Model.Student.Exams.Any()) { %> 
        <table class="defaultTable">
            <tr>
                <th>
                    ���� �����
                </th>
                <th>
                    �������
                </th>
            </tr>

        <% foreach (var sig in Model.Student.Exams) { %>
            <tr>
                <td><%= sig.Group.DateBeg.DefaultString() %></td>
                <% if(sig.Exam == null) { %>
                <td><%= sig.Group.Course.Name %></td>
                <% }else{ %>
                <td><%= Html.ExamLinkName(sig.Exam) %></td>
                <% } %>
            </tr>
        <% } %>
        </table>
    <% }else{ %>
        ���� ������ ���
    <% } %>
    
    <% if(Model.SigEvents.Any()){ %>
    <h2>��������</h2>
     <table class="defaultTable">
            <tr>
                <th>����</th>
                <th>����������</th>
                <th>���</th>
            </tr>
    <% foreach (var sigEvent in Model.SigEvents) { %>
         <tr>
            
            <td><%= sigEvent.EventDate.DefaultString() %></td> 
            <td><%= sigEvent.EventNotes %></td>
             <td><%= sigEvent.PostalTrackingNumber %></td>
         </tr>
    <% } %>
         </table>
    <% } %>
	
	<% if(Model.NextCourses.Any()){ %>
	<h2>��� ������������ �������� ����������� �������� ���� ������������ �� ��������� ������</h2>
<table>
    <tr>
        <td>
	<%= Images.Employee(Model.Manager.Employee_TC) %> <br/>
    <p><%= Model.Manager.FullName %></p>
            
        </td>
        <td style="vertical-align: top;">
            
	<%= Htmls.DefaultList(Model.NextCourses.Select(x => Html.CourseLink(x).ToString())) %>
        </td>
    </tr>
</table>
	<% } %>
	
	<% if(Model.NextGroups.Any()){ %>
	<h2>������������� ������</h2>
	<% Html.RenderPartial(Views.Shared.Education.NearestGroupList,
		   new NearestGroupsVM(Model.NextGroups)); %>
	<% } %>

    <p>
    ���� � ������ ������ ��������������������������������� ���� ������ (������� ���� ���������) - ����������, 
	<%= Html.ActionLink<PageController>(c => c.SendForWebMaster(
	"����������� ���� � �������"),
                        "�������� ���")%>.
    
    </p>
	

</asp:Content>
