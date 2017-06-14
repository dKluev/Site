<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Specialist.Web.Cms.Root.Recommendations.FullCourseCoef>>" %>
<%@ Import Namespace="SimpleUtils.Util"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Helper"%>
<%@ Import Namespace="Specialist.Web.Cms.Const"%>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Entities.Context"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="Specialist.Web.Cms.Core.ViewModel"%>
<%@ Import Namespace="DynamicForm.Utils" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc.Extensions" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Рекомендации по курсу</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1> Рекомендации по курсу</h1>

 <% using(Html.BeginForm<PublicController>(c => c.CourseRecommendations(null))){ %>
 <p>
       <label>Код курса</label>
       <%= Html.TextBox("id") %>
 </p>
<%= HtmlControls.Submit("Показать") %>
 <% } %>
<% if(Model.Any()){ %>
    <%= H.table[Model.Select(x =>H.Row(x.Coef.ToString("n2"), x.Course.Course_TC, x.Course.Name))] %>
<% }else if(Request.IsPost()){ %>
    Ничего не найдено
 <% } %>


</asp:Content>

