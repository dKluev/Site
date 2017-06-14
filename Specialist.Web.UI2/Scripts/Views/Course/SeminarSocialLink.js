$(function(){
	$('#social-link-control').on('input',function(){
		var url = $(this).val();
		var show = url.indexOf("http") >= 0;
		var button = $('#social-submit-button');
		button.prop("disabled", !show);
	});
});
