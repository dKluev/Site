<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CartVM>" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="Specialist.Web.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers.Shop" %>
<%@ Import Namespace="Specialist.Web.Common.Extension" %>
<%@ Import Namespace="SimpleUtils.Common.Extensions" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>

<script src="/Scripts/Views/Group/jquery.autocomplete.js" type="text/javascript"></script>
<script src="/Scripts/Views/Group/autocomplete.js" type="text/javascript"></script>

		<div style='float:left'>
				<strong> Если вам известен промокод: </strong> 
	   
	   <%= H.Form(Url.Action<EditCartController>(c => c.UpdatePromocode(null)))
	   	.Id("promocode-form")[H.InputText("promocode", "").Style("width:250px").Id("promocode-text"),
	   			H.Submit("Применить"), H.span.Class("loader")[Images.Indicator().Style("margin:0;")].Hide(), H.span.Class("form-result")].Style("display:inline;") %>
	   </div>

 <% if (Model.User != null && Model.User.IsStudent) { %> 
		<div style='float: left; margin-top: 10px;'>
				<strong>Кто рекомендовал курс: </strong> 
            
<script type="text/javascript">
    setAutocomplete('#favtrainer-input',
        '<%=Url.Action<EmployeeController>(c => c.TrainersJson(null)) %>', 2);
</script>

	   <%= H.Form(Url.Action<EditCartController>(c => c.UpdateFavoriteTrainer(null)))
	           .Id("favtrainer-form")[
	               H.InputText("fullName", Model.FavTrainer).Style("width:290px").Id("favtrainer-input"),
                   H.Submit("Сохранить "),
	               H.span.Class("loader")[Images.Indicator().Style("margin:0;")].Hide(),
                    H.span.Class("form-result")
                   ].Style("display:inline;") %>
	   </div>

 <% } %> 
<div class="clear"></div>

<script type="text/javascript">
	$(function () {
		$("#promocode-text").attr("autocomplete", "off");
		$('#promocode-form').unbind('submit').submit(function () {
		    var form = $(this);

	        var loader = $('.loader', form);
	        loader.show();
	        var result = $('.form-result', form);
	        result.html("");
			$.post(
				form.attr("action"),
				form.serializeJson(), function (data) {
					loader.hide();
					if (data == "ok") {
						window.location.reload();
					} else {
						result.html("Данный промокод не существует");
					}
				});
			return false;
		});

		$('#favtrainer-form').unbind('submit').submit(function () {
		    var form = $(this);

	        var loader = $('.loader', form);
	        loader.show();
	        var result = $('.form-result', form);
	        result.html("");
			$.post(
				form.attr("action"),
				form.serializeJson(), function (data) {
					loader.hide();
					if (data == "ok") {
						result.html("Данные сохранены");
					} else {
						result.html("Преподаватель не найдет, введите полное ФИО");
					}
				});
			return false;
		});

//	    $("#favtrainer-input").change(function() {
//	        var form = $('#favtrainer-form');
//	        var loader = $('.loader', form);
//	        loader.show();
//			$.post(
//				form.attr("action"),
//				form.serializeJson(), function (data) {
//					loader.hide();
//				});
//	    });

//		$('#favtrainer-form').unbind('submit').submit(function () {
//			var form = $(this);
//			$('div.loader', form).html(controls.indicator);
//			$.post(
//				form.attr("action"),
//				form.serializeJson(), function (data) {
//					$('#form-result').html("");
//				});
//			return false;
//		});


	});
</script>