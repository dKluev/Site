<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"  
    Inherits="System.Web.Mvc.ViewPage<SearchResumeVM>" %>
<%@ Import Namespace="Specialist.Web.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Profile.Const"%>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Web.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel"%>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Passport"%>
<%@ Import Namespace="Specialist.Web.Controllers.Center"%>
<%--

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<p><h2>Поиск резюме</h2></p>
   <% var user = (User)HttpContext.Current.Session["CurrentUserSessionKey"]; %>
    <%   if ((HttpContext.Current.User.Identity.IsAuthenticated && user != null && user.IsCompany) || (HttpContext.Current.User.Identity.IsAuthenticated && user != null && user.GroupID == 2))  %> 
    <%   { %> 
	<%= Htmls.BorderBegin() %>
		<ul class="bookmarks">
        <% foreach (var simplePage in Model.Pages) { %>
            <% if(simplePage.UrlName == Model.UrlName) { %>
            	<li class="active">
            <% }else{ %>
            <li>
            <% } %>
            $1$    <%= Html.ActionLink<ClientController>(c => c.SearchResume(simplePage.UrlName, null), simplePage.Name)%>#1#
            </li>
        <% } %>
        </ul>
        <div class="tab_content">
        <% if (Model.IsSearchParamsResume) { %>
       <p>
        <% Html.RenderAction<ClientController>(c => c.SearchParamsResume());  %>
       
        </p>
        <% } %>
         <% if (Model.IsSeachResultResume) { %>
        <p>
        <% Html.RenderPartial(PartialViewNames.SearchResultResume, Model.Resume);%>
        <%= Html.GetNumericPagerPretty(Model.Resume)%> 
        </p>
        <% } %>
        </div>
		<%= Htmls.BorderEnd %>
     <%}
      else
      {%>
   <p>
     Сервис "Поиск резюме" доступен только зарегистрированным пользователям-организациям.<br />
     <%= Html.ActionLink<AccountController>(c => c.LogOn(""), "Аутентификация на сайте") %>
     </p>
    <% } %> 

</asp:Content>--%>
