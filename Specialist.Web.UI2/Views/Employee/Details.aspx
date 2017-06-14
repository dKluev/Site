<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" 
    Inherits="System.Web.Mvc.ViewPage<EmployeeVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Const" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% var employee = Model.Employee; %>
<%= Htmls.BorderBegin() %>
		<div class="tabs-control">
			<ul class="bookmarks">
                <li class="active"><a href="#" rel="tab-main">
                <% if(employee.IsTrainer){ %>
                О преподавателе
                <% }else{ %>
                О сотруднике 
                <% } %>
                </a></li>

                <% if(employee.Responses.Any()){ %>
                <li><a href="#" rel="tab-responses">Отзывы</a></li>
                <% } %>
                 <% if(Model.OrgResponses.Any()){ %>
                <li><a href="#" rel="tab-org-responses">Корпоративные отзывы</a></li>
                <% } %>
                <li><a href="#" rel="tab-contacts">Контакты</a></li>
            </ul>
                <div class="tab-main tab_content2">
            		<%= Images.Employee(employee.Employee_TC).FloatLeft() %>
                    <%= employee.SiteDescription %>
                    <% if(Employees.AllManagers.Contains(employee.Employee_TC)){ %>
                    <h3> <%= Url.SendManager(employee.Employee_TC) %> </h3>
                    <% } %>
                    <%= Images.Gallary(employee) %>
                    <% if(employee.Certifications.Any()){ %>
                    <br /> 
                    <strong>Статусы:</strong>
                    <% foreach(var certification in employee.Certifications){ %>
                        <%= Html.CertificationLink(certification) %> <br />
                    <% } %>
                    <% } %>
                    <br />
                    <div class="paddingleft20">
                    <% if(Model.Groups.Any()){ %>
                    <h4>Ближайшие группы</h4> 
                    <% Html.RenderPartial(PartialViewNames.NearestGroupList, 
                           new NearestGroupsVM(Model.Groups){HideTrainer = true}); %>
                    <% }else{ %>
                        <p>
                     </p>
                     <% } %>
                     </div>
                </div>
  
                <div class="tab-responses tab_content2">
                <% Html.RenderPartial(PartialViewNames.ResponseList, employee.Responses.OrderByDescending(r => r.UpdateDate));%>  
                
                </div>
                <div class="tab-org-responses tab_content2">
                <% Html.RenderPartial(PartialViewNames.OrgResponseList, Model.OrgResponses);%>  
                
                </div>
                <div class="tab-contacts tab_content2">
                <% Html.RenderPartial(PartialViewNames.Contacts, employee);%>  
                </div>
        </div>

<%= Htmls.BorderEnd %>
</asp:Content>



<%--<asp:Content ID="Content3" ContentPlaceHolderID="RightColumn" runat="server">--%>
<%----%>
<%--<% foreach (var response in Model.HighRatingResponses) { %>--%>
<%--<div class="contRightBox">--%>
<%--	<div class="rightBox1Top">--%>
<%--        <div class="up3">--%>
<%--            <a name="recall"></a>--%>
<%--            <% if(response.Course != null) { %>--%>
<%--            <h4> <%= Html.CourseLinkShortName(response.Course) %> </h4>--%>
<%--            <% } %>--%>
<%--         </div>--%>
<%--	</div>--%>
<%--    <div class="rightBox1Center">--%>
<%--        <div class="newsBlock">--%>
<%--            <p class="tiserText3">--%>
<%--            <%= response.Description %>--%>
<%--            </p>--%>
<%--            <p class="rightText"><strong><%= response.Authors %></strong></p>--%>
<%--        </div>--%>
<%--		<br/>--%>
<%--    </div>--%>
<%--	<div  class="rightBox1Bot"></div>--%>
<%--</div>--%>
<%--    --%>
<%--<div class="claer"></div>--%>
<%--<br/>--%>
<%----%>
<%--<% } %>--%>
<%----%>
<%--</asp:Content>--%>

