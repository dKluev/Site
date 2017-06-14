<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.ViewModel.TestCertGroupVM>" %>
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


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Сертификаты тестирования группы</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Сертификаты тестирования группы</h2>

 <% using(Html.BeginForm<OrderEntityController>(c => c.TestCertGroup(null))){ %>
 <p>
       <label>Номер группы</label>
       <%= Html.TextBoxFor(x => x.GroupId) %>
 </p>
  <p>
       <label>Тест</label>
       <%= Html.DropDownListFor(x => x.TestId, 
           SelectListUtil.GetSelectItemList(Model.Tests, x => x.Name, x => x.Id)) %>
 </p>
<%= HtmlControls.Submit("Найти") %>
 <% } %>

<%= H.Ul(Model.UserTests.Select(x => Html.ActionLink<OrderEntityController>(c => c.UserTestCert(x.Id, null), x.User.FullName).ToString())) %>

<% if(Model.GroupId > 0 && !Model.UserTests.Any()){ %>
<p>
Ничего нет
</p>
 <% } %>
</asp:Content>

