var TimesheetView = {};
TimesheetView.init = function(url){

	$(function() {
			var picker = $("#timesheet-calendar").datepicker({
					minViewMode: 1,
					autoclose: true,
					todayHighlight: true
			});
			picker.on("changeDate", function (e) {
				var date = $(this).datepicker("getFormattedDate");
				document.location = url + "/" + date;
			});
	});

};

