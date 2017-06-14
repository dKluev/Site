<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<Tuple<string, List<Specialist.Entities.Catalog.Links.CourseLink>>>" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Web.Helpers.Shop" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<asp:Content ContentPlaceHolderID="Center" runat="server">
	<div id="content" class="longlist">
		<h2>
			Поиск курсов
		</h2>

<% using(Html.BeginForm<CourseController>(c => c.Search(null),
	   FormMethod.Post,new {id="find"})){ %>
		<p>
			Введите название курса:</p>
		<p>
			<input type="text" name="text" required="required" value="<%= Model.Item1 %>" placeholder="Название курса" id="findbutton" />
			<input type="submit" id="findsubmit" value="Найти" />
		</p>
	<% } %>
	
	<% if(Model.Item2.Any()) {%>
	<p class="findres">Результаты:</p>
	<%= Html.Site().MobileCourses(Model.Item2) %>

	<% }else if(!Model.Item1.IsEmpty()){ %>
	Ничего не найдено
	<% } %>
	</div>
</asp:Content>
