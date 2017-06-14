<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Extension"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Section>>" %>
<%@ Import Namespace="Specialist.Web.Util" %>

<div class="block_opinions">

	<h2 class="h2_block">
	<%= SimpleLinks.Responses("Отзывы выпускников")%>
		</h2>
	<p>

	<%= H.select.Id("sections")[Model.Select(s => H.optgroup.Label(s.Name)[
		s.SubSections.Where(y => y.IsActive)
		.Select(x => H.option[x.Name].Value(x.Section_ID))])] %>


	</p>
		<div id="indicator"><%= Images.Common("indicator.gif") %></div>

	<div id="user-responses">
	
	</div>


</div>



<script type="text/javascript">


	$(function () {
		var indicator = $("#indicator");
		var $userResponses = $('#user-responses');
		function loadResponses() {
			var sectionId = $("#sections").val();
			if (sectionId == null)
				sectionId = <%= Model.FirstOrDefault(x => x.SubSections.Any()).GetOrDefault(ss => ss.SubSections.First(x => x.IsActive).Section_ID) %>;


		    $userResponses.fadeOut(function() {
		        indicator.fadeIn();
		        $userResponses.load('/Section/Responses/' + sectionId,
		            function() {
		                indicator.hide();
		                $userResponses.fadeIn();
		            });
		    });
		}

		$("#sections").change(function () {
			loadResponses();
		});
		loadResponses();


	});

</script>