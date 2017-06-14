<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Entities.Catalog.Interface.IEntityCommonInfo>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="SimpleUtils.Utils" %>

<asp:Content ContentPlaceHolderID="Center" runat="server">
<%= MHtmls.Back( Url.CoursesLink()) %>

    <% var title = Model is Profession
           ? "{0}: подготовка по специальности".FormatWith(Model.Name)
           : StringUtils.CoursesPrefix(Model.Name); %>
    

<div id="content" class="longlist">
<%= MHtmls.Title(title) %>	
	<% Html.RenderAction<CourseController>(c => c.CourseListFor(Model)); %>
</div>
</asp:Content>

