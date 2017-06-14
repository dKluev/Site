function initControls(id){
	var context = $("#" + id);
	if(controls.initUITabs){
		controls.initUITabs(context);
	}
	if(!$("form.ajax-form button",context).button) return;

	$("form.ajax-form button",context).button();
	$("button.ui-button",context).button();
	$("a.ui-button",context).button();
	$("form.ajax-form input",context).addClass("ui-widget-content ui-corner-all");
	$("form.ajax-form select",context).addClass("ui-widget-content ui-corner-all");
	$("form.ajax-form textarea",context).addClass("ui-widget-content ui-corner-all");
	$.datepicker.setDefaults($.datepicker.regional['ru']);
	$('input.date-picker').datepicker({ dateFormat: 'dd.mm.yy', showAnim: 'fadeIn' });

	$("a.open-in-uidialog").unbind("click").click(function(){
		var href = $(this).attr("href");
		controls.openUIDialog(href);
		return false;
	});

}
