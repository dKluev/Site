var controls = {
	indicator: '<img src="//cdn.specialist.ru/Content/Image/Common/indicator.gif"/>'
};

$.fn.serializeJson = function()
{
	var o = {};
	var a = this.serializeArray();
	$.each(a, function() {
		if (o[this.name]) {
			if (!o[this.name].push) {
				o[this.name] = [o[this.name]];
			}
			o[this.name].push(this.value || '');
		} else {
			o[this.name] = this.value || '';
		}
	});
	return o;
};


function confirmDelete() {
	if (confirm("Уверены?") === true)
		return true;
	else
		return false;
	}

	function isInFrame() {
		return top.location != document.location;
	}

	function mceInsert(text) {

		//   console.log(text);
		//   console.log(unescepa(text));
		if (tinyMCE.activeEditor.selection.getContent({ format: 'text' }).length === 0)
			return;
		//    tinyMCE.execCommand('mceInsertContent', false, unescape(text));
		tinyMCE.execCommand('mceReplaceContent', false, '<a href="' + text + '">{$selection}</a>');
	}

	function relationDialogOpen() {
		$('#relationDialog').dialog('open');
	}

	function relationDialogClose() {
		$('#relationDialog').dialog('close');
	}
	function initSimpleTable(){
		$(".simple-table").addClass("ui-widget-content");
		$(".simple-table tr").addClass("ui-widget-content");
		$(".simple-table td").addClass("ui-widget-content");
		$(".simple-table th").addClass("ui-widget-header");
	}
	function initAutoAjaxForm(){
		$('form.auto-ajax-form').submit(function () {
			var form = $(this);
			$("div.ajax-result").html(controls.indicator);
			$.post(
				form.attr("action"), 
				form.serializeJson(), function(data){
					$("div.ajax-result").html(data);
					initSimpleTable();
				});
				return false;
			});
	}




	$(function () {
		$('input.confirm-dialog').click(function () { return confirmDelete(); });
		$("input.date-picker").datepicker({
			dateFormat: 'dd.mm.yy', 
			showAnim: 'fadeIn' 
		});
		initAutoAjaxForm();
		initSimpleTable();
	});


