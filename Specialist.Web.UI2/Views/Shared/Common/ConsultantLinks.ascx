<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%= Images.Main("ico_chat.gif").Alt("���")%>
<a class="block1 ga-click" rel="chat" href="javascript:jivo_api.open();"> ��� ������������ </a>
<br />
<%= Images.Main("ico_icq.gif").Alt("icq")
		.Style("margin-top:0px; margin-bottom:2px;")	%>
<%= Url.Link<CenterController>(c => c.Skype(),
                            "Icq ������������").Class("block1 ga-click").Rel("icq") %>
<br />
<%= Images.Main("ico_skype.gif").Alt("skype")%>
<%= Url.Link<CenterController>(c => c.Skype(),
                        "Skype ������������").Class("block1 ga-click").Rel("skype") %>
<br />

<%= Images.Main("ico_forum.gif").Alt("forum")%>
<%= Url.Message().Forum("�����").Class("block1") %>
<br />