<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Catalog.ViewModel.SectionVM>" %>
<%@ Import Namespace="Specialist.Web.Util" %>
<%@ Import Namespace="Specialist.Web.Controllers.Center" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<% %>
<% var sectionWithWorks = Model.SubSections.Where(s => 
       Model.SectionIdContainUserWorks.Contains(s.Section_ID)); %>
<% if (!sectionWithWorks.Any())
   	return;%>
<div class="block_works_graduate">
	<h2 class="h2_block">
	<%= Html.ActionLink<ClientController>(
						sc => sc.PrivatePerson(SimplePages.Urls.Works,null), 
							"Работы выпускников") %>
		</h2>
	<p>
	<%= Html.DropDownList("sections", 
		SelectListUtil.GetSelectItemList(sectionWithWorks, 
			x=> x.Name, x=> x.Section_ID)) %>
	</p>
		<div id="indicator"><%= Images.Common("indicator.gif") %></div>
	<div id="user-works">
	
	</div>


</div>



<script type="text/javascript">


	$(function () {
		var indicator = $("#indicator");
		var $userWorks  = $('#user-works');
		function loadWorks() {
			var sectionId = $("#sections").val();

			$userWorks.fadeOut(function () {
				indicator.fadeIn();
				$userWorks.load('/Section/UserWorks/' + sectionId,
				function () {
					indicator.fadeOut();
					$userWorks.fadeIn();
				});
			})
		}

		$("#sections").change(function () {
			loadWorks();
		});
		loadWorks();


	});

</script>

