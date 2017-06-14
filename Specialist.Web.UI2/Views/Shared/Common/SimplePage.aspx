<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Common.ViewModel.SimplePageVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc"%>
<%@ Import Namespace="SimpleUtils"%>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface"%>
<%@ Import Namespace="Specialist.Web.Controllers"%>

<%@ Import Namespace="Specialist.Web.Extension" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Context" %>
<%@ Import Namespace="Specialist.Entities.Common.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% Func<SimplePageVM.Control, string> render = 
       x => x.Content ?? Html.Partial(x.PartialViewName, x.Model).ToString();  %>

<% var altTabs = Model.Entity.UseAltTabs; %>
<% if(Model.ShowTabs){ %>
<% var rootEntity = Model.Entity.MainParent; %>
<%= Images.Entity(rootEntity).FloatLeft() %>
    <%= rootEntity.Description %>
    <% if (altTabs) { %>

		<ul class="bookmarks" style="border-radius: 5px 5px 0 0; width: 101%; position: relative; top: 9px; margin-bottom: 20px; background: linear-gradient(to top, #E4EEF8,#E4EEF8 );">
        <% foreach (var simplePage in Model.Tabs) { %>
            <% if(simplePage.SimplePageID == Model.Entity.SimplePageID) { %>
            	<li class="active">
            <% }else{ %>
                <li style="color:#000066;font-weight:bold;" >
            <% } %>
                <%= Html.SimplePageLink(simplePage) %>
                </li>
        <% } %>
        </ul>
    <% }else{ %>

		<%= Htmls2.BorderBegin() %>

		<ul class="bookmarks">
        <% foreach (var simplePage in Model.Tabs) { %>
            <% if(simplePage.SimplePageID == Model.Entity.SimplePageID) { %>
            	<li class="active">
            <% }else{ %>
                <li>
            <% } %>
                <%= Html.SimplePageLink(simplePage) %>
                </li>
        <% } %>
        </ul>
<div class="tab_content2">
    <% } %>
<%} %>


<%= Images.Entity(Model.Entity).FloatLeft() %>
    <%= Model.Description.FirstPart %>
    <% if(Model.Description.HasTag){ %>
        <% Html.RenderPartial(PartialViewNames.SubSections, Model.EntityWithTags); %>
    <% } %>
    <% Html.RenderAction<PageController>(c => c.Banner()); %>	
    <% foreach (var control in Model.Controls.Where(c => !c.OnBottom)) { %>
        <%= control.Title.IsEmpty() ? null : H.h2[control.Title] %>
        <%= render(control) %>
    <% } %>
    <% if(Model.Description.HasTag) { %>
        <%= Model.Description.SecondPart%>
    <% } %>

	<% foreach (var control in Model.Controls.Where(c => c.OnBottom)) { %>
        <%= control.Title.IsEmpty() ? null : H.h2[control.Title] %>
        <%= render(control) %>
    <% } %>

    <% if(Model.ShowTabs && !altTabs){ %>
        </div>
		<%= Htmls2.BorderEnd() %>
    <% } %>
   

<% Html.RenderAction<GroupController>(c => c.ForCourseTCList(Model.Entity.CourseTCList,false,0)); %>

</asp:Content>
<asp:Content ContentPlaceHolderID="RightColumn" runat="server">

<%= Htmls2.ChamBegin(true) %>

<% if (Model.RightColumnControls.Any()) {%>
    <% foreach(var control in Model.RightColumnControls){ %>
        <% Html.RenderPartial(control.PartialViewName, control.Model); %>
    <% } %>
<% Html.RenderAction<PageController>(c => c.NewsFor(Model.Entity)); %>
<% } else { %>
<script type="text/javascript">
    $(function() {
        lazyContent("#advices",
            '<%=Url.Action<CenterController>(c => c.AdviceBlock()) %>');
    });
</script>
<% Html.RenderAction<PageController>(c => c.NewsFor(Model.Entity)); %>

<div id="advices"> </div>
<% } %>
<%= Htmls2.BlockEnd() %>
<% Html.RenderAction<PageController>(c => c.VideoFor(Model.Entity)); %>

</asp:Content>
