
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Profile.PublicProfileVM>" %>
<%@ Import Namespace="System.Xml.XPath" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>

<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>

<div id="profile-href">

		<div>
		    
    		<% if (Model.IsOwner) {%>
            <%= H.Ul(_.List(
            Url.Profile().Public(Model.User.UserID, "��������� �������"),
    		Url.Profile().EditProfile("������������� �������") ,
    		Url.Profile().ChangePassword("������� ������ ��� e-mail (�����)") ,
    		Url.Profile().ExamQuestionnaire(null,"������ ��� ��������") ,
    		Url.Profile().WorkPlace("��� ������") ,
    		Url.Profile().Subscribes("��������") ).Select(x => H.p[x])).Style("float: right; list-style-type: none; margin-top:7px;") %>

    		<%} %>
			<% if (Model.User.UserAddresses.Any()) { %>
			<% HUtil.NotNull(Model.User.UserAddresses.First().Country
						.GetOrDefault(x => x.CountryName), x => { %>
			<p>
				<strong>������:</strong>
				<%= x %></p>
			<% }); %>
			<% HUtil.NotNull(Model.User.UserAddresses.First().City, x => { %>
			<p>
				<strong>�����:</strong>
				<%: x %>
			</p>
			<% }); %>
			<% } %>
		</div>
			</div>
<div class="clear"></div>

		<% if (!Model.User.HideCourses) { %>
			<% if (Model.User.Student.GetOrDefault(x => x.EndedGroups.Any() )) { %>
		  <h2>����������� �����</h2>

        <table class="defaultTable">
            <tr>
                <th>
                    ����
                </th>
            </tr>

        <% Model.User.Student.EndedGroups.ForEach((group, i) => { %>
            <tr>
			<td class="td_course"> <%= i+1 %>. 
                <%=Html.CourseLinkByGroup(group)%>
			</td>
               
            </tr>
		<% }); %>
        </table>
		<% } %>


		<% } %>
