<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WithTitle.Master"
	Inherits="System.Web.Mvc.ViewPage<ComplexVM>" %>

<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="SimpleUtils" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Center.ViewModel" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SpecialistTest.Web.Core.Mvc.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script src="http://api-maps.yandex.ru/1.1/?key=AB9pfUwBAAAAxErqCQIAxh6jx7BlQiY55O2iMWZZ5DFNvFoAAAAAAAAAAAALERIDcydq4JtPztRBssLifX3wnw=="
		type="text/javascript"></script>
	<script src='<%= Urls.ContentRoot(Urls.File + "Complex/Map/" + Model.Complex.UrlName)%>.js?m=211210'
		type="text/javascript"></script>
	<script type="text/javascript">
		$(function () {
			init();
		});
	</script>
	<%= Images.Entity(Model.Complex).FloatLeft() %>
	<%= Model.Description.FirstPart %>
	<h3><%= Url.Link<LocationsController>(c => c.ClassRooms(Model.Complex.Complex_TC), "Схемы классов") %> </h3>
	<% Html.RenderAction<PageController>(c => c.Banner()); %>

	<%= Htmls2.Tabs(_.List("Адрес", "Фотографии и отзывы"), new object[] {
		Html.Partial(PartialViewNames.ComplexAddress),
		Images.Gallary(Model.Complex, true) +
		Model.Responses.Select(r => Html.Partial(PartialViewNames.ResponseBlock,r).ToString()).JoinWith(""),
		}, tabContentType: 2 ) %>
	<h3>
		Ближайшие группы</h3>
	<% Html.RenderPartial(PartialViewNames.NearestGroupList,
		   new NearestGroupsVM(Model.NearestGroupSet.All) {
			   HideComplex = true
		   }); %>
</asp:Content>
