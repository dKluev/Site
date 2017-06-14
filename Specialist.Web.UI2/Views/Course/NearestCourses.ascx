<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Section>>" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Interface" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Entities.Catalog.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.ViewModel" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Common.Mvc" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<div class="next_courses" id="next-courses-block">
	<h2 class="h2_block">
		Анонсы ближайших курсов</h2>

		<p id="section-selector" class="h2_select"> 	
	<%= Html.DropDownList("sections", 
		SelectListUtil.GetSelectItemList(Model.OrderByDescending(x => x.Section_ID == Sections.Network), x=> x.Name, x=> x.Section_ID)) %>
		<span id="indicator"><%= Images.Common("indicator.gif") %></span>		
		</p>
		

</div>




<script type="text/javascript">


	$(function () {
		var indicator = $("#indicator");
		function initCarousel() {
		    var context = $("#next-courses-block");
			var sectionId = $("#sections").val();
			if (!$("div.block_carousel", context).exists())
				$("#section-selector").after("<div class='block_carousel'></div>");
			$("div.block_carousel", context).fadeOut(function () {
				$("div.block_carousel", context).remove();
				indicator.fadeIn();
				$.get('/course/coursesforcarousel/' + sectionId,
		            function (html) {
		            	indicator.hide();
		            	$("#section-selector").after(html);
		            	$("div.block_carousel", context).fadeIn();
		            	var cDiv = $("div.carousel-control", context);
		            	if (cDiv.length)
		            		controls.initCarousel(cDiv);
		            });
			});
		}

		$("#sections").change(function () {
			initCarousel();
		});
		initCarousel();


	});

</script>

