<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Specialist.Entities.Context.ViewModel.ContractVM>" %>
<%@ Import Namespace="Specialist.Entities.Context.Const" %>
<%@ Import Namespace="Specialist.Web.Controllers" %>
<%@ Import Namespace="SimpleUtils.Collections.Extensions" %>
<%@ Import Namespace="SimpleUtils.Utils" %>
<%@ Import Namespace="Specialist.Entities.Catalog.Links.Interfaces" %>
<%@ Import Namespace="Specialist.Entities.Utils" %>
<% var ordersAndTrackCourses = Model.Cart.OrdersAndTrackFirstCourses; %>
<% if( !ordersAndTrackCourses.Any() 
	   || Model.Cart.Order.IsOrganization) return;%>

	<div id="reasons-for-learning" style="width:650px;display:none;">
	<h3>
	В целях повышения качества Вашего обучения и консультаций, 
	пожалуйста, напишите цель Вашего обучения. Заранее спасибо!
	</h3>

	<% var reasons = Model.StudyReasons; %>
	<% using(Html.BeginForm<OrderController>(c => c.UpdateReasonsForLearning(null), FormMethod.Post, new {id="study-reasons-form"})){ %>
	    <% ordersAndTrackCourses.ForEach((orderDetail, i) => {%>
		<div style="margin-bottom:5px"><strong>Цель обучения на курсе 
		<%= StringUtils.AngleBrackets(orderDetail.Course.GetName()) %>:</strong></div>
		
		
		<%= H.Hidden("[" + i + "]." + orderDetail.For(x => x.OrderDetailID), orderDetail.OrderDetailID) %>
	<div class="ui-widget">
		<%= H.select.Class("combobox")
			.Name("[" + i + "]." + orderDetail.For(x => x.ReasonForLearning))[_.List(H.option.Value(orderDetail.ReasonForLearning)[orderDetail.ReasonForLearning]).AddFluent(
					reasons.Select(x => H.option.Value(x)[x]))] %>
		<div class="explanation ">
			Напишите свой вариант или выберите из предложенных.
		</div>
	</div>
	<br />
		<% });%>             
		<%= H.Submit("Сохранить") %> <span id="study-reasons-error-message" style="display:none;color:red;">Выберите один из вариантов или напишите свой</span>
	<% }%>             
	
	</div>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.8/jquery-ui.min.js" type="text/javascript"></script>

	<script type="text/javascript">
		(function ($) {



			$.widget("ui.combobox", {
				_create: function () {
					var self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "";
					var input = this.input = $("<input>").addClass("uicombobox")
					.insertAfter(select)
					.val(value)
					.autocomplete({
						delay: 0,
						minLength: 0,
						source: function (request, response) {
							var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
							response(select.children("option").map(function () {
								var text = $(this).text();
								if (this.value && (!request.term || matcher.test(text)))
									return {
										label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
										value: text,
										option: this
									};
							}));
						},
						select: function (event, ui) {
							ui.item.option.selected = true;
							self._trigger("selected", event, {
								item: ui.item.option
							});
						},
						change: function (event, ui) {
							if (!ui.item) {
								var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
									valid = false;
								select.children("option").each(function () {
									if ($(this).text().match(matcher)) {
										this.selected = valid = true;
										return false;
									}
								});
							}
						}
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");

					input.data("autocomplete")._renderItem = function (ul, item) {
						return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
					};

					this.button = $("<button type='button'>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.insertAfter(input)
					.button({
						icons: {
							primary: "ui-icon-triangle-1-s"
						},
						text: false
					})
					.removeClass("ui-corner-all")
					.addClass("ui-corner-right ui-button-icon")
					.click(function () {
						// close if already visible
						if (input.autocomplete("widget").is(":visible")) {
							input.autocomplete("close");
							return;
						}

						// pass empty string as value to search for, displaying all results
						input.autocomplete("search", "");
						input.focus();
					});
				},

				destroy: function () {
					this.input.remove();
					this.button.remove();
					this.element.show();
					$.Widget.prototype.destroy.call(this);
				}
			});
		})(jQuery);

		$(function () {
			$(".combobox").combobox();

			$("#accept-link").click(function () {
				controls.showDialog("#reasons-for-learning");
				return false;
			});

			var $studyReasonForm = $('#study-reasons-form');

			$studyReasonForm.submit(function () {

				if ($("input.uicombobox").filter(function () { return $(this).val() == ""; }).length == 0) {
					$("#study-reasons-error-message").hide();
					return true;
				}
				$("#study-reasons-error-message").show();
				return false;

			});
		});
	</script>
