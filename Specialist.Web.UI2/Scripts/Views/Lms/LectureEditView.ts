/// <reference path="../../jquery.d.ts"/>
/// <reference path="../../jquery.fileupload.d.ts"/>
module LectureEditView {
    export function init(addLectureUrl: string) {
        $("#all-method-type").change(function () {
            $("select.method-type-control").val($(this).val());
        });
        $("#all-exam-mark").change(function () {
            $("select.exam-mark-control").val($(this).val());
        });
        $("#all-mark").change(function () {
            $("select.mark-control").val($(this).val());
        });

        $("#form-lecture-edit").submit(function () {
            $("#button-lecture-edit").prop("disabled", true);
        });
        $("input.all-present").change(function () {
            $("input.present-control").prop("checked", $(this).prop("checked"));
            $("input.present-control1").prop("checked", $(this).prop("checked"));
            $("input.all-present1").prop("checked", $(this).prop("checked"));
        });
        $("input.all-present1").change(function () {
            $("input.present-control1").prop("checked", $(this).prop("checked"));
        });
        $("input.all-certgiven").change(function () {
            $("input.certgiven-control").prop("checked", $(this).prop("checked"));
            $("input.certgiven-control1").prop("checked", $(this).prop("checked"));
            $("input.all-certgiven1").prop("checked", $(this).prop("checked"));
        });
        $("input.all-certgiven1").change(function () {
            $("input.certgiven-control1").prop("checked", $(this).prop("checked"));
        });
        $("#input-lecture-file").fileupload({
            url: addLectureUrl,
            send() {
                $("#lecture-file-success").hide();
                $("#lecture-file-error").hide();
                $(".ajax-indicator").show();
            },
            done(e, data) {
                var result = data.result;
                $(".ajax-indicator").hide();
                if (result == "ok") {
                    $("#lecture-file-success").show();
                } else {
                    $("#lecture-file-error").show();
                    $("#lecture-file-error").html(result);
                }
            }
        });
    }

}
