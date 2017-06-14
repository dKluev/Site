<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CourseBaseVM>" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<h2 class="h2_block">��������� �� ���������</h2>
<p>� ����������� �� ��������� �������� �������� ��������� ���������<sup>*</sup>:</p>

<% foreach(var certTypes in Model.Certificates.CutInPartCount(3)){ %>
<div class="docs">

<% foreach(var certType in certTypes){ %>
	<div class="doc">
	<% if(certType != null){ %>
		<%= H.Anchor(Urls.Image(
			certType.GetType().Name + "/" + certType.UrlName + ".jpg"),
				Images.Image(certType.GetType().Name + "/Small/" + certType.UrlName + ".jpg").Class("img_left").Width(CertTypes.Vertical
				.Contains(certType.CertType_TC) ? null : "200").ToString())
			.Class("fancy-box").Title(certType.Name).Rel("course-document") %>
		  
		<% } %>
		<br class="clear"></br>
			<p> <%= certType.CertTypeName %></p>
<%--		<% if(!certType.Description.IsEmpty()){ %>--%>
<%--			<p> <%= certType.Description %></p>--%>
<%--		<% } %> --%>
		</div>
<% } %> 
</div>
<% } %>


<p><sup>*</sup>�� ������ �������� ��� ���������� ������������ ����� ������� � ������ ��� ������� ���������������� �����������. </p>
<p>
<% if (Model.Course.IsTrackBool) { %>
 �� ��������� ������� ���������� �����, ��������� � ��������� ��������� ������������, � ������ �������� ��������� ����������� ����������� ����������� �� �������� �� ������� ���������� �����. 

<% if (Model.Course.IsDiplom) { %>
�� ��������� �������� �� ��������� ���������  ���������� �������� ������ � ���������������� �������������� ��������������  �������.
<% }else { %>
�� ��������� �������� �� ��������� ��������� ������������ ���������� �������� ������������� � ��������� ������������ ��������������  �������.
<% } %>
<% }else { %>
    ����������� �������������� ������� ��������� ����� ��������� ����� � ������ �������� ���������. 
<% } %>
</p>



				
	
<%= SimpleLinks.AllDocuments() %>
