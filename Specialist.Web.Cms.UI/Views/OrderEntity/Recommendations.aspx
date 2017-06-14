<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Specialist.Web.Cms.ViewModel.RecommendationsVM>" %>
<%@ Import Namespace="Specialist.Web.Common.Extension"%>
<%@ Import Namespace="Specialist.Web.Cms.Controllers"%>
<%@ Import Namespace="Specialist.Web.Common.Html"%>
<%@ Import Namespace="DynamicForm.Utils" %>
<%@ Import Namespace="Specialist.Web.Helpers" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Подбор курсов</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Подбор курсов</h2>

 <% using(Html.BeginForm<OrderEntityController>(c => c.Recommendations(null))){ %>
 <%= Html.ValidationSummary() %>
 <p>
       <label>Email слушателя</label>
       <%= Html.TextBoxFor(x => x.Email, new{@class="text"}) %>
 </p>
  <p>
       <label>Продукт</label>
       <%= Html.DropDownListFor(x => x.ProductId, 
           SelectListUtil.GetSelectItemList(Model.Products,
                  x => x.EntityName, x => x.Id,"Нет")) %>
 </p>
 
 <p>
       <label>Профессия</label>
       <%= Html.DropDownListFor(x => x.ProfessionId, 
           SelectListUtil.GetSelectItemList(Model.Professions,
                  x => x.EntityName, x => x.Id,"Нет")) %>
 </p>
<%= HtmlControls.Submit("Показать") %>
 <% } %>
 <% if(Model.Courses.Any()){ %>
 <table class="simple-table" id="recommendation-table">
 	 <%= H.Head("Код", "Курс", "Сдал тест", "Прошел обучение") %>
	 <% foreach (var course in Model.Courses) { %>
	 <%= H.Row(course.Course_TC, 
HtmlsExtension.AbsoluteHref(Html.CourseLinkAnchor(course.UrlName, course.GetName())),
		H.InputCheckbox(null, null).SetChecked(Model.TestCourseTCs.Contains(course.Course_TC)),
		H.InputCheckbox(null, null).SetChecked(Model.CompleteCourseTCs
			.Contains(course.Course_TC)) 	
		) %>

	 <% } %>
 	
 </table>
 <button id="result-button" style="margin: 10px 0;">Окончательный список</button>
 <div id="result-container"></div>
<%-- <button id="tracks-button" style="margin: 10px 0;">Треки</button>--%>
 <div id="tracks-container"></div>
 <% } %>
 
 <script type="text/javascript">
 	$(function () {
 		function getSelected() {
 			var result = [];
 			var first = true;
 			$("#recommendation-table tr").each(function () {
 				if (first) {
 					first = false;
 					return;
 				}

 				var row = $(this);
 				if ($(":checked", row).length == 0) {
 					result.push({ CourseTC: $("td:eq(0)", row).html(),
 						Name: $("td:eq(1)", row).html()
 					});
 				}
 			});
 			    return result;
 		}
 		$("#ProductId").change(function () {
 			$("#ProfessionId").val("");
 		});

 		$("#ProfessionId").change(function () {
 			$("#ProductId").val("");
 		});
 		$("#result-button").click(function () {
 			var selected = getSelected();
 			var result = $.map(selected, function (x) {
 				return x.CourseTC + " - " + x.Name;
 			});
 			$("#result-container").html(result.join("<br/>"));
 		});
 		$("#tracks-button").click(function () {
 			var selected = getSelected();
 			var courseTCs = $.map(selected, function (x) {
 				return x.CourseTC;
 			});
 			$("#tracks-container").html(controls.indicator);
 			$.post('<%= Url.Action<OrderEntityController>(c => c.GetTracks(null)) %>', { courseTCs: courseTCs }, function (data) {
	 			$("#tracks-container").html(data.join("<br/>"));
 			});
 		});
 	});
 </script>

</asp:Content>

