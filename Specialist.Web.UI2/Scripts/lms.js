
$(function(){
	function initText2Area(){
		$("input.text2area").each(function(){
			var textbox = $(this);
			var textarea = $("<textarea></textarea>");
			textarea.attr("name", textbox.attr("name"));
			textarea.attr("style", textbox.attr("style"));
			textarea.attr("class", textbox.attr("class"));
			textarea.attr("maxlength", textbox.attr("size"));
			function initFocus(){
				textbox.on("focus", function () {
					change(textarea,textbox);
					textarea.focus();
					initFocusout();
				});
			}
			function initFocusout(){
				textarea.on("focusout", function () {
					change(textbox,textarea);
					initFocus();
				});
			}
			initFocus();
		});
		function change(text1,text2){
			text1.val(text2.val());
			text2.replaceWith(text1);
		}
	}
	initText2Area();
});
