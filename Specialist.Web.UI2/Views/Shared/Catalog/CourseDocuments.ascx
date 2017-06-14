<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CourseBaseVM>" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Util" %>
<%@ Import Namespace="Specialist.Entities.Const" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<h2 class="h2_block">Документы об окончании</h2>
<p>В зависимости от программы обучения выдаются следующие документы<sup>*</sup>:</p>

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


<p><sup>*</sup>До начала обучения вам необходимо предоставить копию диплома о высшем или среднем профессиональном образовании. </p>
<p>
<% if (Model.Course.IsTrackBool) { %>
 По окончании каждого отдельного курса, входящего в Программу повышения квалификации, в личном кабинете слушателя формируются электронные сертификаты об обучении по каждому отдельному курсу. 

<% if (Model.Course.IsDiplom) { %>
По окончании обучения по Дипломной программе  выпускнику выдается Диплом о профессиональной переподготовке установленного  образца.
<% }else { %>
По окончании обучения по Программе повышения квалификации выпускнику выдается Удостоверение о повышении квалификации установленного  образца.
<% } %>
<% }else { %>
    Сертификаты международного образца выводятся после окончания курса в личном кабинете слушателя. 
<% } %>
</p>



				
	
<%= SimpleLinks.AllDocuments() %>
