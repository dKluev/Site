<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>


<div class="paddingleft20">
<h4>������� ����� �������� ������������ ������������</h4> 
<form action="">
    <%= Html.TextBox("text","", new {id="cert-name", size=55}) %>
    <%= HtmlControls.Submit("��������", "search-elearning") %>
</form>
</div>

<div class="indicator"> </div>
<div id="course-list"> </div>
<br />



  



