<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"  
    Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Center.ViewModel.AboutTrainerVM>" %>
<%@ Import Namespace="Specialist.Entities.Filter" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Root.Center.Views" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<%= Htmls.BorderBegin() %>
		<ul class="bookmarks">
        <% foreach (var simplePage in Model.Pages) { %>
            	<li 
            <% if(simplePage.UrlName == Model.UrlName) { %>
				class="active"
            <% } %>>
                <%= Html.ActionLink<EmployeeController>(c => c.AboutTrainer(Model.Employee.Employee.Employee_TC.ToLower(),
                    simplePage.UrlName, null), simplePage.Name) %>
            </li>
        <% } %>
        </ul>
        <div class="tab_content2">
        <% if (Model.IsAboutTrainer) { %>
            		<%= Images.Employee(Model.Employee.Employee.Employee_TC)
              .Alt(Model.Employee.Employee.FullName).FloatLeft() %>
                    <%= Model.Description.FirstPart %>
					<div class="clear"></div>
                  
					<% var certifications = Model.Certifications; %>
                    <% if(certifications.Any()){ %>
                    <br /> 
                    <strong>Статусы:</strong>
					<%= Htmls2.MarkArrow(certifications
						.Select(c => Urls.IsCertEmployeeExists(
                            c.UrlName, Model.Employee.Employee.Employee_TC) 
                            ? H.Anchor(Urls.CertEmployee(Urls.FirstContentRoot, c.UrlName,
                                Model.Employee.Employee.Employee_TC), c.Name).Class("fancy-box").ToString()
                            : Html.CertificationLink(c))) %>
                    <% } %>

					<div class="clear"></div>
                    <%= Images.Gallary(Model.Employee.Employee, true) %>
					<div class="clear"></div>
                <% if(Request.IsAuthenticated){ %>
					<br />
					<%= Url.Link<EmployeeController>(c => 
	c.AddResponse(Model.Employee.Employee.Employee_TC), Images.Button("response")
	.ToString()) %>
                    <% } %>
	<br/>
					<%= Html.AddThis() %>
                    <br />
                    <div class="paddingleft20">
                        <% var groups = Model.Employee.Groups; %>
                    <% if(groups.Any()){ %>
                    <h3>Ближайшие группы</h3> 
                    <% if(groups.Any(x => x.IsOpenLearning) &&
                           groups.Any(x => !x.IsOpenLearning)){ %>
<% var tabs = _.List("Очное обучение", "Открытое обучение"); %>
<% var contents = _.List<object>(
       Html.Partial(Views.Shared.Education.NearestGroupList,
           new NearestGroupsVM(groups.Where(x => !x.IsOpenLearning)
               .ToList()) {HideTrainer = true}),
       Html.Partial(Views.Shared.Education.NearestGroupList,
           new NearestGroupsVM(groups.Where(x => x.IsOpenLearning)
               .ToList()) {HideTrainer = true})); %>
<% if (groups.First().IsOpenLearning) {
       tabs.Reverse();
       contents.Reverse();
   } %>
    <%= Htmls2.Tabs(tabs, contents.ToArray()) %>
                    <% }else{ %>
                    <% Html.RenderPartial(PartialViewNames.NearestGroupList, 
                           new NearestGroupsVM(Model.Employee.Groups){HideTrainer = true}); %>
                    <% } %>
                        <div class="attention2">
                          <h3><%= Html.ActionLinkEx<GroupController>(
                 gc => gc.List(new GroupFilter { EmployeeTC = 
                     Model.Employee.Employee.Employee_TC }, 1), "Все группы преподавателя") %></h3>  
                        </div>
                          
                    <% }else{ %>
                        <p>
                     </p>
                     <% } %>
                     </div>
        <%} %>
                <p>
        <% if (Model.IsTrainerResponses) { %>
                <% Html.RenderPartial(PartialViewNames.ResponseList, 
					   Model.Responses);%>          
                <%= Html.GetNumericPagerPretty(Model.Responses) %>
        <%} %>
        <% if (Model.IsTrainerOrgResponses) { %>
                <% Html.RenderPartial(PartialViewNames.OrgResponseList, Model.OrgResponses);%>  
                <%= Html.GetNumericPagerPretty(Model.OrgResponses) %>
        <%} %>
        <% if (Model.IsAdvices) { %>
                <% Html.RenderPartial(PartialViewNames.AdviceList, Model.Advices);%>  
                <%= Html.GetNumericPagerPretty(Model.Advices) %>
        <%} %>
        <% if (Model.IsTests) { %>
			<%= H.Ul(Model.Tests.Select(x => Url.TestLink(x))) %>
                <%= Html.GetNumericPagerPretty(Model.Tests) %>
        <%} %>
        <% if (Model.IsPortfolio) { %>
                <%= Images.Gallary(Model.Employee.Employee, folder: "Portfolio") %>
        <%} %>
        <% if (Model.IsWorks) { %>
				<%= Model.Description.SecondPart %>
                <%= Html.Site().UserWorks(Model.UserWorks) %>
                <%= Html.GetNumericPagerPretty(Model.UserWorks) %>
        <%} %>
        <% if (Model.IsContacts) { %>
                <% Html.RenderPartial(PartialViewNames.Contacts, Model.Employee.Employee);%>  
        <%} %>
        <% if (Model.IsVideos) { %>
            <%= VideoCategoryView.Videos(Url, Model.Videos, true) %>
        <%} %>
            </p>
        </div>

<%= Htmls.BorderEnd %>
</asp:Content>

<asp:Content ContentPlaceHolderID="RightColumn" runat="server">
   
	<%= Htmls2.ChamBegin(true) %>
        <% Html.RenderPartial(PartialViewNames.MainNews, true);%>
	<%= Htmls2.BlockEnd() %>

</asp:Content>

