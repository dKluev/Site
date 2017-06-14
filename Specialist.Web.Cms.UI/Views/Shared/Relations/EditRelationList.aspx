<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EditVM>" %>
<%@ Import Namespace="SimpleUtils.Linq.Data.LInq" %>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers" %>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<title>Редактирование связей</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% var siteObject = Model.Entity as SiteObject; %>
Редактирование тегов: <h2><%= siteObject.Name %></h2> 
<div class="hideInFrame" >
    <a href="javascript: history.go(-1)">Вернуться</a>
	<% if (siteObject.Type == LinqToSqlUtils.GetTableName(typeof(Course))) { %>
	<%= Url.Link<CourseEntityController>(c =>
	c.TagsReport(siteObject.ID.ToString()),"Отчет о привязках") %>
	<% } %>
	<br /><br /><br />
</div>

<% Html.RenderPartial(PartialViewNames.EditRelationListControl, 
   new EditRelationListVM
   {
       ForLinkCreator = false,
       SiteObject = siteObject,
   }); %>


  

</asp:Content>


