<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.Root.Recommendations.SectionRecVM>" %>
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
	<title>Рекомендации по направлению</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1> Рекомендации по направлению</h1>

 <% using(Html.BeginForm<PublicController>(c => c.SectionRecommendations(null))){ %>
 <p>
       <label>Направление</label>
       <%= Html.DropDownList("id", Specialist.Web.Util.SelectListUtil.GetSelectItemList(Model.Sections, 
        x=> x.Name, x => x.Section_ID)) %>
 </p>
<%= HtmlControls.Submit("Показать") %>
 <% } %>
<% if(Model.ExcludeCoefs.Any()){ %>
    <h2>Добавить</h2>
    <%= H.table[Model.Coefs.Select(x =>H.Row(x.Coef.ToString("n2"), x.Course.Course_TC, x.Course.Name))] %>
    <h2>Убрать</h2>
    <%= H.table[Model.ExcludeCoefs.Select(x =>H.Row(x.Coef.ToString("n2"), x.Course.Course_TC, x.Course.Name))] %>

<% }else if(Request.IsPost()){ %>
    Ничего не найдено
 <% } %>


</asp:Content>

