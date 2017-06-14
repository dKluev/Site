controls.initAutocomplete = function (idControlSelector, nameControlSelector, dataUrl) {
	$('input[name="' + nameControlSelector + '"]').autocomplete({
		source: dataUrl,
		minLength: 0,
		select: function( event, ui ) {
			$('input[name="' + idControlSelector + '"]').val(ui.item.id);
		}
	}).removeAttr('name');
};

controls.initSelect2 = function (idControlSelector, currentName, dataUrl) {
	utils.loadScript("/scripts/select2.min.js?v=2",function(){
		var item = $('input.select2[name="' + idControlSelector + '"]');
		var currentId = item.attr('value');
		item.select2({
			placeholder: " ",
			allowClear: true,
			width: "element",
			initSelection: function(el, cb){
				cb({text:currentName, id: currentId});
			},
			ajax: {
				url: dataUrl,
				quietMillis: 100,
				data: function (term, page) { 
					return {
						term: term
					};
				},
				results: function (data, page) {
					return data;
				}
			}
		});
	});
};

controls.showLoad= function () {
	var div = utils.selectOrCreateById("indicator-dialog").html(controls.bigindicator);
	div.dialog({
		modal: true,
		width: 95,
		minWidth:95,
		minHeight:95,
		height: 95,
		resizable: false,
		title: "Загрузка",
		hide: 'fade',
		show: 'fade',
		closeOnEscape:false,
		open: function(event, ui) { $(".ui-dialog-titlebar-close", $(this).parent()).hide(); }

	});
};
controls.hideLoad = function () {
	$("#indicator-dialog").dialog("close");
};

controls.getParentUIDialog = function($element){
	return $element.parents("[id^='ui-dialog']:first");
};

controls.openUIDialog = function(url, dialog){
	var id = "ui-dialog" + url.replace(/[\/?&=]/g,'-');
	var isNewDialog = !dialog;
	if(isNewDialog){
		dialog = utils.selectOrCreateById(id).css({display:'none'});
		maxWidth = screen.width - 200;
		dialog.empty().css({maxWidth:maxWidth});
	}
	controls.showLoad();
	dialog.load(utils.addRandomPostfix(url), function () {
		controls.hideLoad();
		var h1 = $(this).find("h1");
		var title = h1.html();
		h1.remove();
		if(!isNewDialog){
			$("form.ajax-form", dialog).prepend(h.tag('div', 
																								{'class':'form-info ui-state-highlight ui-corner-all', 'style' : 'margin:5px 0',
																									text:'Операция выполнена успешно'}));
		}
		function resizeInDialog(){
			$("table.resize-in-dialog",dialog).each(function(){
				$(this).setGridWidth(dialog.width());
			});
		}
		var newDialog = $(this).dialog({
			modal: true,
			width: 'auto',
			height: 'auto',
			hide: 'fade',
			show: 'fade',
			closeOnEscape:false,
			close: function(event, ui) { 
				var refreshCallback = dialog.data('refreshCallback');
				var needRefresh = dialog.data('needRefresh');
				if(needRefresh && refreshCallback) refreshCallback();
				dialog.dialog("destroy").remove();
			},
			open: function(event, ui) { 
				resizeInDialog();
			},
			resizeStop: function(event, ui) { resizeInDialog(); }
		});
		var newTitle = h.tag('div').append(h.tag('a', {href:url}).append(h.tag('span',{style:'float:left;','class':'ui-icon ui-icon-extlink'}))).html() + title;
		newDialog.parent().find(".ui-dialog-title").html(newTitle);

	});
	return dialog;
};
utils.allLoadScripts = [];
utils.loadScript = function(src, callback){
	if(!$.isArray(src)){
		src = [src];
	}

	var loaded = 0;
	function onLoaded(url,alreadyLoaded) {
		if(alreadyLoaded !== true)
			utils.allLoadScripts.push(toLoad);
		loaded++;
		if (loaded == src.length) {
			callback();
		}
	}

	var len = src.length; 
	var i = 0;
	for (i = 0; i < len; i++) {
		var toLoad = src[i];
		if($.inArray(toLoad,utils.allLoadScripts) >= 0){
			onLoaded(toLoad,true);
		}else{
			(function(url){
				$.getScript(toLoad, function(){ onLoaded(url);});
			})(toLoad);
		}
	}    
};

utils.postShowLoad = function (url, data, callback) {
	controls.showLoad();
	if (typeof (data) == "function") {
		callback = data;
		data = null;
	}
	$.post(url, data, function (data) {
		controls.hideLoad();
		if (callback)
			callback(data);
	});
};


controls.initUITabs = function($context){
	$( "div.ui-tab-control", $context).tabs();
};



$(function(){
	function initAjaxForms(){
		$(document).on("click",'form.ajax-form button', function () {
			var form = $(this).parent("form.ajax-form");
			$("div.form-info",form).remove();
			$("div.form-error", form).remove();
			$(".input-validation-error").removeClass("input-validation-error");
			if(window.tinyMCE){
				tinyMCE.triggerSave(true,true);
			}
			utils.postShowLoad(form.attr("action"), form.serialize(),
												 function (data) {
													 if (data.Errors) {
														 form.prepend(h.tag("div",{ "class": "form-error ui-state-error ui-corner-all" }).html(data.Errors ));
														 $.each(data.Names,function (index,name) {
															 $("input[name='" + name + "']",form).addClass("input-validation-error");
														 });
													 }else if(data.Url){
														 var dialog = controls.getParentUIDialog(form);
														 dialog.data('needRefresh', true);
														 if(dialog.length)
															 controls.openUIDialog(data.Url,dialog);
														 else
															 window.location = data.Url;
													 }else if(data.Redirect){
															 window.location = data.Redirect;
													 }else if(data == "ok" || data.message){
														 var message = data.message || 'Операция выполнена успешно';
														 controls.getParentUIDialog(form).data('needRefresh', true);
														 form.prepend(h.tag('div', 
																								{'class':'form-info ui-state-highlight ui-corner-all', 'style' : 'margin:5px 0',

																									text:message}));
													 }else if(form.hasClass("content-load-form")){
															 $("div." + form.attr("id")).html(data);
													 }
												 });
												 return false;
		});
	}
	initAjaxForms();


});
