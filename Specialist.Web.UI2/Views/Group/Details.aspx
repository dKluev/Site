<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<GroupVM>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.Const" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Entities.Passport" %>
<%@ Import Namespace="Specialist.Web.Common.Cdn" %>
<%@ Import Namespace="Specialist.Web.Controllers.Message"%>
<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="Specialist.Entities.Education.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Entities.Order.Const" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= H.JQuery("controls.initSlider('content-');") %>
    <h3>
        ���� ����������</h3>
    <p><%= Model.Group.DateInterval %></p>

		<div class="overflow">
							<div class="page_group_l">
    
<% if(Model.Group.ShowTeacher) { %>
    <h3>
        �������������</h3>
        <%= Images.Employee(Model.Group.Teacher_TC).FloatLeft() %>
        <p>
            <strong>
                <%= Html.TrainerLink(Model.Group.Teacher) %></strong></p>
        <p>
            <% if(Request.IsAuthenticated){ %>
            <strong>E-mail:</strong> 
            <%= HtmlControls.MailTo(Model.Group.Teacher.FirstEmail) %>
            <% } %>
        </p>
<% } %>
</div>
	<div class="page_group_r">
							<div class="page_group_r_ie">

    <% if(Model.Lectures.Any()){ %>
    <h3> ��� ����������</h3>
    <table class="defaultTable group_page">
        <tr>
            <th>
                ���� �������
            </th>
            <th>
                �����
            </th>
            <th>
                ��������<br/>
		(����� �������)
            </th>
            <% if(!Model.Group.IsSem && Model.Lectures.Any(x => x.Contents.Any())){ %>
            <th class="plan">
                ���� �������
            </th>
			<% } %>
			<% if(Model.ShowWebinarUrl) { %>
            <th>
            </th>
			<% } %>
            <th>
                &nbsp;
            </th>
        </tr>
    <% foreach (var lectureVM in Model.Lectures) { %>
    <% var lecture = lectureVM.Lecture; %>
    
        <tr>
            <td class="date">
                <%= lecture.LectureDateBeg.Date.DefaultString() %><br/>
                <%= CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)
                    lecture.LectureDateBeg.Date.DayOfWeek]%><br/>
              
            </td>
            <td class="time"> <%= lecture.TimeInterval %> </td>
            <td class="complex">
                <%= Html.ComplexLink(lecture.ClassRoom.GetOrDefault(x => x.Complex) ) %>
            </td>
            <% var count = Model.GetNextEmptyContentLectureCount(lectureVM); %>
            <% if(!Model.Group.IsSem){ %>
                <% if (lectureVM.Contents.Any()) { %>
                <td class="plan" rowspan='<%= count + 1 %>'>
                    <% foreach (var content in lectureVM.Contents) { %>
                <div>
                    <a rel='content-<%= content.CourseContent_ID %>'
                    <%= Htmls.LinkButton %> href="#">
                        <%= content.ModuleName %>
                    </a>
                    <div id='content-<%= content.CourseContent_ID %>' class="td_hiden" 
                        <%= Htmls.DisplayNone() %>>
                            <%= content.ModuleDescription %>
                                 
                    </div>
                </div>
    			<br />
                    <% } %>
                </td>
                <% } %>
            <% } %>
			<% if(Model.ShowWebinarUrl) { %>
            <td>
				<% if(!lecture.WebinarURL.IsEmpty()) { %>
	                <%= H.Anchor(lecture.WebinarURL, "������ �� �������") %>
				<% } %>
            </td>
			<% } %>
            <td class="attendance">
                <%= Images.Attendance(lectureVM.Attendance) %>
            </td>
        </tr>
        <% } %>
    </table>
    <% if (!Request.IsAuthenticated) return; %>
	<%= Images.Attendances() %>
    <% } %>
                                
    <% if(Model.Progress != null && !Model.Group.IsSem){ %>
    <b>�������� �������� �� ����� <%= Model.Group.Course.WebName %></b>

    <span style="float:right">
        <%= "{0}� �� {1}�".FormatWith(Model.Progress.CourseCurrentHours, Model.Progress.CourseTotalHours) %>
    </span>
    <div class="group-progress">
        <div style="width: <%= Model.Progress.Course %>%;"></div>
    </div>
        <% if(Model.Progress.Track > 0){ %>
        <b>�������� �������� �� ����� <%= Model.Progress.TrackName %></b>
        <div class="group-progress">
            <div style="width: <%= Model.Progress.Track %>%;"></div>
        </div>
        <% } %>
    <% } %>
                                
                                
    <% if (Model.ShowVideoAllConditions) { %>
        <% if (Model.HideVideo) { %>
            <%= H.p[H.b["������ �������� ����� �������� ����� ��������� ������� �������"]] %>
        <% } else { %>
    <% if (Model.Group.HasVimeo) { %>
    <% if(Model.Group.MegaGroup_ID == Groups.MegaShev || 
            ((Model.Group.MegaGroup_ID.HasValue || Model.Group.IsIntraExtramural) && Model.Group.DateBeg >= new DateTime(2017,02,13))) { %>
    <h3>
        <%= Url.Group().Videos(Model.Group.Group_ID, "������ ��������") %> 
    </h3>
        <% } else { %>
    <h3>������ ��������</h3>
    <p>
        <%= H.Anchor(Model.Group.WebinarRecordURL) %> <br/>
        <b>������:</b> <%= Model.Group.WbnRecPwd %>
    </p>
        <% } %>


    <% } else if(Model.StudentInGroup != null) { %>
    <h3>������</h3>
    <%= H.Anchor("#", "������").Id("webinar-record-link") %> <br/>
							            <b>�����:</b> <%= Model.Group.WbnRecLogin %> <br/>
    <b>������:</b> <%= Model.Group.WbnRecPwd %>

    <div id="webinar-dialog" style="display: none;width: 600px;">
        <%= Htmls.HtmlBlock(HtmlBlocks.WebinarRecord) %><br/>
        <div style="padding-top:10px;"></div>
        
        <button id="webrec-agree">��������</button>
        <button id="webrec-disagree">�� ��������</button>
        
    </div>
    
    <script type="text/javascript">
        $(function () {
            $("#webinar-record-link").click(function () {
                controls.showDialog("#webinar-dialog",false);
                return false;
            });
            $("#webrec-agree").click(function () {
                var url = '<%= Model.Group.WebinarRecordURL %>';
                var win = window.open(url, '_blank');
                win.focus();
                controls.hideDialog();
                return false;
            });
            $("#webrec-disagree").click(function () {
                controls.hideDialog();
                return false;
            });

        });
    </script>

    <% } %>
    
    <% } %>
    
    <% } %>


    <% if(Model.ShowCert){ %>
        <% var link = Model.Group.IsSem
                ? null
//               ? Url.Graduate().SeminarCert(Model.StudentInGroup.StudentInGroup_ID,
//                   "������� ����������" + H.br + Images.Image(CdnFiles.ImageUrls.ImageUser + "seminar-small-cert.png"))
               : Url.Graduate().GroupCerts("�����������" + H.br + Images.Image(CdnFiles.ImageUrls.ImageUser + "group-small-cert.png")); %>
        <%= H.h3[link] %>
	<% } %>


	<% if (Model.Tests.Any()) { %>
	<h3>	������������ �� �����</h3>
	<%= H.Ul(Model.Tests.Select(x => Url.TestLink(x))) %>
	<% } %>
	<% if (Model.TestsAfterComplete.Any()) { %>
	<h3>	������������ �� ��������� �����</h3>
	<%= H.Ul(Model.TestsAfterComplete.Select(x => Url.TestLink(x))) %>
	<% } %>
	<% if (!Model.TrainerCourseInfo.IsEmpty()) { %>
    <h3> ���������� �� ������������� </h3>
    <%= Model.TrainerCourseInfo %>
	<% } %>
                                
    <% if(Model.HideForUnlimit){ %>
        <p>
            <b>�� �������� ������������� �� �������</b>
        </p>
	<% } %>
	<% if (!Model.HideForUnlimit && (Model.FileList.Files.Any() || Html.InRole(Role.Trainer))) { %>
    <h3> �������� ������� � ��������� ������</h3>
    <% Html.RenderPartial(PartialViewNames.FileList, Model.FileList); %>
	<% } %>
	<% if (!Model.HideForUnlimit && Model.ShowLibrary) { %>
       <h4><%= Html.ActionLink<ProfileController>(
                                c => c.Library(), "���������� ������� ����������") %></h4> 
	<% } %>
                                

    <% if(Model.StudentInGroup != null || Html.InRole(Role.Trainer)){ %>
    <h3>
        ������� ������</h3>
                                

    <% if(Model.StudentInGroup != null && (Model.VkGroupUrl != null || Model.Group.DateEnd < DateTime.Today)){ %>
    <%= H.Anchor(Model.VkGroupUrl ?? Url.Group().Urls.VkGroup(Model.Group.Group_ID, 0, null), "������ ���������") %>
    <% } %>


<p>
    <%= Html.ActionLink<MessageController>(c => c.Group(Model.Group.Group_ID),
        "��� ���������")%>
</p>

<% Html.RenderPartial(PartialViewNames.MessageList, Model.LastMessages); %>

    <% } %>

	</div>
							</div>
</div>
    
    

</asp:Content>